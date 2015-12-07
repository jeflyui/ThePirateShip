using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public int x;
	public int y;
	public static bool isMove;
	public int direction;
	public GameObject explode;
	public AudioSource[] audios;
	// Use this for initialization
	void Start () {
		isMove = false;
		audios = GetComponents<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int[] move() {
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
		Vector3 v = transform.position;
		v.y = 0.5f;
		if (other.CompareTag ("Bullet")) {
			Destroy (other.gameObject);
			Instantiate(explode, v, Quaternion.Euler(new Vector3()));
		} else if (other.CompareTag ("Player")) {
			Destroy (other.gameObject);
			if(!isMove){
				Instantiate(explode, v, Quaternion.Euler(new Vector3()));
			}
		} else if (other.CompareTag ("Enemy")) {
			Destroy (other.gameObject);
			Destroy (this.gameObject);
			Instantiate(explode, v, Quaternion.Euler(new Vector3()));
		}
		Debug.Log("asd");

	}
}
