﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

namespace EldenRingItems.Content.Buffs.StatDebuff
{
    public class BlackFlameDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            DrawEffects(npc);
            if (npc.buffTime[buffIndex] <= 0)
            {
                npc.DelBuff(buffIndex);
                buffIndex--;
            }
        }

        public static void DrawEffects(NPC npc)
        {
            if (Main.rand.NextBool(2))
            {
                Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Wraith);
                dust.noGravity = true;
                dust.velocity = new Vector2(0, Main.rand.NextFloat(-3f, -5f)) + npc.velocity;
                for (int i = 0; i < 3; i++)
                {
                    Dust dust2 = Dust.NewDustDirect(npc.position + new Vector2(Main.rand.NextFloat(-10f, 10f), npc.height / 2), npc.width, npc.height, DustID.Wraith, Alpha:75);
                    dust2.noGravity = true;
                    dust2.velocity = new Vector2(Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-1f, -3f)) + npc.velocity;
                    dust2.scale = 1.2f;
                }
                Lighting.AddLight(npc.position, 0.05f, 0.01f, 0.01f);
            }
        }
    }
}