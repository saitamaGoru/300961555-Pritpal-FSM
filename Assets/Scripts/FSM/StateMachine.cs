using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{

    private IState _currentState;
    private Dictionary<Type, IState>_states = new Dictionary<Type, IState>();

    private List<(Icondition condition, Type nextState)> _transitions = new List<(Icondition condition, Type nextState)>();
    void Start()
    {
        
    }

    // Update is called once per frame

    public void AddState(IState state)
    {
        if(!_states.ContainsKey(state.GetType())) _states.Add(state.GetType(), state);
    }

    public void SetState<T>() where T : IState{

         if (_states.ContainsKey(typeof(T))) // Check if the state exists
    {
        if (_currentState != null)
        {
            _currentState.OnExit();
        }

        _currentState = _states[typeof(T)];
        _currentState.OnEnter();
    }
    else
    {
        Debug.LogError($"State {typeof(T)} not found in the state machine.");
    }
        
       

    }

    public void AddTransition<T>(Icondition condition) where T :IState
    {
        _transitions.Add((condition, typeof(T)));
    }

    public void Update()
{
    if (_currentState == null)
    {
        Debug.LogWarning("No current state set!");
        return;
    }

    // Evaluate transitions
    foreach (var (condition, nextState) in _transitions)
    {
        Debug.Log($"Evaluating condition for transition to: {nextState}");
        if (condition.Evaluate())
        {
            Debug.Log($"Condition met, transitioning to: {nextState.Name}");
            SetState(nextState);
            break;
        }
    }

    // Update the current state after checking transitions
    _currentState?.OnUpdate();
}
     private void FixedUpdate()
    {
        _currentState?.OnFixedUpdate();
    }


    private void SetState(Type stateType)
{
    if (_currentState.GetType() != stateType)
    {
        Debug.Log($"Transitioning from {_currentState.GetType().Name} to {stateType.Name}");

        _currentState.OnExit();
        _currentState = _states[stateType];
        _currentState.OnEnter();

        Debug.Log($"New state after transition: {_currentState.GetType().Name}");
    }
    else
    {
        Debug.Log($"Already in state {stateType.Name}, no transition needed.");
    }
}
    
}
