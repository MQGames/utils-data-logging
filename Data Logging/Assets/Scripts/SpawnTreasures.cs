using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SpawnTreasures : MonoBehaviour {

	public Treasure treasurePrefab;
	public Rect spawnRect;
	public float spawnPeriod = 1.0f;	// seconds

	private float spawnCooldown = 0;

	public LogFile log;

	private int nCoins = 0;

	private int score = 0;


	void Update () {
	
		if (spawnCooldown > 0) {
			// the timer ticks down
			spawnCooldown -= Time.deltaTime;
		}

		if (spawnCooldown <= 0) {
			// the timer hit zero, spawn a new treasure

			// pick a random position within the spawnRect;
			float x = spawnRect.xMin + Random.value * spawnRect.width;
			float y = spawnRect.yMin + Random.value * spawnRect.height;

			// create a new treasure
			Treasure t = Instantiate(treasurePrefab);
			t.id = nCoins;
			nCoins++;

			// name the object to make it easier to identify
			t.gameObject.name = "Treasure " + t.id;

			// attach it to this parent in the hierarchy
			t.transform.parent = transform;

			// move it to the desired position
			t.transform.position = new Vector3(x, y, 0);

			// reset the timer
			spawnCooldown = spawnPeriod;

			if (log != null) {
				// record the creation in the logfile
				log.WriteLine(Time.time, t.id, "create", x, y);
			}
		}
	}


	public void CollectTreasure(Treasure treasure) {
		score++;

		if (log != null) {
			// record the collection in the logfile
			log.WriteLine(Time.time, treasure.id, "collect", treasure.transform.position.x, treasure.transform.position.y);
		}

		Destroy(treasure.gameObject);
	}


	public void DestroyTreasure(Treasure treasure) {

		if (log != null) {
			// record the collection in the logfile
			log.WriteLine(Time.time, treasure.id, "destroy", treasure.transform.position.x, treasure.transform.position.y);
		}

		Destroy(treasure.gameObject);
	}

	void OnDrawGizmos() {
		// draw a box in the Scene view to show the spawnRect
		Gizmos.color = Color.red;

		Vector3 p0 = transform.TransformPoint(new Vector3(spawnRect.xMin, spawnRect.yMin, 0));
		Vector3 p1 = transform.TransformPoint(new Vector3(spawnRect.xMax, spawnRect.yMin, 0));
		Vector3 p2 = transform.TransformPoint(new Vector3(spawnRect.xMax, spawnRect.yMax, 0));
		Vector3 p3 = transform.TransformPoint(new Vector3(spawnRect.xMin, spawnRect.yMax, 0));

		Gizmos.DrawLine(p0, p1);
		Gizmos.DrawLine(p1, p2);
		Gizmos.DrawLine(p2, p3);
		Gizmos.DrawLine(p3, p0);
	}
}
