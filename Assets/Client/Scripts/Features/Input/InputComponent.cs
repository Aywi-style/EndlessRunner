using UnityEngine;

namespace Client
{
    struct InputComponent
    {
        public float Horizontal;
        public float Vertical;

        public Vector2 FirstTapPosition;
        public Vector2 Direction;
    }
}