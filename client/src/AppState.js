import { reactive } from 'vue'
import { Ingredient } from './models/Ingredient.js'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  /**@type {import('@bcwdev/auth0provider-client').Identity} */
  identity: null,
  /** @type {import('./models/Account.js').Account} user info from the database*/
  account: null,
  /** @type {import('./models/Recipe.js').Recipe[]} user info from the database*/
  recipes: [],
  /** @type {import('./models/Recipe.js').Recipe} user info from the database*/
  activeRecipe: null,
  /** @type {import('./models/Ingredient.js').Ingredient[]} user info from the database*/
  ingredients: [],
  /** @type {import('./models/Favorite.js').FavoriteRecipe[]} user info from the database*/
  favoriteRecipes: []
})

