public class PlayerIdleState : PlayerBaseState
{
    public override void Execute(PlayerStateMachine player)
    {
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
