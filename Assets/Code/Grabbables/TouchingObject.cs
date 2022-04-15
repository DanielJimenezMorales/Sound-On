using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingObject : MonoBehaviour
{
    [SerializeField] private Color onLoosingColor = Color.red;
    private Action OnWinning;
    private Action OnLoosing;

    private void OnEnable()
    {
        AddingToOnWinning();
        AddingToOnLoosing();
    }

    private void OnDisable()
    {
        SubstractToOnWinning();
        SubstractToOnLoosing();
    }

    public void OnStartTouchingObject()
    {
        //Comprueba si has ganado o perdido.
        //En el caso de que hayas ganado se para el juego y te sale una pantalla de has ganado de interfaz. Cambiar el direct interactor por el XRay interactor o que se vaya directamente al siguiente nivel
        //En el caso de que pierda, cambiamos el color del objeto para decir que ese ya no es y un audio/texto que diga por unos segundos "Este no es" o similar.

        if (CheckWinning())
        {
            OnWinning?.Invoke();
        }
        else
        {
            OnLoosing?.Invoke();
        }
    }

    private bool CheckWinning()
    {
        if (tag.CompareTo("Target") == 0)
        {
            return true;
        }

        return false;
    }

    private void AddingToOnWinning()
    {
        OnWinning += ShowWinningUI;
    }

    private void AddingToOnLoosing()
    {
        OnLoosing += SetColor;
    }

    private void SubstractToOnWinning()
    {
        OnWinning -= ShowWinningUI;
    }

    private void SubstractToOnLoosing()
    {
        OnLoosing -= SetColor;
    }

    #region OnLoosing Methods
    private void SetColor()
    {
        GetComponent<MeshRenderer>().material.color = onLoosingColor;
    }
    #endregion

    #region OnWinning Methods
    private void ShowWinningUI()
    {
        UIManager.GetInstance().ChangeScreen(ScreenType.WinningScreen);
    }
    #endregion
}
