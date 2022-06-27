using System.Collections.Generic;
using UnityEngine;

public class ReactivePresenter: MonoBehaviour
{
   [SerializeField] private Transform _screensRoot = null;

   private LevelsController _levelsController = null;
   
   public ScoreSystem ScoreSystem { get; private set; }

   private List<IUIElementsHandler> _elementsHandlers = new List<IUIElementsHandler>();
   
   public void Initialize(LevelsController levelsController)
   {
      Initialize();
      
      ScoreSystem = new ScoreSystem();
      _levelsController = levelsController;
      
      Register();
   }

   private void Initialize()
   {
      foreach (Transform screenTransform in _screensRoot)
      {
         if (TryToGetHandler(screenTransform, out IUIElementsHandler handler))
         {
            handler.Register();
            
            _elementsHandlers.Add(handler);
         }
      }
   }

   private void Register()
   {
      foreach (IUIElementsHandler elementsHandler in _elementsHandlers)
      {
         switch (elementsHandler)
         {
            case UITextHandler textHandler:
               textHandler.Subscribe(UITextHandler.EUITextType.GAME_SCORE, ScoreSystem.CurrentScore);
               break;
            case UIButtonHandler buttonHandler:
               buttonHandler.Subscribe(UIButtonHandler.EUIButtonType.WIN_CONTINUE, _levelsController.GoToNextLevel);
               break;
         }
      }
   }

   private bool TryToGetHandler<T>(Transform screenTransform, out T handler) where T: IUIElementsHandler
   {
      if (screenTransform.TryGetComponent(out T returnedHandler))
      {
         handler = returnedHandler;
         return true;
      }
      
      handler = default;
      return false;
   }
}
