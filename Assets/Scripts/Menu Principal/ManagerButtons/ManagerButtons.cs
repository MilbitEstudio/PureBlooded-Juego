using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Unity.VisualScripting;
public class ManagerButtons : MonoBehaviour
{


    public List<Toggle> toggles;
    public Dropdown dropdown; // Asigna tu Dropdown desde el Inspector
    void Start()
    {
        // Agrega un listener a cada toggle para detectar cambios.
        foreach (Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener(delegate { OnToggleChanged(toggle); });
        }

      /*  // Suscribimos al evento onValueChanged
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        // Inicializamos el valor por defecto si deseas
        OnDropdownValueChanged(dropdown.value);

        */
    }

    // Se llama cada vez que cambia el valor de alguno de los toggles.
    void OnToggleChanged(Toggle selectedToggle)
    {
        if (selectedToggle.isOn)
        {
            // Desactivamos los dem�s toggles.
            foreach (Toggle toggle in toggles)
            {
                if (toggle != selectedToggle)
                {
                    toggle.isOn = false;
                }
            }

            // Dependiendo del toggle seleccionado, se llama a la funci�n correspondiente.
            if (selectedToggle == toggles[0])
            {
                NivelDificil();
            }
            else if (selectedToggle == toggles[1])
            {
                NivelMedio();
            }
            else if (selectedToggle == toggles[2])
            {
                NivelFacil();
            }
        }
    }

   public void NivelDificil()
    {
        Debug.Log("Nivel Dif�cil activado");
    }

   public void NivelMedio()
    {
        Debug.Log("Nivel Medio activado");
        
    }

   public void NivelFacil()
    {
        Debug.Log("Nivel F�cil activado");
    }


  /*  void OnDropdownValueChanged(int index)
    {
        // Asume que la opci�n 0 = Ingl�s, 1 = Espa�ol, etc.
        switch (index)
        {
            case 0:
                LocalizationManager.SetLanguage(Language.English);
                break;
            case 1:
                LocalizationManager.SetLanguage(Language.Spanish);
                break;
            default:
                LocalizationManager.SetLanguage(Language.English);
                break;
        }
    }
  */
}
