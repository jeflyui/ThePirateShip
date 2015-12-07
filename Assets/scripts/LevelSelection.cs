using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;


public class LevelSelection : MonoBehaviour {
	int initX = -225;
	int initY = 130;
	int initZ = 500;
	public Button[] levels;
	// Use this for initialization
	void Start () {
			/*
			StreamReader reader = new StreamReader (Application.persistentDataPath+"/Pirate.sv"); 
			string s = reader.ReadLine ();
			if(s==null) throw new FileNotFoundException();
			char[] delimiter = {','};
			string[] stars = s.Split(delimiter);
			*/
			for(int i = 0; i<GameManager.star.Length;i++){
				int x = GameManager.star[i];
                if (x ==1){
					levels[i].GetComponent<Image>().color = Color.blue;
                } else if(x==2){
					levels[i].GetComponent<Image>().color = Color.green;
				} else if(x==3){
					levels[i].GetComponent<Image>().color = Color.yellow;
				}
         	
			}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
