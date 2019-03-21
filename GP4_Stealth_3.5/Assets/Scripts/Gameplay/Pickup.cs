using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			Player player = col.GetComponent<Player>();
			if (!player.hasKey)
			{
				player.hasKey = true;
				Destroy(gameObject);
			}
		}
	}
}
