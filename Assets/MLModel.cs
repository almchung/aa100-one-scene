using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MLModel : MonoBehaviour {
    public int tau; // naumber of frames

    // get input from INPUT-module
    public DataInputModule dataInputModule;
    public float[,] inputArray;

    // feed output to SceneRENDER-module
    //public GameObject sceneRenderModule;
    //public int[,] outputArray;

    // Using dummy model for now
    void Start () {
        // initialize ML clock
        tau = 10;


        //
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.frameCount%tau == 0)
        {
            DummyModel();
        }
	}

    private void DummyModel()
    {
        // get input from input module
        dataInputModule = GameObject.Find("Camera (eye)").GetComponent<DataInputModule>();
        inputArray = dataInputModule.inputArray;

        string strDebug = "";
        int rowLength = inputArray.GetLength(0);
        int colLength = inputArray.GetLength(1);

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                strDebug = string.Concat(strDebug, inputArray[i, j].ToString());
                strDebug = string.Concat(strDebug, ",");
            }
            strDebug = string.Concat(strDebug, "\n");
        }

        Debug.Log(strDebug);

        // get actions from policy list

        getActions();

    }

    public void getActions()
    {
        // update obj matrix

    }
}

