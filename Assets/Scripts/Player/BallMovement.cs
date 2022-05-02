using UnityEngine;

namespace Player
{
    public class BallMovement : MonoBehaviour
    {
        public float Velocity;
        public Vector3 Direction;

        // Update is called once per frame
        public void Update()
        {
            transform.position += (Velocity * Time.deltaTime * Direction.normalized);
        }
    }
}
