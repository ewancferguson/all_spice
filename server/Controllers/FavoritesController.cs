namespace all_spice.Controllers;


[ApiController]
[Route("api/{controller}")]
public class FavoritesController : ControllerBase
{

  public FavoritesController(FavoritesService favoritesService, Auth0Provider auth0Provider)
  {
    _favoritesService = favoritesService;
    _auth0Provider = auth0Provider;
  }

  private readonly FavoritesService _favoritesService;
  private readonly Auth0Provider _auth0Provider;

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<FavoriteRecipe>> CreateFavorite([FromBody] Favorite favoriteData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      favoriteData.AccountId = userInfo.Id;
      FavoriteRecipe favoriteRecipe = _favoritesService.CreateFavorite(favoriteData);
      return Ok(favoriteRecipe);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpDelete("{favoriteId}")]
  public async Task<ActionResult<string>> DeleteFavorite(int favoriteId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _favoritesService.DeleteFavorite(favoriteId, userInfo.Id);
      return Ok(message);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}