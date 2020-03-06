using UnityEngine;

[RequireComponent(typeof(Animator))] // force every instance of Actor to have an Animator component.
public abstract class Actor : MonoBehaviour {
    private StateMachine stateMachine = new StateMachine();
    private Animator anim = null;

    public StateMachine StateMachine {
        get {
            return this.stateMachine;
        }
    }

    public Animator Animator {
        get {
            if (this.anim == null) this.anim = this.GetComponent<Animator>();

            return this.anim;
        }
    }

    protected virtual void Awake() {
        // register idle to our statemachine...
        this.StateMachine.Add("Idle", this.State_Idle);
    }

    protected virtual void Start() {
        // initialize our state machine to idle...
        this.StateMachine.StateId = "Idle";
    }

    protected virtual bool State_Idle(StateMachine.State state) {
        switch(state) {
            case StateMachine.State.Enter:
                this.Animator.SetTrigger("Idle");
                break;
        }

        return true;
    }

    protected virtual void Update() {
        this.StateMachine.Update();
    }
}