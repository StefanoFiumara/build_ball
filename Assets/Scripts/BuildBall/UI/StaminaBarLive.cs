using BuildBall.Player;
using UnityEngine;
using UnityEngine.UI;

namespace BuildBall.UI
{
    public class StaminaBarLive : MonoBehaviour
    {
        [SerializeField] public PlayerStats Stats;
        private Image _image;

        public void Start()
        {
            _image = GetComponent<Image>();
        }

        public void Update()
        {
            if (Stats != null)
            {
                _image.fillAmount = Stats.GetPercentOfMaxStamina();
            }
        }
    }
}
