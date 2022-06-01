using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.LevelGeneration
{
    public class CuttedMeshGenerator : IMeshGenerator
    {
        private readonly MeshGenerationSettings settings;

        private readonly Vector2[] cuttingPoints;

        private readonly CuttedMeshPart meshPart;

        public CuttedMeshGenerator(MeshGenerationSettings settings, Vector3[] cuttingPoints, CuttedMeshPart meshPart)
        {
            this.settings = settings;
            this.cuttingPoints = CalculateCuttingPointsForAllYCoords(cuttingPoints);
            this.meshPart = meshPart;
        }

        public Mesh GenerateMesh()
        {
            var mesh = new Mesh();
            mesh.Clear();

            mesh.vertices = CreateVertices(settings.XSize, settings.YSize, settings.StartedPoint);
            mesh.triangles = CreateTriangles(settings.XSize, settings.YSize);
            mesh.uv = CreateUVs(settings.XSize, settings.YSize);

            mesh.RecalculateNormals();

            return mesh;
        }

        private Vector3[] CreateVertices(int xSize, int ySize, Vector3 startPoint)
        {
            var vertices = new Vector3[xSize * ySize];

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    var currentX = (float)x;
                    if (x > cuttingPoints[y].x)
                    {
                        currentX = cuttingPoints[y].x;
                    }

                    vertices[y * ySize + x] = new Vector3(currentX, y, 0f) + startPoint;
                }
            }

            return vertices;
        }


        private int[] CreateTriangles(int xSize, int ySize)
        {
            var triangles = new int[(xSize - 1) * (ySize - 1) * 6];

            for (int x = 0, t = 0; x < xSize - 1; x++)
            {
                var firstVerticeInColumn = ySize * x;

                for (int y = 0; y < ySize - 1; y++)
                {
                    triangles[t] = firstVerticeInColumn + y;
                    triangles[t + 1] = triangles[t + 4] = firstVerticeInColumn + 1 + y;
                    triangles[t + 2] = triangles[t + 3] = firstVerticeInColumn + ySize + y;
                    triangles[t + 5] = firstVerticeInColumn + ySize + 1 + y;
                    t += 6;
                }
            }

            return triangles;
        }

        private Vector2[] CreateUVs(int xSize, int ySize)
        {
            var UVs = new Vector2[xSize * ySize];

            for (int i = 0, x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    UVs[i] = new Vector2((float)x / xSize, (float)y / ySize);
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
