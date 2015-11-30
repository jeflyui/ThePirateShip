using UnityEngine;
using System.Collections;

public class ButtonListener : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void jump(int target){
		Application.LoadLevel (target);
	}
	public void retry(){
		Application.LoadLevel (GameManager.nextLevel-1);
	}
	public void next(){
		Debug.Log (GameManager.nextLevel);
		if (GameManager.nextLevel > 6) {
			Application.LoadLevel (1);
		} else {
			Application.LoadLevel (GameManager.nextLevel);
		}
	}
}
