using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField]
	private AudioSource primeryAud;
	[SerializeField]
	private AudioSource secondaryAud;

	[SerializeField]
	private AudioClip[] clips; // 0 : screaming 1:machine sound 2:win 3:menu bgm 4:level bgm

	private static AudioManager instance;
	public static AudioManager Instance { get { return instance; } }

	private void Awake()
	{
		instance = this;
	}

	public void PlayGameOver()
	{
		primeryAud.clip = clips[0];
		secondaryAud.clip = clips[1];
		primeryAud.Play();
		secondaryAud.Play();
	}
	public void PlayWin()
	{
		
	}
	public void PlayBGM(int level)
	{

	}
}
