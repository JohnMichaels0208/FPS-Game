using UnityEngine;
public class PlayerJumpState : PlayerBaseState
{
    public override bool executeInFixedUpdate
    {
        get { return false; }
        protected set { }
    }
    public override void Execute(PlayerStateMachine player)
    {
        if (player.isGrounded)
        {
            player.animatorComponent.SetBool("Jumping", true);
            player.rigidbodyComponent.AddForce(Vector3.up * player.jumpForce, ForceMode.Impulse);
            player.jumpKeyPressed = false;
            player.SwitchState(player.playerIdleState);
        }
        player.SwitchState(player.playerIdleState);
    }
}
