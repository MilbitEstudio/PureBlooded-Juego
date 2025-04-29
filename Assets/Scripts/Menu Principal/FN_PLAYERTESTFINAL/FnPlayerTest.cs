using UnityEngine;

public class FnPlayerTest : MonoBehaviour
{

    [SerializeField] private Vector3 direccion;
    [SerializeField] private float velocidad = 5f;

    private void Update()
    {
        MovimientoTestFinalPlayer();
    }

    public void MovimientoTestFinalPlayer()
    {
        float horX = Input.GetAxisRaw("Horizontal");
        float verY = Input.GetAxisRaw("Vertical");

        direccion = new Vector3(horX,0,verY);
        transform.Translate(direccion * velocidad * Time.deltaTime);
    }
}
