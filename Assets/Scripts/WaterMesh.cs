using UnityEngine;
using System.Collections;

public class WaterMesh : MonoBehaviour {
	
	Mesh mesh;

	private float[] minVertices;
	private float[] maxVertices;
	private bool[] vertexAscending;

	public float swellHeight;
	public float swellSpeed;
	
	public GameObject spray;
	public GameObject island;
	private Transform islandT;


	void Start() {
		mesh = GetComponent<MeshFilter> ().mesh;

		minVertices = new float[mesh.vertices.Length];
		maxVertices = new float[mesh.vertices.Length];
		vertexAscending = new bool[mesh.vertices.Length];

		islandT = island.transform;

		SetBounds();

	}

	void SetBounds(){
		Random random = new Random ();

		for(int i = 0; i< mesh.vertices.Length; i++){
			minVertices[i] = mesh.vertices[i].y - (1.0f * swellHeight);
			maxVertices[i] = mesh.vertices[i].y + (1.0f * swellHeight); 

			vertexAscending[i] = RandomBool ();
		}


	}

	bool RandomBool(){
		return (Random.value > 0.5f);
	}
	

	void Update() {
		Vector3[] vertices = mesh.vertices;
		Vector3[] normals = mesh.normals;

		Swell (vertices, normals);

	}
	

	void Swell(Vector3[] vertices, Vector3[] normals){
		int i = 0;
		while (i<vertices.Length) {

			float random = (Random.Range(0,2));

			if(vertexAscending[i]){		
				if(vertices[i].y < maxVertices[i]){			//CASE1
					vertices[i].y += random * Time.deltaTime * swellSpeed;
				}
				else{										//CASE2
					vertexAscending[i] = false;
					vertices[i].y -= (random/2) * Time.deltaTime * swellSpeed;
					if((Vector3.Distance(islandT.position,vertices[i])<75) && (Vector3.Distance(islandT.position,vertices[i])>25) && (Random.value < 0.8 )){
						Vector3 genpos = vertices[i];
						genpos.y -= ((swellHeight*2)+2);
						Instantiate(spray,genpos,Quaternion.LookRotation(Vector3.up)); 
					}
				}
			}
			else{						
				if(vertices[i].y > minVertices[i]){			//CASE3
					vertices[i].y -= random * Time.deltaTime * swellSpeed;
				}
				else{										//CASE4
					vertexAscending[i] = true;
					vertices[i].y += (random/2) * Time.deltaTime * swellSpeed;				
				}
			}
			i++;
		}
		mesh.vertices = vertices;
	}
}
