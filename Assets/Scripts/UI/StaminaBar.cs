using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class StaminaBar : MonoBehaviour
    {
        [SerializeField] private Sprite EmptySprite, FullSprite;

        [FormerlySerializedAs("bars")]
        [SerializeField] private List<Image> Bars;

        private PlayerStats _stats;

        public void Start()
        {
            _stats = GetComponentInParent<PlayerStats>();
        }

        public void Update()
        {
            _stats.LimitMaxStaminaPoints();

            for (int i = 0; i < Bars.Count; i++)
            {
                Bars[i].sprite = i < _stats.StaminaPoints ? FullSprite : EmptySprite;
                Bars[i].enabled = i < _stats.MaxStaminaPoints;
            }
        }
    }
}
