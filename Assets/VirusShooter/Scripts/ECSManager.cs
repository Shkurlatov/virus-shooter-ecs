using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class ECSManager : MonoBehaviour
{
    public static EntityManager manager;

    [SerializeField] private GameObject virusPrefab;
    [SerializeField] private GameObject redBloodPrefab;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject player;

    int numVirus = 500;
    int numRedBlood = 500;
    int numBullets = 10;

    BlobAssetStore store;

    Entity bullet;

    void Start()
    {
        store = new BlobAssetStore();
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, store);
        Entity virus = GameObjectConversionUtility.ConvertGameObjectHierarchy(virusPrefab, settings);
        Entity redBlood = GameObjectConversionUtility.ConvertGameObjectHierarchy(redBloodPrefab, settings);
        bullet = GameObjectConversionUtility.ConvertGameObjectHierarchy(bulletPrefab, settings);

        for (int i = 0; i < numVirus; i++)
        {
            var instance = manager.Instantiate(virus);
            float x = UnityEngine.Random.Range(-50, 50);
            float y = UnityEngine.Random.Range(-50, 50);
            float z = UnityEngine.Random.Range(-50, 50);

            float3 position = new float3 (x, y, z);
            manager.SetComponentData(instance, new Translation { Value = position });

            float rSpeed = UnityEngine.Random.Range(1, 10) / 10.0f;
            manager.SetComponentData(instance, new FloatData { Speed = rSpeed });
        }

        for (int i = 0; i < numRedBlood; i++)
        {
            var instance = manager.Instantiate(redBlood);
            float x = UnityEngine.Random.Range(-50, 50);
            float y = UnityEngine.Random.Range(-50, 50);
            float z = UnityEngine.Random.Range(-50, 50);

            float3 position = new float3 (x, y, z);
            manager.SetComponentData(instance, new Translation { Value = position });

            float rSpeed = UnityEngine.Random.Range(1, 10) / 10.0f;
            manager.SetComponentData(instance, new FloatData { Speed = rSpeed });
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < numBullets; i++)
            {
                var instance = manager.Instantiate(bullet);
                var startPosition = player.transform.position + UnityEngine.Random.insideUnitSphere * 2;
                manager.SetComponentData(instance, new Translation { Value = startPosition });
                manager.SetComponentData(instance, new Rotation { Value = player.transform.rotation });
            }
        }
    }

    void OnDestroy()
    {
        store.Dispose();
    }
}
