using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 AccelerationVector = Vector2.zero;
		Debug.Log(rb.velocity);
		if(Input.GetKeyDown(KeyCode.Space)) {
			rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
		}
		if(Input.GetAxis("Horizontal") > 0) {
			Vector2 accelerationVector;
			if(transform.rotation.z < 0 || transform.rotation.z > 270) {
				accelerationVector = new Vector2(transform.right.x, transform.right.y) * Input.GetAxis("Horizontal") * Time.deltaTime * 10;

			}
			else {
				accelerationVector = new Vector2(transform.right.x * - 1, transform.right.y * -1) * Input.GetAxis("Horizontal") * Time.deltaTime * 10;
			}
			Debug.Log(accelerationVector);
			rb.AddForce(accelerationVector, ForceMode2D.Impulse);
		}
	}

}
