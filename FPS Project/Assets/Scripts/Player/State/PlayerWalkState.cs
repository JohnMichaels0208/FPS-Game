using UnityEngine;
public class PlayerWalkState : PlayerBaseState
{
    public override void Execute(PlayerStateMachine player)
    {
        player.rigidbodyComponent.AddRelativeForce(new Vector3(player.moveVector.x, 0, player.moveVector.y) * player.walkSpeed, ForceMode.Force);
        if (player.jumpKeyPressed)
        {
            player.SwitchState(player.playerJumpState);
        }
        if (!player.moveKeysPressed)
        {
            player.SwitchState(player.playerIdleState);
        }
        if (player.runKeyPressed)
        {
            player.SwitchState(player.playerRunState);
        }
    }
}
