using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{

    private bool restComplete;
   private float timer;
   public float timerLimit  = 4f;
    public IdleState()
    {
      
      timer = 0;

    }
     public void OnEnter()
     {
        Debug.Log("Entering Idle State..");
        restComplete = false;
     }
   public void OnExit()
   {
        Debug.Log("Exiting idle State..");
        restComplete = false;
   }
   public void OnUpdate()
   {
      Debug.Log("Entering IdleState Update");
      timer += Time.deltaTime;
      Debug.Log($"rest:{restComplete}");
      if(timer > timerLimit)
      {
         restComplete = true;
         timer = 0;
      } 
      
   }

   public bool IsRestComplete()
   {
      return restComplete;
   }

   public void OnFixedUpdate()
   {
        
   }
}
