using UnityEngine;
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
    }

    // Function to stop following
    public void StopFollowing()
    {
        isFollowing = false;
        woman.ResetPath(); // Stop any movement
    }
}
