using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class ResourceBar : MonoBehaviour
    {
        [SerializeField] protected Sprite EmptySprite, FullSprite;
        [SerializeField] protected List<Image> Bars;

        [SerializeField] public PlayerStats Stats;

        
        protected void RenderResource(int currentResourceAmount, int maxResourceAmount)
        {
            for (int i = 0; i < Bars.Count; i++) {
                Bars[i].sprite = i < currentResourceAmount ? FullSprite : EmptySprite;
                Bars[i].enabled = i < maxResourceAmount;
            }
        }
    }
}
