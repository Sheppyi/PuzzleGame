using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //variable initiation
    //zoom
    private float DefaultSize = 2;
    public float ScrollAmount = 0.15f;
    private float MaxSize = 2.4f;
    private float MinSize = 0.4f;
    private float CurrentSize;

    //pan
    public float PanSpeed = 4f;                     //mouse drag speed

    //drift prevention
    public static float DefaultX = 2.288f;           //how far the camera can drift
    public static float DefaultY = 1.4f;            //how far the camera can drift



    void Start()
    {
        CurrentSize = DefaultSize;
        GetComponent<Camera>().orthographicSize = CurrentSize;
    }


    void Update()
    {
        CurrentSize -= Mathf.Clamp(Input.GetAxis("Mouse ScrollWheel"), -0.1f, 0.1f) * 10 * ScrollAmount;    //zooming
        CurrentSize = Mathf.Clamp(CurrentSize, MinSize, MaxSize);   //clamp to zoom bounds
        GetComponent<Camera>().orthographicSize = CurrentSize;  //convert to unity cancer language

        //Camera Pan
        if (Input.GetMouseButton(2))
        {
            transform.position -= new Vector3(Input.GetAxis("Mouse X") * PanSpeed * Time.deltaTime * CurrentSize, Input.GetAxis("Mouse Y") * PanSpeed * Time.deltaTime * CurrentSize, 0);   //move camera
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -DefaultX, DefaultX), Mathf.Clamp(transform.position.y, -DefaultY, DefaultY), transform.position.z); //clamp (prevent drift)
        }







      
    }

}
