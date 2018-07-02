using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionScript : MonoBehaviour {

    private const float GridSize = 0.2f;    //size of the grid !SHOULD NEVER BE CHANGED!
    private float opacity = 0.5f;           //alpha level

    public Sprite furnaceSprite;
    public GameObject furnacePrefab;
    

    private void Start()
    {
        //prep
        Color alpha = this.GetComponent<SpriteRenderer>().color;    //change alpha
        alpha.a = opacity;
        this.GetComponent<SpriteRenderer>().color = alpha;
        //assign correct sprite
        switch (BlockPlacer.SelectedElement)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = furnaceSprite;
                break;
        }
    }

    void Update ()
    {
        //grid alignment
        Vector3 worldpoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);   //convert mouse pixel data to coordinates
        transform.position = new Vector3(worldpoint.x, worldpoint.y, worldpoint.z ); //place object right underneath cursor
        transform.position = new Vector3((Mathf.Round((1 / GridSize) * transform.position.x) / (1 / GridSize)), (Mathf.Round((1 / GridSize) * transform.position.y) / (1 / GridSize)), 0); //clamp object to grid
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-2.8f,2.8f), Mathf.Clamp(transform.position.y, -1.6f, 1.6f), BlockPlacer.currentDepth); //clamps the object to the playfield

        //canceling
        if (Input.GetMouseButtonDown(1))
        {
            BlockPlacer.SelectedElement = 0;
            Destroy(gameObject);
            Debug.Log("Cancelled Block palcement");                                         //DEBUG LOG
        }

        //placing block
        if (Input.GetMouseButtonUp(0) /* && no block underneath*/ )
        {
            switch (BlockPlacer.SelectedElement)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    Instantiate(furnacePrefab,new Vector3(transform.position.x, transform.position.y, BlockPlacer.currentDepth), new Quaternion());
                    break;
            }

            Debug.Log("Placed block number " + BlockPlacer.SelectedElement);                // DEBUG LOG
            BlockPlacer.SelectedElement = 0;    //reset selected element
            Destroy(gameObject);    //destroy the object
        }
    }
}
