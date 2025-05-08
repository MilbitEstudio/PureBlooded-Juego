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
    private Vector3 rotacionInicial;

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
                    //Ajustar eje Z Objeto
                    Quaternion rotAlineada = Quaternion.FromToRotation(objetoTomado.transform.forward, Vector3.up) * objetoTomado.transform.rotation;
                    objetoTomado.transform.rotation = rotAlineada;
                    rotacionInicial = objetoTomado.transform.eulerAngles;
                    //Termina_Ajuste
                    Agarrar = true;
                }
            }

            if (Agarrar && objetoTomado != null)
            {
                objetoTomado.transform.position = script.posicionRayo.position + script.posicionRayo.forward * script.distancia;

                // Limitar rotación
                Vector3 rotActual = objetoTomado.transform.localEulerAngles;

                objetoTomado.GetComponent<Rigidbody>().linearVelocity = new Vector3(0,0,0);

                float x = rotActual.x > 180 ? rotActual.x - 360 : rotActual.x;
                float y = rotActual.y > 180 ? rotActual.y - 360 : rotActual.y;
                float z = rotActual.z > 180 ? rotActual.z - 360 : rotActual.z;

                float xIni = rotacionInicial.x > 180 ? rotacionInicial.x - 360 : rotacionInicial.x;
                float yIni = rotacionInicial.y > 180 ? rotacionInicial.y - 360 : rotacionInicial.y;
                float zIni = rotacionInicial.z > 180 ? rotacionInicial.z - 360 : rotacionInicial.z;

                x = Mathf.Clamp(x, xIni - 15f, xIni + 15f);
                y = Mathf.Clamp(y, yIni - 15f, yIni + 15f);
                z = Mathf.Clamp(z, zIni - 15f, zIni + 15f);

                objetoTomado.transform.localEulerAngles = new Vector3(x, y, z);
            }

        }
        else
        {
            Agarrar = false;
            if (hit.collider != null && hit.collider.GetComponent<Rigidbody>())
            {
                hit.collider.gameObject.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, hit.collider.gameObject.GetComponent<Rigidbody>().linearVelocity.y, 0);
            }
        }
    }


}
