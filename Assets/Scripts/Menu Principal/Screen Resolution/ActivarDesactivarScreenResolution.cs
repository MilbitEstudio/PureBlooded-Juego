using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ActivarDesactivarScreenResolution : MonoBehaviour
{

    public GameObject desactivarScreenResolution, activarScreenResolution;


    public void ActivarScreenResolution()
    {
        activarScreenResolution.SetActive(true);
    }

    public void DesactivarScreenResolution()
    {
        desactivarScreenResolution.SetActive(false);    
    }

}
