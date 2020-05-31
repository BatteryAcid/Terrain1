using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOpponent : MonoBehaviour
{
   //Detect collisions between the GameObjects with Colliders attached
   void OnCollisionEnter(Collision collision)
   {
      //Check for a match with the specified name on any GameObject that collides with your GameObject
      if (collision.gameObject.name == "Ball")
      {
         Debug.Log("Opponent Hit! ******");
      }
   }
}
