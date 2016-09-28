using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {
	List<Tile> path;
	float speed = 1.5f;
	float arriveDistance = .05f;
	float attackRange = 1f;
	float attackSpeed = 10f;
	float returnSpeed = 1f;
	// float leaveDistance = 2f;
	GameController gc;
	bool noAttack = false;

	// Use this for initialization
	void Start () {
		gc = GameObject.Find("_Controllers").GetComponent<GameController>();
		StartCoroutine(Attack());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DoFollowPath(List<Tile> _path) {
		path = _path;
		StartCoroutine(FollowPath());
	}

	IEnumerator FollowPath() {
		noAttack = true;
		foreach(Tile goal in path) {
			bool flag = false;
			while (!flag) {
				Vector3 dir = goal.transform.position - transform.position;
				dir.Normalize();
				transform.Translate(dir * speed * Time.deltaTime);
				if (Vector3.Distance(transform.position, goal.transform.position) < arriveDistance) {
					flag = true;
				}
				yield return new WaitForEndOfFrame();
			}
		}
		if (path[path.Count - 1].offset.row == gc.fieldHeight - 1) {
			// print("at end");
			Destroy(this.gameObject);
		}
	}

	IEnumerator Attack() {
		Enemy target = null;
		bool struck = false;
		Vector3 startPos = transform.position;
		while (true) {
			if (noAttack) {
				yield return new WaitForEndOfFrame();
				continue;
			}
			if (target) {
				Vector3 dir = target.transform.position - transform.position;
				dir.Normalize();
				transform.Translate(dir * attackSpeed * Time.deltaTime);
				if (Vector3.Distance(transform.position, target.transform.position) < arriveDistance) {
					struck = true;
					// Destroy(target.gameObject);
					target.TakeDamage();
					target = null;
					// print("have struck");
				}
			} else {
				if (struck) {
					Vector3 dir = startPos - transform.position;
					dir.Normalize();
					transform.Translate(dir * returnSpeed * Time.deltaTime);
					if (Vector3.Distance(transform.position, startPos) < arriveDistance) {
						struck = false;
						// print("have reset");
					}
				} else {
					Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);
					foreach(Collider2D col in hits) {
						Enemy enemy = col.GetComponent<Enemy>();
						if (enemy) {
							// print("enemy in range");
							target = enemy;
							// print("have target");
						}
					}
				}
			}
			yield return new WaitForEndOfFrame();
		}
	}
}
