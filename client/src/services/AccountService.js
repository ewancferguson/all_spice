import { FavoriteRecipe } from '@/models/Favorite.js'
import { AppState } from '../AppState.js'
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger.js'
import { api } from './AxiosService.js'

class AccountService {
  async getFavoriteRecipes() {
    const response = await api.get('account/favorites')
    logger.log('getting fav recipes', response.data)
    const recipes = response.data.map(recipePOJO => new FavoriteRecipe(recipePOJO))
    AppState.favoriteRecipes = recipes
  }
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
}

export const accountService = new AccountService()
