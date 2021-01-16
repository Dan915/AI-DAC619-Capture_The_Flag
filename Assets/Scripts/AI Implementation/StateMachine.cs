using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class StateMachine<AI>
{

    State<AI> _startingState;
    State<AI> _currentState;
    State<AI> _previousState;
    public AI stateOwner; //The AI that is executing the states
        

        public StateMachine(AI agent) //Constructor to initilise the state, setting up the owner and ensuring it isn't already in a state
        {
            stateOwner = agent; //Sets owner of the state machine
            _currentState = null; //Sets the state to null at the start, so that it won't have any issues
        }

        public void ChangeState(State<AI> nextState) //Exits the current state and enters the new one
        {
            if (_currentState != null) //Check that we are already in a state before attempting to exit it
                _currentState.ExitState(stateOwner); //Exits the current state
            _previousState = _currentState; //Sets the last state
            _currentState = nextState; //Sets the current state to the new state
            _currentState.EnterState(stateOwner); //Enters the new state
        }

        public void UpdateState() //Update ticks the current state
        {
            if (_currentState != null) //If the state is not null, we can update it
                _currentState.ExecuteState(stateOwner); //Update the state with a reference to the AI agent that is currently in that state

        }
    
    
}
