using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

using EldenRingItems.Content.Items.Materials.SomberSmithingStones;
using EldenRingItems.Common.Players;

namespace EldenRingItems.Content.Items.Accessories.AmberMedallion
{
    public class CeruleanAmberMedallion : ModItem
    {
        public int ManaBonus { get; set; } = 20;
        public float ManaReduce { get; set; } = 0.02f;

        public override LocalizedText DisplayName => base.DisplayName.WithFormatArgs("");
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ManaBonus, (int)(ManaReduce * 100));

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 46;
            Item.value = Item.buyPrice(0, 3, 0, 0);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ERIPlayer eri_player = player.GetModPlayer<ERIPlayer>();
            eri_player.ManaReduce = ManaReduce;
            player.statManaMax2 += ManaBonus;

        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if (equippedItem.ModItem is CeruleanAmberMedallion && incomingItem.ModItem is CeruleanAmberMedallion)
                return false;
            return true;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            for (int i = 3; i < 9; i++)
                if (player.armor[i].ModItem is CeruleanAmberMedallion)
                    return false;
            return true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.GoldBar, 8);
            r.AddIngredient(ItemID.Chain);
            r.AddIngredient(ItemID.Sapphire, 4);
            r.AddIngredient(ItemID.ManaCrystal);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }

    #region Upgrade
    public abstract class UpgradedCeruleanAmberMedallion : CeruleanAmberMedallion
    {
        public int UpgradeLevel { get; set; }
        public override LocalizedText Tooltip => ModContent.GetModItem(ModContent.ItemType<CeruleanAmberMedallion>()).Tooltip.WithFormatArgs(ManaBonus, (int)(ManaReduce * 100));
        public override LocalizedText DisplayName => ModContent.GetModItem(ModContent.ItemType<CeruleanAmberMedallion>()).DisplayName.WithFormatArgs($" +{UpgradeLevel}");

        protected UpgradedCeruleanAmberMedallion(int upgradeLevel)
        {
            UpgradeLevel = upgradeLevel;
            if (UpgradeLevel == 3)
            {
                ManaBonus = 100;
                ManaReduce = 0.10f;
            }
            else
            {
                ManaBonus += UpgradeLevel * 20;
                ManaReduce += UpgradeLevel * 0.02f;
            }
        }
    }

    public class CeruleanAmberMedallion1 : UpgradedCeruleanAmberMedallion
    {
        public CeruleanAmberMedallion1() : base(1) { }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Orange;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<CeruleanAmberMedallion>());
            r.AddIngredient(SSSUtils.GetSSSByLevel(4));
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }

    public class CeruleanAmberMedallion2 : UpgradedCeruleanAmberMedallion
    {
        public CeruleanAmberMedallion2() : base(2) { }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.LightRed;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<CeruleanAmberMedallion1>());
            r.AddIngredient(SSSUtils.GetSSSByLevel(6));
            r.AddTile(SSSUtils.GetTileByLevel(6));
            r.Register();
        }
    }

    public class CeruleanAmberMedallion3 : UpgradedCeruleanAmberMedallion
    {
        public CeruleanAmberMedallion3() : base(3) { }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Pink;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<CeruleanAmberMedallion2>());
            r.AddIngredient(SSSUtils.GetSSSByLevel(8));
            r.AddTile(SSSUtils.GetTileByLevel(8));
            r.Register();
        }
    }
    #endregion
}