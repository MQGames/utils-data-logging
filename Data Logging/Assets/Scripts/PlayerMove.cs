using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	public float speed = 10.0f;

	public LogFile log;

	private new Rigidbody2D rigidbody;

	void Start () {
		rigidbody = GetComponent<Rigidbody2D>();
	}

	void Update () {
		Vector2 vel;
		vel.x = Input.GetAxis("Horizontal");
		vel.y = Input.GetAxis("Vertical");
		vel = vel.normalized * Mathf.Max(Mathf.Abs(vel.x), Mathf.Abs(vel.y));
		rigidbody.velocity = vel * speed;

		if (log != null) {
			// write the time and the players x and y positions to the file
			log.WriteLine(Time.time,
                 transform.position.x,
                 transform.position.y,
                 rigidbody.velocity.x,
                 rigidbody.velocity.y);
		}
	}
}
