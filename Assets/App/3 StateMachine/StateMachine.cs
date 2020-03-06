using System.Collections.Generic; // for using the Dictionary<T, K> class.

[System.Serializable]
public class StateMachine {
    private Dictionary<string, System.Func<State, bool>> states = new Dictionary<string, System.Func<State, bool>>();
    private string stateId = string.Empty;
    private System.Func<State, bool> currentState = null;

    /// <summary>
    /// The current StateId of the StateMachine.
    /// </summary>
    public string StateId {
        get {
            return this.stateId;
        } set {
            this.Transition(value);
        }
    }

    protected virtual void Transition(string next) {
        // the next state has not been registered...
        if (this.states.ContainsKey(next) == false) return;

        // ignore transitioning into the same state....
        // [THIS IS OPTIONAL YOU CAN CHANGE THIS TO YOUR CHOOSING]
        if (string.Compare(this.StateId, next) == 0) return;

        // Notify the current state that we are attempt to exit it.
        if (this.currentState?.Invoke(State.Exit) == false) return;

        // grab the next state (this is a Method).
        var nextState = this.states[next];

        // let the state know we are entering it.
        if (nextState.Invoke(State.Enter) == false) return; // cannot transition into the next state revert...

        // make sure you update the stateId to the next state id.
        this.stateId = next;

        // point the current state to the next state...
        this.currentState = nextState;
    }

    public virtual void Add(string stateId, System.Func<State, bool> state) {
        // check if the Dictionary contains the stateId
        if(this.states.ContainsKey(stateId) == false) {
            // since it does not, add an entry into the dictionary.
            this.states.Add(stateId, null);
        }

        // update the entry of the dictionary with the new state.
        this.states[stateId] = state;
    }

    public virtual void Remove(string stateId) {
        this.states.Remove(stateId);
    }

    public virtual void Update() {
        this.currentState?.Invoke(State.Update);
    }

    public enum State {
        Enter,
        Update,
        Exit
    }
}