using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour {

    public GameObject player2Piece, player1Piece, player2Pieces, player1Pieces, possibleMove;
    public GameObject[,] board = new GameObject[8, 8];
    public Player[] players = new Player[2];
    public bool turn;
    public int w, h;
	public GameObject[] moveButtons;
	public GameObject currentPiece;

	void Awake(){
	}

	// Use this for initialization
	void Start () {
        setUpBoard();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
	}

    public void setUpBoard()
    {
		int piecenum = 0;
		for (int y = 0; y < board.GetLength(1); y ++)
		{
			for (int x = 0; x < board.GetLength(0); x ++ )
			{
                float xloc, yloc;
				xloc = player2Piece.transform.position.x + (x * .125f) * transform.localScale.x;
				yloc = player2Piece.transform.position.y - (y * .125f) * transform.localScale.x;
                
                Vector3 newpos = new Vector3(xloc, yloc, 0);

				if ((x + y) % 2 == 0) {
					if (y < 3) {
						board [x, y] = (GameObject)Instantiate (player2Piece, player2Piece.transform.position, player2Piece.transform.rotation, player2Pieces.transform);
						board [x, y].transform.position = newpos;
						board [x, y].transform.name = "Red Piece " + piecenum.ToString ();
						piecenum++;
					} else if (y > 4) {
						board [x, y] = (GameObject)Instantiate (player1Piece, player2Piece.transform.position, player2Piece.transform.rotation, player1Pieces.transform);
						board [x, y].transform.position = newpos;
						board [x, y].transform.name = "White Piece " + piecenum.ToString ();
						piecenum++;
					} else {
						piecenum = 0;
					}
				}
            }
        }
        Destroy(player2Piece);
    }

    //swaps regular with king, swaps king w regular
    public void replacePiece()
    {

    }

    //params: coordinates of the piece in board space
	//instantiate buttons in valid move spots
    public void validateMoves(int x, int y)
    {
		if(players[turn?1:0].GetComponent<PlayerSetup>().isLocalPlayer){
			
			clearMoves ();

			Piece piece = board[x, y].GetComponent<Piece>();
			Piece.coord[] moves = piece.getMoves();

			LocalPlayer localPlayer = GameObject.FindObjectOfType<LocalPlayer> ();

			int movenum = 0;
			foreach(Piece.coord move in moves){
				int moveX = x + move.x;
				int moveY = y + move.y;

				Debug.Log (moveX + " " + moveY);

				if (moveX >= 0 && moveX < 8 && moveY >= 0 && moveY < 8 && board[moveX, moveY] == null) {
					Vector3 newPos = new Vector3 (((moveX + 0.5f) / 8) - 0.5f, ((7 - moveY + 0.5f) / 8) - 0.5f, piece.transform.position.z);
					board [moveX, moveY] = (GameObject)Instantiate (possibleMove, Vector3.zero, piece.transform.rotation, transform);
					board [moveX, moveY].transform.localPosition = newPos;
					board [moveX, moveY].GetComponentInChildren<Button> ().onClick.AddListener (delegate{makeMove(moveX, moveY,x,y);});
					board [moveX, moveY].transform.name = "Move " + movenum;
					movenum++;
				}
			}
			printBoard ();

			moveButtons = GameObject.FindGameObjectsWithTag ("move");
		}
    }

	public void makeMove(int moveX, int moveY, int pieceX, int pieceY)
    {
		clearMoves ();
		currentPiece = board [pieceX, pieceY];
		Debug.Log (moveX + " " + moveY);
		board [moveX, moveY] = board[pieceX, pieceY];
		board [pieceX, pieceY] = null;
		currentPiece.transform.localPosition = new Vector3 (((moveX + 0.5f) / 8) - 0.5f, ((7 - moveY + 0.5f) / 8) - 0.5f, currentPiece.transform.position.z);
		printBoard ();
		turn = !turn;
    }

/*
 * Helper Functions
*/

	//prints the board in 0,1 format to the console
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

	//clears the move buttons
	public void clearMoves(){
		for (int j = 0; j < board.GetLength (0); j++) {
			for (int i = 0; i < board.GetLength (1); i++) {
				if (board [i, j] != null && board [i, j].tag == "move") {
					Destroy (board [i, j]);
					board [i, j] = null;
				}
			}
		}
	}
}
