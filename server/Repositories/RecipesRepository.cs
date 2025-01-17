



namespace all_spice.Repositories;

public class RecipesRepository
{

  public RecipesRepository(IDbConnection db)
  {
    _db = db;
  }

  private readonly IDbConnection _db;
  internal Recipe CreateRecipe(Recipe recipeData)
  {
    string sql = @"
    INSERT INTO
    recipes(title, instructions, img, category, creator_id)
    VALUES(@Title, @instructions, @Img, @Category, @CreatorId);
    
    SELECT
    recipes.*,
    accounts.*
    FROM recipes
    JOIN accounts ON recipes.creator_id = accounts.id
    WHERE recipes.id = LAST_INSERT_ID();";

    Recipe recipe = _db.Query(sql, (Recipe recipe, Profile account) =>
    {
      recipe.Creator = account;
      return recipe;
    }, recipeData).SingleOrDefault();

    return recipe;
  }

  internal List<Recipe> GetAllRecipes()
  {
    string sql = @"
        SELECT
        recipes.*,
        accounts.*
        FROM recipes
        JOIN accounts ON recipes.creator_id = accounts.id;";


    List<Recipe> recipes = _db.Query(sql, (Recipe recipe, Profile account) =>
    {
      recipe.Creator = account;
      return recipe;
    }).ToList();
    return recipes;
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    string sql = @"
        SELECT
        recipes.*,
        accounts.*
        FROM recipes
        JOIN accounts ON recipes.creator_id = accounts.id
        WHERE recipes.id = @recipeId;";


    Recipe recipe = _db.Query(sql, (Recipe recipe, Profile account) =>
    {
      recipe.Creator = account;
      return recipe;
    }, new { recipeId }).SingleOrDefault();

    return recipe;
  }

  internal void UpdateRecipe(Recipe recipe)
  {
    string sql = @"
      UPDATE recipes
      SET
      title = @title,
      instructions = @Instructions
      WHERE id = @Id LIMIT 1;";


    int rowsAffected = _db.Execute(sql, recipe);

    if (rowsAffected == 0) throw new Exception("UPDATE WAS UNSUCCESSFUL");
    if (rowsAffected > 1) throw new Exception("UPDATE WAS TOO SUCCESSFUL");
  }

  internal void DeleteRecipe(int recipeId)
  {
    string sql = "DELETE FROM recipes WHERE id = @id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { id = recipeId });

    if (rowsAffected == 0) throw new Exception("Nothing was Deleted");
    if (rowsAffected > 1) throw new Exception("Too much was Deleted");
  }
}