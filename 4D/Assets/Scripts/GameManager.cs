using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	// Use this for initialization
	public Transform[] cases;
	public Transform[,] grid = new Transform[4,4];
	private string letter = "ABCD";

	public float timeBeforeReload;

	public static bool started;

	bool over;

	void Awake () 
	{
		CharacterController.Died += GameOver;
		CharacterController.ReachedExit += Win;
		T_Exit.Ended += GameOver;

		int i = 0;

		for (int x = 0; x < 4; x++) 
		{
			for (int y = 0; y < 4; y++) 
			{
				grid [x, y] = cases [i];
				string input = (x+1).ToString () + letter [y];
				cases [i].GetComponent<Trap> ().input = input;
				i++;
			}
		}
	}

	void Update()
	{
		if (Input.GetButtonDown ("1A") && !started) 
		{
			StartGame ();
		}
	}

	public int getX(Transform checkedCase)
	{
		int position = System.Array.IndexOf (cases, checkedCase);
		return (Mathf.FloorToInt (position / 4));
	}

	public int getY(Transform checkedCase)
	{
		int position = System.Array.IndexOf (cases, checkedCase);
		return (Mathf.FloorToInt (position % 4));
	}

	void StartGame()
	{
		started = true;
	}

	void GameOver()
	{
		if (!over) 
		{
			over = true;
			Invoke ("Reload", timeBeforeReload);
			started = false;
		}
	}

	void Win()
	{
		started = false;
	}

	void Reload()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
