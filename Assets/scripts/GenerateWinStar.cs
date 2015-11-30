using UnityEngine;
using System.Collections;

public class GenerateWinStar : MonoBehaviour {
	public GameObject starPrefab;
	int xInit = -50;
	int yInit = 150;
	int zInit = 425;
	// Use this for initialization
	void Start () {
		Debug.Log (GameManager.starAchievment);
		for(int i = 0; i<GameManager.starAchievment;i++){
			Star tile =((GameObject)(Instantiate(starPrefab, new Vector3((float)(xInit+(i*50)),(float)yInit,(float)zInit), Quaternion.Euler(new Vector3())))).GetComponent<Star>();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
