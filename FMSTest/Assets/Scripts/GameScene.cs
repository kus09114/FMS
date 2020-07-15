using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
	[SerializeField] Button m_btnStart = null;
	[SerializeField] Button m_btnStop = null;
	[SerializeField] Button m_btnClear = null;

	[SerializeField] Text m_txtState = null;
	[SerializeField] Text m_txtStayTime = null;

	private BattleFSM m_BattleFSM = new BattleFSM();

	private int m_nWaveCount = 0;
	private float m_fCurTime = 0;

	void Awake()
	{
		m_BattleFSM.Initialize(	Callback_ReadyState,
								Callback_WaveState,
								Callback_GameState,
								Callback_ResultState);
	}

	void Start()
	{
		Initializ();
	}

	void Initializ()
	{
		m_btnStart.onClick.AddListener(OnClick_StartButton);
		m_btnStop.onClick.AddListener(OnClick_StopButton);
		m_btnClear.onClick.AddListener(OnClick_ClearButton);
	}

	void Update()
	{
		m_BattleFSM.OnUpdate();
	}

	void Callback_ReadyState()
	{
		m_txtState.text = "Ready State";
		Invoke("InvokeCallback_Wave", 2.0f);
	}
	void Callback_WaveState()
	{
		m_txtState.text = "Wave State";
		Invoke("InvokeCallback_Game", 1.0f);
	}
	void Callback_GameState()
	{
		m_txtState.text = "Game State";
		Invoke("InvokeCallback_Result", 4.0f);
	}
	void Callback_ResultState()
	{
		m_txtState.text = "Result State";
		Invoke("InvokeCallback_Ready", 3.0f);
	}

	void InvokeCallback_Ready()
	{
		m_BattleFSM.SetReadyState();
	}
	void InvokeCallback_Wave()
	{
		m_BattleFSM.SetWaveState();
	}
	void InvokeCallback_Game()
	{
		m_BattleFSM.SetGameState();
	}
	void InvokeCallback_Result()
	{
		m_BattleFSM.SetResultState();
	}

	void OnClick_StartButton()
	{
		m_BattleFSM.SetReadyState();
	}
	void OnClick_StopButton()
	{

	}
	void OnClick_ClearButton()
	{
		m_txtState.text = "상태";
		m_BattleFSM.SetNoneState();
	}
}
