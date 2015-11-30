using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour {
	public static bool isMove;
	//public static Vector3 dest1;
	//public static Vector3 dest2;
	public static Vector3[] dest = new Vector3[8];
	public static int playerDirectionState;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Enemy")) {
			Destroy (other.gameObject);
		}
	}
}
