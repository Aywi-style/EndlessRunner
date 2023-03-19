using UnityEngine;

namespace Client
{
    struct UnderPlayerControlle
    {
        public LineType CurrentLine;
        public LineType TargetLine;

        public float CurrentTimeToMove;
        public float MaxTimeToMove;
    }
}