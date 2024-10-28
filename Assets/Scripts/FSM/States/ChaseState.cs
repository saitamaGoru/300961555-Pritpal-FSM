using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IState
{
    // Start is called before the first frame update
   public readonly NavMeshAgent _enemy;
   public readonly GameObject _player;

    public bool chase;
   public ChaseState(NavMeshAgent ag, GameObject pl)
   {
        _enemy = ag;
        _player = pl;
   }


    public void OnEnter()
    {
         _enemy.speed = 8f;
         chase = true;
        Debug.Log("Entering Chase state.........");
    }
   public void OnExit()
   {
        Debug.Log("Exiting Chase state.........");
        chase = false;
   }    
   public void OnUpdate()
   {
        _enemy.SetDestination(_player.transform.position - (_player.transform.position - _enemy.transform.position).normalized * 3f);

   }
   public void OnFixedUpdate(){}
}
