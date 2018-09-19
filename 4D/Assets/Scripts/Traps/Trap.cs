using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : MonoBehaviour {
	public string input;

	public bool activated;
	public bool laser;
	public bool kills { get { return activated || laser; }}

	private float pressTimer = -1f;
	private float inputTime = 0.05f;

	protected virtual void Update()
	{
		if (GameManager.started) {
			if (pressTimer > 0)
				pressTimer -= Time.deltaTime;
		
			if (Input.GetButtonDown (input)) {
				if (pressTimer == -1f) {
					inputPressed ();
				}
				pressTimer = inputTime;

			} else if (pressTimer <= 0 && pressTimer != -1f) {
				pressTimer = -1f;
				inputReleased ();
			}
		}
	}

	public virtual void inputPressed(){}

	public virtual void inputReleased(){}

}
