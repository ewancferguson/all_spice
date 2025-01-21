export class FavoriteRecipe {
  constructor(data) {

    this.favoriteId = data.favoriteId
    this.accountId = data.accountId
    this.id = data.id
    this.createdAt = data.createdAt
    this.updatedAt = data.updatedAt
    this.title = data.title
    this.instructions = data.instructions
    this.img = data.img
    this.category = data.category
    this.creatorId = data.creatorId
    this.creator = data.creator

  }
}