public class PlayerIdleState : PlayerBaseState
{
    public override bool executeInFixedUpdate
    {
        get { return false; }
        protected set { }
    }
    public override void Execute(PlayerStateMachine player)
    {
        player.animatorComponent.SetFloat("Input X", 0, player.animationDampSpeed, UnityEngine.Time.deltaTime);
        player.animatorComponent.SetFloat("Input Y", 0, player.animationDampSpeed, UnityEngine.Time.deltaTime);
        player.animatorComponent.SetBool("Jumping", false);
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
