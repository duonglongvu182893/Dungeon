using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityRoyale
{
    public class TailDamage : MonoBehaviour
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
                Debug.Log("nhan dame tu Dau" + " -10");
                DragonControll.dragoncontrollHeath.TakeDamage(10);
                DragonControll.dragoncontrollHeath.setHealth(DragonControll.dragoncontrollHeath.currentHealth);
                DragonControll.dragoncontrollHeath.takeDamage();
            }
        }
    }
}
