using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Searches its parent hierarchy for an ActorStatManager object
 * and displays information about the health of this actor * 
 */
namespace RaidAI
{
    public class LifeBar : MonoBehaviour
    {
        public Actor actor;
        public GameObject healthBarPrefab;

        // For now lets just let these represent the different life
        // Bar color
        private static Color BASE = new Color(20.0f, 185.0f, 30.0f);
        private static Color BASE_1 = new Color(20.0f, 235.0f, 235.0f);
        private static Color BASE_2 = new Color(220.0f, 235.0f, 230.0f);

        // Start is called before the first frame update
        void Start()
        {
            FillBars();
        }

        // Update is called once per frame
        void Update()
        {
            FillBars();
        }

        private void FillBars()
        {
            Image[] healthBars = GetComponentsInChildren<Image>();
            float lifePercentage = actor.health.Value / actor.health.BaseValue;
            float remainder = lifePercentage;

            // Fill existing bars and remove unused bars
            for (int i = 0; i < healthBars.Length; i++)
            {
                if (remainder > 0.0f)
                {
                    float amountToFill = Mathf.Min(1.0f, remainder);
                    healthBars[i].fillAmount = amountToFill;
                    remainder -= amountToFill;
                }
                else
                {
                    Destroy(healthBars[i]);
                }
            }

            //int addedCount = 0;
            // Add new bars if necessary
            while (remainder > 0)
            {
                float amountToFill = Mathf.Min(1.0f, remainder);
                remainder -= amountToFill;
                // Spawn a new health bar and set the amount
                GameObject additionalBar = Instantiate(healthBarPrefab);
                additionalBar.transform.parent = transform;
                // Set the fill
                additionalBar.GetComponent<Image>().fillAmount = amountToFill;
            }
        }
    }
}