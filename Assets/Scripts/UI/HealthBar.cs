using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] protected Sprite EmptySprite, FullSprite;

    protected PlayerStats _stats;

    public List<Image> bars;

    public void Start()
    {
        _stats = GetComponentInParent<PlayerStats>();
    }
    
    public void Update()
    {
        _stats.LimitMaxHealthPoints();
        
        for (int i = 0; i < bars.Count; i++)
        {
            bars[i].sprite = i < _stats.HealthPoints ? FullSprite : EmptySprite;
            bars[i].enabled = i < _stats.MaxHealthPoints;
        }
    }
}