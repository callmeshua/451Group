using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public GameObject redPiece, whitePiece;
    public GameObject[,] board = new GameObject[8, 8];
    public Player[] players = new Player[2];
    int turn;
    int w, h;

	// Use this for initialization
	void Start () {
        setUpBoard();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setUpBoard()
    {
        for(int i = 1; i <= board.GetLength(0); i++)
        {
            for(int j = 1; j <= board.GetLength(1); j += 2)
            {
                board[i-1, j-1] = (GameObject)Instantiate(redPiece, new Vector3(redPiece.transform.position.x + (j * 64), redPiece.transform.position.y + (i * 64),0), redPiece.transform.rotation);
                Debug.Log(i + " " + j);
            }
        }
    }

    public void replacePiece()
    {

    }

    public void validMoves(int x, int y)
    {

    }

    public void makeMove()
    {

    }
}
