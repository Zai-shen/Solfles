using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public static List<GameObject> Enemies = new List<GameObject>();

    public static Vector3 MapSize = new Vector3(47f, 2.5f, 67f);
    
    public static LayerMask PlayerMask = 1 << 6;
    public static LayerMask GroundMask = 1 << 7;
    public static LayerMask EnemyMask = 1 << 8;
    public static LayerMask DamageableLayers = PlayerMask | EnemyMask;
}