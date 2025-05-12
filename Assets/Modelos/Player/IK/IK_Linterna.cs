using UnityEngine;

public class IK_Linterna : MonoBehaviour
{
    public Transform AnteBrazo;
    public Transform Brazo;
    public Transform Mano;
    public Transform AnteBrazoRef;
    public Transform BrazoRef;
    public Transform ManoRef;
    public Transform Camara;
    public GameObject Linterna;
    public bool ActivarLinterna_;

    [Range(0f, 1f)] public float influenciaBrazo = 0.3f;
    [Range(0f, 1f)] public float influenciaMano = 0.5f;
    [Range(0.01f, 20f)] public float velocidadSuavizado = 5f;
    public PlayerMovementManager controller;

    private Quaternion rotActualBrazo;
    private Quaternion rotActualMano;
    private Quaternion rotActualAnteBrazo;

    private Quaternion rotInicialAnteBrazo;
    private Quaternion rotInicialBrazo;
    private Quaternion rotInicialMano;

    void Start()
    {
        rotActualAnteBrazo = AnteBrazo.rotation;
        rotActualBrazo = Brazo.rotation;
        rotActualMano = Mano.rotation;

        rotInicialAnteBrazo = rotActualAnteBrazo;
        rotInicialBrazo = rotActualBrazo;
        rotInicialMano = rotActualMano;
    }

    void ActivarLinterna()
    {
        if (Input.GetKeyDown(KeyCode.F) && !controller.IsCrouching)
        {
            ActivarLinterna_ = !ActivarLinterna_;
            Linterna.SetActive(ActivarLinterna_);
        }

        if (controller.IsCrouching)
        {
            if (Linterna.activeSelf)
                Linterna.SetActive(false);

            rotActualAnteBrazo = Quaternion.Slerp(rotActualAnteBrazo, rotInicialAnteBrazo, Time.deltaTime * velocidadSuavizado);
            rotActualBrazo = Quaternion.Slerp(rotActualBrazo, rotInicialBrazo, Time.deltaTime * velocidadSuavizado);
            rotActualMano = Quaternion.Slerp(rotActualMano, rotInicialMano, Time.deltaTime * velocidadSuavizado);
        }
        else if (ActivarLinterna_)
        {
            Quaternion rotObjetivoAnteBrazo = Quaternion.Euler(AnteBrazoRef.eulerAngles - new Vector3(Camara.eulerAngles.x, 0f, 0f));
            rotActualAnteBrazo = Quaternion.Slerp(rotActualAnteBrazo, rotObjetivoAnteBrazo, Time.deltaTime * velocidadSuavizado);

            Quaternion rotObjetivoBrazo = Quaternion.Slerp(BrazoRef.rotation, rotActualAnteBrazo, influenciaBrazo);
            Quaternion rotObjetivoMano = Quaternion.Slerp(ManoRef.rotation, rotActualAnteBrazo, influenciaMano);

            rotActualBrazo = Quaternion.Slerp(rotActualBrazo, rotObjetivoBrazo, Time.deltaTime * velocidadSuavizado);
            rotActualMano = Quaternion.Slerp(rotActualMano, rotObjetivoMano, Time.deltaTime * velocidadSuavizado);

            if (!Linterna.activeSelf)
                Linterna.SetActive(true);
        }
        else
        {
            rotActualAnteBrazo = Quaternion.Slerp(rotActualAnteBrazo, rotInicialAnteBrazo, Time.deltaTime * velocidadSuavizado);
            rotActualBrazo = Quaternion.Slerp(rotActualBrazo, rotInicialBrazo, Time.deltaTime * velocidadSuavizado);
            rotActualMano = Quaternion.Slerp(rotActualMano, rotInicialMano, Time.deltaTime * velocidadSuavizado);
        }

        if (Quaternion.Angle(rotActualAnteBrazo, rotInicialAnteBrazo) > 10f)
        {
            AnteBrazo.rotation = rotActualAnteBrazo;
            Brazo.rotation = rotActualBrazo;
            Mano.rotation = rotActualMano;
        }

    }



    void LateUpdate()
    {
        ActivarLinterna();
    }
}
