using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityRoyale
{
    public class HealthBarPlayer : MonoBehaviour
    {
   
        public Slider slider;
        public int maxHealth = 100;
        public int currentHealth=100;
        // Start is called before the first frame update
        void Start()
        {
            currentHealth = maxHealth;
            //SetMaxHealth(currentHealth);
            slider.maxValue = currentHealth;
            slider.value = currentHealth;

        }

        // Update is called once per frame
        void Update()
        {
        
        }
        //void LateUpdate()
        //{
        //    SetHealth(currentHealth);
        //}
       
        //public void SetMaxHealth(int health)
        //{
            
        //}
        public int TakeDamage(int damage)
        {
            return currentHealth -= damage;
        }
        public void setHealth(int health)
        {
            slider.value = health;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == "Fire")
            {
                TakeDamage(5);
                setHealth(currentHealth);

            }
        }
    }
}
