using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
	[SerializeField]
	private GameObject ceiling = null;

	private void Start()
	{
		ceiling.SetActive(true);
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			ceiling.SetActive(false);
		}
	}
	private void OnTriggerExit(Collider col)
	{
		if (col.tag == "Player")
		{
			ceiling.SetActive(true);
		}
	}
}
