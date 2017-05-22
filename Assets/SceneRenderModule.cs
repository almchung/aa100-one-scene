using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRenderModule : MonoBehaviour {
    // get input from INPUT-module
    public MLModel mlModel;
    public int[] actionArray;

    // Use this for initialization
    void Start () {
        // get input from ML module
        mlModel = GameObject.Find("ML_module").GetComponent<MLModel>();
        actionArray = mlModel.outputArray;
    }
	
	// Update is called once per frame
	void Update () {
        actionArray = mlModel.outputArray;
    }

    void RenderScene()
    {

    }
}
