using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			if (col.GetComponent<Player>().hasKey)
			{
				HUD.Instance.Win();
			}
		}
	}
}
