using UnityEngine;
using UnityEngine.UI;

public class Puerta : MonoBehaviour
{
    [SerializeField] public LayerMask Puerta_;
    [SerializeField] public Interactuar script;
    [SerializeField] public Animator anim;
    [SerializeField] public bool abierta = false;
    [SerializeField] public RawImage InteracionColor;
    [SerializeField] public AudioSource[] audios;
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
                abierta = !abierta;
                anim.SetBool("Abrir_Cerrar", abierta);

                if (abierta && anim.speed == 1)
                {
                    if (audios[1].isPlaying) audios[1].Stop();
                    if (!audios[0].isPlaying) audios[0].Play();
                }
                else
                {
                    if (audios[0].isPlaying) audios[0].Stop();
                    if (!audios[1].isPlaying) audios[1].Play();
                }

            }
        }
    }

}
