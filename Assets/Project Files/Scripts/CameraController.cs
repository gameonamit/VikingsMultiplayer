using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float CameraMoveSpeed = 120f;
    [SerializeField] private float minClampAngle = -80.0f;
    [SerializeField] private float maxClampAngle = 80.0f;
    [SerializeField] private float inputSensitivity = 150.0f;
    private float mouseX, mouseY;
    private float rotY = 0.0f;
    private float rotX = 0.0f;

    private void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        CameraLook();
        CameraMovement();
    }

    private void CameraLook()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        rotY += mouseX * inputSensitivity * Time.deltaTime;
        rotX += mouseY * inputSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, minClampAngle, maxClampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }

    private void CameraMovement()
    {
        if (target != null)
        {
            float step = CameraMoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }
}
