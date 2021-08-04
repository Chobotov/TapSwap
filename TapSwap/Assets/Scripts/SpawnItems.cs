using UnityEngine;

namespace TapSwap
{
    public class SpawnItems : MonoBehaviour
    {
        [SerializeField] private DropItem[] itemsToSpawn;
        [SerializeField] private Vector3[] spawnXY;

        public void Spawn()
        {
            var itemIndex = Random.Range(0, itemsToSpawn.Length);
            var posIndex = Random.Range(0, spawnXY.Length);
            var pos = new Vector3(spawnXY[posIndex].x, transform.position.y);

            Instantiate(itemsToSpawn[itemIndex], pos, Quaternion.identity);
        }
    }
}
