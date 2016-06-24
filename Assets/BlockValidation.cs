using UnityEngine;
using System.Collections;

public class BlockValidation : MonoBehaviour {
	public bool valid = true;
	GameObject CheckObject;
	public Color validColor = new Color (0,1,0,0.5f);
	public Color invalidColor = new Color (1,0,0,0.5f);
	new Renderer renderer;
	public void FixedUpdate () {
		if (valid) {
			renderer.material.color = validColor;
		} else {
			renderer.material.color = invalidColor;
		}
		//print (valid);
		valid = true;
	}
	public void Start () {
		renderer = GetComponent<Renderer> ();
		//print ("Start");
	}
	void OnTriggerStay (Collider other) {
		if (GetComponent<Collider>().bounds.Intersects (other.bounds) && other.gameObject != CheckObject) {
			valid = false;
			//print ("invalid");
		}
	}
	public void set (Vector3 position, Quaternion rotation, Mesh mesh, GameObject checkObject) {
		transform.position = position;
		transform.rotation = rotation;
		GetComponent<MeshFilter> ().mesh = mesh;
		CheckObject = checkObject;
	}
}
