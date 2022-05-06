using UnityEngine;

namespace Player.Controllers
{
    [RequireComponent(typeof(MovementController))]
    [RequireComponent(typeof(BallController))]
    [RequireComponent(typeof(AbilityController))]
    [RequireComponent(typeof(PlayerStats))]
    public class PlayerInputController : MonoBehaviour
    {
        private MovementController _movementController;
        private BallController _ballController;
        private AbilityController _abilityController;

        private void Awake()
        {
            _movementController = GetComponent<MovementController>();
            _ballController = GetComponent<BallController>();
            _abilityController = GetComponent<AbilityController>();
        }

        private void Update()
        {
            // Movement
            var inputDirection = GetMovementDirection();
            _movementController.Move(inputDirection);

            // Shooting
            if (Input.GetMouseButtonDown(0)) //Left Click
            {
                _ballController.ShootBall();
            }

            // Standard Ability
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _abilityController.ActivateStandardAbility();
            }

            // Ultimate Ability
            if (Input.GetKeyDown(KeyCode.R))
            {
                _abilityController.ActivateUltimateAbility();
            }

        }

        private Vector2 GetMovementDirection()
        {
            var direction = Vector2.zero;

            if (Input.GetKey(KeyCode.W)) direction += Vector2.up;
            if (Input.GetKey(KeyCode.S)) direction += Vector2.down;
            if (Input.GetKey(KeyCode.A)) direction += Vector2.left;
            if (Input.GetKey(KeyCode.D)) direction += Vector2.right;

            return direction.normalized;
        }
    }
}
