using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public static List<GameObject> Enemies = new List<GameObject>();
    public static List<GameObject> EnemiesInAttackRange = new List<GameObject>();

    public static LayerMask PlayerMask = 1 << 6;
    public static LayerMask EnemyMask = 1 << 8;
    public static LayerMask DamageableLayers = PlayerMask | EnemyMask;
}