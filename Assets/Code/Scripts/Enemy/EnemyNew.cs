using System;
using UnityEngine;
using y01cu;
using UnityEngine.UI;

public class EnemyNew : FighterNew
{
    private void SetProperties()
    {
        name = "Enemy";

        attackingDamage = 5;
        attackCooldown = 1;
        attackRange = 1;

        criticalAttackChance = 0;
        criticalAttackDamageMultiplier = 1;

        movementSpeed = 1;

        castingSpeed = 1;
        magicDamage = 0;

        armour = 0;
        magicResistance = 0;

        criticalMagicChance = 0;
        criticalMagicDamageMultiplier = 1;

        hitpoint = 50;
    }

    private void Start()
    {
        SetProperties();
    }
}