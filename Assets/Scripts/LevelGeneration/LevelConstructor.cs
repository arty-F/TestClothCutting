using UnityEngine;

namespace Assets.Scripts.LevelGeneration
{
    public class LevelConstructor : MonoBehaviour
    {
        private void Awake()
        {
            var meshGenerator = new MeshGenerator();

            var mesh = meshGenerator.GenerateMesh(10, 10, Vector3.zero);
            GetComponent<MeshFilter>().mesh = mesh;
            GetComponent<MeshCollider>().sharedMesh = mesh;

            var cloth = gameObject.AddComponent(typeof(Cloth)) as Cloth;
            cloth.useGravity = false;
        }

        private void Start()
        {

        }
    }
}
