using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapingObjects : MonoBehaviour {
    public GameObject[] prefabs;
    public int selectedModel;
    public int newSelectedModel;
    public int polarPosition; //6
    public int discretePolar = 6; 
    public bool rotation; //bool
    public int distance; //4
    public int scale; //4
    public float minScale = 0.5f;
    public float scaleMultiplier = 1f;
    public float distanceMultiplier;
    public GameObject player;
    public OSCReceiverC data;
    private GameObject[] currentObjects;
    private int index = 0;
	// Use this for initialization
	void Start () {
        currentObjects = new GameObject [5];
    }
	
	// Update is called once per frame
	void Update () {
        newSelectedModel = data.act_object;
        discretePolar = data.act_angle;
        rotation = data.act_rotate;
        scale = data.act_scale;
        distance = data.act_dist;

        if (newSelectedModel != selectedModel)
        {

            selectedModel = newSelectedModel;
            SpawnObject();
            index++;
            index %= currentObjects.Length;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            for(int i = 0; i < currentObjects.Length; i++)
            {
                Destroy(currentObjects[i]);
            }
        }
	}

    void SpawnObject()
    {
        if(currentObjects[index] != null)
        {
            GameObject.Destroy(currentObjects[index]);
        }
        float angle = (Mathf.PI/2) * (polarPosition/discretePolar);
        angle -= ((player.transform.localRotation.eulerAngles.y - 45 )/ 360) * Mathf.PI * 2;
        float Hypotenuse = distance * distanceMultiplier;
        float posX = Mathf.Cos(angle) * Hypotenuse;
        float posZ = Mathf.Sin(angle) * Hypotenuse;
        float objScale = scale * scaleMultiplier + minScale;
        Vector3 position = new Vector3(posX, 0, posZ); 
        currentObjects[index] = Instantiate(prefabs[newSelectedModel], position, Quaternion.identity);
        currentObjects[index].transform.LookAt(transform);
        if (rotation)
        {
            currentObjects[index].transform.Rotate(new Vector3(0, Random.Range(0f, 360f), 0));
        }
        
        currentObjects[index].transform.localScale = new Vector3(objScale, objScale, objScale);
    }
}
