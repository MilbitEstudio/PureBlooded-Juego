using UnityEngine;
using TMPro;
using System.Collections;

public class SistemaDialogo : MonoBehaviour
{
    [Header("Configuración")]
    public TextMeshProUGUI textoDialogo;
    public float velocidadTipiado = 0.05f;
    public DialogueSO[] dialogos;
    public AudioSource audioSource;

    [Header("Debug/Visualización actual")]
    public string[] lineas;
    public AudioClip[] clips;

    public int indiceLinea = 0;
    public int indiceDialogo = 0;
    public bool escribiendo = false;
    public Coroutine rutinaTipiado;
    public bool iniciado = false;

    void Update()
    {
        if (!iniciado) return;

        if (Input.GetKeyDown(KeyCode.E) && textoDialogo.gameObject.activeSelf)
        {
            if (escribiendo)
            {
                StopCoroutine(rutinaTipiado);
                textoDialogo.text = lineas[indiceLinea];
                escribiendo = false;
                audioSource.Stop(); // Detiene el audio actual si se interrumpe el tipiado
            }
            else
            {
                indiceLinea++;
                if (indiceLinea < lineas.Length)
                {
                    MostrarSiguienteLinea();
                }
                else
                {
                    if (indiceDialogo + 1 < dialogos.Length)
                    {
                        indiceDialogo++;
                        IniciarDialogo(dialogos[indiceDialogo]);
                    }
                    else
                    {
                        textoDialogo.text = "";
                        iniciado = false;
                        indiceLinea = 0;
                        audioSource.Stop();
                    }
                }
            }
        }
    }

    public void IniciarDialogo(DialogueSO dialogo)
    {
        if (dialogo == null || dialogo.Lines.Length == 0) return;

        lineas = dialogo.Lines;
        clips = dialogo.Clips;

        indiceLinea = 0;
        iniciado = true;
        textoDialogo.gameObject.SetActive(true);
        MostrarSiguienteLinea();
    }

    void MostrarSiguienteLinea()
    {
        if (clips != null &&
            indiceLinea < clips.Length &&
            clips[indiceLinea] != null)
        {
            audioSource.clip = clips[indiceLinea];
            audioSource.Play();
        }

        rutinaTipiado = StartCoroutine(TipiarLinea(lineas[indiceLinea]));
    }

    IEnumerator TipiarLinea(string linea)
    {
        textoDialogo.text = "";
        escribiendo = true;
        foreach (char letra in linea)
        {
            textoDialogo.text += letra;
            yield return new WaitForSeconds(velocidadTipiado);
        }
        escribiendo = false;
    }
}
