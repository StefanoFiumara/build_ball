using UnityEngine;

namespace BuildBall.UI
{
    public class HealthBar : ResourceBar
    {
        public void Update()
        {
            RenderResource(Mathf.FloorToInt(Stats.HealthPoints), Stats.MaxHealthPoints);
        }
    }
}
