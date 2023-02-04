using UnityEngine;

namespace RootBoy
{

    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController controller; // ci serve un riferimento al character controller
        public float speed = 12f; // vogliamo controllare la velocit� del giocatore

        public float gravity = -9.81f;
        Vector3 velocity;

        public Transform groundCheck; // riferimento alla nostra empty posta alla fine del cilindro
        public float groundDistance = 0.4f; // raggio della sfera per controllare la distanza dal terreno
        public LayerMask groundMask; // vogliamo definire quali oggetti la sfera dovr� controllare
        bool isGrounded; // se il player � a terra oppure no

        public float jumpHeight = 3f;

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
            if (isGrounded && velocity.y < 0) // resettiamo la velocit�
            {
                velocity.y = -2.5f; // meglio di 0, perch� il calcolo potrebbe avvenire prima che siamo a terra
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); // v= Sqrt (h * - 2 * g)
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime); // Delta y = 1/2 g * t ^ 2 - fisica della caduta libera
            
            Vector3 move = transform.right * x + transform.forward * z;
            // vogliamo le coordinate locali, perch� il personaggio pu� ruotare
            controller.Move(move * speed * Time.deltaTime);
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