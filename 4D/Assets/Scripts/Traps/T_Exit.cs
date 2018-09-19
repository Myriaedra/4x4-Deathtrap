using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Exit : Trap {
	Vector3 originPosition;
	public Vector3 endPosition;
	public Transform doorMesh;

	public float limitTime;
	float timer;
	public bool ended;

	public delegate void ExitEvent();
	public static event ExitEvent Ended;
	// Use this for initialization
	void Start () {
		originPosition = doorMesh.localPosition;
		timer = limitTime;
	}

	public override void inputPressed ()
	{
		timer = limitTime;
	}

	protected override void Update ()
	{
		base.Update ();

		if (GameManager.started) 
		{
			if (timer > 0 && !ended) {
				timer -= Time.deltaTime;
			} else if (!ended) {
				Ended ();
				ended = true;
			}

			doorMesh.localPosition = Vector3.Lerp (endPosition, originPosition, timer / limitTime);
		}
	}
}
