using UnityEngine;

namespace Assets.Scripts.LevelGeneration
{
    [CreateAssetMenu(fileName = "LevelObjectsSettings", menuName = "ScriptableObjects/LevelObjectsSettings")]
    public class LevelObjectsSettings : ScriptableObject
    {
        public GameObject ClothPrefab;

        public GameObject CubePrefab;
    }
}
