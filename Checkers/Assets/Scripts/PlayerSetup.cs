using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour{

	//singleton that syncs the player count on the server
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

	//player number
	[SyncVar,HideInInspector] public int pnum;

	//reference to board object
	Board board;

	// Use this for initialization
	void Start () {
		//checks what kind of player to add components
		if(isLocalPlayer){
			gameObject.AddComponent<LocalPlayer>();
		}
	}

	//Awake is called when the script instance is being loaded.
	void Awake(){
		pnum=counter.Instance.count;
		counter.Instance.count++;
	}
	
	// Update is called once per frame
	void Update () {
		if(!board){
			board=GameObject.FindObjectOfType<Board>();
		}
		board.players[pnum]=GetComponent<Player>();
	}

	//send moves from local to server
	[Command] public void CmdMakeMove(int moveX,int moveY,int pieceX,int pieceY){
		Debug.LogError ("sending Move");
		RpcMakeMove (moveX, moveY, pieceX, pieceY);
	}

	//recieve moves from server
	[ClientRpc] void RpcMakeMove(int moveX,int moveY,int pieceX,int pieceY){
		Debug.LogError (moveX + " " + moveY + " " + pieceX + " " + pieceY);
		//this will be called on ALL instances
		//but the local instance is what sent the command in the first place
		//we only want to recieve the command on a remote player
		if(!isLocalPlayer){
			GameObject.FindObjectOfType<Board>().makeMove(7-moveX,7-moveY,7-pieceX,7-pieceY);
		}
	}
}
