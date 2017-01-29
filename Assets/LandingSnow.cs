using UnityEngine;
using System.Collections;

public class LandingSnow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(col.collider.tag == "Player") {
			Debug.Log(col.contacts[0].point);
		}
	}
}
