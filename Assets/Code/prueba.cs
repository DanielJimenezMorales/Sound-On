using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prueba : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(corrutina());
    }

    private IEnumerator corrutina()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("ME EJECUTO");
        pruebaa();
    }
    private void pruebaa()
    {
        UIManager.GetInstance().ChangeScreen(ScreenType.InGameScreen);
    }
}
