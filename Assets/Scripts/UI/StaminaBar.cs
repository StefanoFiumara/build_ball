using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class StaminaBar : ResourceBar
    {
        public void Update()
        {
            Stats.LimitMaxStaminaPoints();

            RenderResource(Mathf.FloorToInt(Stats.StaminaPoints), Stats.MaxStaminaPoints);
        }
    }
}
