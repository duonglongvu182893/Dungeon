using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityRoyale
{
    public class BodyDamege : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == "Fire")
            {
                Debug.Log("nhan dame tu than" + " -20");
                DragonControll.dragoncontrollHeath.TakeDamage(20);
                DragonControll.dragoncontrollHeath.setHealth(DragonControll.dragoncontrollHeath.currentHealth);
                DragonControll.dragoncontrollHeath.takeDamage();
            }
        }
    }
}
