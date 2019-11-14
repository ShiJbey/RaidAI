using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaidAI
{
    // Handles keyboad input to move and rotate the player
    //  As of (11/11/19) players may only move forwad and
    // and backward and rotate left and right
    public class PlayerMovement : MonoBehaviour
    {

        public float rotateSpeed = 1f;
        public float movementSpeed = 20f;

        // Start is called before the first frame update
        void Start()
        {

        }

        public void RotateActor(float axisValue)
        {
            Vector3 rotateVector = new Vector3(0f, 1f, 0f);
            rotateVector *= Time.deltaTime * rotateSpeed * axisValue;
            this.transform.rotation *= Quaternion.Euler(rotateVector);
        }

        public void MoveActor(float axisValue)
        {
            Vector3 movementVector = this.transform.forward.normalized * axisValue;
            this.transform.position += Time.deltaTime * movementSpeed * movementVector;
        }

        // Update is called once per frame
        void Update()
        {
            // Rotate the player
            //if (Input.GetAxisRaw("Horizontal") != 0)
            //{
            //    MoveActor(Input.GetAxis("Horizontal"));
            //}

            // Move the player
            //if (Input.GetAxisRaw("Vertical") != 0)
            //{
            //    MoveActor(Input.GetAxis("Vertical"));
            //}
        }
    }
}
