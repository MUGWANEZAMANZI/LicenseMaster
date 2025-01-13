using UnityEngine;
using UnityEngine.AI;

public class CarNavigation : MonoBehaviour
{
    public GameObject destinationObject; // GameObject to act as the destination
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        // Get the NavMeshAgent component attached to the car
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component is missing from the car GameObject.");
            return;
        }

        if (destinationObject != null)
        {
            SetDestination(destinationObject.transform.position);
        }
        else
        {
            Debug.LogWarning("No destination GameObject has been set for the car.");
        }
    }

    /// <summary>
    /// Sets the destination of the NavMeshAgent to the given position.
    /// </summary>
    /// <param name="targetPosition">The position to navigate to.</param>
    public void SetDestination(Vector3 targetPosition)
    {
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent is not available.");
            return;
        }

        navMeshAgent.SetDestination(targetPosition);
    }

    void Update()
    {
        // Continuously update the destination if the target GameObject moves
        if (destinationObject != null)
        {
            SetDestination(destinationObject.transform.position);
        }

        // Debugging: Check if the car is close to its destination
        if (navMeshAgent.remainingDistance > 0 && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            Debug.Log("Car has reached its destination.");
        }
    }
}
