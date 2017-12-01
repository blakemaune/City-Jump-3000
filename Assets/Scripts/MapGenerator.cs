using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
	public GameObject[] blocks;
	public GameObject[] cars;
	public GameObject wallBuilding;
	public int width, height;
	private int[,] map;
	public bool hardMode = false;
	public GameObject mapParent;
	public GameObject goal;
	public int goalCount;
	public float blocSpacing;
	public Vector3[,] corners;
	public int carNum;
	public GameObject helicopter;

	// Use this for initialization
	void Start () {
		Cursor.visible = true;
	}

	public void Initialize () {
		map = new int[height, width];
		corners = new Vector3[height, width];
		GenerateMap ();
		GenerateGoals ();
		Smooth ();
		SpawnMap ();
		makeBorderBuildings ();
		makeInvisibleWalls ();
		GenerateCorners ();
		PlaceCars (carNum);
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		Instantiate (helicopter);

		Vector3 mapCenter = new Vector3 (50f * height + 50f, 150f, 50f * width + 50f);
		GameObject.FindGameObjectWithTag ("Player").transform.position = mapCenter;
	}

	public void SetSize(int i){
		height = i;
		width = i;
	}

	void GenerateGoals(){
		for (int i = 0; i < goalCount; i++) {
			Instantiate (goal, new Vector3 (blocSpacing*Random.Range(0, height), 0, blocSpacing*Random.Range(0, width)), goal.transform.rotation);
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
				Vector3 position = new Vector3 (blocSpacing*x, 0, blocSpacing*y);
				Quaternion rotation = Quaternion.Euler(0f, 90.0f * Random.Range(0, 4), 0f);
				GameObject newBlock = Instantiate (blocks [map [x, y]], position, rotation);
				newBlock.transform.SetParent(mapParent.transform);
				// Instantiate (blocks [map [x, y]], position, Random.rotationUniform);
			}
		}
	}

	void Update(){
	}

	public void NewGoal(){
		Instantiate (goal, new Vector3 (blocSpacing*Random.Range(0, height), 0, blocSpacing*Random.Range(0, width)), goal.transform.rotation);
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

	private void makeBorderBuildings(){
		float mapHeight = height * 100f;
		float mapWidth = width * 100f;

		for (float y1 = 0f; y1 <= mapHeight; y1 += mapHeight) {
			for (float x1 = 0; x1 <= mapWidth; x1 += 12.5f) {
				GameObject g = Instantiate (wallBuilding, new Vector3 (x1 - 50f, 0f, y1 - 50f), wallBuilding.transform.rotation);
				// g.transform.Rotate(new Vector3(90f, 0f, 0f));
			}
		}
		for (float x2 = 0f; x2 <= mapWidth; x2 += mapWidth) {
			for (float y2 = 0; y2 <= mapHeight; y2 += 12.5f) {
				GameObject h = Instantiate (wallBuilding, new Vector3 (x2 - 50f, 0f, y2 - 50f), wallBuilding.transform.rotation);
				// h.transform.Rotate(new Vector3(90f, 0f, 0f));
			}
		}
	}

	private void makeInvisibleWalls(){
		for (int i = 0; i < 4; i++) {
			Debug.Log ("Creating wall " + i);
			GameObject wall = GameObject.CreatePrimitive (PrimitiveType.Plane);
			float dimx = blocSpacing * height;
			float dimz = blocSpacing * width;
			wall.transform.localScale *= Mathf.Max (dimx, dimz);
			switch (i) {
			case 0:
				wall.transform.position = new Vector3 (.5f * dimx - 50f, 0f, dimx - 50f);
				wall.transform.rotation = Quaternion.Euler (270f, 0f, 0f);
				break;
			case 1:
				wall.transform.position = new Vector3 (.5f * dimx - 50f, 0f, 0f - 50f);
				wall.transform.rotation = Quaternion.Euler (90f, 0f, 0f);
				break;
			case 2:
				wall.transform.position = new Vector3 (dimz - 50f, 0f, .5f * dimz - 50f);
				wall.transform.rotation = Quaternion.Euler (0f, 0f, 90f);
				break;
			case 3:
				wall.transform.position = new Vector3 (0f - 50f, 0f, .5f * dimz - 50f);
				wall.transform.rotation = Quaternion.Euler (0f, 0f, 270f);
				break;
			}
			wall.GetComponent<Renderer> ().enabled = false;
		}

	}

	private void GenerateCorners(){
		for (int x = 0; x < height; x++) {
			for (int y = 0; y < width; y++) {
				corners [x, y] = new Vector3 (x * blocSpacing - 50f, 0f, y * blocSpacing - 50f);
			}
		}

		// Debug

		for (int x = 0; x < height; x++) {
			for (int y = 0; y < width; y++) {
				Debug.Log ("Corners [" + x + "," + y + "] = " + corners [x, y]);
			}
		}

	}

	private void GenerateCar(Vector3 origin){
		GameObject selected = cars [Random.Range (0, cars.Length)];
		GameObject car = Instantiate (selected, origin, selected.transform.rotation);
		car.GetComponent<Drive> ().location = origin;
	}

	private void PlaceCars(int number){
		for (int i = 0; i < number; i++) {
			int x = Random.Range (0, height);
			int y = Random.Range (0, width);
			Vector3 corner = corners [x, y];
			GenerateCar (corner);
		}
	}
}
