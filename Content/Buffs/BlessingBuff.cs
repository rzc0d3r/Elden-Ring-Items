using Terraria;
using Terraria.ModLoader;
using EldenRingItems.Common.Players;

namespace EldenRingItems.Content.Buffs
{
    public class BlessingBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (!player.dead)
                player.GetModPlayer<ERIPlayer>().Blessed = true;
        }
    }
}
