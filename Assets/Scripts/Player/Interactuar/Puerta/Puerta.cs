using UnityEngine;

public class Puerta : MonoBehaviour
{
    [SerializeField] public LayerMask Puerta_;
    [SerializeField] public Interactuar script;
    [SerializeField] public Animator anim;
    [SerializeField] public bool abierta;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = script.DetectarObjeto();
            hit.point = script.posicionRayo.position + script.posicionRayo.forward * script.distancia;

            if (hit.collider !=null && ((1 << hit.collider.gameObject.layer) & Puerta_.value) != 0)
            {
                Debug.Log(hit.collider.gameObject);
                abierta = !abierta;
                anim.SetBool("Abrir_Cerrar", abierta);
            }
        }
    }
}
