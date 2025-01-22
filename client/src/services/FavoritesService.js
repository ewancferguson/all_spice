import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { FavoriteRecipe } from "@/models/Favorite.js"
import { AppState } from "@/AppState.js"

class FavoritesService {
  async deleteFavorite(favorite) {
    const response = await api.get(`api/favorites/${favorite}`)
    logger.log(response.data)
  }
  async createFavorite(favoriteData) {
    const response = await api.post('api/favorites', favoriteData)
    logger.log(response.data)
    const recipe = new FavoriteRecipe(response.data)
    AppState.favoriteRecipes.push(recipe)
  }

}


export const favoritesService = new FavoritesService