using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLModel : MonoBehaviour {
    public int tau = 10; // naumber of frames

    // get input from INPUT-module
    public GameObject mlModel;
    public MLModel mlmodel;
    public float[,] inputArray;

    // Using dummy model for now
    void Start () {
        // initialize ML clock


        //
	}
	
	// Update is called once per frame
	void Update () {
        DummyModel();
	}


    private void DummyModel()
    {
        // get input from input module

        //

        // update obj matrix
    }

    public void UpdateSceneMatrix()
    {
        // update array
    }
}

