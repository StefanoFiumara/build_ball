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
            _stats.LimitMaxStaminaPoints();

            RenderResource(_stats.StaminaPoints, _stats.MaxStaminaPoints);
        }
    }
}
