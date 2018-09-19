using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Fire : Trap {

	public float offTime;
	public float activatedTime;
	public float offTimer;
	public float onValue;
	public float decrement;

	public Transform[] flames;

	void Start()
	{
		onValue = activatedTime;
		activated = true;
	}

	protected override void Update ()
	{
		base.Update ();

		if (onValue < 0 && activated)
		{
			offTimer = offTime;
			activated = false;
			onValue = 0;
		}

		if (offTimer > 0) {
			offTimer -= Time.deltaTime;
		} else if (!activated) 
		{
			activated = true;
			onValue = activatedTime / 2;
		}

		if (onValue < activatedTime && activated) 
		{
			onValue += Time.deltaTime;
		}

		foreach (Transform flame in flames) 
		{
			flame.localScale = Vector3.Lerp (Vector3.zero, Vector3.one, onValue / activatedTime);
		}
	}

	public override void inputPressed ()
	{
		if (activated)
			onValue -= decrement;
		else if (offTimer < offTime)
			offTimer += decrement;
	}

}
