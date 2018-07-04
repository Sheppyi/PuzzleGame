using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMaster : MonoBehaviour {

    public float MaxTimer;
    private float currentTime = 0;
    public static bool AnimationSync = false;
    public static int currentSprite;
    private int animationFrames = 4;
    

	void Update ()
    {
        AnimationSync = false;
        currentTime += Time.deltaTime;
        if (currentTime >= MaxTimer)
        {
            currentTime = 0;
            AnimationSync = true;
        }

        if (AnimationSync)
        {
            currentSprite++;
            if (currentSprite >= animationFrames)
            {
                currentSprite = 0;
            }
        }
    }
}
