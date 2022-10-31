using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityRoyale
{
    public class BoderSkill : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        //private void OnCollisionEnter(Collision collision)
        //{
        //    if(collision.transform.tag == "Fire")
        //    {
        //        Destroy(collision.gameObject);
        //    }
        //}
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == "Damage")
            {
                Destroy(other.gameObject);
            }
        }
    }
}
