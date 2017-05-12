
//You can set these variables in the scene because they are public 
public var RemoteIP : String = "127.0.0.1";
public var SendToPort : int = 6448;
public var ListenerPort : int = 12000;
public var controller : Transform; 
private var handler : Osc;
public var sig1 : int = 0;
public var sig2 : int = 0;
public var sig3 : int = 0;
public var sig4 : int = 0;
public var sig5 : int = 0;


public function Start ()
{
	//Initializes on start up to listen for messages
	//make sure this game object has both UDPPackIO and OSC script attached
	
	var udp : UDPPacketIO = GetComponent("UDPPacketIO");
	udp.init(RemoteIP, SendToPort, ListenerPort);
	handler = GetComponent("Osc");
	handler.init(udp);
			

	//Tell Unity to call function Example1 when message /wek/outputs arrives
	handler.SetAddressHandler("/wek/outputs", Example1);
}
Debug.Log("OSC Running");

//Use the values from OSC to do stuff
function Update () {

}	

//This is called when /wek/outputs arrives, since this is what's specified in Start()
public function Example1(oscMessage : OscMessage) : void
{	
	
	Debug.Log("Called Example One > " + Osc.OscMessageToString(oscMessage));
	Debug.Log("Message Values > " + oscMessage.Values[0] + " " + oscMessage.Values[1] + " " + oscMessage.Values[2] + " " + oscMessage.Values[3] + " " + oscMessage.Values[4]);
	sig1 = oscMessage.Values[0];
	sig2 = oscMessage.Values[1];
	sig3 = oscMessage.Values[2];
	sig4 = oscMessage.Values[3];
	sig5 = oscMessage.Values[4];
	
	
} 