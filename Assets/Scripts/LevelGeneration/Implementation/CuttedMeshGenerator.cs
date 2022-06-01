using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.LevelGeneration
{
    public class CuttedMeshGenerator : ICuttedMeshGenerator
    {
        public Mesh GenerateMesh(MeshGenerationSettings settings, Vector3[] cuttingPoints, CuttedMeshPart meshPart)
        {
            var mesh = new Mesh();
            mesh.Clear();

            var fullCuttingPoints = CalculateCuttingPointsForAllYCoords(cuttingPoints);

            mesh.vertices = CreateVertices(settings, settings.StartedPoint, fullCuttingPoints, meshPart);
            mesh.triangles = CreateTriangles(settings.Size);
            mesh.uv = CreateUVs(settings.Size);

            mesh.RecalculateNormals();

            return mesh;
        }

        private Vector3[] CreateVertices(MeshGenerationSettings settings, Vector3 startPoint, Vector2[] cuttingPoints, CuttedMeshPart meshPart)
        {
            var vertices = new Vector3[settings.Size * settings.Size];
            var xOffset = settings.XOffsetBetweenParts * (int)meshPart;

            for (int x = 0; x < settings.Size; x++)
            {
                for (int y = 0; y < settings.Size; y++)
                {
                    var currentX = (float)x;
                    if (meshPart == CuttedMeshPart.Left && x > cuttingPoints[y].x ||
                        meshPart == CuttedMeshPart.Right && x < cuttingPoints[y].x)
                    {
                        currentX = cuttingPoints[y].x;
                    }

                    vertices[y * settings.Size + x] = new Vector3(currentX + xOffset, y, 0f) + startPoint;
                }
            }

            return vertices;
        }

        private int[] CreateTriangles(int size)
        {
            var triangles = new int[(size - 1) * (size - 1) * 6];

            for (int x = 0, t = 0; x < size - 1; x++)
            {
                var firstVerticeInColumn = size * x;

                for (int y = 0; y < size - 1; y++)
                {
                    triangles[t] = firstVerticeInColumn + y;
                    triangles[t + 1] = triangles[t + 4] = firstVerticeInColumn + 1 + y;
                    triangles[t + 2] = triangles[t + 3] = firstVerticeInColumn + size + y;
                    triangles[t + 5] = firstVerticeInColumn + size + 1 + y;
                    t += 6;
                }
            }

            return triangles;
        }

        private Vector2[] CreateUVs(int size)
        {
            var UVs = new Vector2[size * size];

            for (int i = 0, x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    UVs[i] = new Vector2((float)x / size, (float)y / size);
                    i++;
                }
            }

            return UVs;
        }

        private Vector2[] CalculateCuttingPointsForAllYCoords(Vector3[] cuttingPoints)
        {
            List<Vector2> points = new List<Vector2>();

            for (int i = 0; i < cuttingPoints.Length - 1; i++)
            {
                points.Add(cuttingPoints[i]);

                for (int y = (int)cuttingPoints[i].y + 1; y < (int)cuttingPoints[i + 1].y; y++)
                {
                    var x = ((y - cuttingPoints[i].y) *
                        (cuttingPoints[i + 1].x - cuttingPoints[i].x) /
                        (cuttingPoints[i + 1].y - cuttingPoints[i].y))
                        + cuttingPoints[i].x;

                    points.Add(new Vector2(x, y));
                }
            }

            return points.ToArray();
        }
    }
}
