<script setup>
import { AppState } from '@/AppState';
import { Ingredient } from '@/models/Ingredient';
import { Recipe } from '@/models/Recipe';
import { ingredientsService } from '@/services/IngredientsService';
import Pop from '@/utils/Pop';
import { computed, onMounted, ref } from 'vue';



const recipe = computed(() =>
  AppState.activeRecipe
)

const ingredients = computed(() =>
  AppState.ingredients
)


const editableIngredientData = ref({
  name: '',
  quantity: '',
  recipeId: null
})

async function createIngredient() {
  try {
    editableIngredientData.value.recipeId = AppState.activeRecipe.id
    await ingredientsService.createIngredient(editableIngredientData.value)
    editableIngredientData.value =
    {
      name: '',
      quantity: '',
      recipeId: null
    }

  }
  catch (error) {
    Pop.error(error);
  }

}
</script>


<template>


  <div v-if="recipe" class="modal fade" id="recipeModal" tabindex="-1" aria-labelledby="recipeModalLabel"
    aria-hidden="true">
    <div class="modal-dialog modal-lg">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="recipeModalLabel">{{ recipe.title }}</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <img :src="recipe.img" class="img-fluid mb-3" alt="Recipe Image">
          <h6>Ingredients</h6>
          <ul class="list-group list-group-flush mb-3">
            <li v-for="ingredient in ingredients" :key="ingredient.id" class="list-group-item">{{ ingredient.quantity }}
              {{ ingredient.name }}</li>
          </ul>
          <form @submit="createIngredient()">
            <div class="d-flex mb-3">
              <div class="flex-grow-1 me-2">
                <label for="ingredientName" class="form-label">Ingredient Name</label>
                <input v-model="editableIngredientData.name" type="text" id="ingredientName" class="form-control"
                  required>
              </div>
              <div class="flex-grow-1 me-2">
                <label for="ingredientQuantity" class="form-label">Quantity</label>
                <input v-model="editableIngredientData.quantity" type="text" id="ingredientQuantity"
                  class="form-control" required>
              </div>
              <button type="submit" class="btn btn-primary d-flex align-items-center">
                <span class="me-2">Add</span><i class="bi bi-plus-circle"></i>
              </button>
            </div>
          </form>
          <h6>Instructions</h6>
          <p>{{ recipe.instructions }}</p>
        </div>
      </div>
    </div>
  </div>



</template>


<style lang="scss" scoped></style>