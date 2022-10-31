using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityRoyale
{
    public class FootDamage : MonoBehaviour
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
                Debug.Log("nhan dame tu canh tay" + " -10");
                GolemController.instance.TakeDamage(20);
                GolemController.instance.setHealth(GolemController.instance.currentHealth);

            }
        }
        

    }
}
