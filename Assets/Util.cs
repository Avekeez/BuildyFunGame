using UnityEngine;
using System.Collections;

public static class Util {
	public static Vector3 MultiplyVectors (Vector3 a, Vector3 b) {
		return new Vector3 (a.x * b.x,a.y * b.y,a.z * b.z);
    }
}
