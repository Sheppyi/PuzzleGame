using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {


    public static float stepLength = 0.5f;

    private float stepTimer;


    public static bool gameRunning;       //is game running or not
    public static bool preStep; //prestep activates the frame before step does
    public static bool step;


	void Start ()
    {
        stepTimer = 0;
        step = false;
        preStep = false;
	}
	
	
	void Update ()
    {
        //gamerunning
        if (Input.GetKeyDown(KeyCode.W))
        {
            gameRunning = !gameRunning;
            if (gameRunning)
            {
                RunPrep();
                Debug.Log("GameRunning");
            }
            else
            {
                BuildPrep();
                Debug.Log("GamePaused");
            }
        }


        //game is running
        if (gameRunning)
        {
            step = false;
            if (!preStep)
            {
                stepTimer += Time.deltaTime;
                if (stepTimer >= stepLength)
                {
                    stepTimer = 0;
                    preStep = true;
                }
            }
            else
            {
                preStep = false;
                step = true;
            }
        }
        else
        {
            step = false;
            preStep = false;
            stepTimer = 0;
        }
	}








    private void RunPrep()
    {
        var gameObjects = GameObject.FindGameObjectsWithTag("DestroyOnRun");
        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
            BlockPlacer.SelectedElement = 0;
        }
    }

    private void BuildPrep()
    {

    }






    //collision functions
    public static bool CheckCollisionOnSpot(Vector3 position)
    {
        bool canBePlaced;
        if(!Physics.CheckSphere(new Vector3(position.x, position.y, position.z - 1), 0f))
        { canBePlaced = true; }
        else
        { canBePlaced = false;}
        return (canBePlaced);
    }







}
