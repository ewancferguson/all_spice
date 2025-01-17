


namespace all_spice.Services;


public class RecipesService
{
  public RecipesService(RecipesRepository recipesRepository)
  {
    _repository = recipesRepository;
  }
  private readonly RecipesRepository _repository;
  internal Recipe CreateRecipe(Recipe recipeData)
  {
    Recipe recipe = _repository.CreateRecipe(recipeData);
    return recipe;
  }

  internal List<Recipe> GetAllRecipes()
  {
    List<Recipe> recipes = _repository.GetAllRecipes();
    return recipes;
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    Recipe recipe = _repository.GetRecipeById(recipeId);

    if (recipe == null) throw new Exception($"Invalid recipe id: {recipeId}");

    return recipe;
  }

  internal Recipe UpdateRecipe(int recipeId, string userId, Recipe recipeUpdateData)
  {
    Recipe recipe = GetRecipeById(recipeId);

    if (recipe.CreatorId != userId) throw new Exception("Invalid Request not your recipe");



    recipe.Title = recipeUpdateData.Title ?? recipe.Title;
    recipe.Instructions = recipeUpdateData.Instructions ?? recipe.Instructions;


    _repository.UpdateRecipe(recipe);

    return recipe;
  }

  internal string DeleteRecipe(int recipeId, string userId)
  {
    Recipe recipe = GetRecipeById(recipeId);
    if (recipe.CreatorId != userId) throw new Exception("INVALID REQUEST NOT YOUR RECIPE");
    _repository.DeleteRecipe(recipeId);
    return "Deleted the recipe";
  }
}