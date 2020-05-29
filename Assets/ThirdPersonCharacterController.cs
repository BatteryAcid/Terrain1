using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{
   public float Speed;

   public float maxSpeed = 10;

   Animator animator;

   // Start is called before the first frame update
   void Start()
   {
      animator = GetComponentInChildren<Animator>();
   }

   void Update()
   {
      if (animator == null) return;

      float x = Input.GetAxis("Horizontal");
      float y = Input.GetAxis("Vertical");
      PlayerMovement(x, y);
   }

   void PlayerMovement(float x, float y)
   {
      
      animator.SetFloat("velX", x);
      animator.SetFloat("velY", y);

      //print("hor: " + x + ", ver: " + y);
      Vector3 playerMovement = new Vector3(x, 0f, y) * Speed * Time.deltaTime;

      // transform.position += (Vector3.forward * maxSpeed) * y * Time.deltaTime;
      // transform.position += (Vector3.right * maxSpeed) * x * Time.deltaTime;

      //print("Player movement speed: " + playerMovement.magnitude);
      //print("Time.deltaTime: " + Time.deltaTime);
      //Was working: 
      transform.Translate(playerMovement, Space.Self);
   }
}
