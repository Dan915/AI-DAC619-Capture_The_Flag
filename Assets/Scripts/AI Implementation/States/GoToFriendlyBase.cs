using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToFriendlyBaseState : State<AI>
{
    #region State Instance
    private static GoToFriendlyBaseState instance; //Static instance of the state

    private GoToFriendlyBaseState() //Constructor for the state
    {
        if (instance != null) //If we already have an instance of this state, we don't need another one
            return;
        instance = this;
    }

    public static GoToFriendlyBaseState Instance //Public acsessor of the state, which will return the instance
    {
        get
        {
            if (instance == null)
                new GoToFriendlyBaseState();  //Constructs the state if we don't yet have an instance
            return instance;
        }
    }
    #endregion
    public override bool EnterState(AI agent)
    {
        Debug.Log("ENTERING GO TO FRIENDLY BASE STATE");

        return true;
    }
    public override void ExecuteState(AI agent)
    {
        Debug.Log("EXECUTIND GO TO FRIENDLY BASE STATE");
        
    }
    public override bool ExitState(AI agent)
    {
        Debug.Log("EXITING GO TO FRIENDLY BASE STATE");
        return true;
    }
}
