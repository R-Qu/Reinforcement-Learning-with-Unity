using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPositionLeft : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        // When the object reached end of road, restart from beginning
        if (transform.position.x >= 36.0f)
        {
            transform.position = new Vector3(-5.9f, 0f, 34.83f);
        }
    }
}
