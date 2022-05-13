using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private LevelConfiguration[] configurations = null;
    private int currentLevel = -1;
    private AudioSource audioSource = null;
    private static LevelCreator instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        SubirNivel();
    }

    public static LevelCreator GetInstance()
    {
        return instance;
    }

    public void SubirNivel()
    {
        currentLevel++;

        if (currentLevel != 0)
        {
            audioSource.Play();
        }

        if (currentLevel >= configurations.Length)
        {
            UIManager.GetInstance().ChangeScreen(ScreenType.WinningScreen);
            AudioManager.instance.StopAudio();
            return;
        }

        for (int i = 0; i < configurations[currentLevel].objectsInLevel.Length; i++)
        {
            ResetObject(configurations[currentLevel].objectsInLevel[i]);
        }

        AudioManager.instance.PlayLevelSound(configurations[currentLevel].audio);
    }

    private void ResetObject(ObjectConfiguration objConfig)
    {
        objConfig.obj.transform.position = objConfig.objectPosition.position;
        objConfig.obj.Reset();
        if(objConfig.isTarget)
        {
            objConfig.obj.tag = "Target";
        }
        else
        {
            objConfig.obj.tag = "Untagged";
        }
    }
}