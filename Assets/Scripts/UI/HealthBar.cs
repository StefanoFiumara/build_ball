using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : ResourceBar
    {
        public void Update()
        {
            Stats.LimitMaxHealthPoints();

            RenderResource(Mathf.FloorToInt(Stats.HealthPoints), Stats.MaxHealthPoints);
        }
    }
}
