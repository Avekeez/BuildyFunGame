using UnityEngine;
using System.Collections;

public class CameraOrbit : MonoBehaviour {
	Vector3 focusPoint;
	float zoomDistance = 10;
	GameObject PivotCenter;
	public void Start () {
		focusPoint = new Vector3 (0, 1, 0);
		PivotCenter = Instantiate (Resources.Load ("Prefabs/Pivot")) as GameObject;
	}
	public void LateUpdate () {
		Vector3 flatLook = transform.forward;
		flatLook.y = 0;
		flatLook.Normalize ();
		Vector3 difference = 0.25f * Input.GetAxis ("Vertical") * flatLook - 0.25f * Input.GetAxis ("Horizontal") * Vector3.Cross (flatLook,Vector3.up) + 0.25f * Input.GetAxis ("CamVertical") * Vector3.up;
		Vector3 heading = transform.position - focusPoint;
        focusPoint += difference;
		//zoomDistance += 10*Input.GetAxis ("Mouse ScrollWheel");
		focusPoint.y = Mathf.Clamp (focusPoint.y,0,Mathf.Infinity);
		PivotCenter.transform.position = focusPoint;
		RaycastHit hit;
		if (Physics.Raycast (focusPoint, transform.position - focusPoint, out hit, 10, (1 << 9))) {
			heading = heading.normalized * hit.distance;
		} else {
			heading = heading.normalized * 10;
        }
		Vector3 futureCamPos = focusPoint + heading;
		transform.position = futureCamPos;
		if (Input.GetMouseButton (2)) {
			transform.RotateAround (focusPoint,Vector3.up,5 * Input.GetAxis ("Mouse X"));
			transform.RotateAround (focusPoint,transform.right,-5 * Input.GetAxis ("Mouse Y"));
		}
	}
}
