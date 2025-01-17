



namespace all_spice.Repositories;


public class FavoritesRepository
{
  private readonly IDbConnection _db;

  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal FavoriteRecipe CreateFavorite(Favorite favoriteData)
  {
    string sql = @"
        INSERT INTO 
        favorites(account_id, recipe_id)
        VALUES(@AccountId, @RecipeId);
        SELECT
        favorites.*,
        recipes.*,
        accounts.*
        FROM favorites
        JOIN recipes ON favorites.recipe_id = recipes.id
        JOIN accounts ON accounts.id = recipes.creator_id
        WHERE favorites.id = LAST_INSERT_ID();";
    FavoriteRecipe favorite = _db.Query(sql, (Favorite favorite, FavoriteRecipe recipe, Profile account) =>
    {
      recipe.AccountId = favorite.AccountId;
      recipe.FavoriteId = favorite.Id;
      recipe.Creator = account;
      return recipe;
    }, favoriteData).SingleOrDefault();

    return favorite;
  }

  internal void DeleteFavorite(int favoriteId)
  {
    string sql = "DELETE FROM favorites WHERE id = @favoriteId";

    int rowsAffected = _db.Execute(sql, new { favoriteId });

    switch (rowsAffected)
    {
      case 1: return;

      case 0: throw new Exception("NO ROWS UPDATED");

      default: throw new Exception($"{rowsAffected} ROWS WERE UPDATED AND THAT IS BAD");
    }
  }

  internal Favorite GetFavoriteById(int favoriteId)
  {
    string sql = "SELECT * FROM favorites WHERE id = @favoriteId;";

    Favorite favorite = _db.Query<Favorite>(sql, new { favoriteId }).SingleOrDefault();

    return favorite;
  }

  internal List<FavoriteRecipe> GetFavoritesByAccount(string userId)
  {
    string sql = @"
    SELECT
    favorites.*,
    recipes.*,
    accounts.*
    FROM favorites
    JOIN recipes ON favorites.recipe_id = recipes.id
    JOIN accounts ON accounts.id = recipes.creator_id
    WHERE favorites.account_id = @userId";

    List<FavoriteRecipe> favoriteRecipes = _db.Query(sql, (Favorite favorite, FavoriteRecipe recipe, Profile account) =>
    {
      recipe.AccountId = favorite.AccountId;
      recipe.FavoriteId = favorite.Id;
      recipe.Creator = account;
      return recipe;
    }, new { userId }).ToList();

    return favoriteRecipes;
  }



}