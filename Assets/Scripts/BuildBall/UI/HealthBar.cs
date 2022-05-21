using UnityEngine;

namespace BuildBall.UI
{
    public class HealthBar : ResourceBar
    {
        public void Update()
        {
            if (Stats != null)
            {
                RenderResource(Mathf.FloorToInt(Stats.HealthPoints), Stats.MaxHealthPoints);
            }
        }
    }
}
