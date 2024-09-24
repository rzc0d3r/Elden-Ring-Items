using Terraria;
using Terraria.ModLoader;

using EldenRingItems.Content.Buffs.StatDebuff;

namespace ExampleMod.Common.GlobalNPCs
{
    internal class DamageOverTimeGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.HasBuff<BlackFlameDebuff>())
            {
                damage = 5;
                npc.lifeRegen -= damage*5*2; // damage * 4 per second
            }
        }
    }
}