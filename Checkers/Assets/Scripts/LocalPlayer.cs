using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//inherits from Player abstract class
public class LocalPlayer : Player {
	
	public Piece selectedPiece;

	//sends move for player to server
	override public void takeTurn(int moveX,int moveY,int pieceX,int pieceY){
		GetComponent<PlayerSetup>().CmdMakeMove(moveX,moveY,pieceX,pieceY);
	}
}
