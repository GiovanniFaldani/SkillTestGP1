using UnityEngine;
using UnityEngine.XR;

public class StateController : MonoBehaviour
{
    public State currentState;
    [SerializeField] public string currentStateName;

    public IdleState idleState = new IdleState();
    public MoveState moveState = new MoveState();
    public ActionState actionState = new ActionState();
    public JumpState jumpState = new JumpState();

    private Animator _animator;

    private void Awake()
    {
        idleState.stateName = "Idle";
        moveState.stateName = "Walk";
        actionState.stateName = "Action";
        jumpState.stateName = "Jump";
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        ChangeState(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnUpdate();
        }
    }

    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }

        currentState = newState;
        currentStateName = newState.stateName;
        currentState.OnEnter(this, _animator);
    }
}
