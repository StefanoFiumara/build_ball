using System.Collections;
using UnityEngine;

namespace Player
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject BallPrefab;
        [SerializeField] private float Velocity;

        private bool _isDashActive = false;

        // Minimum amount of time between Dashes  
        private bool _isInternalDashCoolDownActive = false;
        private bool _isResetDashRefreshTimer;

        private CharacterStats _stats;

        public void Start()
        {
            _stats = GetComponentInParent<CharacterStats>();
            StartCoroutine(RefreshDash());
        }

        public float shotStrength;

        public void Update()
        {
            Vector2 movementDirection = GetMovementDirection();

            ProcessMouseClicks();

            ProcessDashAbility();

            Vector3 newPosition = CalculateNewPosition(movementDirection, _isDashActive);

            transform.position += Time.deltaTime * newPosition;
        }

        private bool ProcessDashAbility()
        {
            bool isValidDash = !_isDashActive
                               && !_isInternalDashCoolDownActive
                               && Input.GetKey((KeyCode.LeftShift))
                               && _stats.CanDash();
            if (isValidDash)
            {
                _stats.StaminaPointLoss();

                _isDashActive = true;
                StartCoroutine(DashDuration());

                // restore stamina point after X seconds
                _isResetDashRefreshTimer = true;

                // don't allow for use of Dash TOO fast
                _isInternalDashCoolDownActive = true;
                StartCoroutine(ProcessInternalDashCooldown());
            }

            return isValidDash;
        }

        private IEnumerator DashDuration()
        {
            float dashDurationInSeconds = _stats.DashDuration;
            float totalTime = 0f;

            while (totalTime <= dashDurationInSeconds)
            {
                totalTime += Time.deltaTime;
                yield return null;
            }

            _isDashActive = false;
        }

        private IEnumerator RefreshDash()
        {
            float dashRefreshInSeconds = 5f;
            float totalTime = 0f;

            while (totalTime <= dashRefreshInSeconds)
            {
                if (_isResetDashRefreshTimer)
                {
                    totalTime = 0f;
                    _isResetDashRefreshTimer = false;
                }

                totalTime += Time.deltaTime;
                yield return null;
            }

            _stats.StaminaPointGain();

            StartCoroutine(RefreshDash());
        }

        private IEnumerator ProcessInternalDashCooldown()
        {
            float internalDashCooldownDurationInSeconds = 2f;
            float totalTime = 0f;

            while (totalTime <= internalDashCooldownDurationInSeconds)
            {
                totalTime += Time.deltaTime;
                yield return null;
            }

            _isInternalDashCoolDownActive = false;
        }

        private void ProcessMouseClicks()
        {
            if (Input.GetMouseButtonDown(0)) //Left Click
            {
                var ball = Instantiate(BallPrefab, transform.position, Quaternion.identity);

                var movement = ball.GetComponent<BallMovement>();
                movement.Velocity = shotStrength;
                // TODO: adjust ball direction based on character's facing direction?
            }
        }

        private Vector3 CalculateNewPosition(Vector2 movementDirection, bool isDashActive)
        {
            Vector3 directionToMove = new Vector3(movementDirection.x, movementDirection.y, 0f);
            if (isDashActive)
            {
                return Velocity * _stats.DashVelocity * directionToMove;
            }

            return Velocity * directionToMove;
        }

        private Vector2 GetMovementDirection()
        {
            Vector2 direction = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
            {
                direction += Vector2.up;
            }

            if (Input.GetKey(KeyCode.S))
            {
                direction += Vector2.down;
            }

            if (Input.GetKey(KeyCode.A))
            {
                direction += Vector2.left;
            }

            if (Input.GetKey(KeyCode.D))
            {
                direction += Vector2.right;
            }

            return direction;
        }
    }
}