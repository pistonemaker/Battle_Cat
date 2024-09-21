public class Monster_2 : Entity
{
    public override void Awake()
    {
        base.Awake();

        moveState = new MoveState(this, stateMachine, "run");
        attackState = new MeleeAreaAttackState(this, stateMachine, "attack");
        hurtState = new HurtState(this, stateMachine, "damage");
    }

    private void Start()
    {
        stateMachine.Init(moveState);
    }
}
