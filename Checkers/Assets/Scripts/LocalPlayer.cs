using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayer : Player {
	
	public Piece selectedPiece;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void sendAction(){
	}

	override public void takeTurn(int moveX,int moveY,int pieceX,int pieceY){
		GetComponent<PlayerSetup>().CmdMakeMove(moveX,moveY,pieceX,pieceY);
	}

	override public void endTurn(){
	}
}
