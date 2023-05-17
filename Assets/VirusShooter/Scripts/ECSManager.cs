using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class ECSManager : MonoBehaviour
{
    public static EntityManager manager;
    public GameObject virusPrefab;

    int numVirus = 500;
    BlobAssetStore store;

    void Start()
    {
        store = new BlobAssetStore();
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, store);
        Entity virus = GameObjectConversionUtility.ConvertGameObjectHierarchy(virusPrefab, settings);

        for (int i = 0; i < numVirus; i++)
        {
            var instance = manager.Instantiate(virus);
            float x = UnityEngine.Random.Range(-50, 50);
            float y = UnityEngine.Random.Range(-50, 50);
            float z = UnityEngine.Random.Range(-50, 50);

            float3 position = new float3 (x, y, z);
            manager.SetComponentData(instance, new Translation { Value = position });
        }
    }

    void OnDestroy()
    {
        store.Dispose();
    }
}
