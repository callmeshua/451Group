using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//inherits from type Piece
public class King : Piece {
    
	// Use this for initialization
	void Start () {
		board = FindObjectOfType<Board> ();
		button.onClick.AddListener(onClick);
	}

	//gets the extra moves for King
	public override coord[] getMoves(){
        return new coord[]{
            new coord(-1,-1),
            new coord(1,-1),
            new coord(1,1),
            new coord(-1,1),
        };
    }

	//gets the extra possible captures
	public override CoordPair[] getCaptures(){
		return new CoordPair[]{
            new CoordPair(-2,-2,-1,-1),
            new CoordPair(2,-2,1,-1),
            new CoordPair(2,2,1,1),
            new CoordPair(-2,2,-1,1)
        };
    }
}
