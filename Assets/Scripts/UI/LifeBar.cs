using System;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    [SerializeField] protected Sprite EmptySprite, FullSprite;

    protected CharacterStats _stats;

    public List<Image> bars;

    public void Start()
    {
        _stats = GetComponentInParent<CharacterStats>();
    }
    
    protected virtual void Update()
    {
        if (_stats.healthPoints > _stats.maxHealthPoints)
        {
            _stats.healthPoints = _stats.maxHealthPoints;
        }

        for (int i = 0; i < bars.Count; i++)
        {
            bars[i].sprite = i < _stats.healthPoints ? EmptySprite : FullSprite;
            bars[i].enabled = i < _stats.maxHealthPoints;
        }
    }
}