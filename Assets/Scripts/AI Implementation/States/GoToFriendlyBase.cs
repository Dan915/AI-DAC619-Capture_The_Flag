using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToFriendlyBaseState : State<AI>
{
    private static GoToFriendlyBaseState instance; //Static instance of the state
    int _enemies;
    int _health;
    GameObject _collectable;

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

    public override bool EnterState(AI agent)
    {
        Debug.Log("<color=blue> Going to friendly Base </color>");
        return base.EnterState(agent);
        
    }

    public override void ExecuteState(AI agent)
    {
        _enemies = agent.senses.GetEnemiesInView().Count;
        _health = agent.data.CurrentHitPoints;

        if (agent.data.HasEnemyFlag || agent.data.HasFriendlyFlag)
        {
            agent.actions.MoveTo(agent.data.FriendlyBase);

            if (agent.data.InFriendlyBase)
            {
                if (agent.data.HasEnemyFlag)
                    agent.actions.DropItem(agent.inventory.GetItem(agent.data.EnemyFlagName));

                else if (agent.data.HasFriendlyFlag)
                    agent.actions.DropItem(agent.inventory.GetItem(agent.data.FriendlyFlagName));
            }
            else if (_health < 70 && agent.inventory.HasItem("Health Kit"))
                agent.actions.UseItem(agent.inventory.GetItem("Health Kit"));

            else if (_enemies > 0)
            {
                int i = Random.Range(0,10);
                
                switch (_enemies)
                {
                    case 1:
                        if (agent.inventory.HasItem("Power Up"))
                            agent.StateMachine.ChangeState(ChaseEnemyState.Instance);
                        else if (i >= 6)
                            agent.actions.Flee(agent.senses.GetEnemiesInView()[0]);
                        break;
                    case 2:
                        if (agent.inventory.HasItem("Power Up"))
                            agent.StateMachine.ChangeState(ChaseEnemyState.Instance);
                        else if (i <= 6)
                            agent.actions.Flee(agent.senses.GetEnemiesInView()[0]);
                        break;
                    case 3:
                        agent.actions.Flee(agent.senses.GetEnemiesInView()[0]);
                        break;
                    default:
                        break;
                }

            }
            else if (agent.senses.GetObjectsInViewByTag("Collectable").Count > 0)
            {
                _collectable = agent.senses.GetObjectsInViewByTag("Collectable")[0];

                {
                    if (agent.senses.IsItemInReach(_collectable))
                        agent.actions.CollectItem(_collectable);
                    else
                        agent.actions.MoveTo(_collectable);
                }
            }
        }
        else if (agent.senses.GetObjectsInViewByTag("Collectable").Count > 0)
        {
            _collectable = agent.senses.GetObjectsInViewByTag("Collectable")[0];

            {
                if (agent.senses.IsItemInReach(_collectable))
                    agent.actions.CollectItem(_collectable);
                else
                    agent.actions.MoveTo(_collectable);
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
