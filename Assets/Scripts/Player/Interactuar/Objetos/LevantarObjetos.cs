using Unity.VisualScripting;
using UnityEngine;

public class LevantarObjetos : MonoBehaviour
{
    public LayerMask ObjetoLevantar;
    public Interactuar script;  // Referencia al script Interactuar
    public GameObject objetoTomado;
    public Transform posObjeto;
    public bool Agarrar = false;

    void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if (!Agarrar)
            {
                RaycastHit hit = script.DetectarObjeto();

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
