using UnityEngine;
using UnityEngine.SceneManagement;

namespace BuildBall.Player.Controllers
{
    [RequireComponent(typeof(PlayerStats))]
    [RequireComponent(typeof(MovementController))]
    [RequireComponent(typeof(BallController))]
    [RequireComponent(typeof(AbilityController))]
    public class PlayerInputController : MonoBehaviour
    {
        private MovementController _movementController;
        private BallController _ballController;
        private AbilityController _abilityController;
        private PlayerStats _playerStats;

        private void Awake()
        {
            _movementController = GetComponent<MovementController>();
            _ballController = GetComponent<BallController>();
            _abilityController = GetComponent<AbilityController>();
            _playerStats = GetComponent<PlayerStats>();
        }

        private void Update()
        {
            var playerIsDead = _playerStats.IsDead();
            if (playerIsDead)
            {
                _movementController.MoveToGraveyard();
            }

            // Dummy players ignore all input
            if (_playerStats.IsDummy)
            {
                return;
            }

            // In-Game Menu
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Scenes/InGameMenuScene");
            }

            // If dead, ignore all other input
            if (playerIsDead)
            {
                return;
            }

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
