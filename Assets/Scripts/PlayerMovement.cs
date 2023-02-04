using UnityEngine;

namespace RootBoy
{

    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController controller; 
        public float speed = 12f; 

        public float gravity = -9.81f;
        Vector3 velocity;

        public Transform groundCheck; 
        public float groundDistance = 0.4f; 
        public LayerMask groundMask; 
        bool isGrounded; 

        public float jumpHeight;

        [SerializeField]
        private float gravityMultiplier;

        [SerializeField]
        private float jumpBottonGracePeriod;

        private float yspeed;
        private float originalStepOffset;
        private float? jumpBottonPressedTime;
        private float? lastGroundedTime;
        private bool isJumping;


        private void Start()
        {
            PlayerHealth.OnPlayerDead += DisableComponent;
        }

        private void OnDestroy()
        {
            PlayerHealth.OnPlayerDead -= DisableComponent;
        }

        // Update is called once per frame
        void Update()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded && velocity.y < 0) 
            {
                velocity.y = -2.5f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            float gravity = Physics.gravity.y * gravityMultiplier;
            yspeed += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime); 
            
            Vector3 move = transform.right * x + transform.forward * z;
           
            controller.Move(move * speed * Time.deltaTime);

            velocity.y = yspeed;

            if (isJumping && yspeed > 0 && Input.GetButtonDown("Jump") == false)
            {
                gravity *= 2;
            }

            if (controller.isGrounded)
            {
                lastGroundedTime = Time.time;
            }

            yspeed += Physics.gravity.y * Time.deltaTime;

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                yspeed = jumpHeight; //velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); 
            }

            if (Time.time - lastGroundedTime <= jumpBottonPressedTime)
            {
                controller.stepOffset = originalStepOffset;
                yspeed = -0.5f;
                isGrounded = true;
                isJumping = false;


                if (Time.time - jumpBottonPressedTime <= jumpBottonGracePeriod)
                {
                    yspeed = Mathf.Sqrt(jumpHeight * -3 * gravity);
                    isJumping = true;
                    jumpBottonPressedTime = null;
                    lastGroundedTime = null;
                }
            }

            else
            {
                controller.stepOffset = 0;
                isGrounded = false;
            }
                

            
        }


        private void DisableComponent()
        {
            GetComponent<Rigidbody>().Sleep();
            GetComponent<MeshRenderer>().enabled = false;
            controller.enabled = false; 
            enabled = false;
        }
    }
}