using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMaster : MonoBehaviour {

    public  float MaxTimer = 0.05f;
    private float currentTime = 0;
    public static bool AnimationSync = false;
    



    void Start ()
    {
		
	}
	
	
	void Update ()
    {
        AnimationSync = false;
        currentTime += Time.deltaTime;
        if (currentTime >= MaxTimer)
        {
            currentTime = 0;
            AnimationSync = true;
        }
    }
}
