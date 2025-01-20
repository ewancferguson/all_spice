<script setup>
import { AppState } from '@/AppState';
import { Ingredient } from '@/models/Ingredient';
import { Recipe } from '@/models/Recipe';
import { ingredientsService } from '@/services/IngredientsService';
import Pop from '@/utils/Pop';
import { computed, onMounted } from 'vue';



defineProps({
  recipe: { type: Recipe, required: true },
});

const ingredients = computed(() =>
  AppState.ingredients
)

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
          <h6>Instructions</h6>
          <p>{{ recipe.instructions }}</p>
        </div>
      </div>
    </div>
  </div>



</template>


<style lang="scss" scoped></style>