using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
   const float locomationAnimationSmoothTime = .1f;
   Rigidbody playerRigidbody;
   Animator animator;

   // Start is called before the first frame update
   void Start()
   {
      animator = GetComponentInChildren<Animator>();
      playerRigidbody = GetComponent<Rigidbody>(); 
   }

   // Update is called once per frame
   void Update()
   {
      // Vector3 horizontalVelocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
      // float speedPercent = rigidbody.velocity.magnitude / Time.deltaTime; //horizontalVelocity.magnitude / 10;//.velocity.magnitude; //rigidbody.velocity.magnitude / horizontalVelocity.magnitude;
      // string debug = horizontalVelocity.magnitude + ", " + rigidbody.velocity.magnitude + ", " + speedPercent;
      // print("debug: " + debug);

      // print(rigidbody.velocity.magnitude * 10 * Time.deltaTime);
      // speedPercent = rigidbody.velocity.magnitude * 10 * Time.deltaTime;
      // print("debug: " + debug);

      // float speedPercent = 0f;
      // float playerSpeed = playerRigidbody.velocity.magnitude;
      // if (playerSpeed <= .01f) {
      //    speedPercent = .2f;
      // } else if (playerSpeed <= .1f) {
      //    speedPercent = .5f;
      // } else if (playerSpeed <= .5f) {
      //    speedPercent = .8f;
      // } else {
      //    speedPercent = 1.0f;
      // }

      // print("speedPercent: " + speedPercent);
      // animator.SetFloat("speedPercent", speedPercent, locomationAnimationSmoothTime, Time.deltaTime);
   }
}
