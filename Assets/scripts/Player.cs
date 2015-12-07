using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public static Vector3 dest;
	public static bool isMove;
	public static bool isShoot;
	public static int x;
	public static int y;
	public static int direction;
	public GameObject explode;
	public GameManager gm;
	// Use this for initialization
	void Start () {
		isMove = false;
		isShoot = false;
	}


	void OnMouseDown(){
		gm.shoot ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Enemy")) {
			Vector3 v = transform.position;
			v.y = 0.5f;
			Destroy (other.gameObject);
			if(!isMove){
				Instantiate(explode, v, Quaternion.Euler(new Vector3()));
			}
			Destroy (this.gameObject);
		}
	}
}
