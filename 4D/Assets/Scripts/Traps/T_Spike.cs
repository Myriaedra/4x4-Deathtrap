using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Spike : Trap {
	public GameObject onMesh;

	void Start()
	{
		activated = true;
	}

	public override void inputPressed ()
	{
		print ("OFF");
		activated = false;
		onMesh.SetActive (false);
	}

	public override void inputReleased ()
	{
		print ("ON");
		activated = true;
		onMesh.SetActive (true);
	}
}
