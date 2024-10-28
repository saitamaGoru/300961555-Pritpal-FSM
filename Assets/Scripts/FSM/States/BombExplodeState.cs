using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BombExplodeState : IState
{

    public readonly NavMeshAgent _bombAgent;

    
    public readonly GameObject _player;

    private float timeToExplode = 2f;
    float timer;
    public BombExplodeState(NavMeshAgent ba, GameObject pl)
    {
        _bombAgent = ba;
        _player = pl;
        timer = 0;
    }
     public void OnEnter()
     {
        Debug.Log("Bomb Incoming.....");
        _bombAgent.speed = 10f;
     }
   public void OnExit()
   {
        Debug.Log(" Exiting the state...");

   }
   public void OnUpdate()
   {
        Vector3 destination = _player.transform.position;

        _bombAgent.SetDestination(destination);

        if(Vector3.Distance(destination, _bombAgent.transform.position) < 2f)
        {
            _bombAgent.speed = 0f;
             timer += Time.deltaTime;

            _bombAgent.gameObject.SetActive(timer <= timeToExplode);

        }
   }
   public void OnFixedUpdate()
   {

   }

}
