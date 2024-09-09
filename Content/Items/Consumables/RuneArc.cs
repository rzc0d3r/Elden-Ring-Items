using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.Audio;

using EldenRingItems.Common.Players;
using EldenRingItems.Content.Buffs;

namespace EldenRingItems.Content.Items.Consumables
{
    internal class RuneArc : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 40;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = ItemRarityID.Orange;
            Item.consumable = true;
            Item.UseSound = new SoundStyle("EldenRingItems/Sounds/RetrieveLostRunes");
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.maxStack = 9999;
        }

        public override bool CanUseItem(Player player)
        {
            ERIPlayer eri_player = player.GetModPlayer<ERIPlayer>();
            return !player.HasBuff(ModContent.BuffType<BlessingBuff>());
        }

        public override bool? UseItem(Player player)
        {
            if(player.HasBuff(ModContent.BuffType<BlessingBuff>()))
                return false;
            if (player.whoAmI == Main.myPlayer)
            {
                ERIPlayer eri_player = player.GetModPlayer<ERIPlayer>();
                player.AddBuff(ModContent.BuffType<BlessingBuff>(), int.MaxValue);
            }
            return true;
        }

    }
}
