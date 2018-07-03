using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionScript : MonoBehaviour {

    public Sprite furnaceSprite;
    public GameObject furnacePrefab;
    public Sprite conveyorSprite;
    public GameObject conveyorPrefab;

    private int currentRotation;


    private Color rendColor;
    private const float GridSize = 0.2f;    //size of the grid !SHOULD NEVER BE CHANGED!
    private float opacity = 0.6f;           //alpha level



    private void Start()
    {
        //prep
        currentRotation = BlockPlacer.selectionScriptRotation;
        transform.eulerAngles = new Vector3(0, 0, currentRotation);
        rendColor = this.GetComponent<SpriteRenderer>().color;    //change alpha
        rendColor.a = opacity;
        this.GetComponent<SpriteRenderer>().color = rendColor;
        
        //assign correct sprite
        switch (BlockPlacer.SelectedElement)
        {
            case BlockPlacer.conveyorButton:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = conveyorSprite;
                break;
            case BlockPlacer.furnaceButton:
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
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-2.8f,2.8f), Mathf.Clamp(transform.position.y, -1.6f, 1.6f), BlockPlacer.currentDepth - 1); //clamps the object to the playfield

        //changing color
        if (Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), 0f))
        { this.GetComponent<SpriteRenderer>().color = new Color(1, 0.1f, 0.1f,opacity); }
        else
        { this.GetComponent<SpriteRenderer>().color = new Color(1, 1f, 1f, opacity); }

        //canceling
        if (Input.GetMouseButtonDown(1))
        {
            CancelPlacement();                                        //DEBUG LOG
        }

        //changing block
        if (Input.GetKeyDown(System.Convert.ToString(BlockPlacer.furnaceButton)))
        {
            BlockPlacer.SelectedElement = BlockPlacer.furnaceButton;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = furnaceSprite;
        }
        if (Input.GetKeyDown(System.Convert.ToString(BlockPlacer.conveyorButton)))
        {
            BlockPlacer.SelectedElement = BlockPlacer.conveyorButton;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = conveyorSprite;
        }

        //rotating block
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentRotation <= 0) { currentRotation = 360; }
            currentRotation -= 90;
            if (currentRotation <= 0) { currentRotation = 360; }
            BlockPlacer.selectionScriptRotation = currentRotation;
            transform.eulerAngles = new Vector3(0, 0, currentRotation);
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (currentRotation >= 360) { currentRotation = 0; }
            currentRotation += 90;
            if (currentRotation >= 360) { currentRotation = 0; }
            BlockPlacer.selectionScriptRotation = currentRotation;
            transform.eulerAngles = new Vector3(0, 0, currentRotation);
        }

        

        //placing block
        if (Input.GetMouseButtonUp(0) &&  !Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z + 1),0f))
        {
            switch (BlockPlacer.SelectedElement)
            {
                case BlockPlacer.conveyorButton:
                    var conveyor =Instantiate(conveyorPrefab, new Vector3(transform.position.x, transform.position.y, BlockPlacer.currentDepth), new Quaternion());
                    conveyor.transform.eulerAngles = new Vector3(0,0,currentRotation);
                    break;
                case BlockPlacer.furnaceButton:
                    var furnace = Instantiate(furnacePrefab,new Vector3(transform.position.x, transform.position.y, BlockPlacer.currentDepth), new Quaternion());
                    furnace.transform.eulerAngles = new Vector3(0, 0, currentRotation);
                    break;
            }
            Debug.Log("Placed block number " + BlockPlacer.SelectedElement);                // DEBUG LOG
            BlockPlacer.SelectedElement = 0;    //reset selected element
            Destroy(gameObject);    //destroy the object
        }
        else if(Input.GetMouseButtonUp(0))
        {
            Debug.Log("Block cannot be placed here. Space occupied");
            CancelPlacement();

        }
    }



    private void CancelPlacement()
    {
        BlockPlacer.SelectedElement = 0;
        Destroy(gameObject);
        Debug.Log("Cancelled Block palcement");
    }
}
