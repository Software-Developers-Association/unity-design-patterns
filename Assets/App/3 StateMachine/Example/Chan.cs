using UnityEngine;

public class Chan : Actor {
    protected override void Awake() {
        base.Awake();

        this.StateMachine.Add("Win", this.State_Win);
        this.StateMachine.Add("Jump", this.State_Jump);

        this.StateMachine.Add("Walk", this.State_Walk);
    }

    protected override bool State_Idle(StateMachine.State state) {
        switch(state) {
            case StateMachine.State.Update:
                float yAxis = Input.GetAxis("Vertical");

                if(Mathf.Abs(yAxis) >= 0.1F) {
                    this.StateMachine.StateId = "Walk";
                }

                break;
        }

        return base.State_Idle(state);
    }

    protected virtual bool State_Walk(StateMachine.State state) {
        switch(state) {
            case StateMachine.State.Enter:
                this.Animator.SetBool("Walk", true);
                break;
            case StateMachine.State.Update:
                float yAxis = Input.GetAxis("Vertical");

                if (Mathf.Abs(yAxis) < 0.1F) {
                    this.StateMachine.StateId = "Idle";
                }

                this.Animator.SetFloat("YAxis", yAxis);

                break;
            case StateMachine.State.Exit:
                this.Animator.SetBool("Walk", false);
                this.Animator.SetFloat("YAxis", 0.0F);
                break;
        }

        return true;
    }

    protected virtual bool State_Win(StateMachine.State state) {
        switch(state) {
            case StateMachine.State.Enter:
                this.Animator.SetTrigger("Win");
                break;
        }

        return true;
    }

    protected virtual bool State_Jump(StateMachine.State state) {
        switch(state) {
            case StateMachine.State.Enter:
                this.Animator.SetTrigger("Jump");
                break;
        }

        return true;
    }

    protected override void Update() {
        base.Update();

        if(Input.GetKeyDown(KeyCode.J)) {
            // transition into the Jump state...
            this.StateMachine.StateId = "Jump";
        }

        if(Input.GetKeyDown(KeyCode.W)) {
            // transition into the Win state...
            this.StateMachine.StateId = "Win";
        }

        if(Input.GetKeyDown(KeyCode.I)) {
            // transition into the Idle state...
            this.StateMachine.StateId = "Idle";
        }
    }
}