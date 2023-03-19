using UnityEngine;

namespace Client
{
    struct ObstaclesSpawnEvent
    {
        public Vector2 Position;
        
        public void Invoke(Vector2 position)
        {
            Position = position;
        }
    }
}