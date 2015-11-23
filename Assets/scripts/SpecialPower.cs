using UnityEngine;
using System.Collections;

public class SpecialPower : MonoBehaviour {
	public static int powerVal;
	public RectTransform rect;
	public static int point;
	// Use this for initialization
	void Start () {
		rect = GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () {
		float width = point * 80f; 
		rect.sizeDelta = new Vector2 (width, 10f);
	}
}
