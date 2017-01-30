using UnityEngine;
using System.Collections;

public class RockBehavior : MonoBehaviour {

	AudioSource audioSource;
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		audioSource.Play();
	}
}
