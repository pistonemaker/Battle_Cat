public class MoveState : State
{
    protected bool isNearestTargetInAttackRange;

    public MoveState(Entity entity, FSM stateMachine, string animName) 
        : base(entity, stateMachine, animName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
        isNearestTargetInAttackRange = entity.CheckIfNearestTarGetInRange();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        entity.SetVelocityX(entity.dataEntity.speed);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isNearestTargetInAttackRange)
        {
            entity.ResetAttackTarget();
            stateMachine.ChangeState(entity.AttackState);
        }
    }
}
