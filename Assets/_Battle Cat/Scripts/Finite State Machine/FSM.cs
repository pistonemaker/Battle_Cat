using UnityEngine;

public class FSM
{
    private State curState;

    public State CurState
    {
        get => curState;
        set => curState = value;
    }
    
    public void Init(State entryState)
    {
        curState = entryState;
        curState.Enter();
    }

    public void ChangeState(State newState)
    {
        curState.Exit();
        curState = newState;
        curState.Enter();
    }
}