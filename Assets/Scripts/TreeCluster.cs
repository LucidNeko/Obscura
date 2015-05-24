using UnityEngine;
using System.Collections;

public class TreeCluster : MonoBehaviour {

	public GameObject[] m_Trees;
	public int m_NumTrees = 10;
	public Vector3 m_Min = new Vector3(-10, 0, -10);
	public Vector3 m_Max = new Vector3(10, 0, 10);

	// Use this for initialization
	void Start () {
		for(int i = 0; i < m_NumTrees; i++) {
			GameObject tree = m_Trees[(int)((Random.value-0.01f)*m_Trees.Length)];
			Vector3 position = new Vector3(Random.Range(m_Min.x, m_Max.x), transform.position.y, Random.Range(m_Min.z, m_Max.z));
//			RaycastHit hitInfo;
//			if(Physics.Raycast(position, Vector3.down, out hitInfo, 100f)) {
//
//			}
			Instantiate(tree, position, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
