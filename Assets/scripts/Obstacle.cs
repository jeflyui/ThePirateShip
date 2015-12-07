using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	public GameObject explode;
	public AudioSource collideSound;
	// Use this for initialization
	void Start () {
		collideSound = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag ("Tile")) {
			Destroy (other.gameObject);
			Vector3 tmp = other.transform.position;
			tmp.y = 0.5f;
			Instantiate(explode, tmp, Quaternion.Euler(new Vector3()));

			collideSound.Play();
		}
	}
}
