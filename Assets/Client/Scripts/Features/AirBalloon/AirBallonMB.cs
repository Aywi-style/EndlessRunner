using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBallonMB : MonoBehaviour
{
    [field: SerializeField] public SpriteRenderer ViewSprite { private set; get; }

    [field: SerializeField] public Transform CollidingCenter { private set; get; }
    [field: SerializeField] public float CollidingRadius { private set; get; }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(CollidingCenter.position, CollidingRadius);
    }
}
