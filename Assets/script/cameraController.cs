using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed;
    public float cameraHeight;
    public float cameraSpace;

    public GameObject player;

    private void Update()
    {
        Vector3 dir = player.transform.position - this.transform.position;
        float deltaSpeed = cameraSpeed * Time.deltaTime;
        Vector3 moveVector = new Vector3(dir.x * deltaSpeed, (dir.y + cameraHeight) * deltaSpeed, (dir.z - cameraSpace) * deltaSpeed);
        this.transform.Translate(moveVector);
    }
}
