﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F12_Magnesium : F04_Beryllium {

    // Child Class F12_Magnesium inheriting from F03_Lithium.
    [Space( 10), Header("[^ Child: F12_Magnesium ]")]
    #pragma warning disable
    [SerializeField] GameObject Glow;

    void Reset()
    {
        MovementSpeed = 1f;
        AttackRate = 0.75f;
        LookRadius = 4f;
        AttackRadius = LookRadius;
    }
}