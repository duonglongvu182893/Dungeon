using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityRoyale
{
    public class Arrow : MonoBehaviour
    {
        //public GameObject explosionArrow;
        public GameObject arrow;
        //public GameObject Player;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            //if (Vector3.Distance(arrow.transform.position, Player.transform.position) > 30f)
            //{
            //    Destroy(arrow);
            //}
        
        }
        //private void OnCollisionEnter(Collision collision)
        //{
        //    Destroy(this);
        //}
        //private void OnTriggerEnter(Collider other)
        //{
        //    if(other.transform.tag != "Player")
        //    {
        //        explosionArrow.SetActive(true);
        //    }
        //}
        //private void OnTriggerExit(Collider other)
        //{
        //    if (other.transform.tag != "Player")
        //    {
        //        StartCoroutine(DelayDestroy());
        //    }
        //}
        IEnumerator DelayDestroy()
        {
            yield return new WaitForSeconds(5f);
            Destroy(arrow);
        }
    }
}
