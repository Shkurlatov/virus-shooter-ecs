using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;
using Unity.Physics;

public class TimedDestroySystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float dT = Time.DeltaTime;

        Entities
            .WithoutBurst()
            .WithStructuralChanges()
            .ForEach((Entity entity,
                        ref Translation position,
                        ref LifetimeData lifetimeData) =>
            {
                lifetimeData.LifeLeft -= dT;

                if (lifetimeData.LifeLeft <= 0f)
                {
                    EntityManager.DestroyEntity(entity);
                }
            })
            .Run();

        return inputDeps;
    }
}
