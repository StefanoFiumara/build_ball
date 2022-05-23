using BuildBall.Player.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace BuildBall.UI
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
            if (AbilityController == null)
            {
                return;
            }

            if (AbilityController.IsStandardUsageCooldownActive)
            {
                _image.fillAmount = AbilityController.StandardUsageCooldownPercent;
            }
            else
            {
                _image.fillAmount = 0;
            }
        }
    }
}
