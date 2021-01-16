using UnityEngine;

public class AttackState : State<AI>
{
    GameObject _enemy;
    private static AttackState _instance; //Static instance of the state
    private AttackState() //Constructor for the state
    {
        if (_instance != null) //If we already have an instance of this state, we don't need another one
            return;
        _instance = this;
    }

    public static AttackState Instance //Public acsessor of the state, which will return the instance
    {
        get
        {
            if (_instance == null)
                new AttackState();  //Constructs the state if we don't yet have an instance
            return _instance;
        }
    }

    public override bool EnterState(AI agent)
    {
        Debug.Log("<color=red> Kill that motherfucker </color>");
        return base.EnterState(agent);
    }
    public override void ExecuteState(AI agent)
    {
        if (agent.inventory.HasItem("Power Up"))
            agent.actions.UseItem(agent.inventory.GetItem("Power Up"));
        else if (agent.senses.GetEnemiesInView().Count > 0)
        {
            _enemy = agent.senses.GetEnemiesInView()[0];
            if (agent.senses.IsInAttackRange(_enemy))
                agent.actions.AttackEnemy(_enemy);
            else
                agent.actions.MoveTo(_enemy);
        }
        else
        agent.StateMachine.ChangeState(GoToEnemyBaseState.Instance);
    }

    public override bool ExitState(AI agent)
    {
        return base.ExitState(agent);
    }

}