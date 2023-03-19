using UnityEngine;

namespace Client
{
    struct SwipedEvent
    {
        public SwipeDirection Direction;
        
        public void Invoke(SwipeDirection direction)
        {
            Direction = direction;
        }
    }
}