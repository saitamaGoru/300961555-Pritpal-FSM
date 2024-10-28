
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IState
{
    private GameObject[] patrolPoints;
    private int index;
    private NavMeshAgent navMeshAgent;
    private bool patrolComplete;

    public PatrolState(GameObject[] points, NavMeshAgent agent)
    {
        patrolPoints = points;
        navMeshAgent = agent;
        index = 0;
        patrolComplete = false;
    }

    public bool IsPatrolComplete()
    {
        return patrolComplete;
    }

    public void OnEnter()
    {
        Debug.Log("Entering Patrol State...");
        navMeshAgent.speed = 6.5f;
        patrolComplete = false;
    }

    public void OnExit()
    {
        Debug.Log("Exiting Patrol State...");
        patrolComplete = false;
    }

    public void OnUpdate()
    {
        Vector3 destination = patrolPoints[index].transform.position;
        navMeshAgent.SetDestination(destination);

        if (Vector3.Distance(navMeshAgent.transform.position, destination) < 2.5f)
        {
            index = (index + 1) % patrolPoints.Length;

            if (index == 1)
            {
                patrolComplete = true;
               Debug.Log("Patrol cycle complete. Ready to transition to Idle.");  // Mark patrol complete after one cycle
            }
        }
    }

    public void OnFixedUpdate() { }
}
