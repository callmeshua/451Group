using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

    public bool color;

	public struct coord
	{
		int x, y;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public coord[] getMoves()
    {
        coord[] moves = null;
        return moves;
    }
    /*
	public Tuple<coord,coord>[] getCaptures()
    {
		Tuple<coord[]> tuple captures = null;
        return captures;
    }
    */
    public void moveTo(int x, int y)
    {

    }

}
