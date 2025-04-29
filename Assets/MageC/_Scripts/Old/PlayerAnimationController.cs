using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [Header("References")]
    public Animator animator; // Referencia al Animator del personaje
    public FPSController playerController; // Referencia al controlador del jugador
    public Rigidbody playerRigidbody; // Referencia al Rigidbody del jugador

    [Header("Animation Settings")]
    public string speedParameter = "Speed"; // Nombre del parámetro de velocidad en el Animator
    public float smoothTime = 0.1f; // Tiempo de suavizado para la transición de animaciones

    private float currentSpeed;
    private float targetSpeed;
    private float velocity;

    private void Update()
    {
        if (playerRigidbody == null) return;

        // Obtener velocidad en el plano XZ (sin el eje Y)
        Vector3 velocityXZ = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.z);
        float targetSpeed = velocityXZ.magnitude;

        // Suavizar transición de velocidad para animaciones más fluidas
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref velocity, smoothTime);

        // Aplicar la velocidad al Animator
        animator.SetFloat(speedParameter, currentSpeed);
    }
}