public class RangeAttackState : AttackState
{
    public RangeAttackState(Entity entity, FSM stateMachine, string animName) 
        : base(entity, stateMachine, animName)
    {
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (!isTargetInAttackRange)
        {
            stateMachine.ChangeState(entity.MoveState);
        }   
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
        entity.SpawnProjectile();
    }
}
