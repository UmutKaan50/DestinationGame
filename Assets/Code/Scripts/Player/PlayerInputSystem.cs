using UnityEngine;
using UnityEngine.InputSystem;
using y01cu;

public class PlayerInputSystem : MonoBehaviour {
    private Player player;
    private PlayerCombat playerCombat;
    private Player_Unarmed playerUnarmed;
    private PlayerAnimations playerAnimations;
    private PlayerScriptsManager playerScriptsManager;
    private PlayerMovement playerMovement;
    private static PlayerInputActions playerInputActions; // Should it be static?

    private Player_WithSword playerWithSword;

    public PlayerInputActions GetPlayerInputActions() {
        return playerInputActions;
    }

    private void FixedUpdate() {
        // Handling movement
        Vector2 movementInputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        float speed = 1.2f;
        playerMovement.rigidbody2D.velocity = new Vector2(movementInputVector.x * speed, movementInputVector.y * speed);
        playerMovement.UpdatePlayerMovement(movementInputVector);
    }

    private void Awake() {
        player = GetComponent<Player>();
        playerCombat = GetComponent<PlayerCombat>();
        playerAnimations = GetComponent<PlayerAnimations>();
        playerScriptsManager = GetComponent<PlayerScriptsManager>();
        playerMovement = GetComponent<PlayerMovement>();
        playerUnarmed = GetComponent<Player_Unarmed>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Attack.performed += CallPlayerAttackActionFunctions;
    }

    private float lastAttackTime;
    private void CallPlayerAttackActionFunctions(InputAction.CallbackContext context) {
        // Calculate the time elapsed since the last attack
        float elapsedTimeSinceLastAttack = Time.time - lastAttackTime;

        bool isAttackOnCooldown = elapsedTimeSinceLastAttack < player.GetAttackCooldown();
        if (isAttackOnCooldown) {
            return;
        }
            
        // Update the timestamp of the last attack
        lastAttackTime = Time.time;
        
        // TODO: Make it flexible so that it can be used for other weapons as well. And don't forget to update animations based on attack phase.
        // playerCombat.GetTargetColliders(context);
        // playerCombat.SendAttackDamageToTheEnemy();

        playerUnarmed.GetTargetColliders(context);
        playerUnarmed.SendAttackDamageToTheEnemy();

        playerAnimations.TriggerAttack(context);
        playerScriptsManager.CallMethodsInOrder(context);
    }
}