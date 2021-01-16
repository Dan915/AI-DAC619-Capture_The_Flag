using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealState : State<AI>
{
    private static HealState _instance; //Static instance of the state

    public HealState() //Constructor for the state
    {
        if (_instance != null) //If we already have an instance of this state, we don't need another one
            return;
        _instance = this;
        
    }

    public static HealState Instance //Public acsessor of the state, which will return the instance
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
        return base.EnterState(agent);
    }

    public override void ExecuteState(AI agent)
    {
        throw new System.NotImplementedException();
    }

    public override bool ExitState(AI agent)
    {
        return base.ExitState(agent);
    }

}
