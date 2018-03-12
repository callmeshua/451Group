using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public GameObject player1piece, player2piece, player1Pieces, player2Pieces, possibleMove;
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

		for (int y = 0; y < board.GetLength(1); y ++)
		{
			for (int x = 0; x < board.GetLength(0); x ++ )
			{
                float xloc, yloc;
                if (y % 2 == 0)
                {
					xloc = player1piece.transform.position.x + (x * .125f) * transform.localScale.x;
					yloc = player1piece.transform.position.y - (y * .125f) * transform.localScale.x;
                }
                else
                {
					xloc = player1piece.transform.position.x + (x * .125f) * transform.localScale.x;
					yloc = player1piece.transform.position.y - (y * .125f) * transform.localScale.x;
                }
                
                Vector3 newpos = new Vector3(xloc, yloc, 0);

				if ((x + y) % 2 == 0) {
					if (y < 3) {
						board [x, y] = (GameObject)Instantiate (player1piece, player1piece.transform.position, player1piece.transform.rotation, player1Pieces.transform);
						board [x, y].transform.position = newpos;
					} else if (y > 4) {
						board [x, y] = (GameObject)Instantiate (player2piece, player1piece.transform.position, player1piece.transform.rotation, player2Pieces.transform);
						board [x, y].transform.position = newpos;
					}
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
    public void validateMoves(int x, int y)
    {
		for (int j = 0; j < board.GetLength (0); j++) {
			for (int i = 0; i < board.GetLength (1); i++) {
				if (board [i, j] != null && board[i,j].tag == "move") 
				{
					Destroy (board [i, j]);
					board [i, j] = null;
				}
			}
		}
		Piece piece = board[x, y].GetComponent<Piece>();
		Piece.coord[] moves = piece.getMoves();
		foreach(Piece.coord move in moves){
			int moveX = x + move.x;
			int moveY = y + move.y;
			Debug.Log (moveX + " " + moveY);
			if (moveX >= 0 && moveX < 8 && moveY >= 0 && moveY < 8 && board[moveX, moveY] == null) {
				Vector3 newPos = new Vector3 (((moveX + 0.5f) / 8) - 0.5f, ((7 - moveY + 0.5f) / 8) - 0.5f, piece.transform.position.z);
				board [moveX, moveY] = (GameObject)Instantiate (possibleMove, Vector3.zero, piece.transform.rotation, transform);
				board [moveX, moveY].transform.localPosition = newPos;
			}
		}
		printBoard ();
    }

    public void makeMove()
    {

    }

	public void printBoard(){
		string line = "";
		for (int j = 0; j < board.GetLength (0); j++) {
			for (int i = 0; i < board.GetLength (1); i++) {
				if (board [i, j] != null) {
					line += "1 ";
				} else {
					line += "0 ";
				}
			}
			line += "\n";
		}
		Debug.Log (line);
	}
}
