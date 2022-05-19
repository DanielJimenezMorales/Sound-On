using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prueba : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(menuCoroutine());
    }

    private IEnumerator menuCoroutine()
    {
        yield return new WaitForSeconds(3f);
        CambiarAPantallaDeJuego();
    }
    private void CambiarAPantallaDeJuego()
    {
        UIManager.GetInstance().ChangeScreen(ScreenType.InGameScreen);
    }
}
