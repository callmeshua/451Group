using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour {

	//references to prefabs
    public GameObject player2Piece, player1Piece, player2Pieces, player1Pieces, p2king, p1king, possibleMove;

	//board array of size 8x8
    public GameObject[,] board = new GameObject[8, 8];

	//pair of players
    public Player[] players = new Player[2];

	//bool indicating whose turn it is.
    public bool turn;

	//list of available move buttons
	public GameObject[] moveButtons;

	//currently selected piece
	public GameObject currentPiece;

	//text indicating whose turn it is
	public Text turnText;

	// Use this for initialization
	void Start () {
        setUpBoard();
	}
	
	// Update is called once per frame
	void Update () {
		//press escape to exit
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}

		//changes the turn text depending on whose turn it is
		if (players [turn ? 1 : 0].GetComponent<PlayerSetup> ().isLocalPlayer) {
			turnText.text = "Your Turn";
		} 
		else {
			turnText.text = "Their Turn";
		}
	}

	//populates board array with newly instantiated pieces for both players using the red piece and white piece prefabs
    public void setUpBoard()
    {
		int piecenum = 0;//for game piece naming

		for (int y = 0; y < board.GetLength(1); y ++)//y in boardspace
		{
			for (int x = 0; x < board.GetLength(0); x ++ )//x in boardspace
			{
				//location of new piece
                float xloc, yloc;
				xloc = player2Piece.transform.position.x + (x * .125f) * transform.localScale.x;
				yloc = player2Piece.transform.position.y - (y * .125f) * transform.localScale.x;
                Vector3 newpos = new Vector3(xloc, yloc, 0);

				//alternates boardspace and creates new piece
				if ((x + y) % 2 == 0) {
					if (y < 3) {
						board [x, y] = (GameObject)Instantiate (player2Piece, player2Piece.transform.position, player2Piece.transform.rotation, player2Pieces.transform);
						board [x, y].transform.position = newpos;
						board [x, y].transform.name = "Red Piece " + piecenum.ToString ();
						piecenum++;
					} 
					else if (y > 4) {
						board [x, y] = (GameObject)Instantiate (player1Piece, player2Piece.transform.position, player2Piece.transform.rotation, player1Pieces.transform);
						board [x, y].transform.position = newpos;
						board [x, y].transform.name = "White Piece " + piecenum.ToString ();
						piecenum++;
					} 
					else {
						piecenum = 0;
					}
				}
            }
        }

		//done to destroy initial red piece prefab
        Destroy(player2Piece);
    }

    //swaps regular with king, swaps king w regular
	public void replacePiece(int x, int y)
    {
		//checks piece tag in position to swap type of king
		if (board [x, y].transform.tag == "p2piece") {
			Destroy (board [x, y]);
			board [x, y] = (GameObject)Instantiate (p2king, board [x, y].transform.position, board [x, y].transform.rotation, player2Pieces.transform);
			board [x, y].transform.position = board [x, y].transform.position;
			board [x, y].transform.name = "Red King";
		} 
		else if (board [x, y].transform.tag == "p1piece") {
			Destroy (board [x, y]);
			board [x, y] = (GameObject)Instantiate (p1king, board [x, y].transform.position, board [x, y].transform.rotation, player1Pieces.transform);
			board [x, y].transform.position = board [x, y].transform.position;
			board [x, y].transform.name = "White King";
		}

    }

    //params: coordinates of the piece in board space
	//instantiate buttons in valid move spots
    public void validateMoves(int x, int y)
	{
		//makes sure it is the player's turn
		if (players [turn ? 1 : 0].GetComponent<PlayerSetup> ().isLocalPlayer){
			clearMoves ();

			Piece piece = board [x, y].GetComponent<Piece> ();
			Piece.coord[] moves = piece.getMoves ();
			Piece.CoordPair[] captures = piece.getCaptures ();
			LocalPlayer localPlayer = GameObject.FindObjectOfType<LocalPlayer> ();

			//checks if the move is a valid jump
			bool canJump = false;

			//for gameobject naming
			int movenum = 0;
			foreach (Piece.coord move in moves) {
				int moveX = x + move.x;
				int moveY = y + move.y;

				//checks if the move is in a valid location and instantiates new button for the player to click and move the piece to new location
				if (moveX >= 0 && moveX < 8 && moveY >= 0 && moveY < 8) {
					if (board [moveX, moveY] == null) {
						Vector3 newPos = new Vector3 (((moveX + 0.5f) / 8) - 0.5f, ((7 - moveY + 0.5f) / 8) - 0.5f, piece.transform.position.z);
						board [moveX, moveY] = (GameObject)Instantiate (possibleMove, Vector3.zero, piece.transform.rotation, transform);
						board [moveX, moveY].transform.localPosition = newPos;
						board [moveX, moveY].GetComponentInChildren<Button> ().onClick.AddListener (delegate {localPlayer.takeTurn(moveX,moveY,x,y);makeMove (moveX, moveY, x, y);});
						board [moveX, moveY].transform.name = "Move " + movenum;
						movenum++;
					} 
					if (board [moveX, moveY].transform.tag == "p2piece") {
						canJump = true;
					}
				}
			}

			//if valid jump, instantiate move button in valid jump space
			if (canJump) {
				foreach (Piece.CoordPair jump in captures) {
					int jumpX = x + jump.move.x;
					int jumpY = y + jump.move.y;

					if (jumpX >= 0 && jumpX < 8 && jumpY >= 0 && jumpY < 8) {
						if (board [jumpX, jumpY] == null && board[x + jump.capture.x, y + jump.capture.y].transform.tag == "p2piece") {
							Vector3 newPos = new Vector3 (((jumpX + 0.5f) / 8) - 0.5f, ((7 - jumpY + 0.5f) / 8) - 0.5f, piece.transform.position.z);
							board [jumpX, jumpY] = (GameObject)Instantiate (possibleMove, Vector3.zero, piece.transform.rotation, transform);
							board [jumpX, jumpY].transform.localPosition = newPos;
							board [jumpX, jumpY].GetComponentInChildren<Button> ().onClick.AddListener (delegate {localPlayer.takeTurn(jumpX,jumpY,x,y);makeMove (jumpX, jumpY, x, y);});
							board [jumpX, jumpY].transform.name = "Move " + movenum;
							movenum++;
						}
					}
				}
			}

			//assigns the movebuttons list with the newly made move gameobjects
			moveButtons = GameObject.FindGameObjectsWithTag ("move");
		}
    }

	//moves the piece after the move button is clicked. if jump is made, capture is performed
	public void makeMove(int moveX, int moveY, int pieceX, int pieceY)
    {
		clearMoves ();

		currentPiece = board [pieceX, pieceY];

		board [moveX, moveY] = board[pieceX, pieceY];
		board [pieceX, pieceY] = null;

		//moves the gameobject in worldspace using boardspace coordinatees
		currentPiece.transform.localPosition = new Vector3 (((moveX + 0.5f) / 8) - 0.5f, ((7 - moveY + 0.5f) / 8) - 0.5f, currentPiece.transform.position.z);

		//checks if the move was far enough to be a jump, destroys the captured piece
		if (Mathf.Abs(moveX - pieceX) > 1 && Mathf.Abs(moveY - pieceY) > 1) {
			int jumpX = (pieceX + moveX) / 2;
			int jumpY = (pieceY + moveY) / 2;
			Destroy (board [jumpX, jumpY]);
			board [jumpX, jumpY] = null;
		}

		//if the move made the piece to the end of the board, replace piece with king piece
		if ((board [moveX, moveY].transform.tag == "p1piece" && moveY == 0) || (board[moveX,moveY].transform.tag == "p2piece" && moveY == 7)) {
			replacePiece (moveX, moveY);
		}

		//swap turn
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
	}

	//clears all move buttons on the board
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
