using UnityEngine;
using System.Collections;
using Utils;

public class CameraController : MonoBehaviour {
	Vector3 mousePosA;
	// float speed = .08f;
	// bool setPosA = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(1)) {
			mousePosA = Util.MousePos();
		}
		if (Input.GetMouseButton(1)) {
			Vector3 mousePosB = Util.MousePos();
			if (mousePosA != null) {
				Vector3 diff = mousePosB - mousePosA;
				diff.x = 0;
				transform.Translate(diff);
			}
			mousePosA = mousePosB;
		}
	}
}
