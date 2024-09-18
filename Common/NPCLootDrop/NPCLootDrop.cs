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
            // Blasphemous Blade
            if (npc.type == NPCID.BloodEelHead)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BlasphemousBlade>(), 12));
            if (npc.type == NPCID.BloodNautilus)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BlasphemousBlade>(), 4));
            // Rune Arc
            if (npc.type == NPCID.Pixie)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RuneArc>(), 8));
            // Gian Crusher
            if (npc.type == NPCID.Golem)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GiantCrusher>(), 1));
            // Dark Moon Greatsword
            if (npc.type == NPCID.CultistBoss)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DarkMoonGreatsword>(), 1));
            // Rivers Of Blood
            if (npc.type == NPCID.BloodEelHead)
               npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RiversOfBlood>(), 12));
            if (npc.type == NPCID.BloodNautilus)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RiversOfBlood>(), 4));
            // Moonveil
            if (npc.type == NPCID.BlueArmoredBonesSword)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Moonveil>(), 12));
            if (npc.type == NPCID.WyvernHead)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Moonveil>(), 25));
        }
    }
}
