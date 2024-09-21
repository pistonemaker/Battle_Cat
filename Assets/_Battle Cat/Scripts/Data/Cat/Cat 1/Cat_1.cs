public class Cat_1 : Entity
{
    public override void Awake()
    {
        base.Awake();

        moveState = new MoveState(this, stateMachine, "run");
        attackState = new MeleeSingleAttackState(this, stateMachine, "attack");
        hurtState = new HurtState(this, stateMachine, "damage");
    }

    private void Start()
    {
        stateMachine.Init(moveState);
    }   
}
