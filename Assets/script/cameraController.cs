using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float mouseSens;
    public Transform orientation;
    private float xRotation;
    private float yRotation;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void OnLook(InputValue value)
    {
        Vector2 mouseDelta = value.Get<Vector2>();
        yRotation += mouseDelta.x * mouseSens * Time.deltaTime;
        xRotation -= mouseDelta.y * mouseSens * Time.deltaTime;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
