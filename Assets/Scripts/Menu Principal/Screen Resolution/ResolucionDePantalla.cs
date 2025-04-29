using UnityEngine;

public class ResolucionDePantalla : MonoBehaviour
{

    bool fullscreen = false;
    public void Resolucion1920x1080()
    {
        Screen.SetResolution(1920,1080,fullscreen);
    }
    public void Resolucion1366x768()
    {
        Screen.SetResolution(1366, 768, fullscreen);
    }

    public void Resolucion1280x720()
    {
        Screen.SetResolution(1280,720, fullscreen);
    }
    public void Resolucion800x600()
    {
        Screen.SetResolution(800, 600, fullscreen);
    }

}
