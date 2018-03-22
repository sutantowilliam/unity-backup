using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
	public GameObject[] tilePrefabs;

	private Transform ballTransform;
	private float spawnZ = -4.0f;
	private float tileLength = 8.0f;
	private int amnTileOnScreen = 7;
	private float safeZone = 15.0f;

	private List<GameObject> activeTile;
	private int lastPrefabIndex = 0;

	// Use this for initialization
	void Start () {
		activeTile = new List<GameObject>();
		ballTransform = GameObject.FindGameObjectWithTag ("Player").transform;

		for (int i = 0; i < amnTileOnScreen; i++) {
			if (i < 3) {
				SpawnTile (0);
			} else {
				SpawnTile ();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if ((ballTransform.position.z - safeZone) > (spawnZ - amnTileOnScreen * tileLength)) {
			SpawnTile ();
			DeleteTile ();
		}
	}

	private void SpawnTile(int prefabIndex = -1) {
		GameObject go;

		if (prefabIndex == -1) {
			go = Instantiate (tilePrefabs [RandomPrefabIndex ()]) as GameObject;
		} else {
			go = Instantiate (tilePrefabs [prefabIndex]) as GameObject;
		}

		go.transform.SetParent (transform);
		go.transform.position = Vector3.forward * spawnZ;
		spawnZ += tileLength;
		activeTile.Add (go);
	}

	private void DeleteTile() {
		Destroy (activeTile [0]);
		activeTile.RemoveAt (0);
	}

	private int RandomPrefabIndex(){
		if (tilePrefabs.Length <= 1) {
			return 0;
		}

		int randomIndex = lastPrefabIndex;
		while (randomIndex == lastPrefabIndex) {
			randomIndex = Random.Range (0, tilePrefabs.Length);
		}

		lastPrefabIndex = randomIndex;
		return randomIndex;
	}
}
