﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExampleMod.Content.Items;

/*
The items in this file showcase customizing the decrafting feature of the Shimmer liquid.
By default, Shimmer will tranform crafted items back into their original recipe ingredients.

ShimmerShowcaseConditions showcases crimson and corruption specific shimmer decrafting results.

ShimmerShowcaseCustomShimmerResult showcases both preventing a recipe from being decrafted and specifying a custom shimmer decrafting result.

To use Shimmer to transform an item into another item instead of decrafting an item, simply set "ItemID.Sets.ShimmerTransformToItem[Type] = ItemType here;" in SetStaticDefaults. ExampleMusicBox and ExampleTorch show examples of this.

Also note that critter items (Item.makeNPC > 0) will also not attempt to decraft, but will instead transform into the NPC that the Item.makeNPC transforms into. NPCID.Sets.ShimmerTransformToNPC sets which NPC an NPC will transform into, see PartyZombie and ExampleCustomAISlimeNPC for examples of this.
*/
public class ShimmerShowcaseConditions : ModItem
{
	public override string Texture => "ExampleMod/Content/Items/ExampleItem";

	public override void SetDefaults() {
		Item.width = 20;
		Item.height = 20;
	}

	public override void AddRecipes() {

		// Keep in mind that decraft order is reverse of recipe register, so this desert example has priority over the world evil

		CreateRecipe()
			.AddIngredient<ExampleItem>()
			.AddIngredient(ItemID.Cactus)
			.AddTile<Tiles.Furniture.ExampleWorkbench>()
			.AddDecraftCondition(Condition.InDesert)
			.Register();

		// Many items have multiple recipes. The last added recipe will usually be used for shimmer decrafting.
		// Recipe conditions may be used to only allow decrafting under certain conditions, here it is used to make the recipes decraftable in only their respective world types
		CreateRecipe()
			.AddIngredient<ExampleItem>()
			.AddIngredient(ItemID.RottenChunk)
			.AddTile<Tiles.Furniture.ExampleWorkbench>()
			.AddDecraftCondition(Condition.CorruptWorld)
			.Register();

		CreateRecipe()
			.AddIngredient<ExampleItem>()
			.AddIngredient(ItemID.Vertebrae)
			.AddTile<Tiles.Furniture.ExampleWorkbench>()
			.AddDecraftCondition(Condition.CrimsonWorld)
			.Register();
	}
}

public class ShimmerShowcaseCustomShimmerResult : ModItem
{
	public override string Texture => "ExampleMod/Content/Items/ExampleItem";

	public override void SetDefaults() {
		Item.width = 20;
		Item.height = 20;
	}

	public override void AddRecipes() {
		// By default, the first added recipe will be used for shimmer decrafting. We can use DisableDecraft() to tell the game to ignore this recipe and use the below recipe instead.
		CreateRecipe()
			.AddIngredient<ExampleItem>()
			.AddIngredient(ItemID.PadThai)
			.AddTile<Tiles.Furniture.ExampleWorkbench>()
			.DisableDecraft()
			.Register();

		// AddCustomShimmerResult can be used to change the decrafting results. Rather that return 1 ExampleItem, decrafting this item will return 1 Rotten Egg and 3 Chain.
		CreateRecipe()
			.AddIngredient<ExampleItem>()
			.AddTile<Tiles.Furniture.ExampleWorkbench>()
			.AddCustomShimmerResult(ItemID.RottenEgg)
			.AddCustomShimmerResult(ItemID.Chain, 3)
			.Register();
	}
}
