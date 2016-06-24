using UnityEngine;
using System.Collections;

public class Cube : BasicObject {
	public Color Color = Color.white;
	public override void Start () {
		type = BlockType.CUBE;
	}
}
