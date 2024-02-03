using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] Animator animator;
    [SerializeField] PerceptionComponent perceptionComponent;
    [SerializeField] BehaviorTree behaviorTree;

    // Start is called before the first frame update
    void Start()
    {
        if(healthComponent != null)
        {
            healthComponent.onHealthEmpty += StartDeath;
            healthComponent.onTakeDamage += TakeDamage;
        }
        perceptionComponent.onPerceptionTargetChanged += TargetChanged;
    }

    private void TargetChanged(GameObject target, bool sensed)
    {
        if(sensed)
        {
            behaviorTree.Blackboard.SetOrAddData("Target", target);
        }
        else
        {
            behaviorTree.Blackboard.SetOrAddData("LastSeenLoc", target.transform.position);
            behaviorTree.Blackboard.RemoveBlackboardData("Target");
        }
    }

    private void TakeDamage(float health, float delta, float maxHealth, GameObject instigator)
    {

    }

    private void StartDeath()
    {
        TriggerDeathAnimation();
    }

    private void TriggerDeathAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Dead");
        }
    }

    public void OnDeathAnimationFinished()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (behaviorTree && behaviorTree.Blackboard.GetBlackboardData("Target", out GameObject target))
        {
            Vector3 drawTargetPosition = target.transform.position + Vector3.up;
            Gizmos.DrawWireSphere(drawTargetPosition, 0.7f);

            Gizmos.DrawLine(transform.position + Vector3.up, drawTargetPosition);
        }
    }
}
