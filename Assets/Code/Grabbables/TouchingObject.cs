using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public enum ObjectType
{
    NormalObject = 0,
    Aid = 1
}

public class TouchingObject : MonoBehaviour
{
    [SerializeField] private ObjectType type = ObjectType.NormalObject;
    [SerializeField] private Color onLoosingColor = Color.red;
    private Action OnWinning;
    private Action OnLoosing;
    private bool hasInteracted = false;

    private void OnEnable()
    {
        if (type.CompareTo(ObjectType.Aid) != 0)
        {
            AddingToOnWinning();
            AddingToOnLoosing();
        }
        else
        {
            OnWinning += () =>
            {
                if (hasInteracted) return;
                hasInteracted = true;
                AudioManager.instance.ApllyAid();
            };
            OnLoosing += () =>
            {
                if (hasInteracted) return;
                hasInteracted = true;
                AudioManager.instance.ApllyAid();
            };
            OnLoosing += SetColor;
        }
    }

    private void OnDisable()
    {
        if (type.CompareTo(ObjectType.Aid) != 0)
        {
            SubstractToOnWinning();
            SubstractToOnLoosing();
        }
        else
        {
            OnWinning -= () =>
            {
                if (hasInteracted) return;
                hasInteracted = true;
                AudioManager.instance.ApllyAid();
            };
            OnLoosing -= () =>
            {
                if (hasInteracted) return;
                hasInteracted = true;
                AudioManager.instance.ApllyAid();
            };
            OnLoosing -= SetColor;
        }
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
        OnWinning += LevelUpHearingLoss;
    }

    private void AddingToOnLoosing()
    {
        OnLoosing += SetColor;
        OnLoosing += LevelDownHearingLoss;
    }

    private void SubstractToOnWinning()
    {
        OnWinning -= ShowWinningUI;
        OnWinning -= LevelUpHearingLoss;
    }

    private void SubstractToOnLoosing()
    {
        OnLoosing -= SetColor;
        OnLoosing -= LevelDownHearingLoss;
    }

    private void LevelUpHearingLoss()
    {
        if (hasInteracted) return;

        hasInteracted = true;
        AudioManager.instance.LevelUp();
    }

    private void LevelDownHearingLoss()
    {
        if (hasInteracted) return;

        hasInteracted = true;
        AudioManager.instance.LevelDown();
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
        //UIManager.GetInstance().ChangeScreen(ScreenType.WinningScreen);
        LevelCreator.GetInstance().SubirNivel();
        GameObject.FindWithTag("Player").transform.position = new Vector3(0, 1, 0);
    }

    public void Reset()
    {
        hasInteracted = false;
    }
    #endregion
}
