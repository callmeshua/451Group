using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piece : MonoBehaviour {

    public bool color;
	public Button button;
	public Board board;

	public struct coord{
		public int x, y;
        public coord(int x0,int y0){
            x=x0;
            y=y0;
        }
	}
    
    public struct CoordPair{
        public coord move,capture;
        public CoordPair(int mx,int my,int cx,int cy){
            move=new coord(mx,my);
            capture=new coord(cx,cy);
        }
    }

	// Use this for initialization
	void Start () {
		board = FindObjectOfType<Board> ();
		button.onClick.AddListener(onClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    virtual public coord[] getMoves(){
        return new coord[]{
            new coord(-1,-1),
            new coord(1,-1)
        };
    }

	virtual public CoordPair[] getCaptures(){
		return new CoordPair[]{
            new CoordPair(-2,-2,-1,-1),
            new CoordPair(2,-2,1,-1)
        };
    }

    virtual public void moveTo(int x, int y){
        Vector3 pos=new Vector3(x,y,0);
        pos+=new Vector3(.5f,.5f,0);
        pos/=8;
        pos+=new Vector3(-.5f,-.5f,0);
        transform.localPosition=pos;
    }

	public void onClick(){
		Vector3 pos = transform.localPosition;
		pos.x = (0.5f + pos.x) * 8;
		pos.y = (0.5f - pos.y) * 8;
		Debug.Log (pos);
		board.validateMoves ((int)pos.x, (int)pos.y);
	}
}
