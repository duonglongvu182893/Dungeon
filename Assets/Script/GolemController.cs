using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UnityRoyale
{
    public class GolemController : MonoBehaviour
    {
        public static GolemController instance ;
        Animator animator;
        int randomHit;
        public GameObject spellNom;
        public GameObject spellHi;
        public Transform spellPoint;
        public Slider slider;
        public int maxHealth = 100;
        public int currentHealth = 100;
        GameObject arrow;
        bool isDie;
        public GameObject Enemy;
        //public GameObject WinGate;


        // Start is called before the first frame update
        private void Awake()
        {
            instance = this;
        }
        void Start()
        {

            animator = GetComponent<Animator>();
            currentHealth = maxHealth;
            slider.maxValue = currentHealth;
            slider.value = currentHealth;

        }

        // Update is called once per frame
        [System.Obsolete]
        void Update()
        {
            checkAnimator();
            checkDie();
        
        }

        [System.Obsolete]
        void checkAnimator()
        {
            bool checkSpellHi = animator.GetBool("HitSpell");
            animator.SetBool("HitSpell", false);
            bool isGettingHit = animator.GetBool("GetHit");
            randomHit = Random.RandomRange(0, 100);
            if (currentHealth != 0)
            {
                if (randomHit == 0 && !isGettingHit)
                {
                    if (!checkSpellHi)
                    {
                        StartCoroutine(spellHig());
                        arrow = Instantiate(spellHi, spellPoint.position, transform.rotation);
                        arrow.GetComponent<Rigidbody>().AddForce(transform.forward * 25f, ForceMode.Impulse);
                        //StartCoroutine(DestroySpell());
                    }
                }
                else
                {
                    //Debug.Log("Do nothing");
                }
            }

        }
        IEnumerator spellHig()
        {
            animator.SetBool("HitSpell", true);
            yield return new WaitForSeconds(3f);
        }
        //IEnumerator DestroySpell()
        //{
        //    yield return new WaitForSeconds(5f);
        //    Destroy(arrow);
        //}
        private void OnTriggerExit(Collider other)
        {
            if(other.transform.tag == "Fire")
            {
                StartCoroutine(GetHit());
                Destroy(other.gameObject);
                //TakeDamage(50);
                //setHealth(currentHealth);

            }

        }
        IEnumerator GetHit()
        {
            animator.SetBool("GetHit", true);
            yield return new WaitForSeconds(1.5f);
            animator.SetBool("GetHit", false);
        }
        IEnumerator DesTroy()
        {
            yield return new WaitForSeconds(4f);
            Destroy(Enemy);
        }
        public int TakeDamage(int damage)
        {
            return currentHealth -= damage;
        }
        public void setHealth(int health)
        {
            slider.value = health;
        }
        void checkDie()
        {
            if(currentHealth <= 0)
            {
                animator.SetBool("Die", true);
                StartCoroutine(DesTroy());
                //WinGate.SetActive(true);
            }
        }
    }
}
