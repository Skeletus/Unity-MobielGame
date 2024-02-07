using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, BehaviorTreeInterface, TeamInterface
{
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] Animator animator;
    [SerializeField] PerceptionComponent perceptionComponent;
    [SerializeField] BehaviorTree behaviorTree;
    [SerializeField] MovementComponent movementComponent;
    [SerializeField] int TeamID = 2;

    Vector3 previousPosition;

    public int GetTeamID()
    {
        return TeamID;
    }

    public Animator Animator
    {
        get { return animator; }
        private set { animator = value; }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if(healthComponent != null)
        {
            healthComponent.onHealthEmpty += StartDeath;
            healthComponent.onTakeDamage += TakeDamage;
        }
        perceptionComponent.onPerceptionTargetChanged += TargetChanged;
        previousPosition = transform.position;
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

        CalculateSpeed();
    }

    private void CalculateSpeed()
    {
        if(movementComponent == null) return;

        Vector3 positionDelta = transform.position - previousPosition;
        float speed = positionDelta.magnitude / Time.deltaTime;
        //Debug.Log($"current speed is {speed}");
        Animator.SetFloat("Speed", speed);
        previousPosition = transform.position;
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

    public void RotateTowards(GameObject target, bool vertialAim = false)
    {
        Vector3 AimDirection = target.transform.position - transform.position;
        AimDirection.y = vertialAim ? AimDirection.y : 0;
        AimDirection = AimDirection.normalized;

        movementComponent.RotateTowards(AimDirection);
    }

    public virtual void AttackTarget(GameObject target)
    {

    }
}
