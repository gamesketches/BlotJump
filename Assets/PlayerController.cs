using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour {

	Rigidbody2D rb;
	public Text distanceMarker;
	bool grounded;
	bool started = false;
	bool jumping;
	float curGravity = 1;
	bool switched = false;
	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		jumping = false;
		grounded = false;
		rb = GetComponent<Rigidbody2D>();
		rb.gravityScale = 0;
		StartCoroutine(Introduction());
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R)) {
			SceneManager.LoadScene(0);
		}
		if(started) {
			string distance = (Random.value * 1000).ToString();
			distanceMarker.text = distance.Substring(0, distance.IndexOf('.') + 2);
			Vector2 AccelerationVector = Vector2.zero;
			//Debug.Log(rb.velocity);
			if(Input.GetKeyDown(KeyCode.Space) && grounded) {
				rb.gravityScale = 1;
				rb.AddForce(Vector2.up * 100, ForceMode2D.Impulse);
				jumping = true;
				ParticleSystem particles = (ParticleSystem)Instantiate(Resources.Load<ParticleSystem>("JumpBurst"), transform.position, Quaternion.identity);
			}
			if(!jumping) {
				Vector2 accelerationVector;
				if(transform.rotation.z < 0 || transform.rotation.z > 270) {
					accelerationVector = new Vector2(transform.right.x, transform.right.y) * Input.GetAxis("Horizontal") * Time.deltaTime * 10;
					curGravity = grounded ? 10 : 3;
				}
				else {
					accelerationVector = new Vector2(transform.right.x * - 1, transform.right.y * -1) * Input.GetAxis("Horizontal") * Time.deltaTime * 10;
					curGravity = 2;
				}
				rb.gravityScale = curGravity;
				rb.AddForce(accelerationVector, ForceMode2D.Impulse);
				if(transform.position.x > 41 && !switched) {
					SpriteRenderer rend = GetComponentsInChildren<SpriteRenderer>()[1];
					rend.sprite = Resources.Load<Sprite>("SkierTrip");
					audioSource.clip = Resources.Load<AudioClip>("landSound");
					audioSource.loop = false;
					audioSource.Play();
					switched = true;
					curGravity = 3;
				}
				}
			else {
				transform.GetChild(1).RotateAround(Vector3.forward, 1 * Time.deltaTime);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if(other.collider.tag == "Slope") {
			grounded = true;
			jumping = false;
			transform.GetChild(1).up = other.contacts[0].normal;
			audioSource.Play();
		}
		else {
			rb.velocity = Vector2.zero;
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
			started = false;
			string distance = (transform.position.x - 41).ToString();
			distanceMarker.text = distance.Substring(0, distance.IndexOf('.') + 2);
			GameObject.Find("Directional Light").GetComponent<ThunderLightControl>().active = false;
			GameObject.Find("Directional Light").GetComponent<Light>().intensity = 10;
		}
	}
	void OnCollisionExit2D(Collision2D other) {
		if(other.collider.tag == "Slope") {
			grounded = false;
			if(!jumping) {
				rb.gravityScale = 3;
			}
			audioSource.Stop();
		}
	}

	IEnumerator Introduction() {
		float t = 0;
		Light dirLight = GameObject.Find("Directional Light").GetComponent<Light>();
		Vector3 targetPosition = new Vector3(1, 0, -10);
		Vector3 startPosition = Camera.main.transform.localPosition;
		float startSize = Camera.main.orthographicSize;
		float targetSize = 20;
		while(t < 2) {
			dirLight.intensity = 10;
			t += Time.deltaTime;
			yield return null;
		}
		t = 0;
		while(t < 1) {
			dirLight.intensity = 10;
			Camera.main.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
			Camera.main.orthographicSize = Mathf.Lerp(startSize, targetSize, t);
			t += Time.deltaTime / 3;
			yield return null;
		}

		dirLight.intensity = 0;
		started = true;
		rb.gravityScale = 10;
	}
}