using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityRoyale
{
    public class TeleToBoss : MonoBehaviour
    {
        public GameObject Player;
        public Transform bossPosition;
        public GameObject bossRoom;
        public GameObject[] boss;
        public Transform spawPoint;
        public Transform lookPoint;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        //private void OnTrigerEnter(Collision collision)
        //{

        //}
        [System.Obsolete]
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == "Player")
            {

                bossRoom.SetActive(true);
                int selectBoss = Random.RandomRange(0, 2);
                if (selectBoss == 0)
                {
                    boss[0].SetActive(true);
                    GameObject bossClone = Instantiate(boss[0], boss[0].transform.position, boss[0].transform.rotation);
                    boss[0].SetActive(false);
                }
                else
                {
                    boss[1].SetActive(true);
                    GameObject bossClone = Instantiate(boss[1], boss[1].transform.position, boss[1].transform.rotation);
                    boss[1].SetActive(false);
                }
                Player.transform.position = bossPosition.position;
                DungeonGenerator.DungeonSet.DesTroyClone();
            }
        }
    }
}
