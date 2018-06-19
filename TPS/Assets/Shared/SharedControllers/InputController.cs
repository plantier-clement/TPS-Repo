using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {





	public float Vertical;
	public float Horizontal;
	public Vector2 MouseInput;

	public bool IsWalking;
	public bool IsSprinting;
	public bool IsCrouched;

	public bool Fire1;
	public bool Reload;
	public bool MouseWheelUp;
	public bool MouseWheelDown;

	public bool IsAiming;
	public bool Cover;


	// Menu & shit
	public bool Escape;


	void Awake(){
		
		
	}


	void Update(){




			Vertical = Input.GetAxis ("Vertical");
			Horizontal = Input.GetAxis ("Horizontal");
			MouseInput = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));

			IsWalking = Input.GetButton ("Walk");
			IsSprinting = Input.GetButton ("Sprint");
			IsCrouched = Input.GetButton ("Crouch");

			Fire1 = Input.GetButton ("Fire1");
			Reload = Input.GetButtonDown ("Reload");
			MouseWheelUp = Input.GetAxis ("Mouse ScrollWheel") > 0;
			MouseWheelDown = Input.GetAxis ("Mouse ScrollWheel") < 0;

			IsAiming = Input.GetButton ("Aim");
			Cover = Input.GetButtonDown ("Cover");
			Escape = Input.GetButtonDown ("Cancel");



		// Menu & shit

			Escape = Input.GetButtonDown ("Cancel");


	}
}
