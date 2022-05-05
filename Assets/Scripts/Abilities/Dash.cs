using System;
using System.Collections;
using Events;
using UnityEngine;

namespace Abilities
{
    public class Dash : Ability
    {
        // Minimum amount of time between Dashes  
        private bool _isResetDashRefreshTimer = false;

        [SerializeField] public float DashVelocity;
        public void Start()
        {
            StartCoroutine(RefreshDash());
        }

        public bool IsTriggerable()
        {
            return !_isAbilityActive && !_isInternalCoolDownActive;
        }

        public void Trigger()
        {
            if (IsTriggerable()) {
                Actions.DashExecuted?.Invoke();

                _isAbilityActive = true;
                StartCoroutine(DashDuration());

                // restore stamina point after X seconds
                _isResetDashRefreshTimer = true;

                // don't allow for use of Dash TOO fast
                _isInternalCoolDownActive = true;
                StartCoroutine(ProcessInternalDashCooldown());
            }
        }

        private IEnumerator DashDuration()
        {
            float totalTime = 0f;
            while (totalTime <= DURATION_IN_SECONDS) {
                totalTime += Time.deltaTime;
                yield return null;
            }

            _isAbilityActive = false;
        }

        private IEnumerator RefreshDash()
        {
            float totalTime = 0f;

            while (totalTime <= COOLDOWN_IN_SECONDS) {
                if (_isResetDashRefreshTimer) {
                    totalTime = 0f;
                    _isResetDashRefreshTimer = false;
                }

                totalTime += Time.deltaTime;
                yield return null;
            }

            Actions.RefreshDashComplete?.Invoke();

            /*TODO: REVISIT*/
            StartCoroutine(RefreshDash());
        }

        private IEnumerator ProcessInternalDashCooldown()
        {
            float totalTime = 0f;

            while (totalTime <= INTERNAL_COOLDOWN_IN_SECONDS) {
                totalTime += Time.deltaTime;
                yield return null;
            }

            _isInternalCoolDownActive = false;
        }

        public bool IsAbilityActive()
        {
            return _isAbilityActive;
        }
    }
}
