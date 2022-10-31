using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UnityRoyale
{
    public class ArcherController : MonoBehaviour
    {
        public static ArcherController instanceArcher;

        [SerializeField] private FixedJoystick joyStick;

        public int maxHealth = 100;
        public int currentHealth;
        public float speed = 10f;
        public float turnSmoothTime = 0.1f;


        private Vector3 characterMovement;

        CharacterController Archer;
        Animator archerAnimator;
        Vector2 movementVector;
        Vector2 look;

        bool isMovermentPressed;
        public Slider slider;

        public GameObject bag;
        public GameObject arrowObject;
        public GameObject moveCam;
        public GameObject aimCam;
        public GameObject lookAtPoint;
        public GameObject shop;
        public GameObject Reload;

        public Transform arrowPoint;
        public Transform bossPosition;
        public Transform cam;

        bool isAim = false;
        float turnSmoothVelocity;
        float targetAngle;
        bool isBagOpen = false;
        bool isShopOpen = false;
        

        
        void Start()
        {
            currentHealth = maxHealth;
            SetMaxHealth(currentHealth);
            
        
        }

        void Update()
        {

            if (isAim)
            {
                LookAtPoin();
            }
            checkDie();
            

        }
        private void Awake()
        {
            instanceArcher = this;
            Archer = GetComponent<CharacterController>();
            archerAnimator = GetComponent<Animator>();

        }
        
        void OnMove(InputValue movementValue)
        {
            movementVector = movementValue.Get<Vector2>();
            characterMovement.x = movementVector.x;
            characterMovement.z = movementVector.y;
            characterMovement.y = Physics.gravity.y;
            isMovermentPressed = movementVector.x != 0 || movementVector.y != 0;

        }
        void OnAim()
        {
            archerAnimator.SetBool("Aim", true);
            moveCam.SetActive(false);
            aimCam.SetActive(true);
            isAim = true;

        }
        void OnFire()
        {
            moveCam.SetActive(true);
            aimCam.SetActive(false);
            archerAnimator.SetBool("Fire", true);
            isAim = false;
            
        }
        private void FixedUpdate()
        {
            
            if (isMovermentPressed)
            {
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward + Physics.gravity;
                Archer.Move(moveDir * Time.deltaTime * speed);
                
            }
            if (!isMovermentPressed)
            {
                Archer.Move(new Vector3 (0f,Physics.gravity.y,0f));
            }
            charaterRotation();
            CheckAnimator();
            SetHealth(currentHealth);
            
        }
        void CheckAnimator()
        {
            
            bool isRunning = archerAnimator.GetBool("Run");
            archerAnimator.SetBool("Fire", false);
            archerAnimator.SetBool("Aim", false);
            archerAnimator.SetBool("GetHit", false);
            if (isMovermentPressed && !isRunning)
            {
                archerAnimator.SetBool("Run", true);
            }
            else if (!isMovermentPressed && isRunning)
            {
                archerAnimator.SetBool("Run", false);
            }
            
        }
        
        void charaterRotation()
        {
            
                targetAngle = Mathf.Atan2(movementVector.x, movementVector.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

        }

        public void IsShoot()
        {

            GameObject arrow = Instantiate(arrowObject, arrowPoint.position, transform.rotation);
            arrow.GetComponent<Rigidbody>().AddForce(lookAtPoint.transform.forward * 100f, ForceMode.Impulse);
           
        }
       
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == "Damage")
            {
                archerAnimator.SetBool("GetHit", true);
                Destroy(other.gameObject);
                TakeDamage(5);
                SetHealth(currentHealth);
            }
            if (other.transform.tag == "Gate")
            {
                Archer.transform.position = bossPosition.position;
                Debug.Log("Da vuot qua Gate");
            }
           
            
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.transform.tag == "EnemyBlade")
            {
                archerAnimator.SetBool("GetHit", true);
                TakeDamage(7);
            }
        }
        public void SetHealth(int health)
        {
            slider.value = health;
        }
        public void SetMaxHealth(int health)
        {
            slider.maxValue = health;
            slider.value = health;
        }
        void TakeDamage(int damage)
        {
            currentHealth -= damage;
        }

        [System.Obsolete]
        void OnOpenBag()
        {
            Debug.Log("da mo bag");
            if (!isBagOpen)
            {
                bag.SetActive(true);
                Time.timeScale = 0f;
                isBagOpen = true;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            else if (isBagOpen)
            {
                bag.SetActive(false);
                Time.timeScale = 1f;
                isBagOpen = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }   
            
        }
        void OnOpenShop()
        {
            Debug.Log("da mo Shop");
            if (!isShopOpen)
            {
                shop.SetActive(true);
                Time.timeScale = 0f;
                isShopOpen = true;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            else if (isShopOpen)
            {
                shop.SetActive(false);
                Time.timeScale = 1f;
                isShopOpen = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        void OnLook(InputValue value)
        {
            look = value.Get<Vector2>();
        }
        void LookAtPoin()
        {
            lookAtPoint.transform.rotation *= Quaternion.AngleAxis(look.x * 3f, Vector3.up);
            lookAtPoint.transform.rotation *= Quaternion.AngleAxis(look.y * 3f, Vector3.right);
            var angles = lookAtPoint.transform.localEulerAngles;
            angles.z = 0;
            var angle = lookAtPoint.transform.localEulerAngles.x;
            //gioi han goc
            if (isAim)
            {
                if (angle > 180 && angle < 340)
                {
                    angles.x = 340;
                }
                else if (angle < 180 && angle > 40)
                {
                    angles.x = 40;
                }
            }
            else
            {
                if (angle > 180 && angle < 340)
                {
                    angles.x = 340;
                    Debug.Log(angles.x);
                }
                else if (angle < 180 && angle > 40)
                {
                    angles.x = 40;
                    Debug.Log(angles.x);
                }

            }
            
            lookAtPoint.transform.localEulerAngles = angles;
            transform.rotation = Quaternion.Euler(0, lookAtPoint.transform.rotation.eulerAngles.y, 0);
            //reset the y rotation of the look transform
            lookAtPoint.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
        }
        void checkDie()
        {
            if (currentHealth <= 0)
            {
                archerAnimator.SetBool("Die", true);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                Reload.SetActive(true);
            }
        }
    }
}
