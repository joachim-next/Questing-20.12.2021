# Questing-20.12.2021

How to play:
1. Open Bootstrapper-scene
2. Play

How to add new recipes:
1. Open Assets/Data/UnityCraftingFormationProvider.asset
2. Add a new formation ("recipe") to "Entries"
   - ItemToBeCraftedIngredientType = The type of object that this formation will result in.
   - Nodes = Individual items and their coordinates relative to the formation
      - IngredientType = The type of object that this item should be
      - RelativeX = The position on the x-axis where this item should be
      - RelativeY = the position on the y-axis where this item should be
3. Open Assets/Data/CraftingInventoryItemImageHolder.assets
4. Add a new Entry with the same ItemToBeCraftedIngredientType as the new recipe
5. Add a sprite for the new item
