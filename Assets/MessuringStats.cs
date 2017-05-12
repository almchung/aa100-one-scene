using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessuringStats : MonoBehaviour {
    private Vector3 prevRot;
    private Vector3 prevPos;
    public Vector3 currentPos;
    public Vector3 currentRot;
    public float speed;
    public Vector3 angularVel;
    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void Update()
    {
        Messure();
    }

    private void Messure()
    {

        currentPos = transform.position;
        currentRot = transform.rotation.eulerAngles;
        // Debug.Log("Pos: " + currentPos);
        // Debug.Log("Rot: " + currentRot);
        if (prevPos != null)
        {
            Vector3 velocity = currentPos - prevPos;
            speed = Vector3.Magnitude(velocity);
            // Debug.Log("Linear Vel:" + speed);
            prevPos = currentPos;
        }

        if (prevPos != null)
        {
            angularVel = currentRot - prevRot;
            // Debug.Log("Angular Vel:" + angularVel);
            prevRot = currentRot;
        }


    }

}
