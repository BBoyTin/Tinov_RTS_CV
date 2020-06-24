using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ovdje skripta i jos moze se staviti follow objekta
// https://www.youtube.com/watch?v=rnqF6S7PfFA

public class CameraContollerImproved : MonoBehaviour
{
    public Transform cameraTransform;

    public float normalSpeed=0.5f;
    public float fastSpeed=2f;
    public float movementSpeed=1f;
    float movementTime=5f;
    public float rotationAmount=1f;
    Vector3 zoomAmount = new Vector3(0, -3, 3);
    public float minZoom=7f;
    public float maxZoom = 75f;


    Vector3 newPosition;
    Quaternion newRotation;
    Vector3 newZoom;

    Vector3 dragStartPosition;
    Vector3 dragCurrentPosition;
    Vector3 rotateStartPosition;
    Vector3 rotateCurrentPosition;

    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    
    void Update()
    {
        HandleMovmentInput();
        HandleMouseInput();

    }

    void HandleMouseInput()
    {
        //zoom
        if(Input.mouseScrollDelta.y != 0 )
        {
            // && newZoom.y > minZoom && newZoom.y < maxZoom
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
            newZoom.y = Mathf.Clamp(newZoom.y, minZoom, maxZoom);
        }

        //rotacija srednji klik komentirano da mogu srednjim klikom kretanje

        /*
        if (Input.GetMouseButtonDown(2))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            rotateCurrentPosition = Input.mousePosition;

            Vector3 difference = rotateStartPosition - rotateCurrentPosition;
            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
        }
        */


        //kretanje srednji klik
        if (Input.GetMouseButtonDown(2))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if(plane.Raycast(ray,out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }

        if (Input.GetMouseButton(2))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);
                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }
    }


    void HandleMovmentInput()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += transform.forward * movementSpeed;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += transform.forward * -movementSpeed;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += transform.right * movementSpeed;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += transform.right * -movementSpeed;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        
        if (Input.GetKey(KeyCode.R))
        {
            if (newZoom.y > minZoom)
                newZoom += zoomAmount;
        }
        if (Input.GetKey(KeyCode.F))
        {
            if(newZoom.y < maxZoom)
                newZoom -= zoomAmount;
        }


        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);


    }
}
