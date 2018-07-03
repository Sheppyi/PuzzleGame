using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour {

    public Sprite[] spriteList;
    private int currentSprite;

    void Update ()
    {
        if (AnimationMaster.AnimationSync)
        {
            currentSprite++;
            if (currentSprite >= spriteList.Length)
            {
                currentSprite = 0;
            }
            GetComponent<SpriteRenderer>().sprite = spriteList[currentSprite];
        }
	}
}
