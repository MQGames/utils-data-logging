using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour {

	public float lifetime = 3.5f;	// seconds
	public int id;	// the coin ID

	private SpawnTreasures spawner;

	public void Start() {
		// Find the spawner and keep a record for later
		spawner = FindObjectOfType<SpawnTreasures>();
	}

	public void Update() {
		lifetime -= Time.deltaTime;

		if (lifetime <= 0) {
			// tell the spawner the treasure has been destroyed
			spawner.DestroyTreasure(this);
		}
	}

	public void OnTriggerEnter2D(Collider2D collider) {
		// tell the spawner the treasure has been collected
		spawner.CollectTreasure(this);
	}
		
}
