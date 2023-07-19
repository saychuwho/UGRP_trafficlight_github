using UnityEngine;
using System.Collections;

public class Lights : MonoBehaviour {
	
	public LensFlare[] flares;
	public Light[] lights;
	
	public LensFlare[] stopFlares;
	public Light[] stopLights;

	public AudioSource Signal;

	private enum States { On, Off }
	
	private States currentState = States.On;
	
	// Update is called once per frame
	void Start() {
		for (int i = 0; i < stopLights.Length; i++) {
			stopLights[i].enabled = false;
		}
		for (int i = 0; i < stopFlares.Length; i++) {
			stopFlares[i].enabled = false;
		}
	}
	
	void Update () {
		if (Input.GetKeyUp(KeyCode.L)) {
			switch ( currentState ) {
			case States.Off:
				for (int i = 0; i < flares.Length; i++) {
					flares[i].enabled = true;
				}
				
				for (int i = 0; i < lights.Length; i++) {
					lights[i].enabled = true;
				}
				
				currentState = States.On;
				break;
			case States.On:
				for (int i = 0; i < flares.Length; i++) {
					flares[i].enabled = false;
				}
				
				for (int i = 0; i < lights.Length; i++) {
					lights[i].enabled = false;
				}
				
				currentState = States.Off;
				
				break;
			}
		}
		
		if (Input.GetKey(KeyCode.S)) {
			for (int i = 0; i < stopLights.Length; i++) {
				stopLights[i].enabled = true;
			}
			for (int i = 0; i < stopFlares.Length; i++) {
				stopFlares[i].enabled = true;
			}
		} 
		
		if (Input.GetKeyUp(KeyCode.S)) {
			for (int i = 0; i < stopLights.Length; i++) {
				stopLights[i].enabled = false;
			}
			for (int i = 0; i < stopFlares.Length; i++) {
				stopFlares[i].enabled = false;
			}
		}

		if (Input.GetKey(KeyCode.K)) {
			if (!Signal.isPlaying) Signal.Play();
		}
	}
}
