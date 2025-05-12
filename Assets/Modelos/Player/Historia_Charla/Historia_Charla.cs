using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class Historia_Charla : MonoBehaviour
{
    public GameObject chat;
    public SistemaDialogo dialogo;
    public Seguir_Player Amigo;
    public float VelocidadAmigo;
    public bool hablando = false;
    public GameObject LugarCharla;
    public GameObject Camara1;
    public GameObject Camara2;
    public GameObject PlayerPrimeraPersona;
    public GameObject Player3ra;
    public PlayerMovementManager controller;
    public Seguir_Player agent;

    //Puntos de Charla Ubicados isTrigger
    public GameObject[] PuntoCharla;

    //Cambio de Dialogos
    public SistemaDialogo SistemaCharla;
    public DialogueSO[] dialogos;
    public int charla;

    void Start()
    {
        VelocidadAmigo = Amigo.agent.velocity.magnitude;
    }



    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("PuedeHablar")) return;

        chat.SetActive(true);

        // Inicia el diálogo si no está en curso
        if (!dialogo.iniciado && !hablando && charla < dialogos.Length)
        {
            hablando = true;
            dialogo.IniciarDialogo(dialogos[charla]);

            Camara1.SetActive(false);
            PlayerPrimeraPersona.SetActive(false);
            Camara2.SetActive(true);
            Player3ra.SetActive(true);
            controller.enabled = false;
            agent.posicionRelativaLateral = 2;
        }

        // Si el diálogo terminó, preparamos el siguiente
        if (hablando && !dialogo.iniciado)
        {
            Destroy(PuntoCharla[charla]);

            Camara1.SetActive(true);
            PlayerPrimeraPersona.SetActive(true);
            Camara2.SetActive(false);
            Player3ra.SetActive(false);
            controller.enabled = true;
            agent.posicionRelativaLateral = -2;

            charla++;
            hablando = false;
        }
    }



    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PuedeHablar"))
        {
            chat.SetActive(false);
            hablando = false;
            Amigo.agent.velocity = new Vector3(VelocidadAmigo, VelocidadAmigo, VelocidadAmigo);
        }
    }


    void Update()
    {

    }
}
