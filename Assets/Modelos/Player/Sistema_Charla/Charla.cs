using UnityEngine;

public class AudioPorDistancia : MonoBehaviour
{
    public Transform origen;
    public Transform objetivo;
    public AudioSource audioSource;
    public AudioClip[] audios;

    public float distanciaInicial;
    public int indiceActual = 0;
    public float distanciaActual;
    public float progreso;
    public int indiceDeseado;

    void Start()
    {
        distanciaInicial = Vector3.Distance(origen.position, objetivo.position);
    }

    void Update()
    {
        if (audioSource.isPlaying || indiceActual >= audios.Length)
            return;

        distanciaActual = Vector3.Distance(origen.position, objetivo.position);
        progreso = 1f - Mathf.Clamp01(distanciaActual / distanciaInicial);
        indiceDeseado = Mathf.FloorToInt(progreso * audios.Length);

        if (indiceDeseado > indiceActual)
        {
            audioSource.clip = audios[indiceActual];
            audioSource.Play();
            indiceActual++;
        }
    }
}
