using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		if(isLocalPlayer){
			gameObject.AddComponent<LocalPlayer>();
		}else{
			gameObject.AddComponent<NetworkPlayer>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//send moves from local to server
	[Command] public void CmdMakeMove(int moveX,int moveY,int pieceX,int pieceY){
	}

	//recieve moves from server
	[ClientRpc] void RpcMakeMove(int moveX,int moveY,int pieceX,int pieceY){
		//this will be called on ALL instances
		//but the local instance is what sent the command in the first place
		//we only want to recieve the command on a remote player
		if(!isLocalPlayer){
			//GameObject.FindObjectOfType<Board>().
		}
	}
}
