using Leopotam.Ecs;

namespace MY.Scripts.Extensions
{
    public static class EcsFilterExtension
    {
        public static bool AllEntitiesEquals<T>(this EcsFilter filter)
            where T : struct
        {
            for (int i = 0; i < filter.GetEntitiesCount() - 1; i++)
            {
                ref var currentEntity = ref filter.GetEntity(i).Get<T>();
                ref var nextEntity = ref filter.GetEntity(i+1).Get<T>();
                if (!currentEntity.Equals(nextEntity))
                    return false;
            }
            return true;
        }    
    }
}