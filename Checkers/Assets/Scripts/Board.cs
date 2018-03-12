using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public GameObject player1piece, player2piece, player1Pieces, player2Pieces;
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
        for (int i = 0; i < board.GetLength(0); i += 2)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                float xloc, yloc;
                if (j % 2 == 0)
                {
					xloc = player1piece.transform.position.x + (i * .125f) * transform.localScale.x;
					yloc = player1piece.transform.position.y - (j * .125f) * transform.localScale.x;
                }
                else
                {
					xloc = player1piece.transform.position.x + (i * .125f + .125f) * transform.localScale.x;
					yloc = player1piece.transform.position.y - (j * .125f) * transform.localScale.x;
                }
                
                Vector3 newpos = new Vector3(xloc, yloc, 0);

				if (j < 3) 
				{
					board [i, j] = (GameObject)Instantiate (player1piece, player1piece.transform.position, player1piece.transform.rotation, player1Pieces.transform);
					board [i, j].transform.position = newpos;
				} 
				else if (j > 4) 
				{
					board [i, j] = (GameObject)Instantiate (player2piece, player1piece.transform.position, player1piece.transform.rotation, player2Pieces.transform);
					board [i, j].transform.position = newpos;
				}

            }
        }
        Destroy(player1piece);
    }

    //swaps regular with king, swaps king w regular
    public void replacePiece()
    {

    }

    //params: coordinates of the piece in board space
	//instantiate buttons in valid move spots
    public void validMoves(int x, int y)
    {
		Piece piece = board [x, y].GetComponent<Piece>();
		Piece.coord[] moves = piece.getMoves ();
    }

    public void makeMove()
    {

    }
}
