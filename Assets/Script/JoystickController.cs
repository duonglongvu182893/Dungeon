using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityRoyale
{
    public class JoystickController : MonoBehaviour
    {
        [SerializeField] private FixedJoystick inputController;
        [SerializeField] private FixedJoystick lookjoystick;
        public GameObject arrowObject;
        public Transform arrowPoint;
        CharacterController Archer;
        Animator animatorController;
        Vector3 movermentCharacter;
        public float turnSmoothTime = 0.1f;
        float turnSmoothVelocity;
        public Transform cam;
        bool isMove;
        Quaternion look;
        //float targetAngle;

        public float speed;
        // Start is called before the first frame update
        private void Awake()
        {
            Archer = GetComponent<CharacterController>();
            
        }
        void Start()
        {

        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        private void FixedUpdate()
        {
            //getInputOnJoystick();
            getInput();
            CheckAnimator();
            //lookJoystick();
            //Rotation();
        }
        void getInputOnJoystick()
        {
            movermentCharacter = new Vector3(inputController.Horizontal, 0f, inputController.Vertical).normalized;
           
            if (inputController.Horizontal != 0 || inputController.Vertical != 0)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(inputController.Horizontal, 0f, inputController.Vertical));
                
                //Vector3 moverDir = look * Vector3.forward;
                Archer.Move(movermentCharacter * speed * Time.deltaTime);
                
                isMove = true;
            }
            
            else if ( inputController.Horizontal == 0 &&inputController.Vertical == 0)
            {
                isMove = false;
            }
            
        }
        void getInput()
        {
            movermentCharacter = new Vector3(inputController.Horizontal, 0f, inputController.Vertical).normalized;

            if (inputController.Horizontal != 0 || inputController.Vertical != 0)
            {
                //transform.rotation = Quaternion.LookRotation(new Vector3(inputController.Horizontal, 0f, inputController.Vertical));
                float targetAngle = Mathf.Atan2(movermentCharacter.x, movermentCharacter.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f)*Vector3.forward+Physics.gravity;
                Archer.Move(moveDir * speed * Time.deltaTime);

                isMove = true;
            }

             
            else if (inputController.Horizontal == 0 && inputController.Vertical == 0)
            {
                isMove = false;
            }

        }
       

        void CheckAnimator()
        {
            animatorController = GetComponent<Animator>();
            animatorController.SetBool("Aim", false);
            animatorController.SetBool("Fire", false);
            bool moveCharater = animatorController.GetBool("Run");
            if (isMove && !moveCharater)
            {
                animatorController.SetBool("Run", true);
            }
            else if (!isMove && moveCharater)
            {
                animatorController.SetBool("Run", false);
            }
        }
        //void Rotation()
        //{
            
        //}
        //void Shoot()
        //{
        //    GameObject arrow = Instantiate(arrowObject, arrowPoint.position, transform.rotation);
        //    arrow.GetComponent<Rigidbody>().AddForce(transform.forward * 25f, ForceMode.Impulse);
        //}
        public void Fire()
        {
            bool isAim = animatorController.GetBool("Aim");
            if (isAim)
            {
                animatorController.SetBool("Fire", true);
            }
        }
        //public void IsShoot()
        //{
        //    GameObject arrow = Instantiate(arrowObject, arrowPoint.position, transform.rotation);
        //    arrow.GetComponent<Rigidbody>().AddForce(transform.forward * 25f, ForceMode.Impulse);
        //}
        public void onAim()
        {
            animatorController.SetBool("Aim", true);
        }
        
    }
}
