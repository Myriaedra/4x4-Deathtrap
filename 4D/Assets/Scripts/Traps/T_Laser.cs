using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Laser : Trap {
	public GameManager gMan;
	public int h;
	public int v;
	public GameObject laserPrefab;
	private List<Trap> laserHit = new List<Trap>();
	public List<GameObject> laserFB = new List<GameObject>();
	// Use this for initialization
	void Start () 
	{
		//Get affected grid parts
		int selfX = gMan.getX(transform);
		int selfY = gMan.getY(transform);
		if (v < 0 && selfY > 0)
		{
			for (int i = selfX + v; i >= 0; i--) 
			{
				Transform target = gMan.grid [i, selfY];
				Trap targetTrap = target.GetComponent<Trap> ();
				laserHit.Add (targetTrap);
				targetTrap.laser = true;
				laserFB.Add(Instantiate(laserPrefab, target.position, Quaternion.Euler(0f, 90f,0f)));
			}
		}
		else if (v > 0 && selfY < 3)
		{
			for (int i = selfX + v; i < 4; i++) 
			{
				Transform target = gMan.grid [i, selfY];
				Trap targetTrap = target.GetComponent<Trap> ();
				laserHit.Add (targetTrap);
				targetTrap.laser = true;
				laserFB.Add(Instantiate(laserPrefab, target.position, Quaternion.Euler(0f, 90f,0f)));
			}
		}

		if (h < 0 && selfX > 0)
		{
			for (int i = selfY + h; i >= 0; i--) 
			{
				Transform target = gMan.grid [selfX, i];
				Trap targetTrap = target.GetComponent<Trap> ();
				laserHit.Add (targetTrap);
				targetTrap.laser = true;
				laserFB.Add(Instantiate(laserPrefab, target.position, Quaternion.identity));
			}
		}
		else if (h > 0 && selfX < 3)
		{
			for (int i = selfY + h; i < 4; i++) 
			{
				Transform target = gMan.grid [selfX, i];
				Trap targetTrap = target.GetComponent<Trap> ();
				laserHit.Add (targetTrap);
				targetTrap.laser = true;
				laserFB.Add(Instantiate(laserPrefab, target.position, Quaternion.identity));
			}
		}
	}

	public override void inputPressed ()
	{
		activated = false;

		for (int i = 0; i < laserHit.Count; i++) 
		{
			laserHit [i].laser = false;
		}

		for (int i = 0; i < laserFB.Count; i++) 
		{
			laserFB [i].SetActive (false);
		}

	}

	public override void inputReleased ()
	{
		activated = true;

		for (int i = 0; i < laserHit.Count; i++) 
		{
			laserHit [i].laser = true;
		}

		for (int i = 0; i < laserFB.Count; i++) 
		{
			laserFB [i].SetActive (true);
		}
	}

}
