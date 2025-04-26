using UnityEngine;

public class JumpState : State
{
    public new string stateName = "Jump";

    public override void OnEnter(StateController stateController, Animator animator)
    {
        base.OnEnter(stateController, animator);
        animator.SetBool("Jump", true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
        animator.SetBool("Jump", false);
    }
}
