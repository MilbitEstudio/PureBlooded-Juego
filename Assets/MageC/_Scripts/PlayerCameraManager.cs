using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraManager : MonoBehaviour
{
    public Transform PlayerBody;
    public float MouseSensitivity = 100f;
    public float Smooth = 10f;

    float mouseX, mouseY;
    float xClamp = 0f;

    Quaternion camTargetRot;
    Quaternion bodyTargetRot;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        camTargetRot = transform.localRotation;
        bodyTargetRot = PlayerBody.localRotation;
    }

    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        xClamp -= mouseY;
        xClamp = Mathf.Clamp(xClamp, -90f, 90f);

        camTargetRot = Quaternion.Euler(xClamp, 0f, 0f);

        bodyTargetRot *= Quaternion.Euler(0f, mouseX, 0f);

        // Aplicar suavizado
        transform.localRotation = Quaternion.Slerp(transform.localRotation, camTargetRot, Smooth * Time.deltaTime);
        PlayerBody.localRotation = Quaternion.Slerp(PlayerBody.localRotation, bodyTargetRot, Smooth * Time.deltaTime);
    }
}
