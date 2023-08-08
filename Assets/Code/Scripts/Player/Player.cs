//
// copyright (c) y01cu. All rights reserved.
//

using System;
using UnityEngine;

namespace y01cu
{
    /// <summary>
    /// Player
    /// </summary>
    public class Player : FighterNew
    {
        private PlayerCombat playerCombat;

        private void Awake()
        {
            playerCombat = GetComponent<PlayerCombat>();
        }

        public float GetFinalAttackDamageToSendToTheEnemy()
        {
            return GetFinalAttackDamage();
        }

        // TODO: Create a similar method for magic damage but distinguish whether player used magic or not
        
    }
}