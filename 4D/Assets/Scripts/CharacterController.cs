using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour {
	public NavMeshAgent nmA;
	public Transform destination;

	public LayerMask groundMask;
	public float checkDelay;
	private bool going;
	private bool died;
	public GameObject bloodFX;
	public Animator animator;

	public delegate void CharacterEvent();
	public static event CharacterEvent Died;
	public static event CharacterEvent ReachedExit;



	// Use this for initialization
	void Update () {
		if (GameManager.started && !going) 
		{
			ActivateCharacter ();
		}
	}

	
	public void ActivateCharacter()
	{
		nmA.enabled = true;
		nmA.SetDestination (destination.position);
		going = true;
		CheckGround ();
	}

	public void CheckGround()
	{
		Collider[] hitColliders = Physics.OverlapSphere (transform.position, 1f, groundMask);
		for (int i = 0; i < hitColliders.Length; i++) 
		{
			Trap trap = hitColliders [i].GetComponent<Trap> ();

			if (trap.kills) 
			{
				Die ();
			}

			if (hitColliders [i].tag == "Exit") 
			{
				hitColliders [i].GetComponent<T_Exit> ().ended = true;
				ReachedExit ();
				nmA.enabled = false;

			}
		}

		if (!died)
			Invoke ("CheckGround", checkDelay);
	}

	void Die()
	{
		animator.SetBool ("Died", true);
		died = true;
		Instantiate (bloodFX, transform.position, Quaternion.identity);
		Died ();
		nmA.enabled = false;
	}
}
