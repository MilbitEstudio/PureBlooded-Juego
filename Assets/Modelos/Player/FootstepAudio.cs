using UnityEngine;
using UnityEngine.Events;

public class FootstepAudio : MonoBehaviour
{
    public AudioSource footstepSource;
    public AudioClip footstepSound;
    public PlayerMovementManager playerMovement;

    [Header("General Step Settings")]
    public float baseStepInterval = 0.5f; // Intervalo base entre pasos al caminar
    public float runStepInterval = 0.35f; // Intervalo de pasos al correr

    [Header("Stealth Settings")]
    public float stealthStepInterval = 1.2f; // Ajustable en Inspector
    public float stealthVolumeMultiplier = 0.3f;
    public float stealthPitch = 0.9f;

    [Header("Pitch Settings")]
    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;

    private float stepTimer = 0f;
    private bool lastStealthState = false;

    public UnityEvent OnStealthToggle;
    public bool IsStealth { get; private set; }

    void Start()
    {
        if (OnStealthToggle == null)
            OnStealthToggle = new UnityEvent();
    }

    void Update()
    {
        if (playerMovement == null || footstepSource == null) return;

        if (!playerMovement.CanControl) return;

        // Usamos la variable IsCrouching directamente desde el PlayerMovementManager
        bool isCrouching = playerMovement.IsCrouching;
        bool isRunning = playerMovement.IsRunning;
        bool isWalking = playerMovement.IsWalking;

        CharacterController controller = playerMovement.CharacterController;
        if (controller == null) return;

        float speed = controller.velocity.magnitude;
        bool isMoving = speed > 0.1f; // No reproducir pasos si la velocidad es muy baja

        // Determinar si está en sigilo
        IsStealth = isCrouching && !isRunning;
        if (IsStealth != lastStealthState)
        {
            OnStealthToggle.Invoke();
            lastStealthState = IsStealth;
        }

        footstepSource.volume = IsStealth ? stealthVolumeMultiplier : 1f;
        footstepSource.pitch = IsStealth ? stealthPitch : Random.Range(minPitch, maxPitch);

        // Si el jugador se está moviendo y está en el suelo
        if (controller.isGrounded && isMoving)
        {
            // Determinar el intervalo del paso dependiendo de si está caminando o corriendo
            float stepInterval = IsStealth ? stealthStepInterval : (isRunning ? runStepInterval : baseStepInterval);

            // Cambiar el intervalo rápidamente para la transición entre caminar y correr
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f; // Detener el temporizador cuando no está en el suelo
        }
    }

    void PlayFootstep()
    {
        if (footstepSound != null)
        {
            footstepSource.PlayOneShot(footstepSound);
        }
    }
}
