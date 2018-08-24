using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoveScript : MonoBehaviour {

	// Background scroll speed can be set in Inspector with slider
	[Range(0f, 20f)]
	public float scrollSpeed = 1f;

	// Scroll offset value to smoothly repeat backgrounds movement
	public float scrollOffset;

    public float z_value;

	// Start position of background movement
	Vector2 startPos;

	// Backgrounds new position
	float newPos;

	// Use this for initialization
	void Start () {
		// Getting backgrounds start position
		startPos = transform.position;
        //scrollOffset -= startPos.x;
	}
	
	// Update is called once per frame
	void Update () {
		// Calculating new backgrounds position repeating it depending on scrollOffset
		newPos = Mathf.Repeat (Time.time * - scrollSpeed, scrollOffset - startPos.x);

		// Setting new position
		transform.position = startPos + Vector2.right * newPos;

        transform.position = new Vector3(transform.position.x, transform.position.y, z_value);
	}
}
