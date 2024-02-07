using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class BT_TaskGroup_AttackTarget : BT_Task_Group
{
    float moveAcceptableDistance;
    float rotationAcceptableRaidus;
    float attackCooldownDuration;
    public BT_TaskGroup_AttackTarget(BehaviorTree tree, float moveAcceptableDistance = 2f, float rotationAcceptableRaidus = 10f, float attackCooldownDuration = 0) : base(tree)
    {
        this.moveAcceptableDistance = moveAcceptableDistance;
        this.rotationAcceptableRaidus = rotationAcceptableRaidus;
        this.attackCooldownDuration = attackCooldownDuration;
    }

    protected override void ConstructTree(out BT_Node Root)
    {
        Sequencer attackTargetSeq = new Sequencer();
        BT_Task_MoveToTarget moveToTarget = new BT_Task_MoveToTarget(tree, "Target", moveAcceptableDistance);

        BT_Task_RotateTowardsTarget rotateTowardsTarget = new BT_Task_RotateTowardsTarget(tree, "Target", rotationAcceptableRaidus);
        BT_Task_AttackTarget attackTarget = new BT_Task_AttackTarget(tree, "Target");
        CooldownDecorator attackCooldownDecorator = new CooldownDecorator(tree, attackTarget, attackCooldownDuration);

        attackTargetSeq.AddChild(moveToTarget);
        attackTargetSeq.AddChild(rotateTowardsTarget);
        attackTargetSeq.AddChild(attackCooldownDecorator);

        BlackboardDecorator attackTargetDecorator = new BlackboardDecorator(tree,
                                                                            attackTargetSeq,
                                                                            "Target",
                                                                            BlackboardDecorator.RunCondition.KeyExists,
                                                                            BlackboardDecorator.NotifyRule.RunConditionChange,
                                                                            BlackboardDecorator.NotifyAbort.both
                                                                            );

        Root = attackTargetDecorator;
    }
}
