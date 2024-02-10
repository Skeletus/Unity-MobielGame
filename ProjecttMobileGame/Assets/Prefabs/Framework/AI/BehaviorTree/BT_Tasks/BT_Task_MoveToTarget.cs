using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BT_Task_MoveToTarget : BT_Node
{
    NavMeshAgent agent;
    string targetKey;
    GameObject target;
    float acceptableDistance = 1f;
    BehaviorTree tree;

    public BT_Task_MoveToTarget(BehaviorTree tree, string targetKey, float acceptableDistance = 1f)
    {
        this.targetKey = targetKey;
        this.acceptableDistance = acceptableDistance;
        this.tree = tree;
    }

    protected override NodeResult Execute()
    {
        BlackBoard blackboard = tree.Blackboard;
        if (blackboard == null || !blackboard.GetBlackboardData(targetKey, out target))
            return NodeResult.Failure;

        agent = tree.GetComponent<NavMeshAgent>();
        if (agent == null)
            return NodeResult.Failure;

        if (IsTargetInAcceptableDistance())
            return NodeResult.Sucess;

        blackboard.onBlackboardValueChange += BlackboardValueChanged;

        agent.SetDestination(target.transform.position);
        agent.isStopped = false;
        return NodeResult.InProgress;
    }

    private void BlackboardValueChanged(string key, object val)
    {
        if (key == targetKey)
        {
            target = (GameObject)val;
        }
    }

    protected override NodeResult Update()
    {
        if (target == null)
        {
            agent.isStopped = true;
            return NodeResult.Failure;
        }

        agent.SetDestination(target.transform.position);
        if (IsTargetInAcceptableDistance())
        {
            agent.isStopped = true;
            return NodeResult.Sucess;
        }
        return NodeResult.InProgress;
    }

    bool IsTargetInAcceptableDistance()
    {
        return Vector3.Distance(target.transform.position, tree.transform.position) <= acceptableDistance;
    }

    protected override void End()
    {
        agent.isStopped = true;
        tree.Blackboard.onBlackboardValueChange -= BlackboardValueChanged;
        base.End();
    }
}
