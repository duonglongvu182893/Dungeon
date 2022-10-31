using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UnityRoyale
{
    public class BagAdd : MonoBehaviour
    {
        public static BagAdd instanceBag;
        public Dictionary<string, int> myBag = new Dictionary<string, int>();
        public TextMeshProUGUI numberOfKill;
        public TextMeshProUGUI numberOfCoin;

        // Start is called before the first frame update
        private void Awake()
        {
            instanceBag = this;
        }
        void Start()
        {
            myBag.Clear();
            myBag.Add("Kill", 0);
            myBag.Add("Coin", 0);

        }

        // Update is called once per frame
        void Update()
        {
            show();
        
        }
        public void addCoin()
        {
            myBag["Coin"]++;

        }
        public void addKill()
        {
           
                myBag["Kill"]++;
           
        }
        void show()
        {
            numberOfKill.text = myBag["Kill"].ToString();
            numberOfCoin.text = myBag["Coin"].ToString();
        }
        
    }
}
