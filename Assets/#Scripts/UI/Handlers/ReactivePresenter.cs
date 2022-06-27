using UnityEngine;

public class ReactivePresenter: MonoBehaviour
{
   [SerializeField] private Transform _screensRoot = null;

   private LevelsController _levelsController = null;
   
   public ScoreSystem ScoreSystem { get; private set; }
   
   public void Initialize(LevelsController levelsController)
   {
      ScoreSystem = new ScoreSystem();
      _levelsController = levelsController;      
      
      Register();
   }

   private void Register()
   {
      RegisterTextUpdate();
   }

   private void RegisterTextUpdate()
   {
      foreach (Transform screenTransform in _screensRoot)
      {
         if (TryToGetHandler(screenTransform, out UITextHandler textHandler))
         {
            textHandler.Subscribe(UITextHandler.EUITextType.GAME_SCORE, ScoreSystem.CurrentScore);
         }

         if (TryToGetHandler(screenTransform, out UIButtonHandler buttonHandler))
         {
            buttonHandler.Subscribe(UIButtonHandler.EUIButtonType.WIN_CONTINUE, _levelsController.GoToNextLevel);
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
