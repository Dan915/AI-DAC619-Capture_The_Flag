using System.Collections;
using UnityEngine;


public abstract class State<AI>
{
    public virtual bool EnterState(AI agent)
    {
        //Debug.Log("Entering new state");
        return true;
    }
    public abstract void ExecuteState(AI agent);
    public virtual bool ExitState(AI agent)
    {
       // Debug.Log("Exiting current state");
        return true;
    }
    
}

