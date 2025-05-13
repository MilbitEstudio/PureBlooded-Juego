using UnityEngine;

public class Interactuar : MonoBehaviour
{
    public float distancia = 10f;
    public Transform posicionRayo;

    void Update()
    {

    }

    public RaycastHit DetectarObjeto()
    {
        Ray ray = new Ray(posicionRayo.transform.position, posicionRayo.transform.forward);
        RaycastHit hit;

        // Dibujar el rayo en la escena
        Debug.DrawRay(posicionRayo.transform.position, posicionRayo.transform.forward * distancia, Color.red);

        if (Physics.Raycast(ray, out hit, distancia))
        {
            //Debug.Log("Objeto detectado: " + hit.collider.name);
            return hit;
        }

        return default(RaycastHit);
    }
}
