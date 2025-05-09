using UnityEngine;

public class VendedorHabla : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audios;
    public bool Saludo = false;
    public GameObject chat;
    public SistemaDialogo dialogo;
    public bool hablando = false;

    void Start()
    {

    }

    bool yaEjecutado = false;

    void OnTriggerStay(Collider other)
    {
        if (!Saludo && other.gameObject.tag == "PuedeHablar")
        {
            audioSource.clip = audios[0];
            audioSource.Play();
            Saludo = true;
        }
        if (other.CompareTag("PuedeHablar"))
        {
            chat.SetActive(true);
            if (!dialogo.iniciado && !hablando && dialogo.dialogos.Length > 0)
            {
                dialogo.IniciarDialogo(dialogo.dialogos[dialogo.indiceDialogo]);
                hablando = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PuedeHablar"))
        {
            chat.SetActive(false);
            hablando = false;
        }
    }


    void Update()
    {

    }
}
