using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBehaviour : MonoBehaviour

{

    public float RotateSpeed;

    void Update()

    {

        transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);

    }

}