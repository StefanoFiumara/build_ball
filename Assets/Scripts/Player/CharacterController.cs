using UnityEngine;

namespace Player
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject BallPrefab;
        [SerializeField] private float Velocity;

        public float ShotStrength;

        public void Update()
        {

            if (Input.GetKey(KeyCode.A))
            {
                transform.position += (Velocity * Time.deltaTime * Vector3.left);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.position += (Velocity * Time.deltaTime * Vector3.right);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                var ball = Instantiate(BallPrefab, transform.position, Quaternion.identity);

                var movement = ball.GetComponent<BallMovement>();
                movement.Velocity = ShotStrength;
                // TODO: adjust ball direction based on character's facing direction?
            }
        }
    }
}
