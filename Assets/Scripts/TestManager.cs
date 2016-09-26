using UnityEngine;
using System.Collections;
using Utils;

public class TestManager : MonoBehaviour {
	// GameController.UnitType spawnType;
	GameController gc;

	// Use this for initialization
	void Start () {
		gc = GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mousePos = Util.MousePos();
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			gc.SpawnUnit(GameController.UnitType.BLUEHERO, mousePos);
		} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
			gc.SpawnUnit(GameController.UnitType.GREENHERO, mousePos);
		} else if (Input.GetKeyDown(KeyCode.Alpha3)) {
			gc.SpawnUnit(GameController.UnitType.ORANGEHERO, mousePos);
		} else if (Input.GetKeyDown(KeyCode.Alpha4)) {
			gc.SpawnUnit(GameController.UnitType.PURPLEHERO, mousePos);
		} else if (Input.GetKeyDown(KeyCode.Alpha5)) {
			gc.SpawnUnit(GameController.UnitType.BLACKHERO, mousePos);
		} else if (Input.GetKeyDown(KeyCode.Q)) {
			gc.SpawnUnit(GameController.UnitType.BLUESQUIRE, mousePos);
		} else if (Input.GetKeyDown(KeyCode.W)) {
			gc.SpawnUnit(GameController.UnitType.GREENSQUIRE, mousePos);
		} else if (Input.GetKeyDown(KeyCode.E)) {
			gc.SpawnUnit(GameController.UnitType.ORANGESQUIRE, mousePos);
		} else if (Input.GetKeyDown(KeyCode.R)) {
			gc.SpawnUnit(GameController.UnitType.PURPLESQUIRE, mousePos);
		} else if (Input.GetKeyDown(KeyCode.T)) {
			gc.SpawnUnit(GameController.UnitType.BLACKSQUIRE, mousePos);
		}
	}
}
