using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piece : MonoBehaviour {

	//the UI button component for player clicking
	public Button button;

	//the board object
	public Board board;

	//coordinates in boardspace
	public struct coord{
		public int x, y;
        public coord(int x0,int y0){
            x=x0;
            y=y0;
        }
	}
    
	//pair of coords for a capture
    public struct CoordPair{
        public coord move,capture;
        public CoordPair(int mx,int my,int cx,int cy){
            move=new coord(mx,my);		//location after jump
            capture=new coord(cx,cy);	//coord of captured piece
        }
    }

	// Use this for initialization
	void Start () {
		board = FindObjectOfType<Board> ();
		button.onClick.AddListener(onClick);
	}

	//gets list of possible move coords
    virtual public coord[] getMoves(){
        return new coord[]{
            new coord(-1,-1),
            new coord(1,-1)
        };
    }

	//gets list of pairs of capture coord pairs
	virtual public CoordPair[] getCaptures(){
		return new CoordPair[]{
            new CoordPair(-2,-2,-1,-1),
            new CoordPair(2,-2,1,-1)
        };
    }

	//moves piece to the new position in worldspace
    virtual public void moveTo(int x, int y){
        Vector3 pos=new Vector3(x,y,0);
        pos+=new Vector3(.5f,.5f,0);
        pos/=8;
        pos+=new Vector3(-.5f,-.5f,0);
        transform.localPosition=pos;
    }

	//when piece is clicked, current object is assigned to clicked piece, valid moves are shown
	public void onClick(){
		board.currentPiece = gameObject;
		if (transform.parent.tag == "P1") {
			Vector3 pos = transform.localPosition;
			pos.x = (0.5f + pos.x) * 8;
			pos.y = (0.5f - pos.y) * 8;
			board.validateMoves ((int)pos.x, (int)pos.y);
		}
	}
		
/*
* HELPER FUNCTIONS
*/
	//checks if a parent of the gameobject has tag
	public bool checkParentTag(GameObject g, string t){
		Debug.Log (transform.name);
		Transform trans = g.transform;
		while (trans.parent != null)
		{
			if (trans.parent.tag == t)
			{
				Debug.Log (t);
				return true;
			}
			trans = trans.parent.transform;
			Debug.Log (trans.parent.name);

		}
		return false;
	}
}
