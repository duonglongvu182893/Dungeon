using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace UnityRoyale
{
    public class KeyAdd : MonoBehaviour
    {
        public GameObject Key;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        void addKeyInBag()
        {
            File.WriteAllText("Key.txt", "exist");     
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.transform.tag == "Player")
            {
                Destroy(Key);
                addKeyInBag();
            }
        }
    }
}
