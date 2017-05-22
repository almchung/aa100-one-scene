using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MLModel : MonoBehaviour {
    public int tau;

    // get input from INPUT-module
    public DataInputModule dataInputModule;
    public float[,] inputArray;
    private int colLength;
    private int rowLength;

    // feed output to SceneRENDER-module
    //public GameObject sceneRenderModule;
    //public int[,] outputArray;

    // Using dummy model for now
    void Start () {
        // initialize ML clock (cycle with tau = number of frames)
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
        rowLength = inputArray.GetLength(0);
        colLength = inputArray.GetLength(1);

        //Debug.Log(PrintTwoDArray(inputArray));

        // run ML model
        int category = RunMLModel();
        Debug.Log(category);

        // get actions from policy list
        getActions();

    }

    private int RunMLModel()
    {
        // DUMMY ALGORITHM
        //// 1. Find mean values over 10 frames
        float[] mean = new float[colLength];
        for (int j = 0; j < colLength; j++) 
        {
            for (int i = 0; i < rowLength; i++)
            {
                mean[j] = mean[j] + inputArray[i, j];
            }
        }

        //// 2. Assign zones based on mean angles
        int zone = 0;
        //Debug.Log(mean[3]/10+","+mean[4]/10+","+mean[5]/10);
        if (mean[3]/10 > 360 / 2)
        {
            if (mean[4] / 10 > 360 / 2)
            {
                zone = 0;
            }
            else
            {
                zone = 1;
            }
        }
        else
        {
            if (mean[4] / 10 > 360 / 2)
            {
                zone = 2;
            }
            else
            {
                zone = 3;
            }
        }

        return zone;
    }

    public void getActions()
    {
        // update obj matrix

    }

    public string PrintTwoDArray(float[,] arr)
    {
        string strDebug = "";

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                strDebug = string.Concat(strDebug, arr[i, j].ToString());
                strDebug = string.Concat(strDebug, ",");
            }
            strDebug = string.Concat(strDebug, "\n");
        }
        return strDebug;
    }

}

