import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Recipe } from "@/models/Recipe.js"
import { AppState } from "@/AppState.js"

class RecipesService {
  async getRecipes() {
    const response = await api.get('api/recipes')
    logger.log(response.data)
    const recipes = response.data.map(recipePOJO => new Recipe(recipePOJO))
    AppState.recipes = recipes
  }

}


export const recipesService = new RecipesService()