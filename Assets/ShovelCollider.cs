using UnityEngine;
using System.Collections;

public class ShovelCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		DestroyImmediate (GetComponent<SphereCollider> ());
		SphereCollider c = gameObject.AddComponent<SphereCollider> ();
		c.isTrigger = true;
		c.radius = 0.2f;
	}
}
