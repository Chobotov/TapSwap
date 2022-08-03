using System.Linq;
using TapSwap.Runtime.App;
using TapSwap.Utils;
using UnityEngine;

namespace TapSwap.DropItem
{
    public class SpawnItems : MonoBehaviour, IPauseble
    {
        [SerializeField] private Color[] _colors;
        [SerializeField] private DropItem[] _itemsToSpawn;
        [SerializeField] private Transform[] _pipes;

        private bool _isPaused;
        
        private void Start()
        {
            DI.Add(this);

            GameState.GameStateChange += OnGameStateChanged;
        }

        private void Update()
        {
            foreach (var item in _itemsToSpawn)
            {
                item.Tick();
            }
        }

        public void Spawn()
        {
            if (_isPaused) return;
            
            var dropItem = _itemsToSpawn.FirstOrDefault(x => !x.gameObject.activeSelf);
            
            var xPos = _pipes.GetRandomItem().position.x;
            var pos = new Vector3(xPos, transform.position.y);

            var color = _colors.GetRandomItem();
            
            dropItem
                .SetPosition(pos)
                .SetColor(color)
                .SetSpeed(1f)
                .StartFalling();
        }

        public bool IsPaused => _isPaused;

        public void OnGameStateChanged(GameState.State state)
        {
            _isPaused = state == GameState.State.Pause;

            foreach (var item in _itemsToSpawn)
            {
                item.SetPause(_isPaused);
            }
        }
    }
}
