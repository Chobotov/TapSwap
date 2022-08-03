namespace TapSwap.Utils
{
    public static class ArrayExtension
    {
        public static T GetRandomItem<T>(this T[] arr)
        {
            return arr[UnityEngine.Random.Range(0, arr.Length)];
        }
    }
}