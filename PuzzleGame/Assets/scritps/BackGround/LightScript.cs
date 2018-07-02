using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour {

    private float speed = 0.01f;

	void Update ()
    {
        transform.Rotate(0,speed,0);
	}
}
