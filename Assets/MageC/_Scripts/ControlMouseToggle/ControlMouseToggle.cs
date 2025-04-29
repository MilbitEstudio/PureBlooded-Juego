using UnityEngine;

public class ControlMouseToggle : MonoBehaviour
{
    
    private bool cursorVisible = false;

    void Start()
    {
        
        Cursor.visible = cursorVisible;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
          
            cursorVisible = !cursorVisible;

           
            Cursor.visible = cursorVisible;

            
            if (cursorVisible)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
