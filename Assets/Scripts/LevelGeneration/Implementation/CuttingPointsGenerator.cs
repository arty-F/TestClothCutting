using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.LevelGeneration
{
    public class CuttingPointsGenerator : ICuttingPointsGenerator
    {
        private readonly CuttingPointsGenerationSettings pointSettings;

        private readonly MeshGenerationSettings meshSettings;

        public CuttingPointsGenerator(CuttingPointsGenerationSettings pointSettings, MeshGenerationSettings meshSettings)
        {
            this.pointSettings = pointSettings;
            this.meshSettings = meshSettings;
        }

        public Vector3[] GenerateCuttingPoints()
        {
            var xCenter = (float)meshSettings.Size / 2 + meshSettings.StartedPoint.x;
            var xDirection = UnityEngine.Random.Range(0, 1) * 2 - 1;

            var yCuttingDistanceMin = Mathf.RoundToInt(pointSettings.YCuttingDistanceMin);
            var yCuttingDistanceMax = Mathf.RoundToInt(pointSettings.YCuttingDistanceMax) + 1;

            var points = new List<Vector3>();
            var y = meshSettings.StartedPoint.y;
            var yMax = y + meshSettings.Size;
            points.Add(new Vector3(xCenter, y, meshSettings.StartedPoint.z));

            while (y < yMax)
            {
                var nextYOffset = UnityEngine.Random.Range(yCuttingDistanceMin, yCuttingDistanceMax);
                y += nextYOffset;
                if (y > yMax)
                {
                    y = yMax;
                }

                var nextXOffset = UnityEngine.Random.Range(pointSettings.XCuttingDistanceMin, pointSettings.XCuttingDistanceMax + 1);
                var x = nextXOffset * xDirection + xCenter;
                xDirection *= -1;

                points.Add(new Vector3(x, y, meshSettings.StartedPoint.z));
            }

            return points.ToArray();
        }
    }
}
