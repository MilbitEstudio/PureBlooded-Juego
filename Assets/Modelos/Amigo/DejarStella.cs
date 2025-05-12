using System.Collections;
using UnityEngine;

public class DejarStella : MonoBehaviour
{
    public Transform Player;
    public Transform Stella;
    public float Tiempo = 1f;

    void Start()
    {
        StartCoroutine(SeguirJugadorCadaTiempo());
    }

    IEnumerator SeguirJugadorCadaTiempo()
    {
        while (true)
        {
            yield return new WaitForSeconds(Tiempo);
            Stella.position = Player.position;
        }
    }
}
