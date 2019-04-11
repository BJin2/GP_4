using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
	private void Awake()
	{
		Display();
	}
	public void DeleteData()
	{
		PlayerPrefs.DeleteAll();
		Display();
	}

	public void Display()
	{
		for (int i = 0; i < 3; i++)
		{
			RectTransform temp = transform.Find(i.ToString()).GetComponent<RectTransform>();
			for (int j = 0; j < 3; j++)
			{
				Text tempText = temp.Find(j.ToString()).GetComponent<Text>();
				int tempInex = PlayerPrefs.GetInt(i.ToString()+"_"+j.ToString(), 0);
				tempText.text = HUD.alphabet[tempInex];
			}
			Text min = temp.Find("min").GetComponent<Text>();
			Text sec = temp.Find("sec").GetComponent<Text>();
			Text dec = temp.Find("dec").GetComponent<Text>();
			float time = PlayerPrefs.GetFloat("high_" + i.ToString(), -1);
			time = time > 0 ? time : 0;
			HUD.UpdateText(time, min, sec, dec);
		}
	}
}
