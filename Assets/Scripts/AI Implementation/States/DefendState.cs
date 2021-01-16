using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendState : State<AI>
{
    private static DefendState _instance; //Static instance of the state

    public DefendState() //Constructor for the state
    {
        if (_instance != null) //If we already have an instance of this state, we don't need another one
            return;
        _instance = this;
        
    }

    public static DefendState Instance //Public acsessor of the state, which will return the instance
    {
        get
        {
            if (_instance == null)
                new DefendState();  //Constructs the state if we don't yet have an instance
            return _instance;
        }
    }
    public override bool EnterState(AI agent)
    {
        Debug.Log("<color=green>Parking the bus </color>");
        return base.EnterState(agent);
    }
    public override void ExecuteState(AI agent)
    {
        // Debug.Log("EXECUTIND DEFEND STATE");
        // if (agent.data.HasEnemyFlag || agent.data.HasFriendlyFlag)
        // {
        //     agent.StateMachine.ChangeState(GoToFriendlyBaseState.Instance);
        // }
        // else
        //     agent.actions.MoveTo(agent.data.EnemyBase);

        if (agent.data.FriendlyBase.GetComponent<SetScore>()._friendlyFlagInBase)
        {
            if (agent.data.CurrentHitPoints < 70)
                agent.StateMachine.ChangeState(HealState.Instance);
            else if (agent.senses.GetEnemiesInView().Count > 0)
                agent.StateMachine.ChangeState(ChaseEnemyState.Instance);
            else if (agent.senses.GetFriendliesInView().Count > 0)
                agent.StateMachine.ChangeState(GoToEnemyBaseState.Instance);
            else
                agent.actions.MoveToRandomLocation();
        }
        else 
            {
                if (agent.data.CurrentHitPoints < 70)
                    agent.StateMachine.ChangeState(HealState.Instance);
                else if (agent.senses.GetEnemiesInView().Count > 0)
                    agent.StateMachine.ChangeState(ChaseEnemyState.Instance);
                else
                    agent.StateMachine.ChangeState(GoToEnemyBaseState.Instance);
            }
       
    }
    public override bool ExitState(AI agent)
    {
        return base.ExitState(agent);
    }
}
