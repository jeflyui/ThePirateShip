using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System;


public class GameManager : MonoBehaviour {
	public static int starAchievment;
	int totalMove;
	public static int nextLevel;
	//prefab
	public GameObject tilePrefab;
	public GameObject playerPrefab;
	public GameObject bulletPrefab;
	public GameObject obstaclePrefab;
	public GameObject enemyPrefab;
	public int levelStartIndex = 4;
	//leveling
	public Leveling leveling;
	//size
	public int mapSize;
	//player
	Player player;
	GameObject [] objBullets = new GameObject[8];
	//GameObject objBullet1;
	//GameObject objBullet2;
	//peluru
	Bullet[] bullets = new Bullet[8];
	//Bullet bullet1;
	//Bullet bullet2;
	List<List<Tile>> map = new List<List<Tile>> ();
	List<Obstacle> obstacles = new List<Obstacle>();
	List<Enemy> enemies = new List<Enemy>();
	// Use this for initialization
	void Start ()	 {
		leveling = new Leveling ();
		totalMove = 0;
		starAchievment = 0;
		nextLevel = Application.loadedLevel + 1;
		generateMap ();
		generateObstacle ();
		generateEnemy ();
		generatePlayer ();
	}
	
	// Update is called once per frame
	void Update () {
		movePlayer ();
		moveBullet ();
		animateEnemy ();
		checkWinner ();
	}

	void checkWinner(){
		if (player == null) {
			Application.LoadLevel(3);
			Bullet.isMove = false;
			Enemy.isMove = false;
		} else {
			bool isLife = false;
			for(int i = 0; i<enemies.Count;i++){
				if(enemies[i]!=null){
					isLife = true;
				}
			}
			if(!isLife){
				if(leveling.minStep[Application.loadedLevel-levelStartIndex]-totalMove < 0){
					starAchievment = 2;
				} else if(leveling.minStep[Application.loadedLevel-levelStartIndex]-totalMove < -5){
					starAchievment = 1;
				} else {
					starAchievment = 3;
				}
				writeData ();
				Application.LoadLevel(2);
				Bullet.isMove = false;
				Enemy.isMove = false;
			}
		}
	}


	void generateMap(){
		map = new  List<List<Tile>> ();	
		for (int i = 0; i<mapSize; i++) {
			List<Tile> row = new List<Tile>();
			for(int j = 0; j<mapSize;j++){
				Tile tile =((GameObject)(Instantiate(tilePrefab, new Vector3(i-Mathf.Floor(mapSize/2),0,-j+Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3())))).GetComponent<Tile>();
				tile.changeColor(Color.blue);
				tile.position = new Vector2(i,j);
				tile.gm = this;
				tile.setX(j);
				tile.setY(i);
				row.Add (tile);
			}
			map.Add(row);
		}
	}
	
	void generatePlayer(){
		bool isCreate = false;
		int x = 0;
		int y = 0;
		/*
		while (!isCreate) {
			x = Random.Range (0, mapSize);
			y = Random.Range (0, mapSize);
			if(map[y][x].getType() == 0)
				isCreate = true;
		}*/
		x = leveling.player [Application.loadedLevel-levelStartIndex].x;
		y = leveling.player [Application.loadedLevel-levelStartIndex].x;
		player = ((GameObject)(Instantiate (playerPrefab, new Vector3 (y - Mathf.Floor (mapSize / 2), 0.75f, -x + Mathf.Floor (mapSize / 2)), Quaternion.Euler (new Vector3 (90, 0, 0))))).GetComponent<Player> ();
		Player.x = x;
		Player.y = y;
		Player.direction = 4;
		map [y] [x].setType (2); 
		player.gm = this;
	}
	
	void generateObstacle(){
		/*int totalObstacle = Random.Range (3,8);
		for (int i = 0; i<totalObstacle; i++) {
			int x = Random.Range (0, mapSize);
			int y = Random.Range (0, mapSize);
			if(map[y][x].getType() == 0){
				Obstacle tmp = ((GameObject)(Instantiate(obstaclePrefab, new Vector3((float)y-Mathf.Floor(mapSize/2),0,(float)-x+Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3())))).GetComponent<Obstacle>();
				obstacles.Add(tmp);
				map[y][x].setType(3);
			} else {
				i--;
			}
		}*/
		Debug.Log (leveling.obstacle.Count);
		for (int i = 0; i<leveling.obstacle[Application.loadedLevel-levelStartIndex].Count; i++) {
			int x = leveling.obstacle[Application.loadedLevel-levelStartIndex][i].x;
			int y = leveling.obstacle[Application.loadedLevel-levelStartIndex][i].y;
			Obstacle tmp = ((GameObject)(Instantiate(obstaclePrefab, new Vector3((float)y-Mathf.Floor(mapSize/2),0,(float)-x+Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3())))).GetComponent<Obstacle>();
			obstacles.Add(tmp);
			map[y][x].setType(3);
		}
	}

	void generateEnemy(){
		/*for (int i = 0; i<totalEnemies; i++) {
			int x = Random.Range (0, mapSize);
			int y = Random.Range (0, mapSize);
			if(map[y][x].getType() == 0){
				Enemy tmp = ((GameObject)(Instantiate(enemyPrefab, new Vector3((float)y-Mathf.Floor(mapSize/2),0.75f,(float)-x+Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3(90,0,0))))).GetComponent<Enemy>();
				tmp.x = x;
				tmp.y = y;
				tmp.direction = 0;
				enemies.Add (tmp);
				map[y][x].setType(2);
			} else {
				i--;
			}
		}*/
		for (int i = 0; i<leveling.enemy[Application.loadedLevel-levelStartIndex].Count; i++) {
			int x = leveling.enemy[Application.loadedLevel-levelStartIndex][i].x;
			int y = leveling.enemy[Application.loadedLevel-levelStartIndex][i].y;
			Enemy tmp = ((GameObject)(Instantiate(enemyPrefab, new Vector3((float)y-Mathf.Floor(mapSize/2),0.75f,(float)-x+Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3(90,0,0))))).GetComponent<Enemy>();
			tmp.x = x;
			tmp.y = y;
			tmp.direction = 0;
			enemies.Add (tmp);
			map[y][x].setType(2);
		}
	}

	public void movePlayer(){
		if (Player.isMove) {
			if(player!=null){
				Vector3 v = Vector3.zero;
				player.transform.position = Vector3.SmoothDamp (player.transform.position, Player.dest, ref v, 0.1f);
				if(Vector3.Distance(player.transform.position, Player.dest) < 0.1f){
					Player.isMove = false;
					totalMove++;
					Enemy.isMove = true;
					moveEnemy();
				}
			}
		}
	}

	public void moveEnemy(){
		for (int i = 0; i<leveling.enemy[Application.loadedLevel-levelStartIndex].Count; i++) {
			if (enemies [i] != null) {
				int[] tmp = enemies [i].move ();
				//determining direction
				int oldX = tmp [0];
				int oldY = tmp [1];
				int deltaX = Mathf.Abs (oldX - enemies [i].x);
				int deltaY = Mathf.Abs (oldY - enemies [i].y);
				if ((deltaX == 1 && deltaY == 1) || (deltaX == 1 && deltaY == 0) || (deltaX == 0 && deltaY == 1)) {
					//Direction
					if (enemies [i].x == oldX && enemies [i].y < oldY) {
						//moveLeft
						rotateEnemy (-90, enemies [i]);
						enemies [i].direction = 6;
					} else if (enemies [i].x == oldX && enemies [i].y > oldY) {
						//moveRight
						rotateEnemy (90, enemies [i]);
						enemies [i].direction = 2;
					} else if (enemies [i].x < oldX && enemies [i].y == oldY) {
						//moveUp
						rotateEnemy (180, enemies [i]);
						enemies [i].direction = 0;
					} else if (enemies [i].x > oldX && enemies [i].y == oldY) {
						//moveDown
						rotateEnemy (0, enemies [i]);
						enemies [i].direction = 4;
					} else if (enemies [i].x < oldX && enemies [i].y < oldY) {
						//moveUpLeft
						rotateEnemy (-135, enemies [i]);
						enemies [i].direction = 7;
					} else if (enemies [i].x < oldX && enemies [i].y > oldY) {
						//moveUpRight
						rotateEnemy (135, enemies [i]);
						enemies [i].direction = 1;
					} else if (enemies [i].x > oldX && enemies [i].y < oldY) {
						//moveBottomLeft
						rotateEnemy (-45, enemies [i]);
						enemies [i].direction = 5;
					} else if (enemies [i].x > oldX && enemies [i].y > oldY) {
						//moveBottomRight
						rotateEnemy (45, enemies [i]);
						enemies [i].direction = 3;
					}
				}
				animateEnemy ();
			}
		}
	}

	public void animateEnemy(){
		if (Enemy.isMove) {
			bool isMoving = false;
			for(int i = 0 ; i<leveling.enemy[Application.loadedLevel-levelStartIndex].Count;i++){
				if(enemies[i]!=null){
					Vector3 v = Vector3.zero;
					enemies[i].transform.position = Vector3.SmoothDamp (enemies[i].transform.position, new Vector3(enemies[i].y-Mathf.Floor(mapSize/2),0.75f,-enemies[i].x+Mathf.Floor(mapSize/2)), ref v, 0.1f);
					//stop jika semua enemy sudah move
					isMoving = isMoving || Vector3.Distance(enemies[i].transform.position, new Vector3(enemies[i].y-Mathf.Floor(mapSize/2),0.75f,-enemies[i].x+Mathf.Floor(mapSize/2))) > 0.1f;
					/*if(Vector3.Distance(enemies[i].transform.position, new Vector3(enemies[i].y-Mathf.Floor(mapSize/2),0.75f,-enemies[i].x+Mathf.Floor(mapSize/2))) < 0.1f){
						Enemy.isMove = false;
					}*/
				}
			}
			if(!isMoving){
				Enemy.isMove = false;
				Player.isShoot = false;
			}
		}
	}

	public void rotatePlayer(float newDirection){
		player.transform.eulerAngles = new Vector3(90, 0, newDirection);
	}

	public void rotateEnemy(float newDirection, Enemy enemy){
		enemy.transform.eulerAngles = new Vector3(90, 0, newDirection);
	}

	public void shoot(){
		if (!Player.isShoot) {
			//objBullet1 = ((GameObject)(Instantiate (bulletPrefab, player.transform.position, Quaternion.Euler (new Vector3 ()))));
			//bullet1 = objBullet1.GetComponent<Bullet> ();
			//objBullet2 = ((GameObject)(Instantiate (bulletPrefab, player.transform.position, Quaternion.Euler (new Vector3 ()))));
			//bullet2 = objBullet2.GetComponent<Bullet> ();
			for(int i = 0; i<2;i++){
				GameObject temp = ((GameObject)(Instantiate (bulletPrefab, player.transform.position, Quaternion.Euler (new Vector3 ()))));
				objBullets[i] = temp;
				bullets[i] = objBullets[i].GetComponent<Bullet> ();
			}
			totalMove++;
			Bullet.isMove = true;
			setupBulletDirection (2f, false);
			moveBullet ();
			Player.isShoot = true;
		}
	}

	public void longShoot(){
		Debug.Log ("test");
		if (!Player.isShoot) {
			//objBullet1 = ((GameObject)(Instantiate (bulletPrefab, player.transform.position, Quaternion.Euler (new Vector3 ()))));
			//bullet1 = objBullet1.GetComponent<Bullet> ();
			//objBullet2 = ((GameObject)(Instantiate (bulletPrefab, player.transform.position, Quaternion.Euler (new Vector3 ()))));
			//bullet2 = objBullet2.GetComponent<Bullet> ();
			for(int i = 0; i<2;i++){
				GameObject temp = ((GameObject)(Instantiate (bulletPrefab, player.transform.position, Quaternion.Euler (new Vector3 ()))));
				objBullets[i] = temp;
				bullets[i] = objBullets[i].GetComponent<Bullet> ();
			}
			Bullet.isMove = true;
			setupBulletDirection (5f, false);
			moveBullet ();
			Player.isShoot = true;
		}
	}

	public void areaShoot(){
		if (!Player.isShoot) {
			//objBullet1 = ((GameObject)(Instantiate (bulletPrefab, player.transform.position, Quaternion.Euler (new Vector3 ()))));
			//bullet1 = objBullet1.GetComponent<Bullet> ();
			//objBullet2 = ((GameObject)(Instantiate (bulletPrefab, player.transform.position, Quaternion.Euler (new Vector3 ()))));
			//bullet2 = objBullet2.GetComponent<Bullet> ();
			/*for(int i = 0; i<8;i++){
				GameObject temp = ((GameObject)(Instantiate (bulletPrefab, player.transform.position, Quaternion.Euler (new Vector3 ()))));
				objBullets[i] = temp;
				bullets[i] = objBullets[i].GetComponent<Bullet> ();
			}*/
			float diff = 3f;
			GameObject temp = ((GameObject)(Instantiate (bulletPrefab, new Vector3(player.transform.position.x-diff, player.transform.position.y, player.transform.position.z), Quaternion.Euler (new Vector3 ()))));
			objBullets[0] = temp;
			bullets[0] = objBullets[0].GetComponent<Bullet> ();
			temp = ((GameObject)(Instantiate (bulletPrefab, new Vector3(player.transform.position.x-diff, player.transform.position.y, player.transform.position.z+diff), Quaternion.Euler (new Vector3 ()))));
			objBullets[1] = temp;
			bullets[1] = objBullets[1].GetComponent<Bullet> ();
			temp = ((GameObject)(Instantiate (bulletPrefab, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z+diff), Quaternion.Euler (new Vector3 ()))));
			objBullets[2] = temp;
			bullets[2] = objBullets[2].GetComponent<Bullet> ();
			temp = ((GameObject)(Instantiate (bulletPrefab, new Vector3(player.transform.position.x+diff, player.transform.position.y, player.transform.position.z+diff), Quaternion.Euler (new Vector3 ()))));
			objBullets[3] = temp;
			bullets[3] = objBullets[3].GetComponent<Bullet> ();
			temp = ((GameObject)(Instantiate (bulletPrefab, new Vector3(player.transform.position.x+diff, player.transform.position.y, player.transform.position.z), Quaternion.Euler (new Vector3 ()))));
			objBullets[4] = temp;
			bullets[4] = objBullets[4].GetComponent<Bullet> ();
			temp = ((GameObject)(Instantiate (bulletPrefab, new Vector3(player.transform.position.x+diff, player.transform.position.y, player.transform.position.z-diff), Quaternion.Euler (new Vector3 ()))));
			objBullets[5] = temp;
			bullets[5] = objBullets[5].GetComponent<Bullet> ();
			temp = ((GameObject)(Instantiate (bulletPrefab, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z-diff), Quaternion.Euler (new Vector3 ()))));
			objBullets[6] = temp;
			bullets[6] = objBullets[6].GetComponent<Bullet> ();
			temp = ((GameObject)(Instantiate (bulletPrefab, new Vector3(player.transform.position.x-diff, player.transform.position.y, player.transform.position.z-diff), Quaternion.Euler (new Vector3 ()))));
			objBullets[7] = temp;
			bullets[7] = objBullets[7].GetComponent<Bullet> ();

			Bullet.isMove = true;
			setupBulletDirection (2f, true);
			moveBullet ();
			Player.isShoot = true;
		}
	}
	public void moveBullet(){
		Debug.Log (Bullet.isMove);
		if (Bullet.isMove) {
			Vector3 v = Vector3.zero;
			bool isMove = false;
			for(int i = 0; i<8;i++){
				if(bullets[i] != null)
					bullets[i].transform.position = Vector3.SmoothDamp (bullets[i].transform.position, Bullet.dest[i], ref v, 0.05f);
				//if(bullet2 != null)
				//	bullet2.transform.position = Vector3.SmoothDamp (bullet2.transform.position, Bullet.dest2, ref v, 0.05f);

				//bool isBullet1Move = false;
				//bool isBullet2Move = false;
				isMove = isMove || (bullets[i] != null && Vector3.Distance(bullets[i].transform.position, Bullet.dest[i]) > 0.1f);
			}
			if(!isMove){
				Bullet.isMove = false;
				//if(bullet1!=null)
				//	Destroy(objBullet1);
				//if(bullet2!=null)
				//	Destroy(objBullet2);
				for(int i = 0; i<8;i++){
					Destroy(objBullets[i]);
				}
				Enemy.isMove = true;
				moveEnemy();
			}
		}
	}

	public void setupBulletDirection(float shootingRange, bool isAreaShoot){
		if (!isAreaShoot) {
			if (Player.direction == 0 || Player.direction == 4) {
				Bullet.dest [0] = player.transform.position + new Vector3 (shootingRange, 0, 0);
				Bullet.dest [1] = player.transform.position + new Vector3 (-shootingRange, 0, 0);
			} else if (Player.direction == 1 || Player.direction == 5) {
				Bullet.dest [0] = player.transform.position + new Vector3 (shootingRange, 0, -shootingRange);
				Bullet.dest [1] = player.transform.position + new Vector3 (-shootingRange, 0, shootingRange);
			} else if (Player.direction == 2 || Player.direction == 6) {
				Bullet.dest [0] = player.transform.position + new Vector3 (0, 0, -shootingRange);
				Bullet.dest [1] = player.transform.position + new Vector3 (0, 0, shootingRange);
			} else if (Player.direction == 3 || Player.direction == 7) {
				Bullet.dest [0] = player.transform.position + new Vector3 (shootingRange, 0, shootingRange);
				Bullet.dest [1] = player.transform.position + new Vector3 (-shootingRange, 0, -shootingRange);
			}
		} else {
			Bullet.dest [0] = player.transform.position + new Vector3 (shootingRange, 0, 0);
			Bullet.dest [1] = player.transform.position + new Vector3 (-shootingRange, 0, 0);
			Bullet.dest [2] = player.transform.position + new Vector3 (shootingRange, 0, -shootingRange);
			Bullet.dest [3] = player.transform.position + new Vector3 (-shootingRange, 0, shootingRange);
			Bullet.dest [4] = player.transform.position + new Vector3 (0, 0, -shootingRange);
			Bullet.dest [5] = player.transform.position + new Vector3 (0, 0, shootingRange);
			Bullet.dest [6] = player.transform.position + new Vector3 (shootingRange, 0, shootingRange);
			Bullet.dest [7] = player.transform.position + new Vector3 (-shootingRange, 0, -shootingRange);
		}
	}

	public void writeData(){
		Debug.Log (Application.persistentDataPath + "/Pirate.sv");
		FileStream fs = new FileStream (Application.persistentDataPath+"/Pirate.sv",
		                                FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
		try{

			StreamReader reader = new StreamReader (fs);
			string x = reader.ReadLine();
			char[] delimiter = {','};
			if(x == null) throw new FileNotFoundException();
			string[] splits = x.Split(delimiter);
			if(Int32.Parse(splits[Application.loadedLevel-levelStartIndex]) >= starAchievment){
				return;
			}
			reader.Close();
			fs.Close();
			FileStream fs2 = System.IO.File.Create(Application.persistentDataPath+"/Pirate.sv");
			StreamWriter writer = new StreamWriter(fs2);
			splits[Application.loadedLevel-levelStartIndex] = ""+starAchievment;
			writer.WriteLine(splits[0]+","+splits[1]+","+splits[2]);
			writer.Close();
		} catch(FileNotFoundException f){
			string[] splits = {"0","0","0"};
			StreamWriter writer = new StreamWriter(fs);
			splits[Application.loadedLevel-levelStartIndex] = ""+starAchievment;
			writer.WriteLine(splits[0]+","+splits[1]+","+splits[2]);
			writer.Close ();
		}
	}
}
