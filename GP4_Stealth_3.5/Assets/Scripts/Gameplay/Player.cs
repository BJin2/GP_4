using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[HideInInspector]
	public bool hasKey = false;

	private Animator anim = null;

	private AudioSource aud = null;

	private float speed_move = 0.0f;
	private float speed_rotate = 0.0f;

	private float timer_audio = 0.6f;
	private float interval_audio = 0.6f;
	private float interval_multiplier = 1.0f;

	private void Awake()
	{
		anim = gameObject.GetComponentInChildren<Animator>();
		aud = gameObject.GetComponent<AudioSource>();
		speed_move = 0.5f;
		speed_rotate = 110.0f;
	}
	private void Update ()
	{
		if (Time.timeScale > 0)
		{
			timer_audio = Mathf.Clamp(timer_audio + Time.deltaTime, 0, interval_audio*interval_multiplier);
			if (Input.GetKey(KeyCode.W))
			{
				if (timer_audio >= interval_audio * interval_multiplier)
				{
					aud.Play();
					timer_audio = 0.0f;
				}
				interval_multiplier = 1.0f;
				anim.Play("Walking");
				transform.Translate(Vector3.forward * speed_move * Time.deltaTime);
			}
			else if (Input.GetKey(KeyCode.S))
			{
				if (timer_audio >= interval_audio * interval_multiplier)
				{
					aud.Play();
					timer_audio = 0.0f;
				}
				interval_multiplier = 1.7f;
				anim.Play("Walking Backward");
				transform.Translate(Vector3.back * (speed_move / 2.0f) * Time.deltaTime);
			}
			else
			{
				aud.Pause();
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
