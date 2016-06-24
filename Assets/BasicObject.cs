using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicObject : MonoBehaviour {
	public BlockType type = BlockType.CUBE;
	public bool isCore = false;
	public virtual void Start () {
		//
	}

	public virtual void Update () {
		isCore = transform.position == transform.root.position;
	}
	public bool ConnectedToRoot () {
		bool reachedRoot = false;
		List<BasicObject> checkedBlocks = new List<BasicObject> ();
		List<BasicObject> toCheckBlocks = new List<BasicObject> ();
		if (isCore) return true;
		toCheckBlocks.AddRange (transform.root.GetComponent<Root> ().core.GetComponent<BasicObject> ().Adjacent ());
		while (!reachedRoot) {
			List<BasicObject> newToCheckBlocks = new List<BasicObject> ();
			for (int i = 0; i < toCheckBlocks.Count; i++) {
				if (toCheckBlocks[i] == null) continue;
				if (toCheckBlocks[i] == this) {
					reachedRoot = true;
				} else {
					checkedBlocks.Add (toCheckBlocks[i]);
					newToCheckBlocks.AddRange (toCheckBlocks[i].Adjacent ());
				}
			}
			toCheckBlocks = newToCheckBlocks;
			if (checkedBlocks.Count == transform.root.childCount - 1) break;
		}
		print (reachedRoot);
		return reachedRoot;
	}
	public BasicObject[] Adjacent () {
		BasicObject[] b = new BasicObject[6];
		for (int i = 0; i < 6; i++) {
			RaycastHit hit;
			if (AdjacentDirections ()[i] != null) {
				if (Physics.Raycast (transform.position, AdjacentDirections ()[i], out hit, 1, (1 << 8))) {
					b[i] = hit.collider.GetComponent<BasicObject> ();
				}
            }
		}
		return b;
	}
	public Vector3[] AdjacentDirections () {
		switch (type) {
			case BlockType.CUBE:
				return new Vector3[] {
					transform.forward,  //forward
					-transform.forward, //backward
					transform.right,    //right
					-transform.right,   //left
					transform.up,       //up
					-transform.up       //down
				};
			default:
				return null;
		}
	}
}
