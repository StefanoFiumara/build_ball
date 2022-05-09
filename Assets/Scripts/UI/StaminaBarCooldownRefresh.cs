using System.Collections.Generic;
using Abilities;
using Player;
using Player.Controllers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class StaminaBarCooldownRefresh : MonoBehaviour
    {
        [SerializeField] private AbilityController AbilityController;
        private Image _image;

        public void Start()
        {
            _image = GetComponent<Image>();
        }

        public void Update()
        {
            if (AbilityController.StandardAbility.IsUsageCooldownActive) {
                _image.fillAmount = AbilityController.StandardAbility.GetUsageCooldownPercent();
            }
            else {
                _image.fillAmount = 0;
            }
        }
    }
}
