using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemyState : State<AI>
{
    private static ChaseEnemyState _instance; //Static instance of the state

    public ChaseEnemyState() //Constructor for the state
    {
        if (_instance != null) //If we already have an instance of this state, we don't need another one
            return;
        _instance = this;
        
    }

    public static ChaseEnemyState Instance //Public acsessor of the state, which will return the instance
    {
        get
        {
            if (_instance == null)
                new ChaseEnemyState();  //Constructs the state if we don't yet have an instance
            return _instance;
        }
    }

    public override bool EnterState(AI agent)
    {
        Debug.Log("<color=orange>I'm going after him </color>");
        return base.EnterState(agent);
    }

    public override void ExecuteState(AI agent)
    {
        int _enemies = agent.senses.GetEnemiesInView().Count;
        int _health = agent.data.CurrentHitPoints;

        if (_health < 70 && agent.inventory.HasItem("Health Kit"))
                agent.actions.UseItem(agent.inventory.GetItem("Health Kit"));

        else if (_enemies > 0)
        {
            int i = Random.Range(0,10);
            
            switch (_enemies)
            {
                case 1:
                        agent.StateMachine.ChangeState(AttackState.Instance);
                    break;
                case 2:
                    if (agent.inventory.HasItem("Power Up"))
                        agent.StateMachine.ChangeState(AttackState.Instance);
                    else if (i >= 8)
                        agent.actions.Flee(agent.senses.GetEnemiesInView()[0]);
                    break;
                case 3:
                    agent.actions.Flee(agent.senses.GetEnemiesInView()[1]);
                    break;
                default:
                    break;
            }

        }
        else
            agent.StateMachine.ChangeState(GoToEnemyBaseState.Instance);
    }

    public override bool ExitState(AI agent)
    {
        return base.ExitState(agent);
    }
}
