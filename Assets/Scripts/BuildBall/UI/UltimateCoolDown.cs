using BuildBall.Player.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BuildBall.UI
{
    public class UltimateCoolDown : MonoBehaviour
    {
        [SerializeField] private Image CooldownImageOverlay;
        [SerializeField] private TMP_Text CooldownTextOverlay;


        //cooldown timers
        [SerializeField] public PlayerInputController PlayerInputController;
        private AbilityController _abilityController;

        public void Start()
        {
            CooldownTextOverlay.gameObject.SetActive(false);
            CooldownImageOverlay.fillAmount = 0.0f;

            if (PlayerInputController != null)
            {
                _abilityController = PlayerInputController.GetComponent<AbilityController>();
            }
        }

        public void Update()
        {
            if (PlayerInputController == null)
            {
                return;
            }

            ApplyCooldown();

            if (_abilityController.UltimateUsageCooldownPercent < 1) {
                CooldownTextOverlay.gameObject.SetActive(true);
            }
            else {
                CooldownTextOverlay.gameObject.SetActive(false);
            }
        }

        public void ApplyCooldown()
        {
            if (_abilityController.UltimateUsageCooldown < 0.0f) {
                CooldownTextOverlay.gameObject.SetActive(false);
                CooldownImageOverlay.fillAmount = 0.0f;
            }
            else {
                CooldownTextOverlay.text = Mathf.RoundToInt(_abilityController.UltimateUsageCooldown).ToString();
                CooldownImageOverlay.fillAmount = 1 - _abilityController.UltimateUsageCooldownPercent;
            }
        }
    }
}
