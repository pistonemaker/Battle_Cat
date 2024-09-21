public class Monster_3 : Entity
{
    public override void Awake()
    {
        base.Awake();

        moveState = new MoveState(this, stateMachine, "run");
        attackState = new RangeAttackState(this, stateMachine, "attack");
        hurtState = new HurtState(this, stateMachine, "damage");
    }

    private void Start()
    {
        stateMachine.Init(moveState);
    }
}
