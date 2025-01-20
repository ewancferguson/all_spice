import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Recipe } from "@/models/Recipe.js"
import { AppState } from "@/AppState.js"

class RecipesService {
  async getRecipeById(recipeId) {
    AppState.activeRecipe = null
    const response = await api.get(`api/recipes/${recipeId}`)
    logger.log(response.data)
    const recipe = new Recipe(response.data)
    AppState.activeRecipe = recipe
  }
  async getRecipes() {
    const response = await api.get('api/recipes')
    logger.log(response.data)
    const recipes = response.data.map(recipePOJO => new Recipe(recipePOJO))
    AppState.recipes = recipes
  }

}


export const recipesService = new RecipesService()