using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorDatos : MonoBehaviour
{

    private DatosJuego datos;
    string ruta;
    GameObject jugador;

    private void Awake()
    {
        ruta = Path.Combine(Application.persistentDataPath, "PartidasGuardadasJugador.json");
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Se ejecuta cada vez que se carga una nueva escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
        {
            CargarPosicionPlayer();
            Debug.Log("Posición del jugador asignada en OnSceneLoaded: " + jugador.transform.position);
        }
    }

    public void GuardarPartida()
    {
        string sceneActual = SceneManager.GetActiveScene().name;
        datos = new DatosJuego();
        datos.scene = sceneActual;
        datos.posicionJugador = jugador.transform.position;
        string json = JsonUtility.ToJson(datos, true);
        File.WriteAllText(ruta, json);
        Debug.Log("Se ha guardado correctamente en la ruta " + ruta + "\n" + json);
    }

    public void CargarPartida()
    {
        if (File.Exists(ruta))
        {
            string jsonleido = File.ReadAllText(ruta);
            datos = JsonUtility.FromJson<DatosJuego>(jsonleido);
            string sceneActual = datos.scene;
            SceneManager.LoadScene(sceneActual);
            Debug.Log("Se han cargado correctamente los datos: " + sceneActual);
        }
    }

    public void CargarPosicionPlayer()
    {
        if (File.Exists(ruta))
        {
            string jsonleido = File.ReadAllText(ruta);
            datos = JsonUtility.FromJson<DatosJuego>(jsonleido);

            // Desactivar temporalmente el CharacterController para evitar desincronización
            CharacterController cc = jugador.GetComponent<CharacterController>();
            if (cc != null)
                cc.enabled = false;

            jugador.transform.position = datos.posicionJugador;

            if (cc != null)
                cc.enabled = true;

            // Actualizar las variables internas de posición en los scripts de movimiento y animación
            PlayerMovementManager pmm = jugador.GetComponent<PlayerMovementManager>();
            if (pmm != null)
            {
                pmm.SetLastPositionY(datos.posicionJugador.y);
            }
            PlayerAnimationManager pam = jugador.GetComponentInChildren<PlayerAnimationManager>();
            if (pam != null)
            {
               // pam.SetLastPosition(datos.posicionJugador);
            }

            Debug.Log("Posición del jugador asignada: " + jugador.transform.position);
        }
    }



}
