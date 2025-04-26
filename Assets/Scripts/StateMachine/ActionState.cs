using UnityEngine;

public class ActionState : State
{
    public new string stateName = "Action";

    public override void OnEnter(StateController stateController, Animator animator)
    {
        base.OnEnter(stateController, animator);
        animator.SetBool("Action", true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
        animator.SetBool("Action", false);
    }
}
