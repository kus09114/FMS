using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFSM : MonoBehaviour
{
	public delegate void DelegateFunc();

    public class CState
	{
		public DelegateFunc m_OnEnterFunc = null;
		public virtual void Initialize(DelegateFunc func)
		{
			m_OnEnterFunc = new DelegateFunc(func);
		}

		public virtual void OnEnter() { }
		public virtual void OnUpdate() { }
		public virtual void OnExit() { }
	}
	public class CReadyState : CState
	{
		public override void OnEnter()
		{
			if(m_OnEnterFunc != null)
				m_OnEnterFunc();
		}
		public override void OnUpdate() { }
		public override void OnExit() { }
	}
	public class CWaveState : CState
	{
		public override void OnEnter()
		{
			if (m_OnEnterFunc != null)
				m_OnEnterFunc();
		}
		public override void OnUpdate() { }
		public override void OnExit() { }
	}
	public class CGameState : CState
	{
		public override void OnEnter()
		{
			if (m_OnEnterFunc != null)
				m_OnEnterFunc();
		}
		public override void OnUpdate() { }
		public override void OnExit() { }
	}
	public class CResultState : CState
	{
		public override void OnEnter()
		{
			if (m_OnEnterFunc != null)
				m_OnEnterFunc();
		}
		public override void OnUpdate() { }
		public override void OnExit() { }
	}

	private CState m_curState = null;
	private CState m_newState = null;

	private CState m_Ready = new CReadyState();
	private CState m_Wave = new CWaveState();
	private CState m_Game = new CGameState();
	private CState m_Result = new CResultState();

	public void Initialize(DelegateFunc kReady, DelegateFunc kWave, DelegateFunc kGame, DelegateFunc kResult)
	{
		m_Ready.Initialize(kReady);
		m_Wave.Initialize(kWave);
		m_Game.Initialize(kGame);
		m_Result.Initialize(kResult);
	}

	// 상태 변화 셋팅
	public void SetState(CState kState)
	{
		m_newState = kState;
	}

	public void OnUpdate()
	{
		 //	[New State 발생]	>	[현재상태 -> 종료하기]	>	[현재상태 -> 최신상태로]	>	[New State -> 초기화]	>	[현재상태 -> 입장]	>	[현재상태 -> 활동]	
		 //							Cur State - OnExit()		Cur State = New State			New State = null			Cur State - OnEnter		Cur State - OnUpdate()
		if(m_newState != null)
		{
			if (m_curState != null)
			{
				m_curState.OnExit();
			}
			m_curState = m_newState;
			m_newState = null;
			m_curState.OnEnter();
		}

		if(m_curState != null)
		{
			m_curState.OnUpdate();
		}
	}

	public void SetReadyState()
	{
		SetState(m_Ready);
	}
	public void SetWaveState()
	{
		SetState(m_Wave);
	}
	public void SetGameState()
	{
		SetState(m_Game);
	}
	public void SetResultState()
	{
		SetState(m_Result);
	}

	public bool IsCurState(CState kState)
	{
		if (m_curState == null)
			return false;

		return (m_curState == kState) ? true : false;
	}

	public CState GetCurState()
	{
		return m_curState;
	}

	public void SetNoneState()
	{
		m_newState = null;
		m_curState = null;
	}

	public bool IsResultState()
	{
		return (m_curState == m_Result) ? true : false;
	}

	public bool IsNoneState()
	{
		return (m_curState == null) ? true : false;
	}
}
