using Leopotam.Ecs;

namespace MY.Scripts.Extensions
{
    public static class EcsWorldExtension
    {
        public static void SendMessage<T>(this EcsWorld world, in T messageEvent)
            where T : struct
        {
            ref var eventComponent = ref world.NewEntity().Get<T>();
            eventComponent = messageEvent;
        }
    }
}