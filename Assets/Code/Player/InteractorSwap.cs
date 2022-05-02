using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractorSwap : MonoBehaviour
{
    public static InteractorSwap instance = null;
    [SerializeField] private GameObject leftHandRay = null;
    [SerializeField] private GameObject rightHandRay = null;
    [SerializeField] private GameObject leftHandDirect = null;
    [SerializeField] private GameObject rightHandDirect = null;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void EnableXRRayInteractors()
    {
        leftHandRay.SetActive(true);
        rightHandRay.SetActive(true);
        leftHandDirect.SetActive(false);
        rightHandDirect.SetActive(false);
    }

    public void EnableDirectInteractors()
    {
        leftHandRay.SetActive(false);
        rightHandRay.SetActive(false);
        leftHandDirect.SetActive(true);
        rightHandDirect.SetActive(true);
    }
}
