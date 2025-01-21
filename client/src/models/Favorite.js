export class Favorite {
  constructor(data) {
    this.id = data.id
    this.createdAt = data.createdAt
    this.updatedAt = data.updatedAt
    this.accountId = data.accountId
    this.recipeId = data.recipeId
  }
}