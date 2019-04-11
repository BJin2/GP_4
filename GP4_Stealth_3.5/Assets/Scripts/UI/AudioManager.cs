using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField]
	private AudioSource primaryAud;
	[SerializeField]
	private AudioSource secondaryAud;

	[SerializeField]
	private AudioClip[] clips; // 0 : screaming 1:machine sound 2:win 3:menu bgm 4:level bgm 5:button

	private static AudioManager instance;
	public static AudioManager Instance { get { return instance; } }

	private void Awake()
	{
		instance = this;
	}

	public void PlayGameOver()
	{
		secondaryAud.loop = false;
		primaryAud.clip = clips[0];
		secondaryAud.clip = clips[1];
		primaryAud.Play();
		secondaryAud.Play();
	}
	public void PlayWin()
	{
		primaryAud.clip = clips[2];
		primaryAud.Play();
		secondaryAud.Pause();
	}
	public void PlayBGM(int level)
	{
		secondaryAud.loop = true;
		secondaryAud.clip = clips[level];
		secondaryAud.Play();
	}
	public void PlayButton()
	{
		primaryAud.clip = clips[5];
		primaryAud.Play();
		//secondaryAud.Pause();
	}
}
