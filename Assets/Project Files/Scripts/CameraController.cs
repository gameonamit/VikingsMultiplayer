using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private LevelManager levelManager;
    [SerializeField] private float CameraMoveSpeed = 120f;
    [SerializeField] private float minClampAngle = -80.0f;
    [SerializeField] private float maxClampAngle = 80.0f;
    [SerializeField] private float inputSensitivity = 150.0f;
    private float mouseX, mouseY;
    private float rotY = 0.0f;
    private float rotX = 0.0f;

    private bool animating = false;

    private void Awake()
    {
        levelManager = LevelManager.instance;
    }

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
        if (levelManager.isPaused == false  && animating == false)
        {
            CameraLook();
            CameraMovement();
        }
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

    public void AnimateToNewPosition(Vector3 position, Quaternion rotation)
    {
        animating = true;
        StartCoroutine(Co_AnimateToNewPosition(position, rotation));
    }

    private IEnumerator Co_AnimateToNewPosition(Vector3 position, Quaternion rotation)
    {
        while (animating)
        {
            yield return new WaitForEndOfFrame();
            transform.position = Vector3.MoveTowards(transform.position, position, 5);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 5);
            if(transform.position == position && transform.rotation == rotation)
            {
                animating = false;
            }
        }
    }
}
