using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	GameObject mainMenu = null;
	GameObject leaderboard = null;

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
		SceneManager.LoadScene("level_01");
	}
	public void BackToMainMenu()
	{
		mainMenu.SetActive(true);
		leaderboard.SetActive(false);
	}
	public void GoToLeaderboard()
	{
		mainMenu.SetActive(false);
		leaderboard.SetActive(true);
	}
	public void ExitGame()
	{
		Application.Quit();
	}
}
