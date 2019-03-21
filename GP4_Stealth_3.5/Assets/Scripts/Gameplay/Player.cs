using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[HideInInspector]
	public bool hasKey = false;

	private Animator anim = null;
	private float speed_move = 0.0f;
	private float speed_rotate = 0.0f;

	private void Awake()
	{
		anim = gameObject.GetComponentInChildren<Animator>();
		speed_move = 0.5f;
		speed_rotate = 110.0f;
	}
	private void Update ()
	{
		if (Time.timeScale > 0)
		{
			if (Input.GetKey(KeyCode.W))
			{
				anim.Play("Walking");
				transform.Translate(Vector3.forward * speed_move * Time.deltaTime);
			}
			else if (Input.GetKey(KeyCode.S))
			{
				anim.Play("Walking Backward");
				transform.Translate(Vector3.back * (speed_move / 2.0f) * Time.deltaTime);
			}
			else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
			{
				anim.Play("Idle");
			}

			if (Input.GetKey(KeyCode.A))
			{
				transform.Rotate(new Vector3(0, -1 * speed_rotate * Time.deltaTime, 0));
			}
			else if (Input.GetKey(KeyCode.LeftArrow))
			{
				transform.Rotate(new Vector3(0, -1 * speed_rotate * Time.deltaTime, 0));
			}
			else if (Input.GetKey(KeyCode.D))
			{
				transform.Rotate(new Vector3(0, speed_rotate * Time.deltaTime, 0));
			}
			else if (Input.GetKey(KeyCode.RightArrow))
			{
				transform.Rotate(new Vector3(0, speed_rotate * Time.deltaTime, 0));
			}
		}
	}
}
