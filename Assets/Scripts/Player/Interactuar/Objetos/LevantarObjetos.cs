using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevantarObjetos : MonoBehaviour
{
    public LayerMask ObjetoLevantar;
    public LayerMask Puerta_;
    public Interactuar script;  // Referencia al script Interactuar
    public GameObject objetoTomado;
    public Transform posObjeto;
    public RawImage InteracionColor;
    public bool Agarrar = false;

    void LateUpdate()
    {
        RaycastHit hit = script.DetectarObjeto();

        if (hit.collider != null &&(((1 << hit.collider.gameObject.layer) & ObjetoLevantar.value) != 0 ||((1 << hit.collider.gameObject.layer) & Puerta_.value) != 0))
        {
            InteracionColor.color = Color.green;
        }
        else
        {
            InteracionColor.color = Color.white;
        }

        if (Input.GetMouseButton(0))
        {
            if (!Agarrar)
            {

                if (hit.collider != null && ((1 << hit.collider.gameObject.layer) & ObjetoLevantar) != 0)
                {
                    objetoTomado = hit.collider.gameObject;
                    Agarrar = true;
                }
            }

            if (Agarrar && objetoTomado != null)
            {
                objetoTomado.transform.position = script.posicionRayo.position + script.posicionRayo.forward * script.distancia;
            }
        }
        else
        {
            Agarrar = false;
        }
    }


}
