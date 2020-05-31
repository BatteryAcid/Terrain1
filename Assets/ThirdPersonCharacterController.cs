using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{
   public float maxSpeed = 10;
   Animator animator;
   public Rigidbody player;
   public Rigidbody ball;
   public Transform playerCamera;
   public float baseThrust = 20.0f;
   private float yAxisSpawnPointAdjustment = 2.0f;
   private float throwKeyPressedStartTime;

   // Start is called before the first frame update
   void Start()
   {
      animator = GetComponentInChildren<Animator>();
      player = GetComponent<Rigidbody>();
   }

   void Update()
   {
      if (animator == null) return;

      float x = Input.GetAxis("Horizontal");
      float y = Input.GetAxis("Vertical");
      PlayerMovement(x, y);

      if (Input.GetMouseButtonDown(0))
      {
         throwKeyPressedStartTime = Time.time;
         Debug.Log("Pressed primary button. " + baseThrust);
      }

      if (Input.GetMouseButtonUp(0))
      {
         //Debug.Log((Time.time - throwKeyPressedStartTime).ToString("00:00.00"));
         float throwKeyPressedTime = Time.time - throwKeyPressedStartTime;

         if (ball != null)
         {
            Debug.Log("Ball good");

            Vector3 spawnPos = getSpawnPointInFrontOfPlayer();

            if (!Physics.CheckSphere(spawnPos, 0.5f))
            {
               ball.position = spawnPos;
               ball.velocity = new Vector3(0, 0, 0);

               ball.AddForce(determineVectorOfThrow() * determineThrustOfBall(throwKeyPressedTime), ForceMode.Impulse);
            }
            else
            {
               Debug.Log("Has overlapping colliders");
            }
         }
      }
   }

   Vector3 determineVectorOfThrow()
   {
      Vector3 cameraVector = playerCamera.transform.forward;
      return cameraVector;
   }

   float determineThrustOfBall(float throwKeyPressedTime)
   {
      float thrustOfBall = baseThrust * 1.25f;
      if (throwKeyPressedTime >= 1.5)
      {
         thrustOfBall = baseThrust * 2.5f;
      }
      else if (throwKeyPressedTime >= 1)
      {
         thrustOfBall = baseThrust * 2.0f;
      }
      else if (throwKeyPressedTime >= .5)
      {
         thrustOfBall = baseThrust * 1.75f;
      }

      Debug.Log("Throw speed: " + thrustOfBall);

      return thrustOfBall;
   }

   // Returns a point just in front of the facing direction/vector of the player
   Vector3 getSpawnPointInFrontOfPlayer()
   {
      Vector3 playerPosition = player.transform.position;
      Vector3 playerDirection = player.transform.forward;

      Vector3 spawnPos = playerPosition + playerDirection * 1.3f;

      // moves the spawn position up the Y axis 
      spawnPos.y = spawnPos.y + yAxisSpawnPointAdjustment;
      return spawnPos;
   }

   void PlayerMovement(float x, float y)
   {
      animator.SetFloat("velX", x);
      animator.SetFloat("velY", y);

      //print("hor: " + x + ", ver: " + y);
      Vector3 playerMovement = new Vector3(x, 0f, y) * maxSpeed * Time.deltaTime;

      //print("Player movement speed: " + playerMovement.magnitude);
      //print("Time.deltaTime: " + Time.deltaTime);
      transform.Translate(playerMovement, Space.Self);
   }

   // Use to debug what the object is overlapping with
   void printOverlapShereObjects(Vector3 spawnPos)
   {
      Collider[] hitColliders = Physics.OverlapSphere(spawnPos, 0.5f);
      int i = 0;
      while (i < hitColliders.Length)
      {
         Debug.Log(hitColliders[i].name);
         i++;
      }
   }
}
