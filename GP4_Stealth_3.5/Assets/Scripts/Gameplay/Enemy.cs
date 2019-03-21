﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private enum State
	{
		Looking,// Idle animation
		Turning,// Turning animation
		Chasing,// Running animation
		Retreating// Walking animation
	}
	private State currentState = State.Looking;

	private Animator anim = null;

	private Transform arrow = null;
	private Transform leftPivot = null;
	private Transform rightPivot = null;
	private Transform centerPivot = null;
	private Transform rayGoal = null;

	private Transform target = null;
	[SerializeField]
	private Vector3 originalPosition;

	private float speed = 1.0f;

	private float turnTimer = 0.0f;
	private const float TURN_TIMER = 10.0f;

	private float rotateDir = 1.0f;
	private int turnCount = 0;
	private float rotateSpeed;
	private float turningTimer;

	private const int LAYER_DEFAULT = 1 << 0;
	private const int LAYER_MAP = 1 << 9;

	private void Awake()
	{
		anim = gameObject.GetComponent<Animator>();
		arrow = transform.Find("Arrow");
		leftPivot = arrow.Find("LeftPivot");
		rightPivot = arrow.Find("RightPivot");
		centerPivot = arrow.Find("CenterPivot");
		rayGoal = arrow.Find("RayRange");
		originalPosition = transform.position;
	}
	private void Update()
	{
		Detect();
		switch ((int)currentState)
		{
			#region Looking
			case (int)State.Looking:
				turnTimer += Time.deltaTime;
				if (turnTimer >= TURN_TIMER)
				{
					if (turnCount < 4)
					{
						if (rotateDir < 0)
						{
							anim.SetTrigger("Left");
						}
						else
						{
							anim.SetTrigger("Right");
						}
						currentState = State.Turning;
						turnTimer = 0;
						turnCount++;
					}
					else
					{
						turnCount = 0;
						rotateDir *= -1;
					}
				}
				break;
			#endregion
			#region Turning
			case (int)State.Turning:
				if (anim.GetCurrentAnimatorStateInfo(0).IsName("Right") ||
					anim.GetCurrentAnimatorStateInfo(0).IsName("Left"))
				{
					rotateSpeed = 1.0f / anim.GetCurrentAnimatorStateInfo(0).length;
					turningTimer = anim.GetCurrentAnimatorStateInfo(0).length;
					arrow.Rotate(0, rotateDir * 90 * rotateSpeed * Time.deltaTime, 0);

					turnTimer += Time.deltaTime;
					if (turnTimer >= turningTimer)
					{
						turnTimer = 0.0f;
						anim.Play("Idle_0");
					}
				}
				else
				{
					arrow.Rotate(0, -rotateDir * 90, 0);
					transform.Rotate(0,rotateDir * 90, 0);
					currentState = State.Looking;
				}
				break;
			#endregion Turning
			case (int)State.Chasing:
				speed = 1.0f;
				// move to player
				transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
				transform.Translate(Vector3.forward * speed * Time.deltaTime);
				break;
			case (int)State.Retreating:
				speed = 0.3f;
				transform.LookAt(originalPosition);
				transform.Translate(Vector3.forward * speed * Time.deltaTime);
				if (Vector3.Distance(originalPosition, transform.position) < 0.1f)
				{
					anim.SetTrigger("BackToIdle");
					currentState = State.Looking;
					target = null;
				}
				//timer or position -> back to looking
				break;
		}
	}

	private void Detect()
	{
		RaycastHit hit_left = new RaycastHit();
		RaycastHit hit_right = new RaycastHit();
		RaycastHit hit_center = new RaycastHit();
		if (DetectPlayer(leftPivot.position, rayGoal.position, ref hit_left, Color.red) ||
			DetectPlayer(rightPivot.position, rayGoal.position, ref hit_right, Color.blue) ||
			DetectPlayer(centerPivot.position, rayGoal.position, ref hit_center, Color.green))
		{
			currentState = State.Chasing;
			anim.SetTrigger("Chasing");

			transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
			arrow.LookAt(new Vector3(target.position.x, arrow.position.y, target.position.z));
		}
		else if (currentState == State.Chasing || 
			anim.GetCurrentAnimatorStateInfo(0).IsName("Running"))
		{
			target = null;
			transform.LookAt(originalPosition);
			arrow.LookAt(new Vector3(originalPosition.x, arrow.position.y, originalPosition.z));
			currentState = State.Retreating;
			anim.SetTrigger("Retreating");
		}
	}
	private bool DetectPlayer(Vector3 origin, Vector3 goal, ref RaycastHit hit, Color debugColor)
	{
		Vector3 dir = goal - origin;
		Ray ray = new Ray(origin, dir);
		Debug.DrawRay(origin, dir, debugColor);

		if (Physics.Raycast(ray, out hit, dir.magnitude, LAYER_DEFAULT | LAYER_MAP))
		{
			if (hit.collider.tag == "Player")
			{
				target = hit.collider.transform;
				return true;
			}
			else
				return false;
		}
		return false;
	}

	private void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player")
		{
			Debug.Log("Game Over");
			Time.timeScale = 0.0f;
		}
	}
}
