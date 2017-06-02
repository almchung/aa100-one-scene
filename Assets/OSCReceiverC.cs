using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCReceiverC : MonoBehaviour {
    //You can set these variables in the scene because they are public 
    public string RemoteIP = "127.0.0.1";
    public int SendToPort = 6448;
    public int ListenerPort = 12000;
    public GameObject myCube; 
    private Osc handler;
    public float sig1 = 0;
    public float sig2 = 0;
    public float sig3 = 0;
    public float sig4 = 0;
    public float sig5 = 0;
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
    }
	
	// Update is called once per frame
	void Update () {
        myCube.transform.eulerAngles = new Vector3(sig1 * 360, sig2 * 360, myCube.transform.eulerAngles.z);
        myCube.transform.position = new Vector3(sig4 * 10, sig3 * 10, myCube.transform.position.z);
        myCube.GetComponent<Renderer>().material.color = Color.HSVToRGB(sig5, 1, 1);

    }

    public void messageHandler(OscMessage oscMessage)
    {	
	Debug.Log("Called Example One > " + Osc.OscMessageToString(oscMessage));
	Debug.Log("Message Values > " + oscMessage.Values[0] + " " + oscMessage.Values[1] + " " + oscMessage.Values[2] + " " + oscMessage.Values[3] + " " + oscMessage.Values[4]);
	sig1 = (float)oscMessage.Values[0];
	sig2 = (float)oscMessage.Values[1];
	sig3 = (float)oscMessage.Values[2];
	sig4 = (float)oscMessage.Values[3];
	sig5 = (float)oscMessage.Values[4];
	
	
    } 
}
