using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class OutcomeHandler : MonoBehaviour
{
   public Text outcomeText;

   private SceneHandlerService _sceneHandlerService;

   [Inject]
   public void Construct(SceneHandlerService sceneHandlerService) {
      _sceneHandlerService = sceneHandlerService;
   }

   public void setText(string text) {
      Debug.Log("OutcomeHandler set text");
      outcomeText.text = _sceneHandlerService.Outcome;
   }

   // Start is called before the first frame update
   void Start()
   {
      outcomeText.text = _sceneHandlerService.Outcome;//"start";
   }

   // Update is called once per frame
   void Update()
   {
      outcomeText.text = _sceneHandlerService.Outcome;
   }
}
