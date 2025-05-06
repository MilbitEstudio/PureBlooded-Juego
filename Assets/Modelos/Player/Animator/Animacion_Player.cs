using UnityEngine;

public class Animacion_Player : MonoBehaviour
{
    public Animator anim;
    public float moveX = 0f; // Velocidad en el eje X
    public float moveY = 0f; // Velocidad en el eje Y
    public PlayerMovementManager controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Agachado_Caminar()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            anim.SetBool("Agachado", true);
            controller.IsCrouching = true;
        }
        else
        {
            anim.SetBool("Agachado", false);
            controller.IsCrouching = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Captura las entradas de movimiento
        moveX = Input.GetAxis("Horizontal"); // A, D o flechas izquierda/derecha
        moveY = Input.GetAxis("Vertical"); // W, S o flechas arriba/abajo

        // Actualizar los parámetros del Blend Tree
        anim.SetFloat("X_Speed", moveX);
        anim.SetFloat("Y_Speed", moveY);

        //Inicia la funcion de caminar agachado.
        Agachado_Caminar();
    }
}
