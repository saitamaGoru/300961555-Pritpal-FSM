using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
   
   public NavMeshAgent agent, bombAgent;
    private StateMachine _stateMachine;

    [SerializeField] GameObject[] points;  
    [SerializeField] GameObject player;
    
   
    [SerializeField] Transform bulletSpawn;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bombPrefab;


    private IdleTimer idleTimerCondition;  
    private PatrolCompletion patrolCompletionCondition;
    private RangeBase rangeBase;  
    private BackToPatrol bkt;

    private AttackRange attackRange;
    private void Start()
    {
         player = GameObject.FindWithTag("Player");
        _stateMachine = new StateMachine();
        
        // Create the states
        var idleState = new IdleState();
        var patrolState = new PatrolState(points, agent);
        var chaseState = new ChaseState(agent,player );
        var attackState = new AttackState(bulletPrefab, bulletSpawn,player.transform, agent);
        var bombState = new BombExplodeState(bombAgent, player);
        // Add states to the state machine
        _stateMachine.AddState(idleState);
        _stateMachine.AddState(patrolState);
        _stateMachine.AddState(chaseState);
        _stateMachine.AddState(attackState);

        // Initialize the conditions
        idleTimerCondition = new IdleTimer(idleState);  // Wait for idleTime to transition to patrol
        patrolCompletionCondition = new PatrolCompletion(patrolState);
        rangeBase = new RangeBase(chaseState, agent, player, 15f);  // Patrol complete to return to Idle
        bkt = new BackToPatrol(chaseState, agent, player, 15f);
        attackRange = new AttackRange(attackState, agent, player, 9f);
        // Set transitions: Idle -> Patrol, Patrol -> Idle
         // Go to patrol after idle time
       
        _stateMachine.AddTransition<IdleState>( patrolCompletionCondition);
        _stateMachine.AddTransition<ChaseState>(rangeBase);
        _stateMachine.AddTransition<PatrolState>(bkt);
        _stateMachine.AddTransition<PatrolState>(idleTimerCondition);
        _stateMachine.AddTransition<AttackState>(attackRange);
       

        // Start with IdleState
        _stateMachine.SetState<IdleState>();
    }

    private void Update()
    {
        // Update the state machine each frame
        _stateMachine.Update();
       
    }

  
}
