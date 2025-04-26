using UnityEngine;

public class MoveState : State
{
    public new string stateName = "Move";

    public override void OnEnter(StateController stateController, Animator animator)
    {
        base.OnEnter(stateController, animator);
        animator.SetBool("Walk", true);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
        animator.SetBool("Walk", false);
    }
}
