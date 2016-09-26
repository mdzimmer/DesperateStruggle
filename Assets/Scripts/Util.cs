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

		// public static Point AxialToPoint(Axial axial) {
		// 	float x = size * Mathf.Sqrt(3) * (axial.q + axial.r / 2f);
		// 	float y = size * 3f / 2f * axial.r;
		// 	return new Point(x, y);
		// }

		public static Point OffsetToPoint(Offset offset) {
			float x = size * 3f / 2f * offset.col;
			float y = size * Mathf.Sqrt(3f) * (offset.row + .5f * (offset.col & 1));
			return new Point(x, y);
		}

		// public static Axial PointToAxial(Point point) {
		// 	int q = (int)((point.x * Mathf.Sqrt(3) / 3 - point.y / 3) / size);
		// 	int r = (int)(point.y * 2 / 3 / size);
		// 	// return AxialRound(new Axial(q, r));
		// 	return new Axial(q, r);
		// }

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
				if(hit.collider.GetComponent<GroundTile>()) {
					return hit.transform.position;
				}
			}
			return Vector3.zero;
		}

		// public static Vector3 CenterToTile(Vector3 v3) {
		// 	Point point = new Point(v3.x, v3.y);
		// 	Axial axial = PointToAxial(point);
		// 	Point point2 = AxialToPoint(axial);
		// 	Vector3 output = PointToVector3(point2);
		// 	print(point + " : " + axial + " : " + point2);
		// 	return output;
		// }

		// public Axial AxialRound(Axial axial) {
		// 	return CubeToAxial(CubeRound(AxialToCube(axial)));
		// }

		// public Cube CubeRound(Cube cube) {
		// 	int rx = Mathf.Round(cube.x);
		// 	int ry = Mathf.Round(cube.y);
		// 	int rz = Mathf.Round(cube.z);
		// 	int x_diff = Mathf.Abs(x - cube.x);
		// 	int y_diff = Mathf.Abs(y - cube.y);
		// 	int z_diff = Mathf.Abs(z - cube.z);
		// 	if (x_diff > y_diff && x_diff > z_diff) {
		// 		x = -y - z;
		// 	} else if (y_diff > z_diff) {
		// 		y = -x - z;
		// 	} else {
		// 		z = -x - y;
		// 	}
		// 	return new Cube(x, y, z);
		// }
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

		// public Point(Vector3 v3) {
		// 	x = v3.x;
		// 	y = v3.y;
		// }

		public override string ToString() {
			return "(" + x + ", " + y + ")";
		}
	}
}

// odd-q