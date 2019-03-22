using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	private RectTransform gameOver = null;
	private Image blackScreen = null;
	private Image bloodyScreen = null;
	private RectTransform pause = null;
	private RectTransform win = null;
	private RectTransform rank = null;
	private RectTransform keyIndicator = null;
	private Text timer_min = null;
	private Text timer_sec = null;
	private Text timer_sec_dec = null;
	private float timerTime = 0.0f;

	private static HUD instance;
	public static HUD Instance { get { return instance; } }

	private bool isGameOver = false;
	private float blackAlpha = 0.0f;
	private float bloodyAlpha = 0.0f;

	private const float interval = 0.2f;

	private void Awake()
	{
		instance = this;
		gameOver = transform.Find("GameOver").GetComponent<RectTransform>();
		blackScreen = gameOver.transform.Find("Black").GetComponent<Image>();
		bloodyScreen = gameOver.transform.Find("Bloody").GetComponent<Image>();

		pause = transform.Find("Pause").GetComponent<RectTransform>();
		win = transform.Find("Win").GetComponent<RectTransform>();
		rank = win.transform.Find("Rank").GetComponent<RectTransform>();
		keyIndicator = transform.Find("Key").GetComponent<RectTransform>();
		timer_min = transform.Find("Timer_Min").GetComponent<Text>();
		timer_sec = transform.Find("Timer_Sec").GetComponent<Text>();
		timer_sec_dec = transform.Find("Timer_Sec_Dec").GetComponent<Text>();
	}

	private void Start()
	{
		gameOver.gameObject.SetActive(false);
		pause.gameObject.SetActive(false);
		rank.gameObject.SetActive(false);
		win.gameObject.SetActive(false);
		keyIndicator.gameObject.SetActive(false);
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
		{
			Pause();
		}

		if (isGameOver)
		{
			blackAlpha = Mathf.Clamp(blackAlpha + (Time.unscaledDeltaTime), 0, 1);
			if(blackAlpha >= 0.5f)
				bloodyAlpha = Mathf.Clamp(bloodyAlpha + (Time.unscaledDeltaTime * 2), 0, 1);
			blackScreen.color = new Color(1, 1, 1, blackAlpha);
			bloodyScreen.color = new Color(1, 1, 1, bloodyAlpha);
		}

		UpdateText();
	}
	private void UpdateText()
	{
		timerTime += Time.deltaTime;

		int minute = (int)timerTime / 60;
		string minute_text = string.Format("{0:00}", minute);
		timer_min.text = minute_text;

		int second = (int)timerTime % 60;
		
		string second_text = string.Format("{0:00}", second);
		timer_sec.text = second_text;

		float second_decimal = timerTime%60.0f - second;
		second_decimal *= 100;
		string second_dec_text = string.Format("{0:00}", second_decimal);
		timer_sec_dec.text = second_dec_text;
	}
	
	public void Pause()
	{
		Time.timeScale = 0.0f;
		pause.gameObject.SetActive(true);
		AudioManager.Instance.PlayButton();
	}
	public void Resume()
	{
		Time.timeScale = 1.0f;
		pause.gameObject.SetActive(false);
		AudioManager.Instance.PlayButton();
	}
	public void Win()
	{
		Debug.Log("win");
		Time.timeScale = 0.0f;
		if (CheckRanking())
			TurnOnRankingUpdate();
	}
	public bool CheckRanking()
	{
		return true;
	}
	public void TurnOnRankingUpdate()
	{
	}
	public void UpdateRanking()
	{

	}
	public void WinReplay()
	{
		UpdateRanking();
		Replay();
	}
	public void WinToMenu()
	{
		UpdateRanking();
		ToMenu();
	}
	public void Replay()
	{
		AudioManager.Instance.PlayButton();
		StartCoroutine("SceneChange", "level_01");
	}
	public void ToMenu()
	{
		AudioManager.Instance.PlayButton();
		StartCoroutine("SceneChange", "main_menu");
	}

	IEnumerator SceneChange(string sceneName)
	{
		yield return new WaitForSecondsRealtime(interval);
		Time.timeScale = 1.0f;
		SceneManager.LoadScene(sceneName);
	}
	public void Pickup()
	{
		keyIndicator.gameObject.SetActive(true);
	}
	public void GameOver()
	{
		Debug.Log("Game Over");
		isGameOver = true;
		gameOver.gameObject.SetActive(true);
		AudioManager.Instance.PlayGameOver();
	}
}
