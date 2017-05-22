using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLModel : MonoBehaviour {
    public int tau;

    public int[][] objectArray;

    // Using dummy model for now
	void Start () {
        // initialize ML clock

        // set tau(delta t) [in number of frames]
        tau = 10;
        //
	}
	
	// Update is called once per frame
	void Update () {
        DummyModel();
	}


    private void DummyModel()
    {
        // do something
    }
}

