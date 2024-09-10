using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;
using EldenRingItems.Projectiles.Melee;

namespace EldenRingItems.Content.Items.Weapons.Melee
{
    public class GiantCrusher : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 120;
            Item.height = 120;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.damage = 110;
            Item.knockBack = 9f;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.shoot = ModContent.ProjectileType<GiantCrusherProj>();
            Item.shootSpeed = 17f;
        }

    }

    
}
