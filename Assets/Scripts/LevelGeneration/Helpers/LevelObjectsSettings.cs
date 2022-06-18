using UnityEngine;

namespace Assets.Scripts.LevelGeneration
{
    /// <summary>
    /// Stored prefabs of main gameplay objects.
    /// </summary>
    [CreateAssetMenu(fileName = "LevelObjectsSettings", menuName = "ScriptableObjects/LevelObjectsSettings")]
    public class LevelObjectsSettings : ScriptableObject
    {
        [Tooltip("Prefab of a cloth object.")]
        public GameObject ClothPrefab;

        [Tooltip("Player prefab.")]
        public GameObject CubePrefab;
    }
}
