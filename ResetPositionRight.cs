using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPositionRight : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
		// When the object reached end of road, restart from beginning
        if (transform.position.x <= -6.0f)
        {
            transform.position = new Vector3(36.1f,0f,-23.1f);
        }
    }
}
