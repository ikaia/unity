using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAgent : MonoBehaviour
{
	UnityEngine.AI.NavMeshAgent agent;
	public Transform[] waypoints;
	int waypointIndex;
	Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
       if (Vector3.Distance(transform.position, target) < 1)
	   {
		   IterateWaypointIndex();
		   UpdateDestination();
	   }
    }
	void UpdateDestination()
	{
		target = waypoints[waypointIndex].position;
		agent.SetDestination(target);
	}
	void IterateWaypointIndex()
	{
	waypointIndex++;
	if (waypointIndex == waypoints.Length)
	{
		waypointIndex = 0;
	}
	}
}