using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This Client inheritated class acts like Client but using UI elements like buttons and input fields.
/// </summary>
public class CustomClient : Client
{
	public GameObject[] objectsSent;
	public GameObject[] objectsReceived;
	public GameObject[] anchors;
	
	public Matrix4x4 homography;

	public string gameState;
	public string partnerGameState;

	private void Awake()
	{
		// for(int i = 0; i < anchors.Length; i++) {
		// 	Debug.Log(anchors[i].transform.position);
		// }

		//Start Client
		serverConnected = false;
		gameState = "clientwaitforready";
		base.StartClient();
	}
	void Update() {
		if(serverConnected) {
			sendPacket();

			checkGameState();
		} else {
			base.StartClient();
		}
		Debug.Log(serverConnected);
	}
	void checkGameState() {
		if(gameState == "ready" && partnerGameState == "ready") {
			// start_game();
		}
	}
	public void setGameState(string _gameState) {
		gameState = _gameState;
	}
	
	void sendPacket() {
		string message = PacketHandler.m_packetHead;
		for(int i = 0; i < objectsSent.Length; i++) {
			message += PacketHandler.makeElement(i, "null", objectsSent[i].transform);
		}

		message += PacketHandler.makeElement(gameState);

		message += PacketHandler.m_packetFoot;
		SendMessageToServer(message);
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
				case 0:
					partnerGameState = PacketHandler.getState(eles[i]);
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
	private void SendMessageToServer(string message)
	{
		base.SendMessageToServer(message);
	}
	private void SendCloseToServer()
	{
		base.SendMessageToServer("Close");
	}

	//Custom Client Log
	#region ClientLog
	protected override void ClientLog(string msg)
	{
		base.ClientLog(msg);
	}
	protected override void ClientLog(string msg, Color color)
	{
		base.ClientLog(msg, color);
	}
	#endregion
}