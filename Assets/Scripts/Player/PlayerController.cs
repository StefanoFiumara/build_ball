using System.Collections;
using Abilities;
using Events;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject BallPrefab;
        [SerializeField] private float Velocity;

        [SerializeField] private PlayerStats _stats;

        [SerializeField] private AbilityManager _abilityManager;
        public float shotStrength;

        public void Start()
        {
            _stats = GetComponentInParent<PlayerStats>();
            _abilityManager = GetComponentInParent<AbilityManager>();
        }

        public void Update()
        {
            Vector2 movementDirection = GetMovementDirection();

            ProcessMouseClicks();
            ProcessKeyboardClicks();

            Vector3 newPosition = CalculateNewPosition(movementDirection);

            transform.position += Time.deltaTime * newPosition;
        }

        public Vector2 GetMovementDirection()
        {
            Vector2 direction = Vector2.zero;
            if (Input.GetKey(KeyCode.W)) {
                direction += Vector2.up;
            }

            if (Input.GetKey(KeyCode.S)) {
                direction += Vector2.down;
            }

            if (Input.GetKey(KeyCode.A)) {
                direction += Vector2.left;
            }

            if (Input.GetKey(KeyCode.D)) {
                direction += Vector2.right;
            }

            return direction;
        }

        public void ProcessKeyboardClicks()
        {
            if (Input.GetKey((KeyCode.LeftShift))) {
                Actions.DashTriggered?.Invoke(_stats);
            }
        }


        public void ProcessMouseClicks()
        {
            if (Input.GetMouseButtonDown(0)) //Left Click
            {
                var ball = Instantiate(BallPrefab, transform.position, Quaternion.identity);

                var movement = ball.GetComponent<BallMovement>();
                movement.Velocity = shotStrength;
                // TODO: adjust ball direction based on Player's facing direction?
            }
        }

        public Vector3 CalculateNewPosition(Vector2 movementDirection)
        {
            Vector3 directionToMove = new Vector3(movementDirection.x, movementDirection.y, 0f);
            if (_abilityManager.dash.IsAbilityActive()) {
                return Velocity * _abilityManager.dash.DashVelocity * directionToMove;
            }

            return Velocity * directionToMove;
        }
    }
}
