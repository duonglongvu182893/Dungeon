using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace UnityRoyale
{
    public class NextFloor : MonoBehaviour
    {
        public static NextFloor cloneFloor;
        public GameObject GenMap;
        public Transform position;
        public GameObject bossRoom;
        //public GameObject nextFloor;
        public GameObject Player;
        GameObject clone;
        
        // Start is called before the first frame update
        private void Awake()
        {
            cloneFloor = this;
        }
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        [System.Obsolete]
        private void OnTriggerExit(Collider other)
        {
            if(other.transform.tag == "Player")
            {
                //DungeonGenerator.DungeonSet.DesTroyClone();
                DungeonGenerator.DungeonSet.size += new Vector2(1, 1);
                DungeonGenerator.DungeonSet.MazeGenerator();
                Player.transform.position = DungeonGenerator.DungeonSet.child[0].transform.position;
                bossRoom.SetActive(false);
                File.WriteAllText("Key.txt", "hasNoKey");

            }
        }
        //public void Destroy()
        //{
        //    Destroy(nextFloor);
        //}
    }
}
