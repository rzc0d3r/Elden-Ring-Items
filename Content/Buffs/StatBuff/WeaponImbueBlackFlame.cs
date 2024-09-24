using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using EldenRingItems.Common.Players;

namespace EldenRingItems.Content.Buffs.StatBuff
{
    public class WeaponImbueBlackFlame : ModBuff
    {
        public override void SetStaticDefaults()
        {
            BuffID.Sets.IsAFlaskBuff[Type] = true;
            Main.meleeBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ERIPlayer>().WeaponImbueBlackFlame = true;
            player.MeleeEnchantActive = true;
        }
    }
}