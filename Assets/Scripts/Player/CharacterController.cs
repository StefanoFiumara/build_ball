using UnityEngine;

namespace Player
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private float velocity;

        private Vector2 _direction;

        public float shotStrength;

        public void Update()
        {
            _direction = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
            {
                _direction += Vector2.up;
            }

            if (Input.GetKey(KeyCode.S))
            {
                _direction += Vector2.down;
            }

            if (Input.GetKey(KeyCode.A))
            {
                _direction += Vector2.left;
            }

            if (Input.GetKey(KeyCode.D))
            {
                _direction += Vector2.right;
            }

            if (Input.GetMouseButtonDown(0)) //Left Click
            {
                var ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);

                var movement = ball.GetComponent<BallMovement>();
                movement.Velocity = shotStrength;
                // TODO: adjust ball direction based on character's facing direction?
            }

            transform.position += (velocity * Time.deltaTime * new Vector3(_direction.x, _direction.y, 0f));
        }
    }
}