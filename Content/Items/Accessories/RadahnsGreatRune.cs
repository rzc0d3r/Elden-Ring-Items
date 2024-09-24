using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

using EldenRingItems.Content.Buffs.StatBuff;

namespace EldenRingItems.Content.Items.Accessories
{
    public class RadahnsGreatRune : ModItem
    {
        public static readonly int LifeBonus = 15;
        public static readonly int ManaBonus = 60;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(LifeBonus, ManaBonus);

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.buyPrice(0, 7, 50, 0);
            Item.rare = ItemRarityID.Lime;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.HasBuff<BlessingBuff>())
            {
                player.statLifeMax2 += (player.statLifeMax / 100) * LifeBonus;
                player.statManaMax2 += ManaBonus;
            }
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.PixieDust, 100);
            r.AddIngredient(ItemID.CrystalShard, 25);
            r.AddIngredient(ItemID.SoulofNight, 6);
            r.AddIngredient(ItemID.SoulofLight, 6); 
            r.AddIngredient(ItemID.LifeCrystal, 1);
            r.AddIngredient(ItemID.ManaCrystal, 1);
            r.AddTile(TileID.MythrilAnvil);
            r.Register();
        }
    }
}
