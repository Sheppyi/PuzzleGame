using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialScript : MonoBehaviour {


    Vector3 currentPosition;
    Vector3 nextPosition;
    public bool hasToMove = false;


    private void Start()
    {
        currentPosition = transform.position;
    }


    void Update()
    {
        if ((transform.position.z != BlockPlacer.currentDepth - 1))
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

        //moving 
        if (hasToMove)
        {
            transform.position = nextPosition;
            currentPosition = transform.position;
        }


        if (GameMaster.preStep)
        {
            if (hasToMove)
            {
                currentPosition = nextPosition;
                this.gameObject.transform.position = currentPosition;
            }
            hasToMove = false;
        }

    }



    public void moveToPosition(Vector3 positionToMoveTo)
    {
        nextPosition = positionToMoveTo;
        hasToMove = true;
    }

}
