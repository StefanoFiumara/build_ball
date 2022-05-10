using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class BallMovement : MonoBehaviour
    {
        public float Velocity;
        public float Friction;
        public float FrictionThreshold;
        public Vector3 Direction;
        public BallStateEnum BallState;
        public TeamAffiliationEnum TeamAffiliation;

        private void Start()
        {
            BallState = BallStateEnum.Stationary;
        }

        // Update is called once per frame
        public void Update()
        {
            if (BallState == BallStateEnum.Moving)
            {
                transform.position += (Velocity * Time.deltaTime * Direction.normalized);
                Velocity -= (Friction * Time.deltaTime);
                if (Velocity < FrictionThreshold)
                {
                    BallState = BallStateEnum.Stationary;
                }
            }
        }
    }
}
