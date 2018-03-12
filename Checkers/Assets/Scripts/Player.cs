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
	}

	abstract public void takeTurn(int x0,int y0,int x1,int y1);

	abstract public void endTurn();
}
