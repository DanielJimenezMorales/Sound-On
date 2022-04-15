using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ScreenType initalScreenType = ScreenType.WinningScreen;
    [SerializeField] private UIScreen[] screens = null;
    private int currentScreenIndex = -1;

    private static UIManager singletonInstance = null;

    private void Awake()
    {
        if(singletonInstance == null)
        {
            singletonInstance = this;
            Object.DontDestroyOnLoad(this);
            Init();
        }
    }

    public static UIManager GetInstance()
    {
        return singletonInstance;
    }

    private void Init()
    {
        //Buscar y coger todas UIScreen
        screens = GetComponentsInChildren<UIScreen>(true);

        //Deshabilitar todas
        for (int screen = 0; screen < screens.Length; screen++)
        {
            screens[screen].Disable();
        }

        //Habilitar la inicial
        ChangeScreen(initalScreenType);
    }

    public void StartGame()
    {
        ChangeScreen(ScreenType.InGameScreen);
    }

    public void ChangeScreen(ScreenType type)
    {
        //Desactivar la pantalla actual
        if(currentScreenIndex != -1)
        {
            screens[currentScreenIndex].Disable();
        }

        for (int screen = 0; screen < screens.Length; screen++)
        {
            if(screens[screen].type.CompareTo(type) == 0)
            {
                currentScreenIndex = screen;
            }
        }
        //Activar la nueva
        screens[currentScreenIndex].Enable();
    }
}
