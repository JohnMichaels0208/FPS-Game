public class PlayerIdleState : PlayerBaseState
{
    public override bool executeInFixedUpdate
    {
        get { return false; }
        protected set { }
    }
    public override void Execute(PlayerStateMachine player)
    {
        player.animatorComponent.SetBool("Jumping", false);
        player.animatorComponent.SetFloat("Speed", 0);
        if (player.jumpKeyPressed)
        {
            player.SwitchState(player.playerJumpState);
        }
        if (player.moveKeysPressed)
        {
            player.SwitchState(player.playerWalkState);
        }
    }
}
