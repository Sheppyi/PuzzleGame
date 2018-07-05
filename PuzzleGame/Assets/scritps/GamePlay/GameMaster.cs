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
    public static bool CheckCollisionOnSpot(Vector3 position, int offSet)       //OnSpot
    {
        bool isFree;
        if(!Physics.CheckSphere(new Vector3(position.x, position.y, position.z + offSet), 0f))
        { isFree = true; }
        else
        { isFree = false;}
        return (isFree);
    }

    public static bool CheckCollisionRight(Vector3 position, int offSet)       //Right
    {
        bool isFree;
        if (!Physics.CheckSphere(new Vector3(position.x + SelectionScript.GridSize, position.y, position.z + offSet), 0f))
        { isFree = true; }
        else
        { isFree = false; }
        return (isFree);
    }



    //moving functions;
    public static void MoveMaterial(Vector3 startPosition, string direction)
    {
        Debug.Log("Moving initiated");
        startPosition.z--;
        Vector3 endPosition;
        if (direction == "Right")
        {
            endPosition = new Vector3(startPosition.x + SelectionScript.GridSize, startPosition.y, startPosition.z);
        }
        else
        {
            return;
        }

        bool done = false;
        int blocksToMove = 1;
        Vector3 finalPosition = endPosition;
        while (!done)
        {
            done = true;
            if (Physics.CheckSphere(finalPosition, 0f))
            {
                blocksToMove++;
                done = false;
                switch (direction)
                {
                    case "Right":
                        finalPosition.x += SelectionScript.GridSize;
                        break;
                    case "Left":
                        finalPosition.x -= SelectionScript.GridSize;
                        break;
                    case "Up":
                        finalPosition.y += SelectionScript.GridSize;
                        break;
                    case "Down":
                        finalPosition.y -= SelectionScript.GridSize;
                        break;
                }
            }
        }
        Vector3 collisionPosition = finalPosition;
        switch (direction)
        {
            case "Right":
                collisionPosition.x -= SelectionScript.GridSize;
                break;
            case "Left":
                collisionPosition.x += SelectionScript.GridSize;
                break;
            case "Up":
                collisionPosition.y -= SelectionScript.GridSize;
                break;
            case "Down":
                collisionPosition.y += SelectionScript.GridSize;
                break;
        }
        //actual moving
        while (blocksToMove > 0)
        {
            Debug.Log("blocks to move = " + blocksToMove);
            blocksToMove--;
            Collider[] toBeMoved = Physics.OverlapSphere(collisionPosition, 0f);
            toBeMoved[0].gameObject.GetComponent<MaterialScript>().moveToPosition(finalPosition);

            switch (direction)
            {
                case "Right":
                    finalPosition.x -= SelectionScript.GridSize;
                    collisionPosition.x -= SelectionScript.GridSize;
                    break;
                case "Left":
                    finalPosition.x += SelectionScript.GridSize;
                    collisionPosition.x += SelectionScript.GridSize;
                    break;
                case "Up":
                    finalPosition.y -= SelectionScript.GridSize;
                    collisionPosition.y -= SelectionScript.GridSize;
                    break;
                case "Down":
                    finalPosition.y += SelectionScript.GridSize;
                    collisionPosition.y += SelectionScript.GridSize;
                    break;
            }

        }

    }











}
