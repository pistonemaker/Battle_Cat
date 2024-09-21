public class MeleeSingleAttackState : AttackState
{
    public MeleeSingleAttackState(Entity entity, FSM stateMachine, string animName) 
        : base(entity, stateMachine, animName)
    {
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        if (entity.curTarget != null)
        {
            var damagable = entity.curTarget.GetComponent<IDamagable>();
            
            if (damagable != null)
            {
                damagable.TakeDamage(entity.dataEntity.damage);
            }
        }
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (!isTargetInAttackRange)
        {
            stateMachine.ChangeState(entity.MoveState);
        }   
    }
}
