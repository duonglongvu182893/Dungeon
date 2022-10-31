using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityRoyale
{
    public class ArmDamage : MonoBehaviour
    {
        
        private void Start()
        {
            //instance = GetComponent<GolemController>();

        }

        // Update is called once per frame
        void Update()
        {
            
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == "Fire")
            {
                Debug.Log("nhan dame tu canh tay"+ " -10");
                GolemController.instance.TakeDamage(10);
                GolemController.instance.setHealth(GolemController.instance.currentHealth);
                
            }
        }
       
        
    }
}
