using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Player : MonoBehaviour {

	bool myTurn;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if(myTurn){
            takeTurn();
            myTurn=false;
        }
	}

	abstract public void takeTurn();

	abstract public void endTurn();
}
