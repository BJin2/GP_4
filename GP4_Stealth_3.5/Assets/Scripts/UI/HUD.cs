using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	private RectTransform gameOver = null;
	private Image blackScreen = null;
	private Image bloodyScreen = null;
	private RectTransform pause = null;
	private RectTransform keyIndicator = null;
	private Text timer = null;
	private float timerTime = 0.0f;

	private static HUD instance;
	public static HUD Instance { get { return instance; } }

	private bool isGameOver = false;
	private float blackAlpha = 0.0f;
	private float bloodyAlpha = 0.0f;

	private void Awake()
	{
		instance = this;
		gameOver = transform.Find("GameOver").GetComponent<RectTransform>();
		blackScreen = gameOver.transform.Find("Black").GetComponent<Image>();
		bloodyScreen = gameOver.transform.Find("Bloody").GetComponent<Image>();

		pause = transform.Find("Pause").GetComponent<RectTransform>();
		keyIndicator = transform.Find("Key").GetComponent<RectTransform>();
		timer = transform.Find("Timer").GetComponent<Text>();
	}

	private void Start()
	{
		gameOver.gameObject.SetActive(false);
		pause.gameObject.SetActive(false);
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

		timerTime += Time.deltaTime;
		timer.text = timerTime.ToString();
	}
	public void Pause()
	{
		Time.timeScale = 0.0f;
		pause.gameObject.SetActive(true);
	}
	public void Resume()
	{
		Time.timeScale = 1.0f;
		pause.gameObject.SetActive(false);
	}
	public void Replay()
	{

	}
	public void ToMenu()
	{
		
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
