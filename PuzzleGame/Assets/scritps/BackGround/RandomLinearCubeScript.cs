using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLinearCubeScript : MonoBehaviour {

    
    private float boundX = 7.5f;
    private float bigCubeBoundX = 9f;
    private float boundY = 4.5f;
    private float bigCubeBoundY = 6f;
    private float depth;
    private float scale;
    private float xSpeed;
    private float ySpeed;
    private Vector3 direction;
    private Vector3 rotation;
    private float rotationMultiplier;
    private bool isBigCube = false;

    

    private static float maxRotationSpeed = 5;
    private static float maxSpeed = 0.05f;
    private static float bigCubeSpeed = 0.008f;
    private static float bigCubePossibility = 2f;   
    private static float bigCubeScale = 2.4f;
    public static bool bigCubeExists = false;

	
	void Start ()
    { 
        GetComponent<Renderer>().material.color = new Color(0.3f,0.3f,1,1);

        if (Random.Range(0f, 100f) <= bigCubePossibility && !bigCubeExists)
        {
            depth = 2;
            scale = bigCubeScale;
            bigCubeExists = true;
            rotationMultiplier = 0.5f;
            xSpeed = 0;
            isBigCube = true;
            while (xSpeed == 0)
            {
                xSpeed = Random.Range(-bigCubeSpeed, bigCubeSpeed);
            }
            ySpeed = 0;
            while (ySpeed == 0)
            {
                ySpeed = Random.Range(-bigCubeSpeed, bigCubeSpeed);
            }

        }
        else
        {
            depth = Random.Range(3f, 18f);
            scale = 1 / (depth) * 2f;
            xSpeed = 0;
            rotationMultiplier = 1;
            while (xSpeed == 0)
            {
               xSpeed = Random.Range(-maxSpeed,maxSpeed);
            }
            ySpeed = 0;
            while (ySpeed == 0)
            {
                ySpeed = Random.Range(-maxSpeed, maxSpeed);
            }
        }

        transform.position = new Vector3(Random.Range(-boundX, boundX), Random.Range(-boundY, boundY), depth);
        transform.localScale = new Vector3(scale, scale, scale);
        transform.eulerAngles = new Vector3(Random.Range(1f, 360f), Random.Range(1f, 359f), Random.Range(1f, 360f));
        direction = new Vector3(xSpeed,ySpeed,0);
        rotation = new Vector3(Random.Range(-maxRotationSpeed, maxRotationSpeed), Random.Range(-maxRotationSpeed, maxRotationSpeed), Random.Range(-maxRotationSpeed, maxRotationSpeed));
    }
	
	
	void Update ()
    {
        transform.position += direction * Time.deltaTime;       //move the cube
        transform.Rotate(rotation.x * Time.deltaTime * rotationMultiplier, rotation.y * Time.deltaTime * rotationMultiplier, rotation.z * Time.deltaTime * rotationMultiplier); //rotate the cube

        if (isBigCube)
        {
            if (-bigCubeBoundX >= transform.position.x || transform.position.x >= bigCubeBoundX || -bigCubeBoundY >= transform.position.y || transform.position.y >= bigCubeBoundY)
            {
                Respawn();
            }
        }
        else
        {
            if (-boundX >= transform.position.x || transform.position.x >= boundX || -boundY >= transform.position.y || transform.position.y >= boundY)
            {
                Respawn();
            }
        }
	}


    void Respawn()
    {
        if (isBigCube)
        {
            isBigCube = false;
            bigCubeExists = false;
        }

        if (Random.Range(0f, 100f) <= bigCubePossibility && !bigCubeExists)
        {
            depth = 2;
            scale = bigCubeScale;
            bigCubeExists = true;
            xSpeed = 0;
            isBigCube = true;
            rotationMultiplier = 0.5f;
            while (xSpeed == 0)
            {
                xSpeed = Random.Range(-bigCubeSpeed, bigCubeSpeed);
            }
            ySpeed = 0;
            while (ySpeed == 0)
            {
                ySpeed = Random.Range(-bigCubeSpeed, bigCubeSpeed);
            }
        }
        else
        {
            depth = Random.Range(3f, 18f);
            scale = 1 / (depth) * 2f;
            xSpeed = 0;
            rotationMultiplier = 1f;
            while (xSpeed == 0)
            {
                xSpeed = Random.Range(-maxSpeed, maxSpeed);
            }
            ySpeed = 0;
            while (ySpeed == 0)
            {
                ySpeed = Random.Range(-maxSpeed, maxSpeed);
            }
        }

        var Side = Random.Range(1, 4);
        if (!isBigCube)
        {
            switch (Side)
            {
                case 1:
                    transform.position = new Vector3(boundX, Random.Range(-boundY, boundY), depth);
                    break;
                case 2:
                    transform.position = new Vector3(-boundX, Random.Range(-boundY, boundY), depth);
                    break;
                case 3:
                    transform.position = new Vector3(Random.Range(-boundX, boundX), boundY, depth);
                    break;
                case 4:
                    transform.position = new Vector3(Random.Range(-boundX, boundX), -boundY, depth);
                    break;
            }
        }
        else
        {
            switch (Side)
            {
                case 1:
                    transform.position = new Vector3(bigCubeBoundX, Random.Range(-bigCubeBoundY, bigCubeBoundY), depth);
                    break;
                case 2:
                    transform.position = new Vector3(-bigCubeBoundX, Random.Range(-bigCubeBoundY, bigCubeBoundY), depth);
                    break;
                case 3:
                    transform.position = new Vector3(Random.Range(-bigCubeBoundX, bigCubeBoundX), bigCubeBoundY, depth);
                    break;
                case 4:
                    transform.position = new Vector3(Random.Range(-bigCubeBoundX, bigCubeBoundX), -bigCubeBoundY, depth);
                    break;
            }
        }

        

        transform.localScale = new Vector3(scale, scale, scale);
        transform.eulerAngles = new Vector3(Random.Range(1f, 360f), Random.Range(1f, 360f), Random.Range(1f, 360f));
        direction = new Vector3(xSpeed, ySpeed, 0);
        rotation = new Vector3(Random.Range(-maxRotationSpeed, maxRotationSpeed), Random.Range(-maxRotationSpeed, maxRotationSpeed), Random.Range(-maxRotationSpeed, maxRotationSpeed));
    }


   













}
