using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utils;

public class MouseController : MonoBehaviour {
	Unit dragTarget;
	List<Tile> path;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mousePos = Util.MousePos();
		// print(Util.TileAtMouse());
		if (dragTarget) {
			Tile mouseOverTile = Util.TileAtMouse();
			if (mouseOverTile && !path.Contains(mouseOverTile)) {
				path.Add(mouseOverTile);
				// print(mouseOverTile);
			}
			if (Input.GetMouseButtonUp(0)) {
				dragTarget.DoFollowPath(path);
				dragTarget = null;
				path = null;
			}
		} else {
			if (Input.GetMouseButtonDown(0)) {
				RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.up);
				foreach(RaycastHit2D hit in hits) {
					Unit hitUnit = hit.collider.GetComponent<Unit>();
					if (hitUnit) {
						// print(hitUnit);
						dragTarget = hitUnit;
						path = new List<Tile>();
						break;
					}
				}
			}
		}
	}
}
