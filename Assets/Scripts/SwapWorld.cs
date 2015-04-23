using UnityEngine;
using System.Collections;

public class SwapWorld : MonoBehaviour {

	public string darkWorld;
	public string lightWorld;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Tab)){
			print(Application.loadedLevel);
		}
	}
}
