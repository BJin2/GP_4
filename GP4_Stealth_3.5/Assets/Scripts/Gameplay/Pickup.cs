using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
	private AudioSource aud = null;
	private void Awake()
	{
		aud = gameObject.GetComponent<AudioSource>();
	}
	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			Player player = col.GetComponent<Player>();
			if (!player.hasKey)
			{
				player.hasKey = true;
				HUD.Instance.Pickup();
				gameObject.GetComponent<MeshRenderer>().enabled = false;
				gameObject.GetComponent<BoxCollider>().enabled = false;
				aud.Play();
				Destroy(gameObject,1);
			}
		}
	}
}
