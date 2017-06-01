using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapingObjects : MonoBehaviour {
    public GameObject[] prefabs;
    public int selectedModel;
    public int newSelectedModel;
    public float polarPosition;
    public float rotation;
    public float distance;
    public float scale;
    public float minScale = 0.5f;
    public float scaleMultiplier = 1f;
    public float distanceMultiplier;
    
    private GameObject currentObject;
	// Use this for initialization
	void Start () {
		if(selectedModel != null)
        {
            SpawnObject();
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(newSelectedModel != selectedModel)
        {
            selectedModel = newSelectedModel;
            SpawnObject();
        }
	}

    void SpawnObject()
    {
        if(currentObject != null)
        {
            GameObject.Destroy(currentObject);
        }
        float angle = 2 * Mathf.PI * polarPosition;
        float Hypotenuse = distance * distanceMultiplier;
        float posX = Mathf.Sin(angle) * Hypotenuse;
        float posZ = Mathf.Cos(angle) * Hypotenuse;
        float objScale = scale * scaleMultiplier + minScale;
        Vector3 position = new Vector3(posX, 0, posZ); 
        currentObject = Instantiate(prefabs[newSelectedModel], position, Quaternion.identity);
        currentObject.transform.LookAt(transform);
        currentObject.transform.Rotate(new Vector3 (0, rotation * 360 , 0));
        currentObject.transform.localScale = new Vector3(objScale, objScale, objScale);
    }
}
