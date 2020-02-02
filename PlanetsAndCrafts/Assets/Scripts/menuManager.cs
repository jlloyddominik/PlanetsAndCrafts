using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuManager : MonoBehaviour
{
	GameObject[] menuObjects;
	GameObject[] confirmObjects;
	GameObject[] creditObjects;

	void Start()
	{
		Time.timeScale = 0;
		menuObjects = GameObject.FindGameObjectsWithTag("Main Menu");
		confirmObjects = GameObject.FindGameObjectsWithTag("Confirm Menu");
		creditObjects = GameObject.FindGameObjectsWithTag("Credits");
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


	//reloads
	public void Reload()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	//shows objects with Main Menu tag
	public void showMenu()
	{
		foreach (GameObject g in menuObjects)
		{
			g.SetActive(true);
		}
	}

	//hides objects with Main Menu tag
	public void hideMenu()
	{
		foreach (GameObject g in menuObjects)
		{
			g.SetActive(false);
		}
	}

	//shows objects with Main Menu tag
	public void showConfirm()
	{
		foreach (GameObject g in confirmObjects)
		{
			g.SetActive(true);
		}
	}

	//hides objects with Main Menu tag
	public void hideConfirm()
	{
		foreach (GameObject g in confirmObjects)
		{
			g.SetActive(false);
		}
	}

	//shows objects with Main Menu tag
	public void showCredits()
	{
		foreach (GameObject g in creditObjects)
		{
			g.SetActive(true);
		}
	}

	//hides objects with Main Menu tag
	public void hideCredits()
	{
		foreach (GameObject g in creditObjects)
		{
			g.SetActive(false);
		}
	}

	//loads
	public void LoadLevel(string level)
	{
		Application.LoadLevel(level);
	}
}
