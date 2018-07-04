using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

    public int selectedSpawn;
    public static int spawnInterval = 3;     //in steps

    private const int materialWood = 1;
    public GameObject woodPrefab;

    private int counter;

	void Update ()
    {
        if (GameMaster.step)
        {
            counter++;
            if (counter == spawnInterval)
            {
                counter = 0;
                Spawn();
            }
        }
	}



    private void Spawn()
    {
        if (GameMaster.CheckCollisionOnSpot(transform.position))
        {
            switch(selectedSpawn)
            {
                case materialWood:
                    Instantiate(woodPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), new Quaternion());
                    break;
            }
        }
        else
        {
            Debug.Log("CannotSpawn");
        }
    }





}


