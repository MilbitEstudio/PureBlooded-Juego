using UnityEngine;
using UnityEngine.UI;

public class Puerta : MonoBehaviour
{
    [SerializeField] public LayerMask Puerta_;
    [SerializeField] public Interactuar script;
    [SerializeField] public RawImage InteracionColor;
    [SerializeField] public bool algunAudioActivo = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = script.DetectarObjeto();

            if (hit.collider != null && ((1 << hit.collider.gameObject.layer) & Puerta_.value) != 0)
            {
                Debug.Log(hit.collider.gameObject);
                hit.collider.gameObject.GetComponent<Puerta_Bool>().abierta = !hit.collider.gameObject.GetComponent<Puerta_Bool>().abierta;
                hit.collider.gameObject.GetComponent<Animator>().SetBool("Abrir_Cerrar", hit.collider.gameObject.GetComponent<Puerta_Bool>().abierta);

                if (hit.collider != null && hit.collider.gameObject.GetComponent<Puerta_Bool>().abierta && hit.collider.gameObject.GetComponent<Animator>().speed == 1)
                {
                    if (!hit.collider.gameObject.GetComponents<AudioSource>()[0].isPlaying) hit.collider.gameObject.GetComponents<AudioSource>()[0].Play();
                }
                else
                {
                    if (!hit.collider.gameObject.GetComponents<AudioSource>()[1].isPlaying) hit.collider.gameObject.GetComponents<AudioSource>()[1].Play();
                }

            }
        }
    }

}
