using UnityEngine;

namespace Client
{
    struct PlayExplosionEvent
    {
        public Vector3 Position;
        
        public void Invoke(Vector3 position)
        {
            Position = position;
        }
    }
}