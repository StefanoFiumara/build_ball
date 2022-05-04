using System;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : LifeBar
{
    protected override void Update()
    {
        if (_stats.healthPoints > _stats.maxHealthPoints)
        {
            _stats.healthPoints = _stats.maxHealthPoints;
        }

        for (int i = 0; i < bars.Count; i++)
        {
            bars[i].sprite = i < _stats.staminaPoints ? FullSprite : EmptySprite;
            bars[i].enabled = i < _stats.maxStaminaPoints;
        }
    }

    public void Reset()
    {
        _stats.staminaPoints = _stats.maxStaminaPoints;
    }
}