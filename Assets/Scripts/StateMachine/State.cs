using UnityEngine;

public abstract class State
{
    protected StateController sc;
    protected Animator animator;
    public string stateName;

    public virtual void OnEnter(StateController stateController, Animator animator)
    {
        sc = stateController;
        this.animator = animator;
    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnExit()
    {

    }
}
