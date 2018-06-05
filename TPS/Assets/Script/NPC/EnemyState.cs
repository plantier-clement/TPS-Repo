using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour {


	public enum EMode {
		AWARE,
		UNAWARE
		
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

	public event System.Action<EMode> OnModeChanged;


	void Start () {
		CurrentMode = EMode.UNAWARE;
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
