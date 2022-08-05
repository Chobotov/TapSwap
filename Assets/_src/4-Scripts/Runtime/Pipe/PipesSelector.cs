using System;
using TapSwap.Game;
using TapSwap.Runtime.App;
using UnityEngine;

namespace TapSwap.Pipe
{
    public class PipesSelector : MonoBehaviour
    {
        [SerializeField] private Pipe[] _pipes;
        
        private Pipe selectedPipe;
        private bool isPaused;
        
        private void Awake()
        {
            GameState.GameStateChange += OnGameStateChanged;
            
            foreach (var pipe in _pipes)
            {
                pipe.PipeSelected += OnPipeClicked;
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
            var colorsEquals = color1 == color2;
            CircleTouchPipe?.Invoke(colorsEquals);
        }
        
        private void OnGameStateChanged(GameState.State state)
        {
            isPaused = state == GameState.State.Pause;
        }

        public void AllPipesToDefaultPosition()
        {
            foreach (var pipe in _pipes)
            {
                pipe.Down();
            }
        }

        public Action<bool> CircleTouchPipe { get; set; }
    }
}