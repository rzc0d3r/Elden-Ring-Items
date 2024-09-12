using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EldenRingItems.Content.Items.Materials.SomberSmithingStones
{
    #region Pre-Hardmode
    public class SomberSmithingStones1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;
            ItemID.Sets.SortingPriorityMaterials[Type] = 69;
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;
            Item.maxStack = 9999;
            Item.value = Item.buyPrice(0, 0, 30, 0);
            Item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.IronBar, 12);
            r.AddTile(TileID.Anvils);
            r.Register();

            Recipe r2 = CreateRecipe();
            r2.AddIngredient(ItemID.LeadBar, 12);
            r2.AddTile(TileID.Anvils);
            r2.Register();
        }
    }

    public class SomberSmithingStones2 : SomberSmithingStones1
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.buyPrice(0, 1, 50, 0);
            Item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.GoldBar, 12);
            r.AddTile(TileID.Anvils);
            r.Register();

            Recipe r2 = CreateRecipe();
            r2.AddIngredient(ItemID.PlatinumBar, 12);
            r2.AddTile(TileID.Anvils);
            r2.Register();
        }
    }

    public class SomberSmithingStones3 : SomberSmithingStones1
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.buyPrice(0, 3, 75, 0);
            Item.rare = ItemRarityID.Green;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.CrimtaneBar, 12);
            r.AddIngredient(ItemID.TissueSample, 3);
            r.AddTile(TileID.Anvils);
            r.Register();

            Recipe r2 = CreateRecipe();
            r2.AddIngredient(ItemID.DemoniteBar, 12);
            r2.AddIngredient(ItemID.ShadowScale, 3);
            r2.AddTile(TileID.Anvils);
            r2.Register();
        }
    }

    public class SomberSmithingStones4 : SomberSmithingStones1
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Orange;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.HellstoneBar, 12);
            r.AddTile(TileID.Anvils);
            r.Register();
        }
    }
    #endregion

    #region HardMode
    public class SomberSmithingStones5 : SomberSmithingStones1
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.buyPrice(0, 2, 50, 0);
            Item.rare = ItemRarityID.LightRed;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.CobaltBar, 12);
            r.AddTile(TileID.Anvils);
            r.Register();

            Recipe r2 = CreateRecipe();
            r2.AddIngredient(ItemID.PalladiumBar, 12);
            r2.AddTile(TileID.Anvils);
            r2.Register();
        }
    }

    public class SomberSmithingStones6 : SomberSmithingStones1
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.buyPrice(0, 5, 50, 0);
            Item.rare = ItemRarityID.LightRed;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.MythrilBar, 12);
            r.AddTile(TileID.MythrilAnvil);
            r.Register();

            Recipe r2 = CreateRecipe();
            r2.AddIngredient(ItemID.OrichalcumBar, 12);
            r2.AddTile(TileID.MythrilAnvil);
            r2.Register();
        }
    }
    public class SomberSmithingStones7 : SomberSmithingStones1
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.buyPrice(0, 7, 50, 0);
            Item.rare = ItemRarityID.LightRed;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.MythrilBar, 12);
            r.AddTile(TileID.MythrilAnvil);
            r.Register();

            Recipe r2 = CreateRecipe();
            r2.AddIngredient(ItemID.OrichalcumBar, 12);
            r2.AddTile(TileID.MythrilAnvil);
            r2.Register();
        }
    }
    public class SomberSmithingStones8 : SomberSmithingStones1
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.buyPrice(0, 16, 0, 0);
            Item.rare = ItemRarityID.Pink;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.HallowedBar, 12);
            r.AddIngredient(ItemID.SoulofFright, 5);
            r.AddIngredient(ItemID.SoulofMight, 5);
            r.AddIngredient(ItemID.SoulofSight, 5);
            r.AddIngredient(ItemID.SoulofNight, 5);
            r.AddIngredient(ItemID.SoulofLight, 5);
            r.AddTile(TileID.MythrilAnvil);
            r.Register();
        }
    }
    public class SomberSmithingStones9 : SomberSmithingStones1
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.buyPrice(0, 25, 0, 0);
            Item.rare = ItemRarityID.Lime;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.ChlorophyteBar, 15);
            r.AddIngredient(ItemID.SoulofFright, 5);
            r.AddIngredient(ItemID.SoulofMight, 5);
            r.AddIngredient(ItemID.SoulofSight, 5);
            r.AddIngredient(ItemID.SoulofNight, 5);
            r.AddIngredient(ItemID.SoulofLight, 5);
            r.AddTile(TileID.MythrilAnvil);
            r.Register();
        }
    }
    public class SomberAncientDragonSmithingStone : SomberSmithingStones1
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = Item.buyPrice(0, 45, 0, 0);
            Item.rare = ItemRarityID.Yellow;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddIngredient(ItemID.ShroomiteBar, 15);
            r.AddIngredient(ItemID.SpectreBar, 15);
            r.AddIngredient(ItemID.SoulofFright, 5);
            r.AddIngredient(ItemID.SoulofMight, 5);
            r.AddIngredient(ItemID.SoulofSight, 5);
            r.AddIngredient(ItemID.SoulofNight, 5);
            r.AddIngredient(ItemID.SoulofLight, 5);
            r.AddTile(TileID.MythrilAnvil);
            r.Register();
        }
    }
    #endregion

    #region Utils
    public class SSSUtils
    {
        public static int GetSSSByLevel(int level)
        {
            switch (level)
            {
                case 1: return ModContent.ItemType<SomberSmithingStones1>();
                case 2: return ModContent.ItemType<SomberSmithingStones2>();
                case 3: return ModContent.ItemType<SomberSmithingStones3>();
                case 4: return ModContent.ItemType<SomberSmithingStones4>();
                case 5: return ModContent.ItemType<SomberSmithingStones5>();
                case 6: return ModContent.ItemType<SomberSmithingStones6>();
                case 7: return ModContent.ItemType<SomberSmithingStones7>();
                case 8: return ModContent.ItemType<SomberSmithingStones8>();
                case 9: return ModContent.ItemType<SomberSmithingStones9>();
                case 10: return ModContent.ItemType<SomberAncientDragonSmithingStone>();
                default: return -1;
            }
        }

        public static int GetTileByLevel(int level)
        {
            switch (level)
            {
                case 1: return TileID.Anvils;
                case 2: return TileID.Anvils;
                case 3: return TileID.Anvils;
                case 4: return TileID.Anvils;
                case 5: return TileID.Anvils;
                case 6: return TileID.MythrilAnvil;
                case 7: return TileID.MythrilAnvil;
                case 8: return TileID.MythrilAnvil;
                case 9: return TileID.MythrilAnvil;
                case 10: return TileID.MythrilAnvil;
                default: return -1;
            }
        }
    }
    #endregion
}
