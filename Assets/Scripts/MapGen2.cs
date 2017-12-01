using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen2 : MonoBehaviour {
	public GameObject[] blocks;
	public int width, height;
	private int[,] map;
	public bool hardMode = false;
	public GameObject mapParent;
	public GameObject goal;
	public int goalCount;

	// Use this for initialization
	void Start () {
		map = new int[height, width];
		GenerateMap ();
		GenerateGoals ();
		Smooth ();
		SpawnMap ();
	}

	void GenerateGoals(){
		for (int i = 0; i < goalCount; i++) {
			Instantiate (goal, new Vector3 (100f*Random.Range(0, height), 0, 100f*Random.Range(0, width)), goal.transform.rotation);
		}
	}

	void Smooth(){
		GameObject currentBlock;
		for (int x = 1; x < width-1; x++) {
			for (int y = 1; y < height-1; y++) {
				currentBlock = blocks[map[x,y]];
				Vector3 tallness = currentBlock.GetComponent<MeshCollider>().bounds.size;
				// Debug.Log ("Height of block " + x + ", " + y + " is " + tallness.ToString ());
			}
		}
	}

	void SpawnMap(){
		for (int x = 0; x < height; x++) {
			for (int y = 0; y < width; y++) {
				Vector3 position = new Vector3 (100*x, 0, 100*y);
				Quaternion rotation = Quaternion.Euler(0f, 90.0f * Random.Range(0, 4), 0f);
				GameObject newBlock = Instantiate (blocks [map [x, y]], position, rotation);
				newBlock.transform.SetParent(mapParent.transform);
				// Instantiate (blocks [map [x, y]], position, Random.rotationUniform);
			}
		}
	}

	void Update(){
		if (Input.GetMouseButtonDown(0)) {
			foreach(GameObject block in GameObject.FindGameObjectsWithTag("City Block")){
				Destroy(block);
			}
			GenerateMap ();
			SpawnMap();
		}
	}

	void GenerateMap(){
		Debug.Log ("generating map");
		for (int x=0; x < height; x++) {
			for (int y = 0; y < width; y++) {
				int index = 0;
				bool valid = false;
				while(valid == false){
					index = Random.Range (0, blocks.Length);
					CityBlockInfo info = blocks [index].GetComponent<CityBlockInfo> ();
					if (info.unique && !info.exists) {
						info.exists = true;
						valid = true;
					} else if (info.unique == false) {
						valid = true;
					}
				}
				Debug.Log ("[" + x + ", " + y + "] = " + index);
				map [x, y] = index;
			}
		}
	}
}
