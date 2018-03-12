using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour{

	class counter{
		[SyncVar] public int count=0;
		private static counter instance=null;

		public static counter Instance{
			get{
				if(instance==null){
					instance=new counter();
				}
				return instance;
			}
		}

		private counter(){
			instance=this;
		}
	}

	[SyncVar,HideInInspector] public int pnum;
	Board board;

	// Use this for initialization
	void Start () {
		if(isLocalPlayer){
			gameObject.AddComponent<LocalPlayer>();
		}else{
			gameObject.AddComponent<NetworkPlayer>();
		}

		//ClientScene.ConnectLocalServer().RegisterHandler(MsgType.Connect, OnConnected);
	}

	//public void OnConnected(NetworkMessage netMsg){
	void Awake(){
		pnum=counter.Instance.count;
		counter.Instance.count++;
	}
	
	// Update is called once per frame
	void Update () {
		if(!board){
			board=GameObject.FindObjectOfType<Board>();
		}
		//Debug.LogError(pnum+" "+isLocalPlayer);
		board.players[pnum]=GetComponent<Player>();
	}

	//send moves from local to server
	[Command] public void CmdMakeMove(int moveX,int moveY,int pieceX,int pieceY){
	}

	//recieve moves from server
	[ClientRpc] void RpcMakeMove(int moveX,int moveY,int pieceX,int pieceY){
		Debug.LogError (moveX + " " + moveY + " " + pieceX + " " + pieceY);
		//this will be called on ALL instances
		//but the local instance is what sent the command in the first place
		//we only want to recieve the command on a remote player
		if(!isLocalPlayer){
			GameObject.FindObjectOfType<Board>().makeMove(moveX,7-moveY,pieceX,7-pieceY);
		}
	}
}
