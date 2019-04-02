using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundYAxis : MonoBehaviour
{
	public float speed = 30;
	
	void Update ()
	{
		transform.Rotate(Vector3.up * Time.deltaTime * speed, Space.World);
	}
}
