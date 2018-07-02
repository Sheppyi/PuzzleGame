using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacer : MonoBehaviour
{
    public static int SelectedElement = 0 ;     //selected element determains which element is currently selected (duh
    public GameObject Selection;

    private bool isForeGround = true;
    private float foregroundDepth = -8f;
    private float backgroundDepth = -4f;
    private bool inTabAnimation = false;
    private float TabAnimationLenght = 0.15f;
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
        //select element
        if (Input.GetKeyDown("3") && SelectedElement == 0)
        {      
            SelectElement(3);
        }



        //change level
        if (Input.GetKeyDown(KeyCode.Tab)  && !inTabAnimation)
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
        }   //animation
        if (inTabAnimation && remainingTime > 0)
        {
            var modifier = (foregroundDepth - backgroundDepth) / TabAnimationLenght * Time.deltaTime;
            if (!isForeGround)
            {
                Camera.main.transform.position -= new Vector3(0,0,modifier);
                var scaleModifier = PlayFieldForeGround.transform.localScale.x - (modifier * 15);
                scaleModifier = Mathf.Clamp(scaleModifier, 1, 63);
                PlayFieldForeGround.transform.localScale = new Vector3(scaleModifier, scaleModifier, scaleModifier);
            }
            else
            {
                Camera.main.transform.position += new Vector3(0, 0, modifier);
                var scaleModifier = PlayFieldForeGround.transform.localScale.x - (-modifier * 16);
                scaleModifier = Mathf.Clamp(scaleModifier, 1, 63);
                PlayFieldForeGround.transform.localScale = new Vector3(scaleModifier, scaleModifier, scaleModifier);
            }
            remainingTime -= Time.deltaTime;
        }
        if(remainingTime <= 0)
        {
            inTabAnimation = false;
        }
    }

    private void SelectElement(int ElementNumber)
    {
        SelectedElement = ElementNumber;    //apply correct elementNumber
        Instantiate(Selection, Camera.main.ScreenToWorldPoint(Input.mousePosition), new Quaternion());
    }

}
