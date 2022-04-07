using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Class3
{
    public class PlayerInputComponent : MonoBehaviour
    {
        public float MovementSpeed;
        private GameObject TestGameObject;
        public Rigidbody rigidbody;
        public Transform CameraContainer;
        public float DistanceToGround;
        private Vector3 processedInput;
        private Vector3 processedMovementInput;
        private float processedLookInput;
        private float ProcessedTurnInput;
        public float JumpHeight;
        public float JumpSpeed;
        public float LookSpeed;
        bool IsJumping;
        private bool Jumping;
        public float TurnSpeed;
        public Animator BearAnim;
        [SerializeField] Text HealthText;
        int PlayerHealth = 100;
        [SerializeField] GameObject AmmoCrate;
        [SerializeField] GameObject HealthCrate;
        void OnEnable()
        {
            // Gameobject...The load method, is a generic method that is able to accept parameters of any type.
            TestGameObject = Resources.Load<GameObject>("ResourcesTestPrefab"); // Load is static


        }
        private Rigidbody rb;
        // Start is called before the first frame update
        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            BearAnim.GetComponent<Animator>();
            SetHealthText();           
        }
        
        public void SetHealthText()
        {
            HealthText.text = "Health:" + PlayerHealth.ToString();
        }
        public void TakeDamage(int DamageAmt)
        {
            PlayerHealth -= DamageAmt;
            SetHealthText();
        }
        public void Heal(int HealAmt)
        {
            PlayerHealth += HealAmt;
            if(PlayerHealth>100)
            {
                PlayerHealth = 100;
            }
            SetHealthText();
        }
        public void Die()
        {
            if(PlayerHealth<=0)
            {
                SceneManager.LoadScene("SampleScene");
            }
        }

        // Update is called once per frame
        private void Update()
        {
            Cursor.lockState = CursorLockMode.Locked;          
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");
            processedMovementInput = transform.forward * verticalInput + transform.right * horizontalInput;
            if (BearAnim)
            {
                BearAnim.SetFloat("HorizontalInput", horizontalInput);
                BearAnim.SetFloat("VerticalInput", verticalInput);
            }
            //var processedInput = new Vector3(horizontalInput, 0, verticalInput);
            //transform.Translate(processedInput*MovementSpeed*Time.deltaTime);// Time between frames. Every time you move for update, you need to use Time.deltaTime.
            //Debug.Log($"X:{rigidbody.velocity.x}|Y:{rigidbody.velocity.z}");
            //Debug.Log($"Horizontal Input:{horizontalInput}|Vertical Input:{verticalInput}");       
            var turnInput = Input.GetAxis("Mouse X");
            ProcessedTurnInput = turnInput;
            var lookInput = Input.GetAxis("Mouse Y");
            processedLookInput = lookInput;
            CameraContainer.Rotate(new Vector3(processedLookInput, 0, 0) * LookSpeed * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
            {
                Vector3 JumpVelocity = new Vector3(0, JumpHeight, 0);
                rigidbody.velocity = transform.up + JumpVelocity;
                BearAnim.SetTrigger("Jump");
                Debug.Log("Jump!");

            }
            Die();
        }

        private void FixedUpdate()
        {
            rigidbody.MovePosition(transform.position + (processedMovementInput * MovementSpeed * Time.deltaTime));
            //Debug.Log($"X:{rigidbody.velocity.x}|Y:{rigidbody.velocity.z}");
            rigidbody.MoveRotation(Quaternion.Euler(transform.eulerAngles + (Vector3.up * ProcessedTurnInput) * TurnSpeed * Time.deltaTime));


            //Debug.Log(isGrounded());

        }
        bool isGrounded()
        {
            return Physics.Raycast(transform.position, Vector3.down, DistanceToGround);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag=="EnemyBullet")
            {
                TakeDamage(20);
            }
            if(collision.gameObject.tag=="HealthBag")
            {
                Heal(40);
                Destroy(HealthCrate);
            }
            
        }
        public void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<AmmoPickup>()!=null)
            {
                GetComponentInChildren<WeaponLogics>().HasPickedUpAmmo = true;
                Destroy(AmmoCrate);
            }
        }
    }
}

    

