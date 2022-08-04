using System.Linq;
using TapSwap.Managers.Speed;
using TapSwap.Runtime.App;
using TapSwap.Utils;
using UnityEngine;

namespace TapSwap.DropItem
{
    public class SpawnItems : MonoBehaviour
    {
        [SerializeField] private Color[] _colors;
        [SerializeField] private DropItem[] _itemsToSpawn;
        [SerializeField] private Transform[] _pipes;

        private ISpeedManager _speedManager;

        private bool _isPaused;
        
        private void Start()
        {
            DI.Add(this);

            _speedManager = DI.Get<ISpeedManager>();

            GameState.GameStateChange += OnGameStateChanged;
        }

        private void Update()
        {
            foreach (var item in _itemsToSpawn)
            {
                item.Tick();
            }
        }

        public void HideItems()
        {
            foreach (var item in _itemsToSpawn)
            {
                item.Deactivate();
            }
        }

        public void Spawn()
        {
            if (_isPaused) return;
            
            if (_itemsToSpawn.Count(x => x.gameObject.activeSelf) >= 3) return;
            
            var dropItem = _itemsToSpawn.FirstOrDefault(x => !x.gameObject.activeSelf);
            
            if (dropItem == null) return;

            var xPos = _pipes.GetRandomItem().position.x;
            var pos = new Vector3(xPos, transform.position.y);

            var color = _colors.GetRandomItem();
            
            dropItem
                .SetPosition(pos)
                .SetColor(color)
                .SetSpeed(_speedManager.CurrentSpeed)
                .Activate();
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
