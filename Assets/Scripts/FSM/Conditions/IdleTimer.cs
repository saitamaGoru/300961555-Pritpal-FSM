using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class IdleTimer : Icondition
{
   
   private IdleState _idleState;

    public IdleTimer(IdleState idls)
    {
        
        _idleState = idls;
    }

       public bool Evaluate()
    { 
        Debug.Log("Evaluating resting time");
        return _idleState.IsRestComplete();
    }

  
}
