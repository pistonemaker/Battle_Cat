using UnityEngine;

public class MeleeAreaAttackState : AttackState
{
    public MeleeAreaAttackState(Entity entity, FSM stateMachine, string animName)
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

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(entity.attackTrf.position,
            entity.dataEntity.attackRadius, entity.dataEntity.opponentLayer);

        foreach (var collider2D in detectedObjects)
        {
            var damagable = collider2D.GetComponent<IDamagable>();

            if (damagable != null)
            {
                damagable.TakeDamage(entity.dataEntity.damage);
            }
        }
    }
}