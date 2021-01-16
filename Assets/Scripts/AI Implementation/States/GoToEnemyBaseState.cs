using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToEnemyBaseState : State<AI>
{
    private static GoToEnemyBaseState instance; //Static instance of the state

    private GoToEnemyBaseState() //Constructor for the state
    {
        if (instance != null) //If we already have an instance of this state, we don't need another one
            return;
        instance = this;
    }

    public static GoToEnemyBaseState Instance //Public acsessor of the state, which will return the instance
    {
        get
        {
            if (instance == null)
                new GoToEnemyBaseState();  //Constructs the state if we don't yet have an instance
            return instance;
        }
    }

    public override bool EnterState(AI agent)
    {
        Debug.Log("<color=yellow> Going To Enemy Base State </color>");
        return base.EnterState(agent);
    }

    public override void ExecuteState(AI agent)
    {
        GameObject enemyFlag = agent.senses.GetObjectInViewByName(agent.data.EnemyFlagName);
        GameObject friendlyFlag = agent.senses.GetObjectInViewByName(agent.data.FriendlyFlagName);
        int _health = agent.data.CurrentHitPoints;
        if (agent.inventory.HasItem(agent.data.FriendlyFlagName) || agent.inventory.HasItem(agent.data.EnemyFlagName))
                agent.StateMachine.ChangeState(GoToFriendlyBaseState.Instance);
        

        else if (friendlyFlag != null && !agent.data.FriendlyBase.GetComponent<SetScore>()._friendlyFlagInBase)
        {
            if (agent.senses.IsItemInReach(friendlyFlag))
                agent.actions.CollectItem(friendlyFlag);
            else 
                agent.actions.MoveTo(friendlyFlag);
        }
        else if (enemyFlag != null && !agent.data.FriendlyBase.GetComponent<SetScore>()._enemyFlagInBase)
        {
                if (agent.senses.IsItemInReach(enemyFlag))
                    agent.actions.CollectItem(enemyFlag);
                else 
                    agent.actions.MoveTo(enemyFlag);           
        }
        else if (agent.senses.GetObjectsInViewByTag("Collectable").Count > 0)
        {
           GameObject _collectable = agent.senses.GetObjectsInViewByTag("Collectable")[0];

            {
                if (agent.senses.IsItemInReach(_collectable))
                    agent.actions.CollectItem(_collectable);
                else
                    agent.actions.MoveTo(_collectable);
            }
        }
        else if (_health < 70 && agent.inventory.HasItem("Health Kit"))
                agent.actions.UseItem(agent.inventory.GetItem("Health Kit"));
        
        else if (agent.senses.GetEnemiesInView().Count > 0)
            agent.StateMachine.ChangeState(ChaseEnemyState.Instance);
        else   
            agent.actions.MoveTo(agent.data.EnemyBase);
    }

    public override bool ExitState(AI agent)
    {
        return base.ExitState(agent);
    }

}
