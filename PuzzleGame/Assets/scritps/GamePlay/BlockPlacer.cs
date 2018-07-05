using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacer : MonoBehaviour
{

    public const int furnaceButton = 3;
    public const int conveyorButton = 2;
    public const int spawnerButton = 1;



    public static int SelectedElement = 0;     //selected element determains which element is currently selected (duh
    public GameObject Selection;
    public static int selectionScriptRotation = 180;
    private bool isForeGround = true;
    private float foregroundDepth = -8f;
    private float backgroundDepth = -4f;
    private bool inTabAnimation = false;
    private float TabAnimationLenght = 0.2f;      //transition lenght in seconds
    public static float currentDepth;
    private float remainingTime;

    GameObject PlayFieldForeGround;

    private void Start()
    {
        currentDepth = foregroundDepth;
        PlayFieldForeGround = GameObject.FindWithTag("PlayFieldForeGround");
        //GameObject PlayFieldBackGround = GameObject.FindWithTag("PlayFieldBackGround");
    }

    private void Update()
    {
        if (!GameMaster.gameRunning)
        {
            //select element
            if (Input.GetKeyDown(System.Convert.ToString(furnaceButton)) && SelectedElement == 0)
            {
                SelectElement(furnaceButton);
            }
            if (Input.GetKeyDown(System.Convert.ToString(conveyorButton)) && SelectedElement == 0)
            {
                SelectElement(conveyorButton);
            }
            if (Input.GetKeyDown(System.Convert.ToString(spawnerButton)) && SelectedElement == 0)
            {
                SelectElement(spawnerButton);
            }


            //deleting Placed Block
            if (Input.GetMouseButtonDown(1) && SelectedElement == 0 && Physics.CheckSphere(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, currentDepth), 0f))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    Destroy(hit.collider.gameObject);
                }
            }

        }

        //change level
        if (Input.GetKeyDown(KeyCode.Tab) && !inTabAnimation)
        {
            remainingTime = TabAnimationLenght;
            inTabAnimation = true;
            if (isForeGround)
            {
                isForeGround = false;
                currentDepth = backgroundDepth;
            }
            else
            {
                isForeGround = true;
                currentDepth = foregroundDepth;
            }
        } 

        //animation
        if (inTabAnimation && remainingTime > 0)
        {
            var modifier = (foregroundDepth - backgroundDepth) / TabAnimationLenght * Time.deltaTime;
            if (!isForeGround)
            {
                Camera.main.transform.position -= new Vector3(0, 0, modifier);
                var scaleModifier = PlayFieldForeGround.transform.localScale.x - (modifier / 4);
                scaleModifier = Mathf.Clamp(scaleModifier, 1, 63);
                PlayFieldForeGround.GetComponent<Renderer>().enabled = false;
                PlayFieldForeGround.transform.localScale = new Vector3(scaleModifier, scaleModifier, scaleModifier);
            }
            else
            {
                Camera.main.transform.position += new Vector3(0, 0, modifier);
                var scaleModifier = PlayFieldForeGround.transform.localScale.x - (-modifier / 3);
                scaleModifier = Mathf.Clamp(scaleModifier, 1, 63);
                PlayFieldForeGround.transform.localScale = new Vector3(scaleModifier, scaleModifier, scaleModifier);
            }
            remainingTime -= Time.deltaTime;
        }
        if (remainingTime <= 0)
        {
            inTabAnimation = false;
            PlayFieldForeGround.GetComponent<Renderer>().enabled = true;
        }

        //prevent camera derailing
        if(-10 > Camera.main.transform.position.z || Camera.main.transform.position.z > -6)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Mathf.Clamp(Camera.main.transform.position.z, -10, -6));
        }
    }

    
    private void SelectElement(int ElementNumber)
    {
        SelectedElement = ElementNumber;    //apply correct elementNumber
        Instantiate(Selection, Camera.main.ScreenToWorldPoint(Input.mousePosition), new Quaternion());
    }

}
