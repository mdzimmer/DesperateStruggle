using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utils;

public class GameController : MonoBehaviour {
	int fieldWidth = 31;
	int fieldHeight = 24;
	Dictionary<Offset, Tile> field;
	GameObject tileParent;

	// Use this for initialization
	void Start () {
		field = new Dictionary<Offset, Tile>();
		tileParent = GameObject.Find("_Tiles");
		BuildField();
	}
	
	// Update is called once per frame
	void Update () {
		// print(Util.TileCenter(Util.MousePos()));
	}

	public void SpawnUnit(UnitType unitType, Vector3 location) {
		string prefabString = "";
		// Point point = new Point(location);
		// Offset tile = PointToOffset(point);
		// point = OffsetToPoint()
		// location = Util.CenterToTile(location);
		location = Util.TileCenter(location);
		switch(unitType) {
			case UnitType.BLUEHERO:
			prefabString = "Prefabs/BlueHero";
			break;
			case UnitType.GREENHERO:
			prefabString = "Prefabs/GreenHero";
			break;
			case UnitType.ORANGEHERO:
			prefabString = "Prefabs/OrangeHero";
			break;
			case UnitType.PURPLEHERO:
			prefabString = "Prefabs/PurpleHero";
			break;
			case UnitType.BLACKHERO:
			prefabString = "Prefabs/BlackHero";
			break;
			case UnitType.BLUESQUIRE:
			prefabString = "Prefabs/BlueSquire";
			break;
			case UnitType.GREENSQUIRE:
			prefabString = "Prefabs/GreenSquire";
			break;
			case UnitType.ORANGESQUIRE:
			prefabString = "Prefabs/OrangeSquire";
			break;
			case UnitType.PURPLESQUIRE:
			prefabString = "Prefabs/PurpleSquire";
			break;
			case UnitType.BLACKSQUIRE:
			prefabString = "Prefabs/BlackSquire";
			break;
		}
		GameObject prefab = (GameObject)Resources.Load(prefabString);
		Instantiate(prefab, location, Quaternion.identity);
	}

	private void BuildField() {
		GameObject tilePrefab = (GameObject)(Resources.Load("Prefabs/Tile", typeof(GameObject)));
		for(int i = 0; i < fieldWidth; i++) {
			for(int j = 0; j < fieldHeight; j++) {
				Offset offset = new Offset(i, j);
				Point point = Util.OffsetToPoint(offset);
				Vector3 v3 = Util.PointToVector3(point);
				v3.y *= -1f;
				GameObject tileGO = (GameObject)Instantiate(tilePrefab, v3, Quaternion.identity);
				tileGO.transform.parent = tileParent.transform;
				field[offset] = tileGO.GetComponent<Tile>();
			}
		}
	}

	public enum UnitType {
		BLUEHERO,
		GREENHERO,
		ORANGEHERO,
		PURPLEHERO,
		BLACKHERO,
		BLUESQUIRE,
		GREENSQUIRE,
		ORANGESQUIRE,
		PURPLESQUIRE,
		BLACKSQUIRE
	}
}
