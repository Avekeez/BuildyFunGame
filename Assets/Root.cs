using UnityEngine;
using System.Collections;

public class Root : MonoBehaviour {
	public GameObject core;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody> ().mass = transform.childCount;
	}
}
