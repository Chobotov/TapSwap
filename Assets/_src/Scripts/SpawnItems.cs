using UnityEngine;

namespace TapSwap
{
    public class SpawnItems : MonoBehaviour
    {
        [SerializeField] private DropItem[] _itemsToSpawn;
        [SerializeField] private Vector3[] _spawnXY;

        public void Spawn()
        {
            var itemIndex = Random.Range(0, _itemsToSpawn.Length);
            var posIndex = Random.Range(0, _spawnXY.Length);
            var pos = new Vector3(_spawnXY[posIndex].x, transform.position.y);

            var dropItem = _itemsToSpawn[itemIndex];
            dropItem.transform.position = pos;
            dropItem.gameObject.SetActive(true);
        }
    }
}
