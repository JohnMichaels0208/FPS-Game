using UnityEngine;
public class PlayerWalkState : PlayerBaseState
{
    public override bool executeInFixedUpdate
    {
        get { return true; }
        protected set { }
    }
    public override void Execute(PlayerStateMachine player)
    {
        player.animatorComponent.SetBool("Jumping", false);
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
