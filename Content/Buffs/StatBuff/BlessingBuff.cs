using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using EldenRingItems.Common.Players;

namespace EldenRingItems.Content.Buffs.StatBuff
{
    public class BlessingBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ERIPlayer>().Blessed = true;
        }
    }
}