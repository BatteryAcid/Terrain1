using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO: try to use character controller
public class ThirdPersonCameraController : MonoBehaviour
{
   public float RotationSpeed = 1;
   public Transform Target, Player, camTransform;
   float mouseX, mouseY;
   //private float distance = 10.0f;
   private float zoom;
   public float zoomSpeed = 2;
   public float zoomMin = -2f;
   public float zoomMax = -10f;

   void Start()
   {
      Cursor.visible = false;
      Cursor.lockState = CursorLockMode.Locked;

      camTransform = transform;
      zoom = -3;
   }

   void LateUpdate()
   {
      // CamControl();
      CamControl2();
   }

   // void CamControl()
   // {
   //    mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
   //    mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed;
   //    mouseY = Mathf.Clamp(mouseY, -35, 60);

   //    transform.LookAt(Target);
   //    Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
   //    Player.rotation = Quaternion.Euler(0, mouseX, 0);
   // }

   private void CamControl2()
   {
      zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

      if (zoom > zoomMin)
         zoom = zoomMin;

      if (zoom < zoomMax)
         zoom = zoomMax;

      mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
      mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed;
      mouseY = Mathf.Clamp(mouseY, -35, 60);

      //Vector3 dir = new Vector3(0, 0, -distance);
      Vector3 dir = new Vector3(0, 0, zoom);

      Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);
      camTransform.position = Target.position + rotation * dir;
      camTransform.LookAt(Target.position);

      Player.rotation = Quaternion.Euler(0, mouseX, 0);
   }
}
