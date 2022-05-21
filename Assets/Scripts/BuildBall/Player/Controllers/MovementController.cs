using UnityEngine;

namespace BuildBall.Player.Controllers
{
    [RequireComponent(typeof(PlayerStats))]
    public class MovementController : MonoBehaviour
    {
        private PlayerStats _stats;

        private void Awake()
        {
            _stats = GetComponent<PlayerStats>();
        }

        public void Move(Vector2 direction, float deltaTime)
        {
            var moveDirection = new Vector3(direction.x, direction.y, 0f);
            var moveDelta = moveDirection * _stats.MovementSpeed * deltaTime;

            transform.position += moveDelta;
        }

        public void MoveToGraveyard()
        {
            transform.position = new Vector3(-1000, -1000, 0); // TODO RH: Is this the right way to move them off the field?
        }

    }
}
