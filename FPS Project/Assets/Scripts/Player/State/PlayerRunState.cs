using UnityEngine;
public class PlayerRunState : PlayerBaseState
{
    public override bool executeInFixedUpdate
    {
        get { return true; }
        protected set { }
    }
    public override void Execute(PlayerStateMachine player)
    {
        player.animatorComponent.SetBool("Jumping", false);
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
