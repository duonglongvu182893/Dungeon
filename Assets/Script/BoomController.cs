using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityRoyale
{
    public class BoomController : MonoBehaviour
    {
        //public static EnemyConTroller instance;
        public float lookRadius = 15f;
        Animator animator;
        bool isFollowPlayer = false;
        bool isHit = false;
        bool dead = false;
        float speedRotation = 5f;
        public GameObject Boom;
      
        Transform target;
        NavMeshAgent agent;

        // Start is called before the first frame update
        //private void Awake()
        //{
        //    instance = this;
        //}
        void Start()
        {
            //target.position = Player.transform.position;
            animator = GetComponent<Animator>();
            target = PlayerManager.instance.player.transform;
            agent = GetComponent<NavMeshAgent>();


        }

        // Update is called once per frame
        void Update()
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= lookRadius)
            {
                agent.SetDestination(target.position);
                if (distance <= agent.stoppingDistance)
                {
                    FaceTarget();
                }
                isFollowPlayer = true;

            }
            else
            {
                isFollowPlayer = false;
            }
            checkAnimator();
            if (dead)
            {
                //StartCoroutine(DeadDestroy());
            }

        }
        void FaceTarget()
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speedRotation);
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, lookRadius);
        }
        void checkAnimator()
        {
            bool isWalking = animator.GetBool("Walk");
            if (isFollowPlayer && !isWalking)
            {
                animator.SetBool("Walk", true);
            }
            else if (isWalking && !isFollowPlayer)
            {
                animator.SetBool("Walk", false);
            }
            else if (isHit)
            {
                animator.SetBool("Attack", true);
                StartCoroutine(DesTroy());
            }
            
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.transform.tag == "Player")
            {
                //StartCoroutine(makeBoomArea());
                isHit = true;
            }

        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == "Fire")
            {
                dead = true;
            }
        }
        
        //IEnumerator makeBoomArea()
        //{
        //    yield return new WaitForSeconds(1.27f);
        //    boomArea.SetActive(true);
        //    yield return new WaitForSeconds(1f);
            
        //    Destroy(boomArea);

        //}
        IEnumerator DesTroy()
        {
            yield return new WaitForSeconds(4f);
            Destroy(Boom);
        }
    }
}
