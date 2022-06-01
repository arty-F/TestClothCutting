using UnityEngine;

namespace Assets.Scripts.LevelGeneration
{
    public class MeshGenerator : IMeshGenerator
    {
        //var mesh = GenerateMesh(xSize, ySize, Vector3.zero);
        //GetComponent<MeshFilter>().mesh = mesh;
        //GetComponent<MeshCollider>().sharedMesh = mesh;

        public Mesh GenerateMesh(int xSize, int ySize, Vector3 startedPoint)
        {
            var mesh = new Mesh();
            mesh.Clear();

            mesh.vertices = CreateVertices(xSize, ySize, startedPoint);
            mesh.triangles = CreateTriangles(xSize, ySize);
            mesh.uv = CreateUVs(xSize, ySize);

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
                    vertices[y * ySize + x] = new Vector3(x, y, 0f) + startPoint;
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




    }
}
