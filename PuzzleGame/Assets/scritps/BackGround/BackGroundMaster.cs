using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMaster : MonoBehaviour {

    private int CubeCount = 200;
    public GameObject prefab;


    void Start () {

        for(int i = 1; i <= CubeCount; i++)
        {
            CreateRandomLinearCube();
        }

	}
	
	
	void Update () {
		
	}




    private void CreateRandomLinearCube()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);  
    }


}
