using UnityEngine;

public class TestOpponent : MonoBehaviour
{
   public ThirdPersonCharacterController controller;

   void Start()
   {
      // hack to keep things short
      controller =  FindObjectOfType<ThirdPersonCharacterController>();
   }

   //Detect collisions between the GameObjects with Colliders attached
   void OnCollisionEnter(Collision collision)
   {
      //Check for a match with the specified name on any GameObject that collides with your GameObject
      if (collision.gameObject.name == "Ball")
      {
         Debug.Log("Block Hit! ******");

         // hack to trigger our main controller we have a block hit
         controller.blockHit();
      }
   }
}
