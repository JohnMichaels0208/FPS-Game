using UnityEngine;
public class PlayerJumpState : PlayerBaseState
{
    public override void Execute(PlayerStateMachine player)
    {
        if (player.isGrounded)
        {
            Debug.Log("Jumping");
            player.rigidbodyComponent.AddForce(Vector3.up * player.jumpForce, ForceMode.Impulse);
            player.jumpKeyPressed = false;
            player.SwitchState(player.playerIdleState);
        }
        player.SwitchState(player.playerIdleState);
    }
}
