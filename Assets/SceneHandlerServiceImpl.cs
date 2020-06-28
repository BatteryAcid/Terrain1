using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHandlerServiceImpl : SceneHandlerService
{
   private string _message = "hello";

   public string Outcome
   {
      get
      {
         return _message;
      }

      set
      {
         _message = value;
      }
   }
}
