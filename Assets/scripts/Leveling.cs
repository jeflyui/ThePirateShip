using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Leveling {
	public List<Point> player = new List<Point>();
	public List<List<Point>> enemy = new List<List<Point>>();
	public List<List<Point>> obstacle = new List<List<Point>>();
	public List<int> minStep = new List<int>();
	public Leveling(){
		//level1
		player.Add (new Point (7,8));
		List<Point> enemyLVL1 = new List<Point> ();
		enemyLVL1.Add (new Point(5, 9));
		enemy.Add (enemyLVL1);
		List<Point> obstacleLVL1 = new List<Point> ();
		obstacle.Add (obstacleLVL1);
		minStep.Add (2);


		//level2
		player.Add (new Point (7,8));
		enemyLVL1 = new List<Point> ();
		enemyLVL1.Add (new Point(5, 5));
		enemyLVL1.Add (new Point(7, 5));
		enemyLVL1.Add (new Point(9, 7));
		enemyLVL1.Add (new Point(9, 9));
		enemyLVL1.Add (new Point(5, 9));
		enemy.Add (enemyLVL1);

		obstacleLVL1 = new List<Point> ();
		obstacleLVL1.Add (new Point (5,7));
		obstacleLVL1.Add (new Point (6,5));
		obstacleLVL1.Add (new Point (7,9));
		obstacle.Add (obstacleLVL1);

		minStep.Add (2);

		//level3
		player.Add (new Point (7,8));
		enemyLVL1 = new List<Point> ();
		//enemyLVL1.Add(new Point(0,0));
		for (int i =0; i<15; i++) {
			enemyLVL1.Add(new Point(0,i));
			enemyLVL1.Add(new Point(14,i));
		}
		enemy.Add (enemyLVL1);
		obstacleLVL1 = new List<Point> ();
		obstacle.Add (obstacleLVL1);
		minStep.Add (7);
	} 
}
