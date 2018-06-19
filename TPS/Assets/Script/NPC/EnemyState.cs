using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour {


	public enum EMode {
		AWARE,
		UNAWARE
		
	}

	public enum EMoveState {
		STANDING,
		CROUCHING,

	}

	private EMode m_CurrentMode;
	public EMode CurrentMode{
		get {
			return m_CurrentMode;
		}
		set {
			if (m_CurrentMode == value)
				return;

			m_CurrentMode = value;
			if (OnModeChanged != null)
				OnModeChanged (value);
		}
	}

	private EMoveState m_CurrentMoveState;
	public EMoveState CurrentMoveState{
		get {
			return m_CurrentMoveState;
		}
		set {
			if (m_CurrentMoveState == value)
				return;

			m_CurrentMoveState = value;
			if (OnMoveStateChanged != null)
				OnMoveStateChanged (value);
		}
	}

	public event System.Action<EMode> OnModeChanged;
	public event System.Action<EMoveState> OnMoveStateChanged;


	void Start () {
		CurrentMode = EMode.UNAWARE;
		CurrentMoveState = EMoveState.STANDING;
	}

	[ContextMenu("set aware")]
	void SetAware(){
		CurrentMode = EMode.AWARE;
	}
	[ContextMenu("set unaware")]
	void SetUnaware(){
		CurrentMode = EMode.UNAWARE;
	}

}
