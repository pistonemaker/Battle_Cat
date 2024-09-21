public class State
{
    protected FSM stateMachine;
    protected Entity entity;
    protected string animName;

    protected State(Entity entity, FSM stateMachine, string animName)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animName = animName;
    }
    
    public virtual void DoChecks()
    {
        
    }

    public virtual void Enter()
    {
        entity.anim.AnimationState.SetAnimation(0, animName, true);
        DoChecks();
    }

    public virtual void LogicUpdate()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void Exit()
    {
        entity.anim.AnimationState.ClearTrack(0);
    }
}
