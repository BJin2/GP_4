using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
	//Object to follow
	[SerializeField] private Transform follow;

	private float speed_rotate = 0.0f;
	private float speed_move = 0.0f;
	private float angleLeft = 0.0f;
	private float rotDir = 0.0f;

	private void Awake()
	{
		speed_rotate = 90.0f;
	}

	private void Update()
	{
		//Following
		/*/
		Vector3 dir = follow.position - transform.position;

		if (dir.magnitude >= 0.05f)
			speed_move = Mathf.Log(dir.magnitude + 1);
		else
		{
			//transform.position = follow.position;
			speed_move = 0.0f;
		}
		dir.Normalize();
		dir = dir * Time.deltaTime * speed_move;
		transform.position = transform.position + dir;
		/*/
		transform.position = follow.position;
		//*/
		//Rotate
		if (angleLeft != 0.0f)
		{
			speed_rotate = Mathf.Log(angleLeft + 2) * Time.deltaTime * 70;
			angleLeft -= speed_rotate;
			transform.Rotate(new Vector3(0, speed_rotate * rotDir, 0));

			if (angleLeft <= 0.0f)
			{
				angleLeft = 0.0f;
				rotDir = 0.0f;
			}
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				rotDir = -1.0f;
				angleLeft = 90.0f;
			}
			else if (Input.GetKeyDown(KeyCode.E))
			{
				rotDir = 1.0f;
				angleLeft = 90.0f;
			}
		}
	}
}
