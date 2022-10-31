# TODOs

## General

- Axe of regrowth needs a lookup to the sapling type of a given modtree

- Check why we alter DrawData.useDestinationRectangle
	https://github.com/tModLoader/tModLoader/commit/e38affeb6a121090555377c74eeed78c40280627

-	```cs
	//unknown justification for preventing odd window sizes. Causes excessive device resets. - ChickenBones
	//width &= 0x7FFFFFFE;
	//height &= 0x7FFFFFFE;
	```
## Main
- Check `NPC.DrawTownAttackSwing` and `DrawTownAttackGun`, may need a consistency update with the new `GetItemDrawFrame` method
- `NPC.CanTownNPCSpawn` money parameter removed check it yourself.

## Item
- Remove `CanBurnInLava` hook
- Remove `CloneWithModdedDataFrom`, investigate `ResetPrefix`
- Check new `OnCreated` hook
- Disable 'illegal prefix removal'?
- Cleanup `applyPrefixOverride` patch, move `ValidateItem` into `TryGetPrefixStatMultipliersForItem`
- Reimplement PrefixLoader.Roll
- Adjust CanHavePrefixes to allow stackable items to be prefixed

## NPC
- Remove `NPCHeadLoader.GetNPCFromHeadSlot`
- Rename `NPCLoader.ScaleExpertStats`
- NPCLoader.CanHitNPC should return a plain bool. The only use of "override true" below is to force the skeleton merchant to be hit by skeletons
- The pile of NPCLoader.TownNPCAttack... hooks hurts me
- Maintainability pass? Check for `NPCLoader`, `CombinedHooks`, `BuffLoader`, `PlayerLoader`

## Projectile
- Move `NPCLoader.CanBeHitByProjectile`, `ProjectileLoader.CanHitNPC` and `PlayerLoader.CanHitNPCWithProj` into `CombinedHooks`
- Update `ArmorPenetration` vanilla values to match switch case after `int num6 = (int)Main.player[owner].armorPenetration`

## Player
- Add hook for `RefreshInfoAccsFromItemType`
- Add `ItemLoader.ConsumeItem` check to `QuickHeal` and `QuickMana`
- Move `OnHit` and `ModifyHit` into `CombinedHooks`
- Check implementation of `rangedMultDamage` and `arrowDamageAdditiveStack`
- `summonerWeaponSpeedBonus`? Is this a class specific weapon bonus. What about `whipUseTimeMultiplier`?
- Remove caps in `CapAttackSpeeds`
- Reimplement `ExtractinatorUse` hooks
- Check `PlayerIO`, make sure `favourited` flag is saved in void vault
- Check all usages of void bag (`bank4`)
- Make sure loadout serialization doesn't save modded data to the vanilla .plr
- Reapply patch for `sItem.useStyle == 13` and `sItem.useStyle == 5`? Do we still want this now that `NetMessage.SendData(13` is sent as well?
```patch
+				// Added by TML. #ItemTimeOnAllClients
+				if (whoAmI != Main.myPlayer)
+					return;
```

## ItemSlot
- `ItemLoader.CanRightClick` and `ItemLoader.RightClick` conditions don't need to check `Main.mouseRight[Release]`

## MessageBuffer
- `ModTile.ChestDrop` and `DresserDrop` code/patches are atrocious.

## TownNPCProfiles
- Investigate with the new shimmer profiles. 
- No need for `AlternateLegacyNPCProfile` since all vanilla NPCs now have profiles
	
## ItemID.cs
- ExtractinatorMode (extractinator has changed in 1.4.4, chloro extractinator now exists)

## TileID.cs
- Check the old/new `TouchDamage*` properties

## PlayerDrawLayers.cs:
- Re-ensure that all upper bound ID checks are gone, some patches had to be removed.
- Add new layer for `DrawPlayer_JimsDroneRadio`

## Recipe.cs
- `Item.PopulateMaterialCache()` do we need this anymore?
- do we need to clone `notDecraftable`

## ItemCreationContexts.cs:
- Merge with the new `Terraria.DataStructures.ItemCreationContext`.
- Adjust `Main.CraftItem`, `Item.OnCreated(ItemCreationContext)`, `RecipeLoader.OnCraft(...)` accordingly.

## Tile(.TML).cs:
- Patches have been reimplemented, check that again.
- Replace `ModTile.OpenDoorID` and `ClosedDoorID` with sets
- Remove `TileLoader.MineDamage` it had a weird 1.2 factor in there.

## Porting Notes:
- `GrantPrefixBenefits` is only called if `Item.accessory` is `true`. This applies in mod accessory slots too now.

## WorldGen.cs:
- TileLoader.Drop can probably be moved to `Item.NewItem` with `GetItemSource_FromTileBreak`
- Comment on Convert needs to be updated for new biome types, `BiomeConversionID` now exists