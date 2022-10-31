using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityRoyale
{
    public class EnemyConTroller : MonoBehaviour
    {
        public static EnemyConTroller instance;
        public float lookRadius = 7f;
        Animator animator;
        bool isFollowPlayer = false;
        bool isHit = false;
        bool dead = false;
        float speedRotation = 5f;
        public GameObject skeleton;
        
        //float speed = 7f;
        //public GameObject Player;

        Transform target;
        NavMeshAgent agent;

        // Start is called before the first frame update
        private void Awake()
        {
            instance = this;
        }
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
                
                StartCoroutine(DeadDestroy());
            }

        }
        void FaceTarget()
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speedRotation);
        }
        private void OnDrawGizmosSelected2D()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 5f);
        }
        void checkAnimator()
        {
            bool isWalking = animator.GetBool("Walk");
            if(isFollowPlayer&& !isWalking)
            {
                animator.SetBool("Walk", true);
            }
            else if(isWalking && !isFollowPlayer)
            {
                animator.SetBool("Walk", false);
            }
            else if(isFollowPlayer && isHit)
            {
                animator.SetBool("Dame", true);
            }
            else if (isFollowPlayer && !isHit)
            {
                animator.SetBool("Dame", false);
            }
            else if (dead)
            {
                animator.SetBool("Dead", true);
                
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if(other.transform.tag == "Player")
            {
                isHit = true;
            }
           
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.transform.tag == "Player")
            {
                isHit = false;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == "Fire")
            {
                dead = true;
                
            }
        }
       
        IEnumerator DeadDestroy()
        {
            yield return new WaitForSeconds(3f);
            BagAdd.instanceBag.addKill();
            Destroy(skeleton);

        }
    }
}
