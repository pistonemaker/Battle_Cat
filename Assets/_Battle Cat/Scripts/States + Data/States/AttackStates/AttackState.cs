public class AttackState : State
{
    protected bool isTargetInAttackRange;

    protected AttackState(Entity entity, FSM stateMachine, string animName) 
        : base(entity, stateMachine, animName)
    {
    }
    
    public override void DoChecks()
    {
        base.DoChecks();
        isTargetInAttackRange = entity.CheckIfAttackTargetInRange(entity.curTarget);
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocityX(0f);
    }

    public virtual void TriggerAttack()
    {
    }

    public virtual void FinishAttack()
    {
    }
}
