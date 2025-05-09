using UnityEngine;
using TMPro;
using System.Collections;

public class SistemaDialogo : MonoBehaviour
{
    [Header("Configuración")]
    public TextMeshProUGUI textoDialogo;
    public float velocidadTipiado = 0.05f;
    public DialogueSO[] dialogos;  // Array de diálogos
    private string[] lineas;       // Guardar las líneas del diálogo actual
    public int indiceLinea = 0;
    public int indiceDialogo = 0; // Índice para controlar el diálogo actual
    public bool escribiendo = false;
    public Coroutine rutinaTipiado;
    public bool iniciado = false;

    void Start()
    {

    }


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
                    // Si hay más diálogos, se carga el siguiente
                    if (indiceDialogo + 1 < dialogos.Length)
                    {
                        indiceDialogo++;
                        IniciarDialogo(dialogos[indiceDialogo]);
                    }
                    else
                    {
                        // Si no hay más diálogos, finalizar
                        textoDialogo.text = "";
                        iniciado = false;
                        indiceLinea = 0;
                    }
                }
            }
        }
    }

    // Iniciar el diálogo con el DialogueSO seleccionado
    public void IniciarDialogo(DialogueSO dialogo)
    {
        if (dialogo == null || dialogo.Lines.Length == 0) return;

        lineas = dialogo.Lines;  // Asigna las líneas del diálogo actual
        indiceLinea = 0;
        iniciado = true;
        textoDialogo.gameObject.SetActive(true);
        MostrarSiguienteLinea();
    }

    void MostrarSiguienteLinea()
    {
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


