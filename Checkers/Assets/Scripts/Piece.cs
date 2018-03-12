using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piece : MonoBehaviour {

    public bool color;
	public Button button;
	public Board board;

	public struct coord
	{
		int x, y;
	}
    
    public class CoordPair{
        coord move,capture;
    }

	// Use this for initialization
	void Start () {
		board = FindObjectOfType<Board> ();
		button.onClick.AddListener(onClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public coord[] getMoves()
    {
        coord[] moves = null;
        return moves;
    }

	public CoordPair[] getCaptures()
    {
		CoordPair[] captures = null;
        return captures;
    }

    public void moveTo(int x, int y)
    {

    }

	public void onClick(){
		Vector3 pos = transform.position;
		pos.x = (0.5f + pos.x) * 8;
		pos.y = (0.5f - pos.y) * 8;
		board.validMoves ((int)pos.x, (int)pos.y);
	}

}
