using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Simple OSC test communication script
/// </summary>
[AddComponentMenu("Scripts/OSCTestSender")]
public class OSCTestSender : MonoBehaviour
{

    private Osc oscHandler;
    public DataInputModule myHeadset;
    public string remoteIp;
    public int sendToPort;
    public int listenerPort;
    private float hue = 0;

    ~OSCTestSender()
    {
        if (oscHandler != null)
        {            
            oscHandler.Cancel();
        }

        // speed up finalization
        oscHandler = null;
        System.GC.Collect();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Initialize();
        }
        //Debug.LogWarning("time = " + Time.time);  
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
       
    }

    public void Send(string prefix)
    {
        string message = prefix + myHeadset.message;
        OscMessage oscM = Osc.StringToOscMessage(message);
        //Debug.Log(myHeadset.message);
        oscHandler.Send(oscM);
    }

    public void SendNext()
    {
        Send("/inputs_next ");
    
    }

  

    void OnDisable()
    {
        // close OSC UDP socket
        Debug.Log("closing OSC UDP socket in OnDisable");
        oscHandler.Cancel();
        oscHandler = null;
    }

    /// <summary>
    /// Start is called just before any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        
        UDPPacketIO udp = GetComponent<UDPPacketIO>();
        udp.init(remoteIp, sendToPort, listenerPort);
        
	    oscHandler = GetComponent<Osc>();
        oscHandler.init(udp);
        
        oscHandler.SetAddressHandler("/hand1", Example);
        

    }

    void Initialize()
    {

        Send("/inputs_current ");
    }

    public static void Example(OscMessage m)
    {
        //Debug.Log("--------------> OSC example message received: ("+m+")");
    }



}
