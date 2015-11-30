using UnityEngine;
using System.Collections;
using System.IO;
public class LevelSelection : MonoBehaviour {
	int initX = -225;
	int initY = 130;
	int initZ = 500;
	public GameObject starPrefab;
	// Use this for initialization
	void Start () {
		try{
			StreamReader reader = new StreamReader (Application.persistentDataPath+"/Pirate.sv"); 
			string s = reader.ReadLine ();
			if(s==null) throw new FileNotFoundException();
			char[] delimiter = {','};
			string[] stars = s.Split(delimiter);
			for(int i = 0; i<stars.Length;i++){
				int x = int.Parse(stars[i]);
				for(int j = 0 ;j<x;j++){
					Star star =((GameObject)(Instantiate(starPrefab, new Vector3((float)(initX+(i*195)+(j*30)),(float)initY,(float)initZ), Quaternion.Euler(new Vector3())))).GetComponent<Star>();
				}
			}
		} catch(FileNotFoundException f){}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
