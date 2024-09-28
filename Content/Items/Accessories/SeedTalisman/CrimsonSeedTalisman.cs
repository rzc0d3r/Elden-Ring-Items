using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

using EldenRingItems.Common.Players;
using EldenRingItems.Content.Items.Materials.SomberSmithingStones;

namespace EldenRingItems.Content.Items.Accessories.SeedTalisman
{
    public class CrimsonSeedTalisman : ModItem
    {
        public float HealingPotionMultiplier { get; set; } = 0.2f;

        public override LocalizedText DisplayName => base.DisplayName.WithFormatArgs("");
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs((int)(HealingPotionMultiplier*100));

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.value = Item.buyPrice(0, 3, 0, 0);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ERIPlayer eri_player = player.GetModPlayer<ERIPlayer>();
            eri_player.CrimsonSeedTalismanIsActive = true;
            eri_player.HealingPotionMultiplier += HealingPotionMultiplier;
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if (equippedItem.ModItem is CrimsonSeedTalisman && incomingItem.ModItem is CrimsonSeedTalisman)
                return false;
            return true;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            for (int i=3; i < 9; i++)
                if (player.armor[i].ModItem is CrimsonSeedTalisman)
                    return false;
            return true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.GoldBar, 8);
            r.AddIngredient(ItemID.Chain);
            r.AddIngredient(ItemID.CorruptSeeds, 5);
            r.AddIngredient(ItemID.LifeCrystal);
            r.AddTile(TileID.Anvils);
            r.Register();

            Recipe r2 = CreateRecipe();
            r2.AddIngredient(ItemID.GoldBar, 8);
            r2.AddIngredient(ItemID.Chain);
            r2.AddIngredient(ItemID.CrimsonSeeds, 5);
            r2.AddIngredient(ItemID.LifeCrystal);
            r2.AddTile(TileID.Anvils);
            r2.Register();
        }
    }

    #region Upgrade
    public abstract class UpgradedCrimsonSeedTalisman : CrimsonSeedTalisman
    {
        public int UpgradeLevel { get; set; }
        public override LocalizedText Tooltip => ModContent.GetModItem(ModContent.ItemType<CrimsonSeedTalisman>()).Tooltip.WithFormatArgs((int)(HealingPotionMultiplier*100));
        public override LocalizedText DisplayName => ModContent.GetModItem(ModContent.ItemType<CrimsonSeedTalisman>()).DisplayName.WithFormatArgs($" +{UpgradeLevel}");

        protected UpgradedCrimsonSeedTalisman(int upgradeLevel)
        {
            UpgradeLevel = upgradeLevel;
        }
    }

    public class CrimsonSeedTalisman1 : UpgradedCrimsonSeedTalisman
    {
        public CrimsonSeedTalisman1() : base(1) {
            HealingPotionMultiplier = 0.3f;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 58;
            Item.height = 62;
            Item.rare = ItemRarityID.Pink;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<CrimsonSeedTalisman>());
            r.AddIngredient(SSSUtils.GetSSSByLevel(8));
            r.AddTile(SSSUtils.GetTileByLevel(8));
            r.Register();
        }
    }
    #endregion
}