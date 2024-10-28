using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolCompletion : Icondition
{
    
    private PatrolState _ps;

    public PatrolCompletion(PatrolState patrolState)
    {
        _ps = patrolState;
    }
    public bool Evaluate()
    {
         bool isComplete = _ps.IsPatrolComplete();
        if (isComplete)
        {
            Debug.Log("Patrol cycle completed. Ready to transition to Idle State.");
        }
        else
        {
            Debug.Log("Patrol still in progress.");
        }
        return isComplete;
    }
    
}
