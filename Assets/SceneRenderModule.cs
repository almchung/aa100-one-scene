using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRenderModule : MonoBehaviour {
    // get input from INPUT-module
    public MLModel mlModel;
    public int[] actionArray;
    public string[] objNameArray;
    public bool[] objVisibleArray;

    private int tau;

    // Use this for initialization
    void Start () {
        // get input from ML module
        mlModel = GameObject.Find("ML_module").GetComponent<MLModel>();
        tau = 10;
        actionArray = mlModel.outputArray;

        // object array setup
        objNameArray = new string[4] {"Object00","Object01","Object02","Object03"};
        objVisibleArray = new bool[4];
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.frameCount % tau == 0)
        {
            actionArray = mlModel.outputArray;
            DecodeActionArray();
        }
        /*RenderScene();*/
    }

    void DecodeActionArray()
    {
        for (int i = 0; i<actionArray.Length; i++)
        {
            if (actionArray[i] == 0)
            {
                for(int n = 0; n<objVisibleArray.Length; n++)
                {
                    objVisibleArray[n] = false;
                }
            }
            else
            {
                for (int n = 0; n < objVisibleArray.Length; n++)
                {
                    objVisibleArray[n] = true;
                }
            }
        }
    }
    /*
    void RenderScene()
    {
        Renderer renderer0 = GameObject.Find(objNameArray[0]).GetComponent<Renderer>();
        Renderer renderer1 = GameObject.Find(objNameArray[1]).GetComponent<Renderer>();
        ParticleSystemRenderer renderer2 = GameObject.Find(objNameArray[2]).GetComponent<ParticleSystemRenderer>();
        ParticleSystemRenderer renderer3 = GameObject.Find(objNameArray[3]).GetComponent<ParticleSystemRenderer>();

        renderer0.enabled = objVisibleArray[0];
        renderer1.enabled = objVisibleArray[1];
        renderer2.enabled = objVisibleArray[2];
        renderer3.enabled = objVisibleArray[3];
    }*/
}
