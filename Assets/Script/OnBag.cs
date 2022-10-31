using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UnityRoyale
{
    public class OnBag : MonoBehaviour
    {
        public TextMeshProUGUI Heath;
        public TextMeshProUGUI speed;
        public TextMeshProUGUI buyHealth;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            Heath.text = ArcherController.instanceArcher.maxHealth.ToString();
            speed.text = ArcherController.instanceArcher.speed.ToString();
            buyHealth.text = ArcherController.instanceArcher.slider.value.ToString();
        }
        public void UpHeath()
        {
            if (BagAdd.instanceBag.myBag["Coin"] >= 50)
            {
                ArcherController.instanceArcher.maxHealth += 10;
                ArcherController.instanceArcher.currentHealth += 10;
                BagAdd.instanceBag.myBag["Coin"] -= 50;
            }
            else
            {
                Debug.Log("Khong du tien");
            }
            //if(BagAdd.instanceBag.m)
            
           
        }
        public void UpSpeed()
        {
            if (BagAdd.instanceBag.myBag["Coin"] > 70  )
            {
                ArcherController.instanceArcher.speed += 2f;
                BagAdd.instanceBag.myBag["Coin"] -= 70;
            }
            else
            {
                Debug.Log("khong du tien");
            }
            
        }
        public void buyHeath()
        {
            if (BagAdd.instanceBag.myBag["Coin"] >= 5)
            {
                ArcherController.instanceArcher.currentHealth += 5;
                BagAdd.instanceBag.myBag["Coin"] -= 5;
            }
        }
        
        
    }
}
