using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour {

	public bool isRewinding;

	List<PointInTime> pointsInTime;

	Rigidbody rb;
	// Use this for initialization
	void Start () {
		pointsInTime = new List<PointInTime>();
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Return))
			StartRewind();
		if (Input.GetKeyUp(KeyCode.Return))
			StopRewind();


	}

	private void FixedUpdate()
	{
		if (isRewinding)
			Rewind();
		else Record();
	}

	void Rewind()
	{
		if (pointsInTime.Count > 0)
		{
			PointInTime PIT = pointsInTime[0];
			transform.position = PIT.position;
			transform.rotation = PIT.rotation;
			pointsInTime.RemoveAt(0);
		}
		else StopRewind();
	}

	void Record()
	{
		pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
	}

	void StartRewind()
	{
		isRewinding = true;
		rb.isKinematic = true ;
	}

	void StopRewind()
	{
		isRewinding = false;
		rb.isKinematic = false;
	}
}
