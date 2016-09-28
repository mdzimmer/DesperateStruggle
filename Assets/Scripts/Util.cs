using UnityEngine;
using System.Collections;

namespace Utils {
	public class Util : MonoBehaviour {
		static float size = .45f;

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public static Axial CubeToAxial(Cube cube) {
			return new Axial(
				cube.x,
				cube.z
				);
		}

		public static Cube AxialToCube(Axial axial) {
			return new Cube(
				axial.q,
				axial.r,
				-axial.q - axial.r
				);
		}

		public static Offset CubeToOffset(Cube cube) {
			return new Offset(
					cube.x,
					cube.z + (cube.x - (cube.x & 1)) / 2
				);
		}

		public static Cube OffsetToCube(Offset offset) {
			int x = offset.col;
			int z = offset.row - (offset.col - (offset.col & 1)) / 2;
			int y = -x - z;
			return new Cube(x, y, z);
		}

		public static Point OffsetToPoint(Offset offset) {
			float x = size * 3f / 2f * offset.col;
			float y = size * Mathf.Sqrt(3f) * (offset.row + .5f * (offset.col & 1));
			return new Point(x, y);
		}

		public static Vector3 PointToVector3(Point point) {
			return new Vector3(point.x, point.y, 0f);
		}

		public static Vector3 MousePos() {
			Vector3 output = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			output.z = 0;
			return output;
		}

		public static Vector3 TileCenter(Vector3 pos) {
			RaycastHit2D[] hits = Physics2D.RaycastAll(pos, Vector2.up);
			foreach(RaycastHit2D hit in hits) {
				GroundTile groundTile = hit.collider.GetComponent<GroundTile>();
				if(groundTile) {
					Vector3 output = groundTile.centerPoint.transform.position;
					// return hit.transform.position;
					return output;
				}
			}
			return Vector3.zero;
		}

		public static Tile TileAtMouse() {
			Vector3 mousePos = MousePos();
			RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.up);
			foreach(RaycastHit2D hit in hits) {
				Transform parent = hit.collider.transform.parent;
				if (parent) {
					Tile tileHit = parent.gameObject.GetComponent<Tile>();
					// print(hit.collider);
					if (tileHit) {
						// print (tileHit);
						return tileHit;
					}
				}
			}
			return null;
		}
	}

	public struct Axial {
		public int q;
		public int r;

		public Axial(int _q, int _r) {
			q = _q;
			r = _r;
		}

		public override string ToString() {
			return "(" + q + ", " + r + ")";
		}
	}

	public struct Cube {
		public int x;
		public int y;
		public int z;

		public Cube(int _x, int _y, int _z) {
			x = _x;
			y = _y;
			z = _z;
		}

		public override string ToString() {
			return "(" + x + ", " + y + ", " + z + ")";
		}
	}

	public struct Offset {
		public int col;
		public int row;

		public Offset(int _col, int _row) {
			col = _col;
			row = _row;
		}

		public override string ToString() {
			return "(" + col + ", " + row + ")";
		}
	}

	public struct Point {
		public float x;
		public float y;

		public Point(float _x, float _y) {
			x = _x;
			y = _y;
		}

		public override string ToString() {
			return "(" + x + ", " + y + ")";
		}
	}

	public struct Neighbors {
		public Tile u;
		public Tile ul;
		public Tile ur;
		// public Tile l;
		// public Tile r;
		public Tile bl;
		public Tile br;
		public Tile b;
	}
}
