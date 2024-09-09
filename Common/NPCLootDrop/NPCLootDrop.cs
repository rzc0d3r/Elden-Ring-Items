using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

using EldenRingItems.Content.Items.Weapons.Melee;
using EldenRingItems.Content.Items.Consumables;

namespace EldenRingItems.Common.NPCLootDrop
{
    public class NPCLootDrop : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            // BlasphemousBlade
            if (npc.type == NPCID.BloodEelHead)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BlasphemousBlade>(), 12));
            if (npc.type == NPCID.BloodNautilus)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BlasphemousBlade>(), 4));
            // Rune Arc
            if (npc.type == NPCID.Pixie)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RuneArc>(), 8));
        }
    }
}
