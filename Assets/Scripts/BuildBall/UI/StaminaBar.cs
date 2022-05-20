using UnityEngine;

namespace BuildBall.UI
{
    public class StaminaBar : ResourceBar
    {
        public void Update()
        {
            RenderResource(Mathf.FloorToInt(Stats.StaminaPoints), Stats.MaxStaminaPoints);
        }
    }
}
