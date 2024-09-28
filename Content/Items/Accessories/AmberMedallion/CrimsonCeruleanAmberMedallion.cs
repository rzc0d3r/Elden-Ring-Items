using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

using EldenRingItems.Common.Players;
using EldenRingItems.Content.Items.Materials.SomberSmithingStones;

namespace EldenRingItems.Content.Items.Accessories.AmberMedallion
{
    public class CrimsonCeruleanAmberMedallion: ModItem
    {
        public float LifeBonusMultiplier { get; set; } = 0.20f;
        public int ManaBonus { get; set; } = 100;
        public float ManaReduce { get; set; } = 0.1f;

        public override LocalizedText DisplayName => base.DisplayName.WithFormatArgs("");
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs((int)(LifeBonusMultiplier * 100), ManaBonus, (int)(ManaReduce * 100));

        public override void SetDefaults()
        {
            Item.width = 58;
            Item.height = 62;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ERIPlayer eri_player = player.GetModPlayer<ERIPlayer>();
            eri_player.ManaReduce = ManaReduce;
            player.statManaMax2 += ManaBonus;
            player.statLifeMax2 += (int)(player.statLifeMax * LifeBonusMultiplier);
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if (equippedItem.ModItem is CrimsonCeruleanAmberMedallion && incomingItem.ModItem is CrimsonCeruleanAmberMedallion)
                return false;
            else if (equippedItem.ModItem is CrimsonAmberMedallion && incomingItem.ModItem is CrimsonCeruleanAmberMedallion)
                return false;
            else if (equippedItem.ModItem is CeruleanAmberMedallion && incomingItem.ModItem is CrimsonCeruleanAmberMedallion)
                return false;
            return true;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            for (int i=3; i < 9; i++)
                if (player.armor[i].ModItem is CrimsonCeruleanAmberMedallion || player.armor[i].ModItem is CrimsonAmberMedallion || player.armor[i].ModItem is CeruleanAmberMedallion)
                    return false;
            return true;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ModContent.ItemType<CrimsonAmberMedallion3>());
            r.AddIngredient(ModContent.ItemType<CeruleanAmberMedallion3>());
            r.AddIngredient(SSSUtils.GetSSSByLevel(10));
            r.AddTile(TileID.MythrilAnvil);
            r.Register();
        }
    }
}