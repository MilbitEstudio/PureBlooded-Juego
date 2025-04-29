using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class ActivarDesactivarOpciones : MonoBehaviour
{
    public GameObject desactivarPanelOpciones, activarPanelOpciones;


    public void ActivarPanelOpciones()
    {
        activarPanelOpciones.SetActive(true);
    }

    public void DesactivarPanelOpciones()
    {
        desactivarPanelOpciones.SetActive(false);
    }
}
