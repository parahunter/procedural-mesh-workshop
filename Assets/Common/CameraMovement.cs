using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public float speed = 10;
	
	void Update ()
	{
		transform.Translate(Input.GetAxis("Horizontal") * speed * Vector3.right);
	}
}
