using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class ActivarDesactivaridiomas : MonoBehaviour
{
    public GameObject desactivaridiomas, activaridiomas;


    public void Activaridiomas()
    {
        activaridiomas.SetActive(true);
    }

    public void Desactivaridiomas()
    {
        desactivaridiomas.SetActive(false);
    }
}
