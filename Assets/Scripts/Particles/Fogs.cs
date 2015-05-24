using UnityEngine;
using System.Collections;

public class Fogs : MonoBehaviour {

	// Use this for initialization
	void Start () {
		RenderSettings.fog = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftShift)){
			RenderSettings.fog = !RenderSettings.fog;
		}
	}
}
