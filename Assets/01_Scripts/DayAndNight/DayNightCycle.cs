using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jun
{
    public class DayNightCycle : MonoBehaviour
    {
        [SerializeField] private Light sun;
        [SerializeField, Range(0, 24)] private float timeOfDay;

        [Tooltip("Can control the speed time of day")]
        [SerializeField] private float sunRotationSpeed;

        [Header("LightingPreset")]
        [SerializeField] private Gradient skyColor;
        [SerializeField] private Gradient equatorColor;
        [SerializeField] private Gradient sunColor;

        // Events for day and night
        public event EventHandler OnDayStart;
        public event EventHandler OnDayEnd;

        private static DayNightCycle _instance;
        public static DayNightCycle Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<DayNightCycle>();
                }
                return _instance;
            }
        }



        private void Update()
        {
            DayLoop();

            UpdateLighting();
            UpdateSunRotation();
        }

        private void DayLoop()
        {
            timeOfDay += Time.deltaTime * sunRotationSpeed;

            if (timeOfDay > 24)
            {
                timeOfDay = 0;

                OnDayEnd?.Invoke(this, EventArgs.Empty);

            }
        }

        private void UpdateSunRotation()
        {
            float sunRotation = Mathf.Lerp(-90, 270, timeOfDay / 24);
            sun.transform.rotation = Quaternion.Euler(sunRotation, sun.transform.rotation.y, sun.transform.rotation.z);

        }

        private void UpdateLighting()
        {
            float timeFraction = timeOfDay / 24;
            RenderSettings.ambientEquatorColor = equatorColor.Evaluate(timeFraction);
            RenderSettings.ambientSkyColor = skyColor.Evaluate(timeFraction); // Corrected line
            sun.color = sunColor.Evaluate(timeFraction);
        }

    }

}
