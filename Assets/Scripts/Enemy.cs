using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utils;

public class Enemy : MonoBehaviour {
	public GameController gc;
	public Tile goal;

	float speed = .75f;
	float arriveDistance = .1f;
	int health = 3;
	// List<Tile> explored;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 dir = goal.transform.position - transform.position;
		dir.Normalize();
		transform.Translate(dir * speed * Time.deltaTime);
		if (Vector3.Distance(transform.position, goal.transform.position) < arriveDistance) {
			ChooseNewGoal();
		}
	}

	public void TakeDamage() {
		// print(health);
		health--;
		if (health <= 0) {
			Destroy(this.gameObject);
		}
	}

	private void ChooseNewGoal() {
		Neighbors neighbors = gc.GetNeighbors(goal.offset);
		// if (neighbors.bl && neighbors.br) {
		// 	goal = (Random.Range(0f, 1f) < .5f) ? neighbors.bl : neighbors.br;
		// } else if (neighbors.bl) {
		// 	goal = neighbors.bl;
		// } else if (neighbors.br) {
		// 	goal = neighbors.br;
		// } else if (neighbors.l && neighbors.r) {
		// 	goal = (Random.Range(0f, 1f) < .5f) ? neighbors.l : neighbors.r;
		// } else if (neighbors.l) {
		// 	goal = neighbors.l;
		// } else if (neighbors.r) {
		// 	goal = neighbors.r;
		// } else if (neighbors.ul && neighbors.ur) {
		// 	goal = (Random.Range(0f, 1f) < .5f) ? neighbors.ul : neighbors.ur;
		// } else if (neighbors.ul) {
		// 	goal = neighbors.ul;
		// } else if (neighbors.ur) {
		// 	goal = neighbors.ur;
		// }
		if (AssignGoal(neighbors.b, null)) {
			return;
		}
		if (AssignGoal(neighbors.bl, neighbors.br)) {
			return;
		}
		if (AssignGoal(neighbors.ul, neighbors.ur)) {
			return;
		}
		if (AssignGoal(neighbors.u, null)) {
			return;
		}
	}

	private bool AssignGoal(Tile tileA, Tile tileB) {
		if (tileA && tileB) {
			if (tileA.groundTileType == GroundTile.Type.LOW &&
				tileB.groundTileType == GroundTile.Type.LOW) {
				goal = (Random.Range(0f, 1f) < .5f) ? tileA : tileB;
				return true;
			} else if (tileA.groundTileType == GroundTile.Type.LOW) {
				goal = tileA;
				return true;
			} else if (tileB.groundTileType == GroundTile.Type.LOW) {
				goal = tileB;
				return true;
			}
		} else if (tileA) {
			if (tileA.groundTileType == GroundTile.Type.LOW) {
				goal = tileA;
				return true;
			}
		} else if (tileB) {
			if (tileB.groundTileType == GroundTile.Type.LOW) {
				goal = tileB;
				return true;
			}
		}
		return false;
	}
}
