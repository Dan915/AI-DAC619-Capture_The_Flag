using System.Collections;


public abstract class State<AI>
{
    public virtual bool EnterState(AI agent)
    {
        return true;
    }
    public abstract void ExecuteState(AI agent);
    public virtual bool ExitState(AI agent)
    {
        return true;
    }
    
}

