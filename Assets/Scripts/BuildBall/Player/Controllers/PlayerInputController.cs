using BuildBall.Networking;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BuildBall.Player.Controllers
{
    [RequireComponent(typeof(PlayerStats))]
    [RequireComponent(typeof(MovementController))]
    [RequireComponent(typeof(BallController))]
    [RequireComponent(typeof(AbilityController))]
    public class PlayerInputController : NetworkBehaviour
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
            /*
            // TODO: Figure out what to do with these
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
            */
        }

        public override void FixedUpdateNetwork()
        {
            if (GetInput(out NetworkInputData input))
            {
                // Movement
                _movementController.Move(input.MoveDirection, Runner.DeltaTime);

                // Shooting
                if (input.IsShootPressed)
                {
                    _ballController.ShootBall();
                }

                // Standard Ability
                if (input.IsStandardAbilityPressed)
                {
                    _abilityController.ActivateStandardAbility();
                }

                // Ultimate Ability
                if (input.IsUltimateAbilityPressed)
                {
                    _abilityController.ActivateUltimateAbility();
                }
            }
        }
    }
}
