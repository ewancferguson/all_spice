


namespace all_spice.Services;

public class FavoritesService
{

  public FavoritesService(FavoritesRepository favoritesRepository)
  {
    _repository = favoritesRepository;
  }

  private readonly FavoritesRepository _repository;

  internal FavoriteRecipe CreateFavorite(Favorite favoriteData)
  {
    FavoriteRecipe favorite = _repository.CreateFavorite(favoriteData);
    return favorite;
  }

  internal List<FavoriteRecipe> GetFavoritesByAccount(string userId)
  {
    List<FavoriteRecipe> favoriteRecipes = _repository.GetFavoritesByAccount(userId);
    return favoriteRecipes;
  }


  private Favorite GetFavoriteById(int favoriteId)
  {
    Favorite favorite = _repository.GetFavoriteById(favoriteId);

    if (favorite == null) throw new Exception("does not exist");
    return favorite;
  }

  internal string DeleteFavorite(int favoriteId, string userId)
  {
    Favorite favorite = GetFavoriteById(favoriteId);
    if (favorite.AccountId != userId) throw new Exception("Not you account");
    _repository.DeleteFavorite(favoriteId);

    return "No longer favorited";
  }
}