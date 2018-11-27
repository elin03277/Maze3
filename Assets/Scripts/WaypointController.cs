using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour {

	public List<Transform> waypoints = new List<Transform>();
	private Transform targetWaypoint;
	public int targetWaypointIndex;
	private float minDistance = .1f;
	private int lastWaypointIndex;
	public float movementSpeed;
	public float rotationSpeed = 2.0f;

	// Use this for initialization
	void Start () {
		targetWaypoint = waypoints [targetWaypointIndex];
		lastWaypointIndex = waypoints.Count - 1;
	}
	
	// Update is called once per frame
	void Update () {
		float rotationStep = rotationSpeed * Time.deltaTime;
		float movementStep = movementSpeed * Time.deltaTime;
		float distance = Vector3.Distance (transform.position, targetWaypoint.position);

		Vector3 directionToTarget = targetWaypoint.position - transform.position;
		Quaternion rotationToTarget = Quaternion.LookRotation (directionToTarget);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotationToTarget, rotationStep);

		CheckDistanceToWaypoint (distance);
		transform.position = Vector3.MoveTowards (transform.position, targetWaypoint.position, movementStep);
	}

	void CheckDistanceToWaypoint(float currentDistance){
		if (currentDistance <= minDistance) {
			targetWaypointIndex++;
			UpdateTargetWaypoint();
		}
	}

	void UpdateTargetWaypoint(){
		if (targetWaypointIndex > lastWaypointIndex)
			targetWaypointIndex = 0;
		targetWaypoint = waypoints [targetWaypointIndex];
	}
}
