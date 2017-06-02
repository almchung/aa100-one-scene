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
    
    private GameObject currentObject;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Jump") != 0)
        {
            SpawnObject();
        }
		if(newSelectedModel != selectedModel)
        {
            selectedModel = newSelectedModel;
            SpawnObject();
        }
	}

    void SpawnObject()
    {
        transform.rotation = player.transform.rotation;
        if(currentObject != null)
        {
            GameObject.Destroy(currentObject);
        }
        float angle = (Mathf.PI/2) * (polarPosition/discretePolar);
        angle -= ((player.transform.localRotation.eulerAngles.y - 45 )/ 360) * Mathf.PI * 2;
        float Hypotenuse = distance * distanceMultiplier;
        float posX = Mathf.Cos(angle) * Hypotenuse;
        float posZ = Mathf.Sin(angle) * Hypotenuse;
        float objScale = scale * scaleMultiplier + minScale;
        Vector3 position = new Vector3(posX, 0, posZ); 
        currentObject = Instantiate(prefabs[newSelectedModel], position, Quaternion.identity);
        currentObject.transform.LookAt(transform);
        if (rotation)
        {
            currentObject.transform.Rotate(new Vector3(0, Random.Range(0f, 360f), 0));
        }
        
        currentObject.transform.localScale = new Vector3(objScale, objScale, objScale);
    }
}
