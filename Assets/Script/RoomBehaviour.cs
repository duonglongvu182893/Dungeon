using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityRoyale
{
    public class RoomBehaviour : MonoBehaviour
    {
        public GameObject[] walls;
        public GameObject[] doors;
        public GameObject[] coin;
       
        public void UpdateRoom(bool[] status)
        {
            for(int i=0; i < status.Length; i++)
            {
                doors[i].SetActive(status[i]);
                walls[i].SetActive(!status[i]);

            }
        }

        [System.Obsolete]
        public void Coin()
        {
            bool[] status = new bool[20];
            for (int i = 0; i < 11; i++)
            {
                int random = Random.RandomRange(0, 2);
                if (random == 0)
                {
                    status[i] = true;
                }
                else
                {
                    status[i] = false;
                }
            }

            for (int i = 0; i <  11; i++)
            {
                coin[i].SetActive(status[i]);
            }
        }
    }
}
