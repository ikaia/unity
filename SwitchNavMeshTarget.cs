using UnityEngine;
using UnityEngine.AI;


public class SwitchNavMeshTarget : MonoBehaviour
{
    [SerializeField] private NavMeshAgent womanAgent; // Reference to the NavMeshAgent component
    [SerializeField] private Transform defaultTarget; // The default target
    [SerializeField] private Transform newTarget; // The target to switch to

    private Transform currentTarget; // Tracks the current target

    private void Start()
    {
        if (womanAgent != null && defaultTarget != null)
        {
            currentTarget = defaultTarget;
            womanAgent.SetDestination(currentTarget.position);
        }
    }

    private void Update()
    {
        // Update the destination regularly
        if (currentTarget != null && womanAgent != null)
        {
            womanAgent.SetDestination(currentTarget.position);
        }
    }

    public void SwitchTarget()
    {
        // Toggle between the targets
        if (currentTarget == defaultTarget)
        {
            currentTarget = newTarget;
        }
        else
        {
            currentTarget = defaultTarget;
        }
    }
}
/*using UnityEngine;
using UnityEngine.AI;

public class SwitchNavMeshTarget : MonoBehaviour
{
    [SerializeField] private NavMeshAgent womanAgent; // Reference to the NavMeshAgent component
    [SerializeField] private Transform defaultTarget; // The default target
    [SerializeField] private Transform newTarget; // The target to switch to

    private Transform currentTarget; // Tracks the current target

    private void Start()
    {
        if (womanAgent != null && defaultTarget != null)
        {
            currentTarget = defaultTarget;
            womanAgent.SetDestination(currentTarget.position);
        }
    }

    private void Update()
    {
        // Update the destination regularly
        if (currentTarget != null && womanAgent != null)
        {
            womanAgent.SetDestination(currentTarget.position);
        }
    }

    public void SwitchTarget()
    {
        // Toggle between the targets
        if (currentTarget == defaultTarget)
        {
            currentTarget = newTarget;
        }
        else
        {
            currentTarget = defaultTarget;
        }
    }
}*/
