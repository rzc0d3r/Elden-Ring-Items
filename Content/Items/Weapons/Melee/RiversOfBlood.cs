using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using EldenRingItems.Content.Items.Materials.SomberSmithingStones;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using EldenRingItems.Projectiles.Melee;

namespace EldenRingItems.Content.Items.Weapons.Melee
{
    public class RiversOfBlood : ModItem
    {
        public override string Texture => "EldenRingItems/Content/Items/Weapons/Melee/RiversOfBlood";
        public int BaseDamage { get; set; } = 65;

        SoundStyle IsChargedSound = new SoundStyle("EldenRingItems/Sounds/RiversOfBlood/ChargeSound");

        bool IsCharged = false;
        const int MAX_CHARGED_ATTACKS = 3;
        int MadeChargedAttacks = 0;
        const int MANA_FOR_CHARGE = 50;

        public override LocalizedText DisplayName => base.DisplayName.WithFormatArgs("");
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MANA_FOR_CHARGE);

        public override void SetDefaults()
        {
            Item.width = 75;
            Item.height = 75;
            Item.DamageType = DamageClass.Melee;
            Item.damage = BaseDamage;
            Item.knockBack = 5f;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.shoot = ProjectileID.None;
            Item.shootSpeed = 10f;
            Item.scale = 1.25f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (IsCharged)
            {
                if (MadeChargedAttacks == MAX_CHARGED_ATTACKS)
                {
                    MadeChargedAttacks = 0;
                    IsCharged = false;
                    Item.noUseGraphic = false;
                    Item.noMelee = false;
                    Item.shoot = ProjectileID.None;
                    return false;
                }
                if (MadeChargedAttacks == MAX_CHARGED_ATTACKS - 1)
                    Item.UseSound = SoundID.Item1;
                MadeChargedAttacks++;
                return true;
            }
            return false;
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (Main.mouseRight && !IsCharged)
            {
                if (player.CheckMana(MANA_FOR_CHARGE, true))
                {
                    //player.Hurt(PlayerDeathReason.ByCustomReason(""), Main.rand.Next(MIN_HP_FOR_CHARGE, MAX_HP_FOR_CHARGE+1), player.direction, armorPenetration: 1000, dodgeable: false, cooldownCounter:1);
                    IsChargedSound.Volume = 0.6f;
                    SoundEngine.PlaySound(IsChargedSound);
                    IsCharged = true;
                    MadeChargedAttacks = 0;
                    Item.shoot = ModContent.ProjectileType<RiversOfBloodProj>();
                    Item.noUseGraphic = true;
                    Item.noMelee = true;
                }
            }
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Blood, Scale: Main.rand.NextFloat(0.5f, 1.2f));
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!target.HasBuff(BuffID.Venom))
                target.AddBuff(BuffID.Venom, 60*15); // 15 seconds
        }
    }

    #region Upgrade
    public abstract class UpgradedRiversOfBlood : RiversOfBlood
    {
        public int UpgradeLevel { get; set; }
        public Recipe recipe { get; set; }
        public override LocalizedText Tooltip => ModContent.GetModItem(ModContent.ItemType<RiversOfBlood>()).Tooltip;
        public override LocalizedText DisplayName => ModContent.GetModItem(ModContent.ItemType<RiversOfBlood>()).DisplayName.WithFormatArgs($" +{UpgradeLevel}");

        protected UpgradedRiversOfBlood(int upgradeLevel)
        {
            UpgradeLevel = upgradeLevel;
        }

        public override void SetDefaults()
        {
            BaseDamage += UpgradeLevel * 8;
            base.SetDefaults();
        }

        public override void AddRecipes()
        {
            recipe = CreateRecipe();
            recipe.AddIngredient(SSSUtils.GetSSSByLevel(UpgradeLevel));
            recipe.AddTile(SSSUtils.GetTileByLevel(UpgradeLevel));
        }
    }

    public class RiversOfBlood1 : UpgradedRiversOfBlood
    {
        public RiversOfBlood1() : base(1) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<RiversOfBlood>());
            recipe.Register();
        }
    }

    public class RiversOfBlood2 : UpgradedRiversOfBlood
    {
        public RiversOfBlood2() : base(2) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<RiversOfBlood1>());
            recipe.Register();
        }
    }

    public class RiversOfBlood3 : UpgradedRiversOfBlood
    {
        public RiversOfBlood3() : base(3) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<RiversOfBlood2>());
            recipe.Register();
        }
    }
    public class RiversOfBlood4 : UpgradedRiversOfBlood
    {
        public RiversOfBlood4() : base(4) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<RiversOfBlood3>());
            recipe.Register();
        }
    }

    public class RiversOfBlood5 : UpgradedRiversOfBlood
    {
        public RiversOfBlood5() : base(5) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<RiversOfBlood4>());
            recipe.Register();
        }
    }

    public class RiversOfBlood6 : UpgradedRiversOfBlood
    {
        public RiversOfBlood6() : base(6) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<RiversOfBlood5>());
            recipe.Register();
        }
    }

    public class RiversOfBlood7 : UpgradedRiversOfBlood
    {
        public RiversOfBlood7() : base(7) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<RiversOfBlood6>());
            recipe.Register();
        }
    }

    public class RiversOfBlood8 : UpgradedRiversOfBlood
    {
        public RiversOfBlood8() : base(8) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<RiversOfBlood7>());
            recipe.Register();
        }
    }

    public class RiversOfBlood9 : UpgradedRiversOfBlood
    {
        public RiversOfBlood9() : base(9) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<RiversOfBlood8>());
            recipe.Register();
        }
    }

    public class RiversOfBlood10 : UpgradedRiversOfBlood
    {
        public RiversOfBlood10() : base(10) { }
        public override void AddRecipes()
        {
            base.AddRecipes();
            recipe.AddIngredient(ModContent.ItemType<RiversOfBlood9>());
            recipe.Register();
        }
    }
    #endregion
}
