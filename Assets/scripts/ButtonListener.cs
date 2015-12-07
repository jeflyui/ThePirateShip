using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ButtonListener : MonoBehaviour {

	public Image dummy;
	// Use this for initialization
	public AudioSource click;
	void Start () {
		click = GetComponent<AudioSource> ();
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	//asd
	// Update is called once per frame
	void Update () {
	
	}

	public void jump(int target){
		if (Application.loadedLevel == 0) {
			dummy.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 30f);
		}
		dummy.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 30f);
		Application.LoadLevel (target);
		click.Play ();
	}
	public void retry(){
		dummy.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 30f);
		Application.LoadLevel (GameManager.nextLevel-1);
		click.Play ();
	}
	public void next(){
		Debug.Log (GameManager.nextLevel);
		dummy.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 30f);
		click.Play ();
		if (GameManager.nextLevel > 8) {
			Application.LoadLevel (1);
		} else {
			Application.LoadLevel (GameManager.nextLevel);
		}
	}
	public void exit(){
		click.Play ();
		Application.Quit ();
	}
}
