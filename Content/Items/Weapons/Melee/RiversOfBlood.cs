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
        public int BaseDamage { get; set; } = 60;

        SoundStyle IsChargedSound = new SoundStyle("EldenRingItems/Sounds/RiversOfBlood/ChargeSound");

        bool Hemmorhage = false;
        bool IsCharged = false;
        const int MAX_CHARGED_ATTACKS = 3;
        int MadeChargedAttacks = 0;
        const int MANA_FOR_CHARGE = 60;

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
            Item.shoot = ModContent.ProjectileType<RiversOfBloodProj>();
            Item.shootSpeed = 10f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (IsCharged)
            {
                if (MadeChargedAttacks == MAX_CHARGED_ATTACKS - 1)
                {
                    Item.UseSound = SoundID.Item1;
                    MadeChargedAttacks = 0;
                    IsCharged = false;
                }
                else
                    MadeChargedAttacks++;
                return true;
            }
            else
            {
                Item.noMelee = false;
                Item.noUseGraphic = false;
                return false;
            }
        }

        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {
            if (Main.mouseRight && !IsCharged)
            {
                if (player.CheckMana(MANA_FOR_CHARGE, true))
                {
                    IsChargedSound.Volume = 0.3f;
                    SoundEngine.PlaySound(IsChargedSound);
                    IsCharged = true;
                    MadeChargedAttacks = 0;
                    Item.noUseGraphic = true;
                    Item.noMelee = true;
                }
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Blood, Scale: Main.rand.NextFloat(0.5f, 1.2f));
        }
        
        public static void DrawHemmorhageEffect(NPC npc)
        {
            Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.RedTorch, Alpha:180);
            dust.noGravity = true;
            dust.velocity = new Vector2(0, Main.rand.NextFloat(-3f, -5f)) + npc.velocity;
            for (int i = 0; i < 4; i++)
            {
                Dust dust2 = Dust.NewDustDirect(npc.position + new Vector2(Main.rand.NextFloat(-10f, 10f), npc.height / 3f), npc.width, npc.height, DustID.Blood, Alpha: 50);
                dust2.noGravity = true;
                dust2.velocity = new Vector2(Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-1f, -3f)) + npc.velocity;
                dust2.scale = 1.35f;
            }
        }
        
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Hemmorhage)
            {
                Hemmorhage = false;
                Item.damage = BaseDamage;
                SoundEngine.PlaySound(SoundID.Item152);
                for (int i = 0; i < 5; i++)
                    DrawHemmorhageEffect(target);
            }
            if (Main.rand.NextBool(6)) // 16.66 %
            {
                Hemmorhage = true;
                Item.damage *= 2;
            }
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
            if (UpgradeLevel <= 5)
                BaseDamage += UpgradeLevel * 3;
            else
                BaseDamage += UpgradeLevel * 7;
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
