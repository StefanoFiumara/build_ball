using System;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] protected Sprite EmptySprite, FullSprite;

    protected CharacterStats _stats;

    public List<Image> bars;

    public void Start()
    {
        _stats = GetComponentInParent<CharacterStats>();
    }

    public void Update()
    {
        _stats.LimitMaxStaminaPoints();

        for (int i = 0; i < bars.Count; i++)
        {
            bars[i].sprite = i < _stats.StaminaPoints ? FullSprite : EmptySprite;
            bars[i].enabled = i < _stats.MaxStaminaPoints;
        }
    }
}