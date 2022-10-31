using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace UnityRoyale
{
    public class EnableGate : MonoBehaviour
    {
        public GameObject Gate;
        public GameObject enableCup;
        // Start is called before the first frame update
        void Start()
        {
            checkEnable();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        public string ReadFromBag()
        {
            return File.ReadAllText("Key.txt");
        }
        public bool checkTarget()
        {
            if (ReadFromBag() == "exist")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.transform.tag == "Fire")
            {
                if (checkTarget())
                {
                    Gate.SetActive(true);
                    enableCup.SetActive(true);
                }
                else
                {
                    Debug.Log("Khong co chia khoa ");
                }
               
            }
        }
        void checkEnable()
        {
            if (!checkTarget())
            {
                Gate.SetActive(false);
                enableCup.SetActive(false);
            }
        }
    }
}
