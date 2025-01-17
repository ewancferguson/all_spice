

namespace all_spice.Services;


public class IngredientsService
{


  public IngredientsService(IngredientsRepository repository, RecipesService recipesService)
  {
    _repository = repository;
    _recipesService = recipesService;
  }

  private readonly IngredientsRepository _repository;
  private readonly RecipesService _recipesService;

  internal Ingredient CreateIngredient(Ingredient ingredientData, string userId)
  {
    Recipe recipe = _recipesService.GetRecipeById(ingredientData.RecipeId);

    if (userId != recipe.CreatorId) throw new Exception("NOT UR RECIPE BUD");

    ingredientData.RecipeId = recipe.Id;

    Ingredient ingredient = _repository.CreateIngredient(ingredientData);
    return ingredient;
  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    List<Ingredient> ingredients = _repository.GetIngredientsByRecipeId(recipeId);
    return ingredients;
  }

  internal string DeleteIngredient(int ingredientId, string userId)
  {
    Ingredient ingredient = GetIngredientById(ingredientId);
    Recipe recipe = _recipesService.GetRecipeById(ingredient.RecipeId);

    if (userId != recipe.CreatorId) throw new Exception("NOT UR RECIPE BUD");

    _repository.DeleteIngredient(ingredientId);

    return "Ingredient was deleted";

  }


  private Ingredient GetIngredientById(int ingredientId)
  {
    Ingredient ingredient = _repository.GetIngredientId(ingredientId);

    if (ingredient == null) throw new Exception($"Invalid Ingredient id: {ingredientId}");

    return ingredient;
  }
}