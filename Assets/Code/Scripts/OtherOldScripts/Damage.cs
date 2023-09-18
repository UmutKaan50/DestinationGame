//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using Destination.Fighters;
using Destination.Player;
using UnityEngine;

namespace Code.Scripts {
    public struct Damage {
        public FighterNew.FighterType fighterType;
        public PlayerCombat.AttackType attackType;
        public Vector3 origin;
        public float attackDamageAmount;
        public float magicDamageAmount;
        public float pushForce;
    }
}