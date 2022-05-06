using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] protected Sprite EmptySprite, FullSprite;
        [SerializeField] private List<Image> Bars;

        private PlayerStats _stats;

        public void Start()
        {
            _stats = GetComponentInParent<PlayerStats>();
        }

        public void Update()
        {
            _stats.LimitMaxHealthPoints();

            for (int i = 0; i < Bars.Count; i++)
            {
                Bars[i].sprite = i < _stats.HealthPoints ? FullSprite : EmptySprite;
                Bars[i].enabled = i < _stats.MaxHealthPoints;
            }
        }
    }
}
