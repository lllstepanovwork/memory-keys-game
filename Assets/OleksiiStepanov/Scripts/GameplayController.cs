using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

namespace OleksiiStepanov.Skillshare.MVC
{
    public class GameplayController : MonoBehaviour
    {
        private enum GameState
        {
            Remembering,
            Gameplay,
            None
        }

        [Header("Settings")]
        [SerializeField] private Combination combination;

        [Header("Views")]
        [SerializeField] private CombinationView combinationView;
        [SerializeField] private UIView uiView;
        [SerializeField] private TimerView timerView;

        private int currentLevelNumber = 1;
        private int correctKeyCounter = 0;

        private GameState currentGameState = GameState.None;

        private void Start()
        {
            combination.CreateCombination();
            InitRemembering();
        }

        private void InitRemembering()
        {
            currentGameState = GameState.Remembering;

            combinationView.Init(combination);
            uiView.Init(currentLevelNumber);
            uiView.SetMessageText(Constants.REMEMBER_COMBINATION_TEXT);

            timerView.StartTimer(combination.keyCombination.Count);
        }

        private void StartGame()
        {
            InitGameplayState();
            timerView.StartTimer(combination.keyCombination.Count);
        }

        private void InitGameplayState()
        {
            currentGameState = GameState.Gameplay;
            combinationView.HideCombination();
            uiView.SetMessageText(Constants.ENTER_COMBINATION_TEXT);
        }

        private void ResetState()
        {
            currentGameState = GameState.None;
            correctKeyCounter = 0;
        }

        private void Update()
        {
            if (currentGameState == GameState.Gameplay)
            {
                DetectKeyPress();
            }
        }

        private void DetectKeyPress()
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        CheckWin(keyCode);
                        break;
                    }
                }
            }
        }

        private void CheckWin(KeyCode keyCode)
        {
            bool result = combinationView.TryToSetCombinationKeyAsCorrect(keyCode);

            if (result)
            {
                correctKeyCounter++;
            }
            else
            {
                OnWrongKeyPressedAsync().Forget();
            }

            if (correctKeyCounter == combination.keyCombination.Count)
            {
                OnCombinationCompleteAsync().Forget();
            }
        }

        private async UniTask OnCombinationCompleteAsync()
        {
            ResetState();
            uiView.SetMessageText(Constants.WIN_TEXT);
            combination.CreateCombination();

            currentLevelNumber++;

            await UniTask.Delay(1000);
            InitRemembering();
        }

        private async UniTask OnWrongKeyPressedAsync()
        {
            ResetState();
            uiView.SetMessageText(Constants.FAIL_TEXT);

            await UniTask.Delay(1000);
            InitGameplayState();
        }

        private async void GameOverAsync()
        {
            ResetState();
            combinationView.ShowCombination();
            uiView.SetMessageText(Constants.FAIL_TEXT);

            await UniTask.Delay(1000);
            InitRemembering();
        }

        private void OnEnable()
        {
            TimerView.OnTimerElapsed += HandleTimerElapsed;
        }

        private void OnDisable()
        {
            TimerView.OnTimerElapsed -= HandleTimerElapsed;
        }

        private void HandleTimerElapsed()
        {
            if (currentGameState == GameState.Remembering)
            {
                StartGame();
            }
            else if (currentGameState == GameState.Gameplay)
            {
                GameOverAsync();
            }
        }
    }
}