using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This Server inheritated class acts like Server but using UI elements like buttons and input fields.
/// </summary>
public class CustomServer : Server
{

	private bool serverConnected = false;
	private bool clientConnected = false;

	public GameObject[] objectsSent;
	public GameObject[] objectsReceived;

	public GameObject[] anchors;
	
	public Matrix4x4 homography;

	protected virtual void Awake()
	{
		//Start Server
		StartServer();

		//Populate Server delegates
		OnClientConnected = () => { clientConnected = true; };
		OnClientDisconnected = () => { clientConnected = false; };
		OnServerClosed = () => { serverConnected = false; };
		OnServerStarted = () => { serverConnected = true; };

		for(int i = 0; i < anchors.Length; i++) {
			Debug.Log(anchors[i].transform.position);
		}
	}

	protected override void Update()
	{
		base.Update();
		sendObjectsTransform();
	}
	void sendObjectsTransform() {
		string message = PacketHandler.m_packetHead;
		for(int i = 0; i < objectsSent.Length; i++) {
			message += PacketHandler.makeElement(i, "null", objectsSent[i].transform);
		}
		message += PacketHandler.m_packetFoot;
		SendMessageToClient(message);
	}
	protected override void OnMessageReceived(string receivedMessage) {
		string packet = PacketHandler.getLastPacket(receivedMessage);
		string[] eles = PacketHandler.parseElements(packet);
		for(int i = 0; i < eles.Length; i++) {
			int mode = PacketHandler.getPacketMode(eles[i]);
			switch (mode) {
				case 1:	
					int objIdx = PacketHandler.getObjectIndex(eles[i]);
					Pose pose = PacketHandler.packet2Pose(eles[i]);
					applyTransform(pose, objectsReceived[objIdx].transform);
					break;
				case -1: 
					break;
			}
		}
	}

	private void applyTransform(Pose _pose, Transform _target_transform) {
		Matrix4x4 mat = homography * Matrix4x4.TRS(_pose.position, _pose.rotation, new Vector3(1, 1, 1));
		
		_target_transform.position = mat.GetPosition();
		_target_transform.rotation = Quaternion.LookRotation(mat.GetColumn(2), mat.GetColumn(1));
	}

	//Get input field text and send it to client
	private void SendMessageToClient(string message)
	{
		base.SendMessageToClient(message);
	}

	//Custom Server Log
	#region ServerLog
	//With Text Color
	protected override void ServerLog(string msg)
	{
		base.ServerLog(msg);
	}
	//Without Text Color
	protected override void ServerLog(string msg, Color color)
	{
		base.ServerLog(msg, color);
	}
	#endregion
}