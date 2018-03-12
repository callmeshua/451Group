using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override coord[] getMoves(){
        return new coord[]{
            new coord(-1,-1),
            new coord(1,-1),
            new coord(1,1),
            new coord(-1,1),
        };
    }

	public override CoordPair[] getCaptures(){
		return new CoordPair[]{
            new CoordPair(-2,-2,-1,-1),
            new CoordPair(2,-2,1,-1),
            new CoordPair(2,2,1,1),
            new CoordPair(-2,2,-1,1)
        };
    }
}
