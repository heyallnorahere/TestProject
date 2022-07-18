using SGE;
using SGE.Components;
using System.Collections.Generic;

namespace TestProject.Assets.Scripts
{
    public sealed class ExampleScript : Script
    {
        public void OnUpdate(Timestep ts)
        {
            if (!HasComponent<RigidBodyComponent>())
            {
                return;
            }

            float timestep = (float)ts;
            Vector2 force = new Vector2(Speed * timestep, 0f);

            float modifier = 0f;
            modifier += Input.GetKey(KeyCode.D) ? 1f : 0f;
            modifier -= Input.GetKey(KeyCode.A) ? 1f : 0f;

            var rigidbody = GetComponent<RigidBodyComponent>();
            rigidbody.ApplyForce(force * modifier);
        }

        public void OnCollision(Entity entity)
        {
            var names = new List<string>();

            var rigidbody = entity.GetComponent<RigidBodyComponent>();
            var filter = new CollisionFilter(rigidbody);

            int categories = filter.CategoryBits;
            var categoryNames = Entity.Scene.CollisionCategoryNames;

            for (int i = 0; i < categoryNames.Count; i++)
            {
                int mask = 0x1 << i;
                if ((categories & mask) != 0)
                {
                    string name = categoryNames[i];
                    names.Add(name);
                }
            }

            string categoryString = string.Empty;
            foreach (string name in names)
            {
                if (categoryString.Length > 0)
                {
                    categoryString += ", ";
                }

                categoryString += name;
            }

            Log.Info($"Collided with entity in categories: {categoryString}");
        }

        public float Speed { get; set; } = 500f;

        [SectionHeader("Foo")]
        public int OtherProperty1 { get; set; } = 42;

        [SectionHeader("Foo/Bar")]
        public bool OtherProperty2 { get; set; } = true;
    }
}
