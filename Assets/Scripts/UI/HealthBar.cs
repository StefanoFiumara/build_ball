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
            _stats.LimitMaxHealthPoints();

            RenderResource(_stats.HealthPoints, _stats.MaxHealthPoints);
        }
    }
}
