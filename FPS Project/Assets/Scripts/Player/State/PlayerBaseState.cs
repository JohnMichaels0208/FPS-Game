public abstract class PlayerBaseState
{
    public abstract bool executeInFixedUpdate
    {
        get; protected set; 
    }
    public abstract void Execute(PlayerStateMachine player);
}
