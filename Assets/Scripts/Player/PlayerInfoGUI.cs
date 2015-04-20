using UnityEngine;
using System.Collections;

public class PlayerInfoGUI : MonoBehaviour {


	public GUISkin skin;
	

	private float hbWidth;
	private float healthLevel;

	void Start () {
		hbWidth = 0;
	}

	void Update () {
		hbWidth = healthLevel;
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

}