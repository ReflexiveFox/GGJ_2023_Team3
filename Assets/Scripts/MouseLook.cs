﻿using UnityEngine;

namespace RootBoy
{
    public class MouseLook : MonoBehaviour
    {
        public float mouseSensitivity = 250f; // vogliamo controllare la velocità del nostro mouse
        public Transform playerBody; // ci serve un riferimento per far ruotare il nostro personaggio
        float yRotation = 0f;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked; // non vogliamo vedere il cursore quando ruotiamo
            PlayerHealth.OnPlayerDead += DisableComponent;
        }

        private void OnDestroy()
        {
            PlayerHealth.OnPlayerDead -= DisableComponent;
        }
        private void DisableComponent()
        {
            Cursor.lockState = CursorLockMode.None;
            enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

            yRotation += mouseX; // se incrementato, il movimento risulterebbe al contrario
            transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
        }
    }
}