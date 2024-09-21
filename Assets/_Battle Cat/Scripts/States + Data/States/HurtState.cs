using UnityEngine;

public class HurtState : State
{
    protected bool isGrounded;
    protected float groundCheckTime;
    protected const float groundCheckDuration = 0.5f;

    public HurtState(Entity entity, FSM stateMachine, string animName) : base(entity, stateMachine, animName)
    {
    }
    
    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = entity.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();
        groundCheckTime = 0f;
        
        entity.KnockbackDefault();
        if (entity.isDead)
        {
            entity.StartCoroutine(entity.Dead());
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (isGrounded)
        {
            groundCheckTime += Time.deltaTime;
        }
        else
        {
            groundCheckTime = 0f;
        }
        
        if (isGrounded && groundCheckTime >= groundCheckDuration)
        {
            stateMachine.ChangeState(entity.MoveState);
        }
    }
}
