/*using UnityEngine;
using UnityEngine.AI;

public class WomanFollow : MonoBehaviour
{
    public NavMeshAgent woman;
    public Transform player;
    public bool isFollowing = false;

    private float followDistance = 1.5f; // Desired distance to maintain from player

    void Update()
    {
        if (isFollowing)
        {
            // Calculate the direction from the woman to the player
            Vector3 directionToPlayer = player.position - woman.transform.position;
            directionToPlayer.y = 0; // Keep the woman on the same height level as the player

            // If the woman is farther than followDistance from the player, follow
            if (directionToPlayer.magnitude > followDistance)
            {
                // Normalize direction and calculate new destination point at the desired distance
                Vector3 targetPosition = player.position - directionToPlayer.normalized * followDistance;
                
                // Set the NavMeshAgent destination to the target position
                woman.SetDestination(targetPosition);
            }
            else
            {
                // If close enough, stop moving
                woman.ResetPath();
            }
        }
    }

    // Function to enable following
    public void StartFollowing()
    {
   
    isFollowing = true;
    woman.speed = 2.0f; // Set a slower speed (default is typically 3.5)
    woman.acceleration = 2.0f; // Reduce acceleration for smoother, slower movement
}

    // Function to stop following
    public void StopFollowing()
    {
        isFollowing = false;
        woman.ResetPath(); // Stop any movement
    }
}*/

using UnityEngine;
using UnityEngine.AI;

public class WomanFollow : MonoBehaviour
{
    public NavMeshAgent woman;
    public Transform currentTarget;  // The target the woman is following
    public Transform newTarget;      // New target for when the "Exit" button is pressed
    public bool isFollowing = false;

    private float followDistance = 1.5f; // Desired distance to maintain from player

    void Start()
    {
        if (woman == null)
        {
            woman = GetComponent<NavMeshAgent>(); // Ensure the NavMeshAgent is attached
        }

        if (currentTarget == null)
        {
            currentTarget = newTarget; // Default to the newTarget as the first target (if available)
        }
    }

    void Update()
    {
        if (isFollowing && currentTarget != null)
        {
            // Calculate the direction from the woman to the current target
            Vector3 directionToTarget = currentTarget.position - woman.transform.position;
            directionToTarget.y = 0; // Keep the woman on the same height level as the target

            // If the woman is farther than followDistance from the target, follow
            if (directionToTarget.magnitude > followDistance)
            {
                // Normalize direction and calculate new destination point at the desired distance
                Vector3 targetPosition = currentTarget.position - directionToTarget.normalized * followDistance;
                
                // Set the NavMeshAgent destination to the target position
                woman.SetDestination(targetPosition);
            }
            else
            {
                // If close enough, stop moving
                woman.ResetPath();
            }
        }
    }

    // Function to enable following
    public void StartFollowing()
    {
        isFollowing = true;
        woman.speed = 1.5f; // Set a slower speed (default is typically 3.5)
        woman.acceleration = 1.5f; // Reduce acceleration for smoother, slower movement
    }

    // Function to stop following
    public void StopFollowing()
    {
        isFollowing = false;
        woman.ResetPath(); // Stop any movement
    }

    // Function to switch the currentTarget to newTarget
    public void SwitchTarget()
    {
        // If newTarget is assigned, switch the currentTarget to the newTarget
        if (newTarget != null)
        {
            currentTarget = newTarget;
            Debug.Log("Switched target to: " + currentTarget.name);
        }
        else
        {
            Debug.LogError("newTarget is not assigned!");
        }
    }
}
