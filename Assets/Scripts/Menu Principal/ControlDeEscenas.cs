using UnityEngine;
using UnityEngine.SceneManagement;


public class ControlDeEscenas : MonoBehaviour
{
    public string _newGameEscena;



    public void CambiarDeEscena()
    {
        SceneManager.LoadScene("NivelPrueba");
    }

    public void RegresarEscena()
    {
        SceneManager.LoadScene("Menu Principal");
    }

    public void NewGameEscena()
    {
        SceneManager.LoadScene(_newGameEscena);
    }

}
