using UnityEngine;
using UnityEngine.AI;

public class Seguir_Player : MonoBehaviour
{
    public Transform followTarget; // Player
    public Transform destinationPoint; // Punto al que el agente debe ir
    public float distanciaMinima = 2f;
    public float distanciaMaxima = 10f;
    public float distanciaParaAvanzar = 2f;
    public float distanciaAlDestino;
    public Animator anim;
    private Quaternion rotAnterior;

    public float posicionRelativaLateral = 2f;
    public float posicionRelativaAdelanteAtras = 2f;

    public NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }

    void Update()
    {
        float distanciaAlJugador = Vector3.Distance(transform.position, followTarget.position);
        distanciaAlDestino = Vector3.Distance(transform.position, destinationPoint.position);

        if (distanciaAlJugador > distanciaParaAvanzar)
        {
            Vector3 destinoJugador = followTarget.position + followTarget.right * posicionRelativaLateral + followTarget.forward * posicionRelativaAdelanteAtras;
            destinoJugador.y = transform.position.y;
            agent.isStopped = false;
            agent.destination = destinoJugador;
        }
        else
        {
            if (distanciaAlDestino > distanciaMinima)
            {
                agent.isStopped = false;
                agent.destination = destinationPoint.position;
            }
            else
            {
                agent.isStopped = true;
            }
        }

        if (agent.velocity.magnitude > 0.1f && !agent.pathPending && agent.remainingDistance > agent.stoppingDistance)
        {
            anim.SetBool("Camina", true);
        }
        else
        {
            anim.SetBool("Camina", false);
        }

        if (agent.velocity.magnitude > 0.1f)
        {
            Vector3 direccionMovimiento = agent.velocity.normalized;
            float angulo = Vector3.Angle(transform.forward, direccionMovimiento);

            if (angulo > 10f)
            {
                Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionMovimiento);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, 5f * Time.deltaTime);
            }
        }


        if (agent.velocity.sqrMagnitude > 0.1f)
        {
            Vector3 direccionMovimiento = agent.velocity.normalized;
            direccionMovimiento.y = 0; // solo en eje Y
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionMovimiento);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, 5f * Time.deltaTime);
        }

        // Verificar si ha llegado al destino
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && distanciaAlDestino < 7)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                destinationPoint = followTarget;
            }
        }
    }
}
