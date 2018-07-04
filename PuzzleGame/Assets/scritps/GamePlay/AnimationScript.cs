using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour {

    public Sprite[] spriteList;

    void Update ()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[AnimationMaster.currentSprite];
    }
}
