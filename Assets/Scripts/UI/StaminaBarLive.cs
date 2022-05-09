using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
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
            _image.fillAmount = Stats.GetPercentOfMaxStamina();
        }
    }
}
