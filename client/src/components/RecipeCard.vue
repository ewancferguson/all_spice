<script setup>
import { AppState } from '@/AppState';
import { Recipe } from '@/models/Recipe';
import { recipesService } from '@/services/RecipesService';
import Pop from '@/utils/Pop';
import { computed } from 'vue';
import RecipeModal from './RecipeModal.vue';
import { Modal } from 'bootstrap';
import { ingredientsService } from '@/services/IngredientsService';

defineProps({
  recipe: { type: Recipe, required: true },
});

const activeRecipe = computed(() =>
  AppState.activeRecipe
)

async function getRecipeById(recipeId) {
  try {
    await recipesService.getRecipeById(recipeId)
    getIngredients(recipeId)
    Modal.getOrCreateInstance('#recipeModal').show()
  }
  catch (error) {
    Pop.error(error);
  }
}


async function getIngredients(recipeId) {
  try {
    await ingredientsService.getIngredients(recipeId)
  }
  catch (error) {
    Pop.error(error);
  }
}


</script>

<template>
  <div v-if="recipe" @click="getRecipeById(recipe.id)"
    class="cover-img p-2 d-flex flex-column justify-content-end border border-secondary rounded shadow-lg position-relative overflow-hidden"
    :style="{ backgroundImage: `url(${recipe.img})` }" role="button" tabindex="0">

    <span v-if="recipe" class="badge glass-card text-capitalize position-absolute top-0 start-0 m-2">
      {{ recipe.category }}
    </span>

    <div class="glass-card text-light p-3 d-flex justify-content-between align-items-center">
      <div>
        <span class="mb-1"><b>{{ recipe.title }}</b></span>
      </div>
    </div>
  </div>

  <RecipeModal :recipe="activeRecipe" />
</template>

<style lang="scss" scoped>
.cover-img {
  min-height: 40dvh;
  background-size: cover;
  background-position: center;
  transition: transform 0.3s ease, box-shadow 0.3s ease;
}

.cover-img:hover {
  transform: scale(1.03);
}

.glass-card {
  background: rgba(0, 0, 0, 0.6);
  border-radius: 8px;
}
</style>
