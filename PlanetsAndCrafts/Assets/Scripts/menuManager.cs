using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuManager : MonoBehaviour
{
	GameObject[] menuObjects;
	GameObject[] confirmObjects;
	GameObject[] creditObjects;
	GameObject[] playerObjects;

	void Start()
	{
		Time.timeScale = 0;
		menuObjects = GameObject.FindGameObjectsWithTag("Main Menu");
		confirmObjects = GameObject.FindGameObjectsWithTag("Confirm Menu");
		creditObjects = GameObject.FindGameObjectsWithTag("Credits");
		playerObjects = GameObject.FindGameObjectsWithTag("Player");

		showMenu();
		hideConfirm();
		hideCredits();
		hidePlayer();
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

	//quits
	public void Quit()
	{
		Application.Quit();
	}

	//shows objects with Main Menu tag
	public void showMenu()
	{
		foreach (GameObject g in menuObjects)
		{
            Debug.Log(g.name);
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

		showPlayer();
	}

	//shows objects with Confirm tag
	public void showConfirm()
	{
		foreach (GameObject g in confirmObjects)
		{
			g.SetActive(true);
			Debug.Log("bonk");
		}

		hidePlayer();
	}

	//hides objects with Confirm tag
	public void hideConfirm()
	{
		foreach (GameObject g in confirmObjects)
		{
			g.SetActive(false);
		}
	}

	//shows objects with Credit tag
	public void showCredits()
	{
		foreach (GameObject g in creditObjects)
		{
			g.SetActive(true);
		}

		hideMenu();
	}

	//hides objects with Credit tag
	public void hideCredits()
	{
		foreach (GameObject g in creditObjects)
		{
			g.SetActive(false);
		}

		showMenu();
	}

	//hides objects with Player tag
	public void showPlayer()
	{
        Time.timeScale = 1;
        foreach (GameObject g in playerObjects)
		{
			g.SetActive(true);
		}
	}

	//hides objects with Player tag
	public void hidePlayer()
	{
		foreach (GameObject g in playerObjects)
		{
			g.SetActive(false);
		}
	}
}
