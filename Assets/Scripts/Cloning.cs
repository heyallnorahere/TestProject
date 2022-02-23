using SGE;
using SGE.Components;
using SGE.Events;

namespace TestProject.Assets.Scripts
{
    public sealed class Cloning : Script
    {
        public void OnEvent(Event @event)
        {
            var dispatcher = new EventDispatcher(@event);
            dispatcher.Dispatch<KeyPressedEvent>(OnKey);
        }

        private bool OnKey(KeyPressedEvent @event)
        {
            if (@event.RepeatCount > 0 || @event.Key != KeyCode.S)
            {
                return false;
            }

            var transform = GetComponent<TransformComponent>();
            transform.Scale /= 2f;

            Entity.Scene.CloneEntity(Entity);
            return false;
        }
    }
}
