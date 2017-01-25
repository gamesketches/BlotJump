using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	Rigidbody2D rb;
	bool grounded;
	float curGravity = 1;

	// Use this for initialization
	void Start () {
		grounded = false;
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 AccelerationVector = Vector2.zero;
		//Debug.Log(rb.velocity);
		if(Input.GetKeyDown(KeyCode.Space) && grounded) {
			rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
			rb.gravityScale = 1;
		}
		//if(Input.GetAxis("Horizontal") > 0) {
			Vector2 accelerationVector;
			if(transform.rotation.z < 0 || transform.rotation.z > 270) {
				accelerationVector = new Vector2(transform.right.x, transform.right.y) * Input.GetAxis("Horizontal") * Time.deltaTime * 10;
				curGravity = grounded ? 10 : 1;
			}
			else {
				accelerationVector = new Vector2(transform.right.x * - 1, transform.right.y * -1) * Input.GetAxis("Horizontal") * Time.deltaTime * 10;
				curGravity = 1;
			}
			rb.gravityScale = curGravity;
			Debug.Log(curGravity);
			rb.AddForce(accelerationVector, ForceMode2D.Impulse);
		//}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if(other.collider.tag == "Slope") {
			grounded = true;
		}
	}
	void OnCollisionExit2D(Collision2D other) {
		if(other.collider.tag == "Slope") {
			grounded = false;
			rb.gravityScale = 1;
		}
	}

}
