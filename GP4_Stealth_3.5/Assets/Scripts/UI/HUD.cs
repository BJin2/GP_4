﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	private static HUD instance;
	public static HUD Instance { get { return instance; } }

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
	private Text result_min = null;
	private Text result_sec = null;
	private Text result_sec_dec = null;
	private float timerTime = 0.0f;

	private Text[] initials;
	public static string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
	private int[] indices = { 0, 0, 0 };

	private int ranking;

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

		result_min = win.transform.Find("Result").Find("Timer_Min").GetComponent<Text>();
		result_sec = win.transform.Find("Result").Find("Timer_Sec").GetComponent<Text>();
		result_sec_dec = win.transform.Find("Result").Find("Timer_Sec_Dec").GetComponent<Text>();

		initials = new Text[3];
		for (int i = 0; i < initials.Length; i++)
		{
			initials[i] = rank.transform.Find("Initial_" + i.ToString()).GetComponent<Text>();
		}
		ranking = -1;
	}

	private void Start()
	{
		gameOver.gameObject.SetActive(false);
		pause.gameObject.SetActive(false);
		rank.gameObject.SetActive(false);
		win.gameObject.SetActive(false);
		keyIndicator.gameObject.SetActive(false);

		AudioManager.Instance.PlayBGM(4);
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

		timerTime += Time.deltaTime;
		UpdateText(timerTime, timer_min, timer_sec, timer_sec_dec);
	}

	public void Up(int i)
	{
		indices[i]++;
		indices[i] %= alphabet.Length;
		initials[i].text = alphabet[indices[i]];
	}
	public void Down(int i)
	{
		indices[i]--;
		if (indices[i] < 0)
			indices[i] = (alphabet.Length - 1);

		initials[i].text = alphabet[indices[i]];
	}

	public static void UpdateText(float totalTime, Text Min, Text Sec, Text Dec)
	{
		int minute = (int)totalTime / 60;
		string minute_text = string.Format("{0:00}", minute);
		Min.text = minute_text;

		int second = (int)totalTime % 60;

		string second_text = string.Format("{0:00}", second);
		Sec.text = second_text;

		float second_decimal = totalTime % 60.0f - second;
		second_decimal *= 100;
		string second_dec_text = string.Format("{0:00}", second_decimal);
		Dec.text = second_dec_text;
	}


	public bool CheckRanking()// Compare player's result with existing record
	{
		for (int i = 0; i < 3; i++)
		{
			float comp = PlayerPrefs.GetFloat("high_" + i.ToString(), -1) > 0 ? PlayerPrefs.GetFloat("high_" + i.ToString(), float.MaxValue) : float.MaxValue;
			if (timerTime < comp)
			{
				ranking = i;
				return true;
			}
		}

		return false;
	}
	public void TurnOnRankingUpdate()
	{
		rank.gameObject.SetActive(true);
	}
	public void UpdateRanking()//Save player's rank
	{
		// push back
		for (int i = 2; i > ranking; i--)
		{
			PlayerPrefs.SetFloat("high_" + i.ToString(), PlayerPrefs.GetFloat("high_" + (i - 1).ToString(), -1));

			for (int j = 0; j < 3; j++)
			{
				PlayerPrefs.SetInt(i.ToString() + "_" + j.ToString(), PlayerPrefs.GetInt((i - 1).ToString() + "_" + j.ToString(), 0));
			}
		}

		// insert ranking value
		for (int i = 0; i < 3; i++)
		{
			PlayerPrefs.SetInt(ranking.ToString() + "_" + i.ToString(), indices[i]);
		}
		PlayerPrefs.SetFloat("high_" + ranking.ToString(), timerTime);
	}

//Functions for button
	public void WinReplay()
	{
		if(ranking >= 0)
			UpdateRanking();
		Replay();
	}
	public void WinToMenu()
	{
		if (ranking >= 0)
			UpdateRanking();
		ToMenu();
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

//Functions for state change
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
	public void Win()
	{
		Debug.Log("win");
		Time.timeScale = 0.0f;
		win.gameObject.SetActive(true);
		AudioManager.Instance.PlayWin();
		UpdateText(timerTime, result_min, result_sec, result_sec_dec);
		if (CheckRanking())
			TurnOnRankingUpdate();
	}


	IEnumerator SceneChange(string sceneName)
	{
		yield return new WaitForSecondsRealtime(interval);
		Time.timeScale = 1.0f;
		SceneManager.LoadScene(sceneName);
	}
}
