<script setup>
import { AppState } from '@/AppState';
import { ingredientsService } from '@/services/IngredientsService';
import Pop from '@/utils/Pop';
import { computed, ref } from 'vue';

const recipe = computed(() => AppState.activeRecipe);
const ingredients = computed(() => AppState.ingredients);
const account = computed(() => AppState.account);

const editableIngredientData = ref({
  name: '',
  quantity: '',
  recipeId: null
});

async function createIngredient() {
  try {
    editableIngredientData.value.recipeId = recipe.value.id;
    await ingredientsService.createIngredient(editableIngredientData.value);
    editableIngredientData.value = { name: '', quantity: '', recipeId: null };
  } catch (error) {
    Pop.error(error);
  }
}

async function deleteIngredient(ingredientId) {
  try {
    await ingredientsService.deleteIngredient(ingredientId);
  } catch (error) {
    Pop.error(error);
  }
}
</script>

<template>
  <div v-if="recipe" class="modal fade" id="recipeModal" tabindex="-1" aria-labelledby="recipeModalLabel"
    aria-hidden="true">
    <div class="modal-dialog modal-xl modal-dialog-scrollable">
      <div class="modal-content">
        <div class="modal-header border-0">
          <h5 class="modal-title fw-bold" id="recipeModalLabel">{{ recipe.title }}</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>

        <div class="modal-body">
          <div class="row g-4 flex-column flex-md-row">
            <!-- Left: Image -->
            <div class="col-12 col-md-5">
              <img :src="recipe.img" alt="Recipe Image" class="img-fluid w-100 recipe-img rounded shadow-sm" />
            </div>

            <!-- Right: Content -->
            <div class="col-12 col-md-7">
              <h6 class="fw-semibold mb-3">Ingredients</h6>
              <ul class="list-group list-group-flush mb-4">
                <li v-for="ingredient in ingredients" :key="ingredient.id"
                  class="list-group-item d-flex justify-content-between align-items-center">
                  <span>{{ ingredient.quantity }} {{ ingredient.name }}</span>
                  <button v-if="recipe.creatorId == account?.id" @click="deleteIngredient(ingredient.id)"
                    class="btn btn-sm btn-outline-danger d-flex align-items-center" title="Delete">
                    <i class="bi bi-trash"></i>
                  </button>
                </li>
              </ul>

              <!-- Ingredient Form -->
              <form v-if="recipe.creatorId == account?.id" @submit.prevent="createIngredient" class="mb-4">
                <div class="row g-2">
                  <div class="col-12 col-md-5">
                    <label for="ingredientName" class="form-label">Name</label>
                    <input v-model="editableIngredientData.name" type="text" id="ingredientName" class="form-control"
                      required />
                  </div>
                  <div class="col-12 col-md-4">
                    <label for="ingredientQuantity" class="form-label">Quantity</label>
                    <input v-model="editableIngredientData.quantity" type="text" id="ingredientQuantity"
                      class="form-control" required />
                  </div>
                  <div class="col-12 col-md-3 d-flex align-items-end">
                    <button type="submit" class="btn btn-primary w-100">
                      <i class="bi bi-plus-circle me-1"></i> Add
                    </button>
                  </div>
                </div>
              </form>

              <h6 class="fw-semibold">Instructions</h6>
              <p class="text-muted">{{ recipe.instructions }}</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
.recipe-img {
  max-height: 500px;
  object-fit: cover;
  border-radius: 0.75rem;
}

@media (max-width: 768px) {
  .recipe-img {
    max-height: 300px;
    border-radius: 0.5rem;
  }
}

.modal-content {
  border-radius: 1rem;
  overflow: hidden;
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.1);
}

.modal-title {
  font-size: 1.75rem;
}

.list-group-item {
  font-size: 1rem;
  background-color: #fff;
  transition: background-color 0.2s;
}

.list-group-item:hover {
  background-color: #f8f9fa;
}
</style>
