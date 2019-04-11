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

		AudioManager.Instance.PlayBGM(3);
	}
	public void StartGame()
	{
		AudioManager.Instance.PlayButton();
		StartCoroutine("SceneChange", "level_01");
	}
	public void BackToMainMenu()
	{
		AudioManager.Instance.PlayButton();
		StartCoroutine("ScreenChange", true);
	}
	public void GoToLeaderboard()
	{
		AudioManager.Instance.PlayButton();
		StartCoroutine("ScreenChange", false);
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
	IEnumerator SceneChange(string sceneName)
	{
		yield return new WaitForSecondsRealtime(interval);
		SceneManager.LoadScene(sceneName);
	}
	IEnumerator ScreenChange(bool isMainMenu)
	{
		yield return new WaitForSeconds(interval);
		mainMenu.SetActive(isMainMenu);
		leaderboard.SetActive(!isMainMenu);
	}
}
