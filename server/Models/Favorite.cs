namespace all_spice.Models;


public class Favorite
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public string AccountId { get; set; }
  public int RecipeId { get; set; }
}


public class FavoriteProfile : Profile
{
  public int FavoriteId { get; set; }
  public int RecipeId { get; set; }
}


public class FavoriteRecipe : Recipe
{
  public int FavoriteId { get; set; }
  public string AccountId { get; set; }
}
