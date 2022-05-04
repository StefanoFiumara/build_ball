using UnityEngine;

namespace Player
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject BallPrefab;
        [SerializeField] private float Velocity;

        private Vector2 _direction;
        private CharacterStats _stats;

        public void Start()
        {
            _stats = GetComponentInParent<CharacterStats>();
        }
        
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
                var ball = Instantiate(BallPrefab, transform.position, Quaternion.identity);

                var movement = ball.GetComponent<BallMovement>();
                movement.Velocity = shotStrength;
                // TODO: adjust ball direction based on character's facing direction?
            }
            
            if (Input.GetKey((KeyCode.LeftShift)))
            {
                
            }

            transform.position += (Velocity * Time.deltaTime * new Vector3(_direction.x, _direction.y, 0f));
        }
    }
}