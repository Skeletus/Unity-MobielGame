using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownDecorator : Decorator
{
    float cooldownTime;
    float lastExecutionTime = -1;
    bool failOnCooldown;
    public CooldownDecorator(BehaviorTree tree, BT_Node child, float cooldownTime, bool failOnCooldown = false) : base(child)
    {
        this.cooldownTime = cooldownTime;
        this.failOnCooldown = failOnCooldown;
    }

    protected override NodeResult Execute()
    {
        if (cooldownTime == 0)
            return NodeResult.InProgress;

        //first execution
        if (lastExecutionTime == -1)
        {
            lastExecutionTime = Time.timeSinceLevelLoad;
            return NodeResult.InProgress;
        }

        //cooldown not finished
        if (Time.timeSinceLevelLoad - lastExecutionTime < cooldownTime)
        {
            if (failOnCooldown)
            {
                return NodeResult.Failure;
            }
            else
            {
                return NodeResult.Sucess;
            }
        }

        //cooldown is finished since last time
        lastExecutionTime = Time.timeSinceLevelLoad;
        return NodeResult.InProgress;
    }

    protected override NodeResult Update()
    {
        return GetChild().UpdateNode();
    }
}
