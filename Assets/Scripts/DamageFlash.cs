using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaidAI
{
    public class DamageFlash : MonoBehaviour
    {

        public Color damageColor= new Color(1.0f, 0f, 0f);
        public float flashDuration = 1f;
        public float flashFrequencyHz = 10f;

        public float elapsedTime = 0f;

        private Material material;
        private Color baseColor;

        // Start is called before the first frame update
        void Start()
        {
            material = this.GetComponent<Renderer>().material;
            baseColor = material.color;
        }

        void Reset()
        {
            elapsedTime = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonUp("Jump"))
            {
                Debug.Log("Damage!");
            }
        }
    }
}
