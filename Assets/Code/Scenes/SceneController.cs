using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static SceneController singletonInstance = null;

    private void Awake()
    {
        if (singletonInstance == null)
        {
            singletonInstance = this;
            Object.DontDestroyOnLoad(this);
        }
    }

    public static SceneController GetInstance()
    {
        return singletonInstance;
    }

    public void ChangeToFirstLevel()
    {
        ChangeScene(SceneType.Level1);
    }

    public void ChangeScene(SceneType type)
    {
        SceneManager.LoadScene(type.GetHashCode(), LoadSceneMode.Single);
    }
}
