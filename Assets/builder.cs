using UnityEngine;
using System.Collections;

public class builder : MonoBehaviour {
	public GameObject BasicCube;
	public GameObject Root;
	GameObject[] inventory;
	GameObject PlacementValidator;
	int currentBlock = 0;
	void Start () {
		inventory = Inventory.InitializeInventory (
				"cube"
			);
		Root = Resources.Load ("Prefabs/Root") as GameObject;
		PlacementValidator = Instantiate (Resources.Load ("Prefabs/Validator") as GameObject) as GameObject;
		print (PlacementValidator.name);
	}
	void Update () {
		if (gameObject != Camera.main.gameObject) {
			return;
		}
		/*
		for (int i = 0; i < GameObject.Find ("Core").transform.root.childCount; i++) {
			GameObject.Find ("Core").transform.GetChild (i).GetComponent<Renderer> ().material.color = Color.white;
		}
		*/
		PlacementValidator.SetActive (false);
		RaycastHit hit;
		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition),out hit,Mathf.Infinity,(1 << 8)|(1 << 9))) {
			Vector3 futureOrigin;
			Quaternion futureRotation;
			BasicObject hitObject = hit.collider.GetComponent<BasicObject> ();
			bool rooted = hitObject != null;
			if (hitObject != null) {
				futureOrigin = hitObject.transform.position + Util.MultiplyVectors (hit.normal,inventory[currentBlock].transform.localScale);
			} else {
				futureOrigin = hit.point + Util.MultiplyVectors (hit.normal, 0.5f * inventory[currentBlock].transform.localScale);
			}
			if (hit.normal != hit.collider.transform.root.up && hit.normal != -hit.collider.transform.root.up) {
				futureRotation = Quaternion.LookRotation (hit.normal,hit.collider.transform.root.up);
            } else {
				futureRotation = Quaternion.LookRotation (hit.normal,-hit.collider.transform.root.forward);
            }
            PlacementValidator.SetActive (true);
			PlacementValidator.GetComponent<BlockValidation> ().set (futureOrigin,futureRotation,inventory[currentBlock].GetComponent<MeshFilter> ().sharedMesh, hit.collider.gameObject);
			if (Input.GetMouseButtonDown (0) && PlacementValidator.GetComponent<BlockValidation>().valid) {
				GameObject newBlock = Instantiate (inventory[currentBlock],futureOrigin,futureRotation) as GameObject;
				if (!rooted) {
					GameObject newRoot = Instantiate (Root,futureOrigin,Quaternion.identity) as GameObject;
					newRoot.GetComponent<Root> ().core = newBlock;
					newBlock.transform.parent = newRoot.transform;
				} else {
					newBlock.transform.parent = hitObject.transform.root;
				}
				if (newBlock.GetComponent<BasicObject> ().ConnectedToRoot ()) print ("yee");
			}
		}

		//print (~LayerMask.NameToLayer ("Blocks"));
		/*
		if (Physics.Raycast (screenRay,out hit,Mathf.Infinity, (1 << 8))) {

			if (Input.GetMouseButtonDown (0)) {
				Vector3 origin = new Vector3 ();
				if (hit.collider.GetComponent<BasicObject> ().type == BlockType.CUBE) {
					origin = hit.collider.transform.position + multiplyVectors (hit.normal,0.5f * hit.collider.transform.localScale);
				}
				GameObject createdCube = (GameObject) Instantiate (BasicCube,origin + multiplyVectors (hit.normal,0.5f * BasicCube.transform.localScale),hit.collider.transform.rotation);
				createdCube.transform.parent = hit.collider.transform.root;
			}
			if (Input.GetMouseButton (1)) {
				GameObject[] adjacent = new GameObject[6];
				Vector3[] directions = {
					new Vector3 (1,0,0),
					new Vector3 (-1,0,0),
					new Vector3 (0,1,0),
					new Vector3 (0,-1,0),
					new Vector3 (0,0,1),
					new Vector3 (0,0,-1)
				};
				RaycastHit adjHit;
				for (int i = 0; i < 6; i++) {
					Debug.DrawRay (hit.collider.transform.position,directions[i],Color.green);
					if (hit.collider.name != "Core") hit.collider.GetComponent<Renderer> ().material.color = Color.red;
					if (Physics.Raycast (hit.collider.transform.position,directions[i],out adjHit,1)) {
						if (adjHit.collider.name != "Core" && adjHit.collider.gameObject.layer == ~(1 << 8)) adjHit.collider.GetComponent<Renderer> ().material.color = Color.red;
					}
				}
			}
		}
		*/
	}
}
