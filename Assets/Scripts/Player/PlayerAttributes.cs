using UnityEngine;
using System.Collections;

public class PlayerAttributes : MonoBehaviour {


	public GUISkin skin;
	

	private float hbWidth;
	private float healthLevel;

	private Vector3 ogPos;

	void Start () {
		healthLevel = hbWidth = 10;
		ogPos = transform.position;
	}

	void Update () {
		hbWidth = healthLevel;
		if(CheckDead()) Respawn();

	}


	void OnGUI(){
		GUI.skin = skin;

		Rect healthBorder = new Rect (10, Screen.height - 25, Screen.width / 4, 20);
		Rect healthBar = new Rect(15, Screen.height - 20, hbWidth, 10);


		GUI.Box (healthBorder, "",skin.GetStyle("healthBarBorder"));

		GUI.Box (healthBar, "",skin.GetStyle("health"));

	}

	public void AddHealth(float amount){
		healthLevel += amount;
	}

	public void RemoveHealth(float amount){
		healthLevel -= amount;
	}

	public float GetHealth(){
		return healthLevel;
	}




	void OnTriggerEnter(Collider other) {
		if (other.tag == "Orb") {
			other.gameObject.SetActive(false);
			AddHealth(20);
		}
	}

	bool CheckDead(){
		return healthLevel <= 0;
	}

	void Respawn(){
		transform.position = ogPos;
		healthLevel = 10;
	}
	
}