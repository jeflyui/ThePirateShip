using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpecialPower : MonoBehaviour {
	public static int powerVal;
	RectTransform rect;
	public static int point;
	public Button longShoot;
	public Button areaShoot;
	// Use this for initialization
	void Start () {
		rect = GetComponent<RectTransform> ();
		point = 0;
	}
	
	// Update is called once per frame
	void Update () {
		float width = point * 80f; 
		if (point == 0) {
			rect.anchoredPosition = new Vector2 (-150f, 240f);
		} else {
			if(point == 1){
				rect.anchoredPosition = new Vector2 (-310f, 240f);
			} else {
				rect.anchoredPosition = new Vector2 (-310f + (float)((point-1)*40), 240f);
			}
			if(point == 5){
				longShoot.GetComponent<RectTransform>().anchoredPosition = new Vector2(250f, 200f);
				areaShoot.GetComponent<RectTransform>().anchoredPosition = new Vector2(160f,200f);
			} else {
				longShoot.GetComponent<RectTransform>().anchoredPosition = new Vector2(10000f, 200f);
				areaShoot.GetComponent<RectTransform>().anchoredPosition = new Vector2(10000f,200f);
			}
		}
		rect.sizeDelta = new Vector2 (width, 10f);

	}
}
