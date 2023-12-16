using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerForPlane : MonoBehaviour
{
    [Tooltip("An array of transforms represention camera positions")]
    [SerializeField] Transform[] povs;
    [Tooltip("The speed at which the camera follows the plane")]
    [SerializeField] float speed = 100;

    private int index = 2;
    private Vector3 target;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) index = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) index = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) index = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha4)) index = 3;

        // Set out target to the relevant POV
        target = povs[index].position;
    }

    private void FixedUpdate()
    {
        // Move camera to desired position/orientation
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        transform.forward = povs[index].forward;
    }

}
