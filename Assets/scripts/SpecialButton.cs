using UnityEngine;
using System.Collections;

public class SpecialButton : MonoBehaviour {
	public GameManager gm;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void special1(){
		if (SpecialPower.point == 5) {
			gm.longShoot ();
			SpecialPower.point = 0;
		}
	}

	public void special2(){
		if (SpecialPower.point == 5) {
			gm.areaShoot ();
			SpecialPower.point = 0;
		}
	}
}
