using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendState : State<AI>
{
    #region State Instance
    private static DefendState instance; //Static instance of the state

    private DefendState() //Constructor for the state
    {
        if (instance != null) //If we already have an instance of this state, we don't need another one
            return;
        instance = this;
    }

    public static DefendState Instance //Public acsessor of the state, which will return the instance
    {
        get
        {
            if (instance == null)
                new DefendState();  //Constructs the state if we don't yet have an instance
            return instance;
        }
    }
    #endregion
    public override bool EnterState(AI agent)
    {
        Debug.Log("ENTERING DEFEND STATE");

        return true;
    }
    public override void ExecuteState(AI agent)
    {
        Debug.Log("EXECUTIND DEFEND STATE");
        if (agent.data.HasEnemyFlag || agent.data.HasFriendlyFlag)
        {
            agent.StateMachine.ChangeState(GoToFriendlyBaseState.Instance);
        }
        else
            agent.actions.MoveTo(agent.data.EnemyBase);
    }
    public override bool ExitState(AI agent)
    {
        Debug.Log("EXITING DEFEND STATE");
        return true;
    }
}
