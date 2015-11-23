using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public static Vector3 dest;
	public static bool isMove;
	public static bool isShoot;
	public static int x;
	public static int y;
	public static int direction;
	public GameManager gm;
	// Use this for initialization
	void Start () {
		isMove = false;
		isShoot = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		gm.shoot ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Enemy")) {
			Destroy (other.gameObject);
		}
	}
}
