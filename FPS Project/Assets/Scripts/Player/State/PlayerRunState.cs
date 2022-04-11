using UnityEngine;
public class PlayerRunState : PlayerBaseState
{
    public override void Execute(PlayerStateMachine player)
    {
        player.rigidbodyComponent.AddRelativeForce(new Vector3(player.moveVector.x, 0, player.moveVector.y) * player.runSpeed, ForceMode.Force);
        if (player.jumpKeyPressed)
        {
            player.SwitchState(player.playerJumpState);
        }
        else if (!player.moveKeysPressed)
        {
            player.SwitchState(player.playerIdleState);
        }
        else if (!player.runKeyPressed)
        {
            player.SwitchState(player.playerWalkState);
        }
    }
}
