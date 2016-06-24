using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Inventory {
	public static GameObject[] InitializeInventory (params string[] blocks) {
		//GameObject[] inventory = new GameObject[blocks.Length];
		List<GameObject> inventory = new List<GameObject> ();
		foreach (string f in blocks) {
			GameObject blockToAdd = Resources.Load ("Prefabs/Blocks/" + f.ToLower ()) as GameObject;
            if (blockToAdd != null) inventory.Add (blockToAdd);
		}
		return inventory.ToArray ();
	}
}
