using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuManager : MonoBehaviour
{
	GameObject[] menuObjects;
	GameObject[] confirmObjects;

	void Start()
	{
		Time.timeScale = 0;
		menuObjects = GameObject.FindGameObjectsWithTag("Main Menu");
		confirmObjects = GameObject.FindGameObjectsWithTag("Confirm Menu");
		showMenu();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (Time.timeScale == 1)
			{
				Time.timeScale = 0;
				showMenu();
			}
			else if (Time.timeScale == 0)
			{
				Debug.Log("game is go");
				Time.timeScale = 1;
				hideMenu();
			}
		}
	}


	//Reloads the Level
	public void Reload()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	//controls the pausing of the scene
	public void pauseControl()
	{
		if (Time.timeScale == 1)
		{
			Time.timeScale = 0;
			showMenu();
		}
		else if (Time.timeScale == 0)
		{
			Time.timeScale = 1;
			hideMenu();
		}
	}

	//shows objects with ShowOnPause tag
	public void showMenu()
	{
		foreach (GameObject g in menuObjects)
		{
			g.SetActive(true);
		}
	}

	//hides objects with ShowOnPause tag
	public void hideMenu()
	{
		foreach (GameObject g in menuObjects)
		{
			g.SetActive(false);
		}
	}

	//loads inputted level
	public void LoadLevel(string level)
	{
		Application.LoadLevel(level);
	}
}
