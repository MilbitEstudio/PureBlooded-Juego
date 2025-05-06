using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour

{
    public Keyboard Keycodes;
    [System.Serializable]
    public class Keyboard
    {
        public KeyCode Jump = KeyCode.Space;
        public KeyCode Crouch = KeyCode.C;
    }

    public CharacterController CharacterController;
    public bool CanControl = true;
    public bool UseGravity = true;
    public bool IsRunning;
    public bool IsWalking;

    public GameObject Camera;
    public GameObject CameraStandPosition;
    public GameObject CameraCrouchPosition;
    public bool CanRun = true;
    public GameObject AttachedBody;

    [Header("Crouch Settings")]
    public bool IsCrouching; // Configurable en el Inspector
    public float WalkSpeedStealth = 2f;

    public bool CanStand;
    public float CanStandDistance = 1.5f;
    public float HalfScale = 1f;
    public float FullScale = 2f;
    public float WalkSpeed;
    public float RunSpeed = 6f;
    public float Speed;
    public Vector3 input;

    public float SpeedBoostTime;
    public float JumpBoostTime;
    public bool CanJump = true;
    public float JumpStamina;
    public float JumpSpeed = 8f; // Valor positivo para el salto
    public float Gravity;
    public bool IsFalling;
    public float Stamina;
    public Text StaminaText;
    public Image JumpBoostIcon;
    public Image SpeedBoostIcon;
    public float MaxStamina = 100f;
    public float MinStamina = 0f;
    public float StaminaRegenerationRate = 10f;
    public float StaminaDegenerationRate = 15f;

    // Variables separadas para el movimiento horizontal y vertical
    private Vector3 horizontalVelocity = Vector3.zero;
    private float verticalVelocity = 0f;

    // Variables para el cálculo de caída y daño
    public float LastPositionY;
    public float FallDistance;
    public float FallDamageLow;
    public float FallDamageMedium;
    public float FallDamageHigh;

    void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        Stamina = MaxStamina;
    }

    void Start()
    {
        Gravity = 9.81f;
        if (GameObject.Find("StaminaText") != null)
            StaminaText = GameObject.Find("StaminaText").GetComponent<Text>();
        if (GameObject.Find("SpeedBoostIcon") != null)
            SpeedBoostIcon = GameObject.Find("SpeedBoostIcon").GetComponent<Image>();
        if (GameObject.Find("JumpBoostIcon") != null)
            JumpBoostIcon = GameObject.Find("JumpBoostIcon").GetComponent<Image>();

        if (SpeedBoostIcon != null)
            SpeedBoostIcon.enabled = false;
        if (JumpBoostIcon != null)
            JumpBoostIcon.enabled = false;

        IsFalling = false;
    }

    void Update()
    {
        // Obtener el input horizontal (X,Z) y transformarlo según la orientación
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        input = transform.TransformDirection(input);

        // Determinar si se corre o se camina
        if ((Stamina > MinStamina) && CanRun && !IsCrouching && Input.GetKey(KeyCode.LeftShift))
        {
            IsRunning = true;
            IsWalking = false;
        }
        else
        {
            IsRunning = false;
            IsWalking = true;
        }

        // Ajustar velocidad de caminar según si está agachado
        if (IsCrouching)
            WalkSpeed = WalkSpeedStealth;
        else if (IsWalking)
            WalkSpeed = 3f;

        // Asignar la velocidad de movimiento según el estado
        if (IsWalking)
            Speed = WalkSpeed;
        else if (IsRunning && CanRun)
        {
            Speed = RunSpeed;
            Stamina -= 1 * Time.deltaTime * StaminaDegenerationRate;
        }

        if (StaminaText != null)
            StaminaText.text = Stamina.ToString("0");

        // Calcular la velocidad horizontal a partir del input
        horizontalVelocity = input * Speed;

        // Lógica de salto y gravedad:
        if (CharacterController.isGrounded)
        {
            IsFalling = false;
            // Primero comprobamos el input de salto.
            if (CanControl && Input.GetKeyDown(Keycodes.Jump) && CanJump)
            {
                verticalVelocity = JumpSpeed;
                Stamina -= JumpStamina;
            }
            else
            {
                // Si no se pulsa salto, se fuerza una pequeña velocidad descendente para "pegar" al suelo.
                verticalVelocity = -2f;
            }
        }
        else
        {
            IsFalling = true;
            // Permitir cierto control horizontal en el aire con un factor reducido.
            float airControlFactor = 0.5f;
            horizontalVelocity = input * Speed * airControlFactor;
            // Acumular la gravedad.
            verticalVelocity -= Gravity * Time.deltaTime;
        }

        // Combinar las velocidades horizontal y vertical.
        Vector3 move = horizontalVelocity;
        move.y = verticalVelocity;

        // Calcular la distancia de caída para aplicar daño si corresponde.
        if (LastPositionY > transform.position.y)
            FallDistance += LastPositionY - transform.position.y;
        LastPositionY = transform.position.y;

        if (FallDistance >= 4 && FallDistance <= 10 && CharacterController.isGrounded)
            GetComponent<PlayerHealthManager>().CurrentHealth -= FallDamageLow;
        if (FallDistance >= 10 && FallDistance <= 25 && CharacterController.isGrounded)
            GetComponent<PlayerHealthManager>().CurrentHealth -= FallDamageMedium;
        if (FallDistance >= 25 && CharacterController.isGrounded)
            GetComponent<PlayerHealthManager>().CurrentHealth -= FallDamageHigh;
        if (CharacterController.isGrounded)
            FallDistance = 0;

        // Ajustar el CharacterController y la posición de cámara/cuerpo según el estado de agachado.
        if (IsCrouching)
        {
            CharacterController.height = HalfScale;
            if (CameraCrouchPosition != null)
                Camera.transform.position = CameraCrouchPosition.transform.position;
            if (AttachedBody != null)
                AttachedBody.transform.position = new Vector3(AttachedBody.transform.position.x, transform.position.y - 0.6f, AttachedBody.transform.position.z);
            CanJump = false;
        }
        else
        {
            CharacterController.height = FullScale;
            if (CameraStandPosition != null)
                Camera.transform.position = CameraStandPosition.transform.position;
            if (AttachedBody != null)
                AttachedBody.transform.position = new Vector3(AttachedBody.transform.position.x, transform.position.y - 1, AttachedBody.transform.position.z);
            CanJump = true;
        }

        // Control de agachado.
        if (Input.GetKeyDown(Keycodes.Crouch) && !IsCrouching)
            IsCrouching = true;
        else if (Input.GetKeyDown(Keycodes.Crouch) && IsCrouching && CanStand)
        {
            IsCrouching = false;
            if (AttachedBody != null)
                AttachedBody.transform.position = new Vector3(AttachedBody.transform.position.x, AttachedBody.transform.position.y, AttachedBody.transform.position.z);
        }

        // Gestión de la stamina.
        if ((Stamina >= MinStamina) && (Stamina < MaxStamina) && !IsRunning)
            Stamina += 1 * Time.deltaTime * StaminaRegenerationRate;
        if (Stamina >= MaxStamina)
        {
            Stamina = MaxStamina;
            CanRun = true;
        }
        if (Stamina <= MinStamina)
        {
            Stamina = MinStamina;
            CanRun = false;
            CanJump = false;
        }

        // Aplicar el movimiento al CharacterController.
        CharacterController.Move(move * Time.deltaTime);
    }

    public IEnumerator PickupSpeed()
    {
        WalkSpeed *= 3;
        RunSpeed *= 3;
        if (SpeedBoostIcon != null)
            SpeedBoostIcon.enabled = true;
        yield return new WaitForSeconds(SpeedBoostTime);
        WalkSpeed /= 3;
        RunSpeed /= 3;
        if (SpeedBoostIcon != null)
            SpeedBoostIcon.enabled = false;
    }

    public IEnumerator PickupJump()
    {
        JumpSpeed *= 3;
        if (JumpBoostIcon != null)
            JumpBoostIcon.enabled = true;
        yield return new WaitForSeconds(JumpBoostTime);
        JumpSpeed /= 3;
        if (JumpBoostIcon != null)
            JumpBoostIcon.enabled = false;
    }

    // Método para actualizar LastPositionY tras cargar la posición.
    public void SetLastPositionY(float newY)
    {
        LastPositionY = newY;
    }




}




