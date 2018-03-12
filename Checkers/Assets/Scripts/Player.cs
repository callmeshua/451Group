using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract class for LocalPlayer and NetworkPlayer
abstract public class Player : MonoBehaviour {

	bool myTurn;

	abstract public void takeTurn(int moveX,int moveY,int pieceX,int pieceY);
}
