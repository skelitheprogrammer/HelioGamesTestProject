using System;
using UnityEngine;

namespace SkilliTools.Timers
{
    public sealed class Timer
    {
        public float DesiredValue { get; private set; }

        public float currentValue;

        public event Action OnTimerEnd = null;
        public event Action OnTimerTick = null;

        public Timer(float fromValue,float toValue)
        {
            currentValue = fromValue;
            DesiredValue = toValue;
        }
        
        public Timer(float toValue)
        {
            DesiredValue = toValue;
            currentValue = 0;
        }

        public void Tick(float timeValue)
        {
            OnTimerTick?.Invoke();
            CheckForTimerEnd();

            if (DesiredValue == 0 || DesiredValue < 0)
            {
                currentValue -= timeValue;
            }

            if (DesiredValue > 0)
            {
                currentValue += timeValue;
            }

        }

        private void CheckForTimerEnd()
        {

            if (DesiredValue == 0)
            {
                if (currentValue <= DesiredValue)
                {
                    currentValue = 0;
                    OnTimerEnd?.Invoke();
                }
            }

            if (DesiredValue > 0)
            {
                if (currentValue >= DesiredValue)
                {
                    currentValue = DesiredValue;
                    OnTimerEnd?.Invoke();
                }
            }


        }
    }
}