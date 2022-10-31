using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace UnityRoyale
{
    public class DragonControll : MonoBehaviour
    {
        public static DragonControll dragoncontrollHeath;
        public float lookRadius = 20f;
        bool isAttack = false;
        Animator dragonAnimator;
        Transform target;
        public GameObject testtingTarget;
        public Transform flamePoint;
        public GameObject fireBall;
        public GameObject enemy;
        public Slider slider;
        public int maxHealth = 200;
        public int currentHealth = 200;
        //bool isgetHit = false;
        NavMeshAgent agent;


        // Start is called before the first frame update
        private void Awake()
        {
            dragoncontrollHeath = this;
        }
        void Start()
        {
            dragonAnimator = GetComponent<Animator>();
            target = PlayerManager.instance.player.transform;
            currentHealth = maxHealth;
            slider.maxValue = currentHealth;
            slider.value = currentHealth;

        }

        // Update is called once per frame
        [System.Obsolete]
        void Update()
        {
            bool gethit = dragonAnimator.GetBool("GetHit");
            isPlayerOnAttackZone();
            if (isAttack&&!gethit)
            {
                chooseTypeOfAttack();
                Debug.Log("player trong vung danh");
                StartCoroutine(switchType());
            }
            else if (!isAttack&&gethit)
            {
                ResetAnimation();
                Debug.Log("Player ngoai vung danh");
            }
            //else if ((isgetHit&&isAttack)||(!isAttack&&isgetHit))
            //{
            //    dragonAnimator.SetBool("GetHit", true);
            //    isgetHit = false;
            //}
            flyMode();
            checkDie();
            

        }

        [System.Obsolete]
        void isPlayerOnAttackZone()
        {
            float distan = Vector3.Distance(testtingTarget.transform.position, transform.position);
            if(distan <= lookRadius)
            {
                bool clawA = dragonAnimator.GetBool("ClawAttack");
                bool flameA = dragonAnimator.GetBool("FlameAttack");
                if ((!clawA && !flameA)||(clawA&&!flameA))
                {
                    isAttack = true;

                }
                else
                {
                    isAttack = false;
                }
                
            }
            else if ( distan > lookRadius)
            {
                isAttack = false;
            }
            
        }
        void ResetAnimation()
        {
            bool claw = dragonAnimator.GetBool("ClawAttack");
            if(claw)
            {
                dragonAnimator.SetBool("ClawAttack", false);
            }
            bool flame = dragonAnimator.GetBool("FlameAttack");
            if (flame)
            {
                dragonAnimator.SetBool("FlameAttack", false);
            }
            //bool getHit = dragonAnimator.GetBool("GetHit");
            //if (getHit)
            //{
            //    dragonAnimator.SetBool("GetHit", false);
            //}
           
           
        }

        [System.Obsolete]
        void chooseTypeOfAttack()
        {
            int TypeOfAttack = Random.RandomRange(0, 10);
            if (TypeOfAttack == 0)
            {
                dragonAnimator.SetBool("ClawAttack", true);
            }
            else if(TypeOfAttack == 1)
            {
                bool isFlameAttack = dragonAnimator.GetBool("ClawAttack");
                if (!isFlameAttack)
                {
                    dragonAnimator.SetBool("FlameAttack", true);
                    StartCoroutine(flameAttack());
                }
                else if (isFlameAttack)
                {
                    //do nothing
                }
                
            }
            else
            {
                //do  nothing;
            }
        }
        //void flameAttack()
        //{
            
        //}
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, lookRadius);
        }
        IEnumerator switchType()
        {
            yield return new WaitForSeconds(2f);
            ResetAnimation();
        }
        IEnumerator flameAttack()
        {
            GameObject arrow = Instantiate(fireBall, flamePoint.position, transform.rotation);
            Vector3 go = new Vector3(-(flamePoint.position.x - testtingTarget.transform.position.x),-(flamePoint.position.y-testtingTarget.transform.position.y), -(flamePoint.position.z - testtingTarget.transform.position.z));
            arrow.GetComponent<Rigidbody>().AddForce(go * 2f, ForceMode.Impulse);
            yield return new WaitForSeconds(3f);

        }
        public int TakeDamage(int damage)
        {
            return currentHealth -= damage;
        }
        public void setHealth(int health)
        {
            slider.value = health;
        }
        public void takeDamage()
        {
            StartCoroutine(getHit());
        }
        IEnumerator getHit()
        {
            dragonAnimator.SetBool("GetHit", true);
            yield return new WaitForSeconds(2f);
            dragonAnimator.SetBool("GetHit", false);
        }
        public void flyMode()
        {
            if(currentHealth <= (maxHealth / 2))
            {
                dragonAnimator.SetBool("Fly", true);
            }
        }
        public void checkDie()
        {
            if(currentHealth <= 0)
            {
                dragonAnimator.SetBool("Die", true);
                StartCoroutine(Destroy());
            }
        }
        IEnumerator Destroy()
        {
            yield return new WaitForSeconds(4f);
            Destroy(enemy);
        }
    }

}
