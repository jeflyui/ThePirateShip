using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	public Vector2 position = Vector2.zero;
	public GameManager gm;
	private int x;
	private int y;
	/*
		type :
		0 = plain
		1 = player
		2 = enemy
		3 = obstacle
		private int type;
	 */
	private int type;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseExit(){
		changeColor(Color.blue);
	}
	
	void OnMouseDown(){
		int deltaX = Mathf.Abs (x - Player.x);
		int deltaY = Mathf.Abs (y - Player.y);
		if ((deltaX == 1 && deltaY == 1) || (deltaX == 1 && deltaY == 0) || (deltaX == 0 && deltaY == 1) && !Player.isMove && gm.player !=null && type!=3) {
			Player.dest = transform.position;
			Player.dest.y = 1.05f;
			Player.isMove = true;
			//Direction
			if (this.x == Player.x && this.y < Player.y) {
				//moveLeft
				gm.rotatePlayer (180);
				if(!GameManager.isDefault){
					GameManager.cameraDest = gm.camera.transform.position;
					GameManager.cameraDest.x = gm.camera.transform.position.x - 1f;
				} else {
					GameManager.cameraDest.x = GameManager.cameraDest.x -1f;
				}

				Player.direction = 6;
			} else if (this.x == Player.x && this.y > Player.y) {
				//moveRight
				gm.rotatePlayer (0);
				if(!GameManager.isDefault){
					GameManager.cameraDest = gm.camera.transform.position;
					GameManager.cameraDest.x = gm.camera.transform.position.x + 1f;
				} else {
					GameManager.cameraDest.x = GameManager.cameraDest.x +1f;
				}


				Player.direction = 2;
			} else if (this.x < Player.x && this.y == Player.y) {
				//moveUp
				gm.rotatePlayer (-90);
				if(!GameManager.isDefault){
					GameManager.cameraDest = gm.camera.transform.position;
					GameManager.cameraDest.z = gm.camera.transform.position.z + 1f;
				} else {
					GameManager.cameraDest.z = GameManager.cameraDest.x +1f;
				}

				
				Player.direction = 0;
			} else if (this.x > Player.x && this.y == Player.y) {
				//moveDown
				gm.rotatePlayer (90);
				if(!GameManager.isDefault){
					GameManager.cameraDest = gm.camera.transform.position;
					GameManager.cameraDest.z = gm.camera.transform.position.z - 1f;
				}else{
					GameManager.cameraDest.z = GameManager.cameraDest.z -1f;
				}

				Player.direction = 4;
			} else if (this.x < Player.x && this.y < Player.y) {
				//moveUpLeft
				gm.rotatePlayer (-135);
				if(!GameManager.isDefault){
					GameManager.cameraDest = gm.camera.transform.position;
					GameManager.cameraDest.x = gm.camera.transform.position.x - 1f;
					GameManager.cameraDest.z = gm.camera.transform.position.z + 1f;
				} else {
					GameManager.cameraDest.x = GameManager.cameraDest.x -1f;
					GameManager.cameraDest.z = GameManager.cameraDest.z +1f;
				}

				Player.direction = 7;
			} else if (this.x < Player.x && this.y > Player.y) {
				//moveUpRight
				if(!GameManager.isDefault){
					GameManager.cameraDest = gm.camera.transform.position;
					GameManager.cameraDest.x = gm.camera.transform.position.x + 1f;
					GameManager.cameraDest.z = gm.camera.transform.position.z + 1f;
				}else {
					GameManager.cameraDest.x = GameManager.cameraDest.x +1f;
					GameManager.cameraDest.z = GameManager.cameraDest.z +1f;
				}

				gm.rotatePlayer (-45);
				Player.direction = 1;
			} else if (this.x > Player.x && this.y < Player.y) {
				//moveBottomLeft
				gm.rotatePlayer (135);
				if(!GameManager.isDefault){
					GameManager.cameraDest = gm.camera.transform.position;
					GameManager.cameraDest.x = gm.camera.transform.position.x - 1f;
					GameManager.cameraDest.z = gm.camera.transform.position.z - 1f;
				} else {
					GameManager.cameraDest.x = GameManager.cameraDest.x -1f;
					GameManager.cameraDest.z = GameManager.cameraDest.z -1f;
				}

				Player.direction = 5;
			} else if (this.x > Player.x && this.y > Player.y) {
				//moveBottomRight
				gm.rotatePlayer (45);
				if(!GameManager.isDefault){
					GameManager.cameraDest = gm.camera.transform.position;
					GameManager.cameraDest.x = gm.camera.transform.position.x + 1f;
					GameManager.cameraDest.z = gm.camera.transform.position.z - 1f;
				} else {
					GameManager.cameraDest.x = GameManager.cameraDest.x +1f;
					GameManager.cameraDest.z = GameManager.cameraDest.z -1f;
				}

				Player.direction = 3;
			}
			Player.x = x;
			Player.y = y;
			gm.movePlayer ();
			if(SpecialPower.point < 5){
				SpecialPower.point +=1;
			}

		} else if (deltaX == 0 && deltaY == 0) {
			gm.shoot();
		}
	}

	public void changeColor(Color color){
		GetComponent<Renderer>().material.color = color;
	}

	public void setX(int x){
		this.x = x;
	}
	
	public void setY(int y){
		this.y = y;
	}
	
	public int getX(){
		return x;
	}

	public int getY(){
		return y;
	}

	public int getType(){
		return type;
	}
	public void setType(int type){
		this.type = type;
	}
}
