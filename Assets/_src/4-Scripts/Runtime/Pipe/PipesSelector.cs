using System;
using TapSwap.Runtime.App;
using UnityEngine;

namespace TapSwap.Pipe
{
    public class PipesSelector : MonoBehaviour, IPauseble
    {
        [SerializeField] private Pipe[] _pipes;
        
        private Pipe selectedPipe;
        private bool isPaused;
        
        private void Awake()
        {
            GameState.GameStateChange += OnGameStateChanged;
            
            foreach (var pipe in _pipes)
            {
                pipe.PipeClicked += OnPipeClicked;
                pipe.CircleTouch += OnCircleTouchPipe;
            }
        }

        private void Start()
        {
            DI.Add(this);
        }

        private void SwitchPipesPositions(Pipe firstPipe, Pipe secondPipe)
        {
            var position = firstPipe.transform.position;
            
            firstPipe.transform.position = secondPipe.transform.position;
            secondPipe.transform.position = position;
        }

        private void OnPipeClicked(Pipe pipe)
        {
            if (isPaused) return;

            if (selectedPipe != null)
            {
                if (selectedPipe == pipe)
                {
                    selectedPipe.Down();
                }
                else
                {
                    selectedPipe.Down();
                
                    SwitchPipesPositions(selectedPipe, pipe);
                }

                selectedPipe = null;
            }
            else
            {
                selectedPipe = pipe;
                selectedPipe.Up();
            }
        }

        private void OnCircleTouchPipe(Color color1, Color color2)
        {
            CircleTouchPipe?.Invoke(color1.Equals(color2));
        }

        public Action<bool> CircleTouchPipe { get; set; }

        public bool IsPaused => isPaused;

        public void OnGameStateChanged(GameState.State state)
        {
            isPaused = state == GameState.State.Pause;
        }
    }
}