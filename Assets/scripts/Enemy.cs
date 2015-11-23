using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public int x;
	public int y;
	public static bool isMove;
	public int direction;
	// Use this for initialization
	void Start () {
		isMove = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int[] move() {
		Debug.Log ("before : " + this.x + " " + this.y);
		Debug.Log ("Player : " + Player.x + " " + Player.y);
		int diffX = 0, diffY = 0;
		if (Player.x - this.x > 0) {
			diffX = 1;
		} else if (Player.x - this.x < 0) {
			diffX = -1;
		}

		if (Player.y - this.y > 0) {
			diffY = 1;
		} else if (Player.y - this.y < 0) {
			diffY = -1;
		}
		int[] oldPosition = {this.x, this.y};
		this.x += diffX;
		this.y += diffY;
		Debug.Log (this.x + " " + this.y);
		//return last position
		return oldPosition;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Bullet")) {
			Destroy (other.gameObject);
		} else if (other.CompareTag ("Player")) {
			Destroy (other.gameObject);
		} else if (other.CompareTag ("Enemy")) {
			Debug.Log ("coll");
			Destroy (other.gameObject);
			Destroy (this);
		}
	}
}
