﻿using UnityEngine;

namespace Assets.Scripts.LevelGeneration
{
    [CreateAssetMenu(fileName = "MeshGenerationSettings", menuName = "ScriptableObjects/MeshGenerationSettings")]
    public class MeshGenerationSettings : ScriptableObject
    {
        public Vector3 StartedPoint;

        public int Size;

        public float XOffsetBetweenParts;
    }
}
