using Abilities;
using Player;
using Player.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UltimateCoolDown : MonoBehaviour
    {
        [SerializeField] private Sprite UltimateSpentSprite;
        [SerializeField] private Image CooldownImageOverlay;
        [SerializeField] private TMP_Text CooldownTextOverlay;

        private Image _ultimateImage;

        private Sprite _ultimateRefreshedSprite;

        //cooldown timers
        [SerializeField] public PlayerInputController PlayerInputController;
        private AbilityController _abilityController;

        //private bool _isCooldown = false;
        //private float _cooldownTime = 4f;
        private float _cooldownTimer = 0f;
        public void Start()
        {
            _ultimateImage = GetComponentInParent<Image>();
            //_ultimateRefreshedSprite = _ultimateImage.sprite;
            CooldownTextOverlay.gameObject.SetActive(false);
            CooldownImageOverlay.fillAmount = 0.0f;

            _abilityController = PlayerInputController.GetComponent<AbilityController>();
        }

        public void Update()
        {
            ApplyCooldown();
            
            Debug.Log(_abilityController.GetUltimateCooldownPercent());
            if (_abilityController.GetUltimateCooldownPercent() < 1) {
                CooldownTextOverlay.gameObject.SetActive(true);
                _ultimateImage.sprite = UltimateSpentSprite;
            }
            else {
                _ultimateImage.sprite = _ultimateRefreshedSprite;
                CooldownTextOverlay.gameObject.SetActive(false);
            }
        }

        public void ApplyCooldown()
        {
            if (_abilityController.CurrentUsageCooldown() < 0.0f) {
                CooldownTextOverlay.gameObject.SetActive(false);
                CooldownImageOverlay.fillAmount = 0.0f;
            }
            else {
                CooldownTextOverlay.text = Mathf.RoundToInt(_abilityController.CurrentUsageCooldown()).ToString();
                CooldownImageOverlay.fillAmount = _abilityController.GetUltimateCooldownPercent();
            }
        }

        /*public void UseUltimate()
        {
            if (_isCooldown) {
                _ultimateImage.sprite = _ultimateRefreshedSprite;
            }
            else {
                _isCooldown = true;
                CooldownTextOverlay.gameObject.SetActive(true);
                _cooldownTimer = _cooldownTime;
                _ultimateImage.sprite = UltimateSpentSprite;
            }
        }*/
    }
}
