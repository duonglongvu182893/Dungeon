using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityRoyale
{
    public class CollectCoin : MonoBehaviour
    {
        public float rotationSpeed = 2f;

        public AudioClip collectSound;

        public GameObject collectEffect;

        // Start is called before the first frame update
        void Start()
        {
      
                transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        }

        // Update is called once per frame
        void Update()
        {
        
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.transform.tag == "Player")
            {
                BagAdd.instanceBag.addCoin();
                Collect();
            }
        }
        void Collect()
        {
            if (collectSound)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }
            if (collectEffect)
            {
                Instantiate(collectEffect, transform.position, Quaternion.identity);
            }

            
            Destroy(gameObject);
        }
    }
}
