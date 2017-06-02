using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCReceiverC : MonoBehaviour {
    //You can set these variables in the scene because they are public 
    public string RemoteIP = "127.0.0.1";
    public int SendToPort = 6448;
    public int ListenerPort = 12000;
    public OSCTestSender sender;

    public GameObject myCube; 
    private Osc handler;
    public float sig1 = 0;
    public float sig2 = 0;
    public float sig3 = 0;
    public float sig4 = 0;
    public float sig5 = 0;
    public bool act_rotate;
    public int act_scale;
    public int act_dist;
    public int act_angle;
    public int act_object;
    // Use this for initialization
    void Start () {
        //Initializes on start up to listen for messages
        //make sure this game object has both UDPPackIO and OSC script attached
        myCube = GameObject.Find("Cube1");
        UDPPacketIO udp = GetComponent<UDPPacketIO>();
        udp.init(RemoteIP, SendToPort, ListenerPort);
        handler = GetComponent<Osc>();
        handler.init(udp);


        //Tell Unity to call function Example1 when message /wek/outputs arrives
        handler.SetAddressHandler("/outputs", messageHandler);
        handler.SetAddressHandler("/request", messageHandlerRequest);
    }
	
	// Update is called once per frame
	void Update () {
        myCube.transform.eulerAngles = new Vector3(sig1 * 360, sig2 * 360, myCube.transform.eulerAngles.z);
        myCube.transform.position = new Vector3(sig4 * 10, sig3 * 10, myCube.transform.position.z);
        myCube.GetComponent<Renderer>().material.color = Color.HSVToRGB(sig5, 1, 1);

    }

    public void messageHandlerRequest(OscMessage oscMessage)
    {
        sender.Send("inputCurrent ");
    }

    public void messageHandler(OscMessage oscMessage)
    {
        int action = (int)oscMessage.Values[0];
        int state_size = 14;
        int num_objects = 30;
        int num_angle_step = 6;
        int num_scale_step = 4;
        int num_dist_step = 4;
        int num_rotation_bool = 2;
        

        if (action % num_rotation_bool == 0) {
            act_rotate = true;
        } else
        {
            act_rotate = false;
        }
        action /= num_rotation_bool;
        act_scale = action % num_scale_step;
        action /= num_scale_step;
        act_dist = action % num_dist_step;
        action /= num_dist_step;
        act_angle = action % num_angle_step;
        action /= num_angle_step;
        act_object = action;

        Debug.Log("Called Example One > " + Osc.OscMessageToString(oscMessage));
	    Debug.Log("Message Values > " + oscMessage.Values[0] + " " + oscMessage.Values[1] + " " + oscMessage.Values[2] + " " + oscMessage.Values[3] + " " + oscMessage.Values[4]);

        sender.SendNext();
    } 
}
