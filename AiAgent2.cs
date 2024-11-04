using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent2 : MonoBehaviour
{
    public Transform[] waypoints;       // The waypoints the AI agent will move between
    public Transform userAvatar;        // Reference to the user's avatar (player)
    public float detectionRange = 10f;  // How close the avatar needs to be to start moving to the waypoint

    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;
    private bool userIsNear = false;    // Tracks if the user is near the AI agent

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StopAgent();  // Ensure the agent starts completely stopped
        Debug.Log("AI Agent initialized and stopped.");
    }

    void Update()
    {
        if (waypoints.Length == 0 || userAvatar == null)
        {
            Debug.Log("No waypoints or user avatar assigned.");
            return;
        }

        // Check if the user's avatar is within range of the AI agent
        userIsNear = Vector3.Distance(transform.position, userAvatar.position) <= detectionRange;

        // Log proximity check result
        Debug.Log("User is near: " + userIsNear);

        if (userIsNear)
        {
            // If user is near, enable movement and set a new destination
            if (agent.isStopped)
            {
                StartAgent();
                Debug.Log("Agent started moving.");
            }
            MoveToWaypoint();
        }
        else
        {
            // If user is far, stop the agent and clear its path
            if (!agent.isStopped)
            {
                StopAgent();
                Debug.Log("Agent stopped moving.");
            }
        }
    }

    void MoveToWaypoint()
    {
        // Set the destination to the current waypoint
        if (agent.isOnNavMesh && agent.isStopped == false)
        {
            Debug.Log("Agent is on NavMesh and not stopped.");
            
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                agent.SetDestination(waypoints[currentWaypointIndex].position);
                Debug.Log("Moving to waypoint: " + currentWaypointIndex);
            }
        }
    }

    void StopAgent()
    {
        // Stop the agent and reset the path
        agent.isStopped = true;
        agent.ResetPath();          // This ensures the agent isn't moving or calculating a new path
        agent.velocity = Vector3.zero;  // Completely stop the agent
        Debug.Log("Agent has fully stopped and reset its path.");
    }

    void StartAgent()
    {
        // Allow the agent to move again
        agent.isStopped = false;
        Debug.Log("Agent allowed to move again.");
    }
}
