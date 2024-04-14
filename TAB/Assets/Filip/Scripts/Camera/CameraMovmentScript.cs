using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovmentScript : MonoBehaviour
{
    [SerializeField] private GameObject cameraObj;
    [SerializeField] private float movmentSpeed;
    [SerializeField] private float movmentTime;


    private Vector3 newPosition;
    private Quaternion newRotation;

    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
    }

    void Update()
    {
        HendleMovementInput();
        HendleMouseInput();
    }
    void HendleMouseInput()
    {       

        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movmentTime);
    }
    void HendleMovementInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            newPosition += (transform.forward * movmentSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            newPosition += (transform.forward * -movmentSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            newPosition += (transform.right * movmentSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            newPosition += (transform.right * -movmentSpeed);
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movmentTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movmentTime);
    }
}
