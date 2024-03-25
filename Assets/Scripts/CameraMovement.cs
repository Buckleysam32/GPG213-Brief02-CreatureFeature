using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    public float panningSpeed = 20f;
    public float borderSize = 10f;
    public float zoomAmount = 100f;
    public float maxZoom = 4f;
    public float minZoom = 12f;
    public float cameraFOV;

    public float damping;

    private Camera m_camera;

    // Start is called before the first frame update
    void Start()
    {
        m_camera = GetComponent<Camera>();
        cameraFOV = 70f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        m_camera.fieldOfView = cameraFOV;

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - borderSize)
        {
            pos.z += panningSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= borderSize)
        {
            pos.z -= panningSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - borderSize)
        {
            pos.x += panningSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= borderSize)
        {
            pos.x -= panningSpeed * Time.deltaTime;
        }

        transform.position = pos;

        Vector3 cameraZoom = transform.localPosition;


        if (Input.GetAxis("Mouse ScrollWheel") < 0f && transform.position.y <= 12f) // Zoom out
        {
            transform.localPosition -= transform.forward * Time.deltaTime * zoomAmount;
            print("Zoom Out");
        }

        
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && transform.position.y >= 4f) // Zoom in
        {
            transform.localPosition += transform.forward * Time.deltaTime * zoomAmount;
            print("Zoom In");
        }

    }
}
