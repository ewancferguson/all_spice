export class Ingredient {
  constructor(data) {
    this.id = data.id
    this.createdAt = data.createdAt
    this.updatedAt = data.updatedAt
    this.name = data.name
    this.quantity = data.quantity
    this.recipeId = data.recipeId
  }
}