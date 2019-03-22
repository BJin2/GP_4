using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	GameObject mainMenu = null;
	GameObject leaderboard = null;

	const float interval = 0.2f;

	private void Awake()
	{
		mainMenu = GameObject.Find("Main");
		leaderboard = GameObject.Find("Leaderboard");
	}
	private void Start()
	{
		mainMenu.SetActive(true);
		leaderboard.SetActive(false);
	}
	public void StartGame()
	{
		AudioManager.Instance.PlayButton();
		StartCoroutine("startGame");
	}
	IEnumerator startGame()
	{
		yield return new WaitForSeconds(interval);
		SceneManager.LoadScene("level_01");
	}
	public void BackToMainMenu()
	{
		AudioManager.Instance.PlayButton();
		StartCoroutine("backToMainMenu");
	}
	IEnumerator backToMainMenu()
	{
		yield return new WaitForSeconds(interval);
		mainMenu.SetActive(true);
		leaderboard.SetActive(false);
	}
	public void GoToLeaderboard()
	{
		AudioManager.Instance.PlayButton();
		StartCoroutine("goToLeaderboard");
	}
	IEnumerator goToLeaderboard()
	{
		yield return new WaitForSeconds(interval);
		mainMenu.SetActive(false);
		leaderboard.SetActive(true);
	}
	public void ExitGame()
	{
		AudioManager.Instance.PlayButton();
		StartCoroutine("exitGame");
	}
	IEnumerator exitGame()
	{
		yield return new WaitForSeconds(interval);
		Application.Quit();
	}
}
