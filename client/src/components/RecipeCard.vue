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

const account = computed(() =>
  AppState.account
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

async function deleteRecipe(recipeId) {
  try {
    const yes = await Pop.confirm("Are you sure you want to delete this recipe?")
    if (!yes) return
    await recipesService.deleteRecipe(recipeId)
    Pop.toast('Recipe Deleted')
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

    <button @click="deleteRecipe(recipe.id)" v-if="recipe.creatorId == account?.id"
      class="delete-button position-absolute top-0 end-0 m-2" onclick="event.stopPropagation()">
      <svg class="delete-svgIcon" viewBox="0 0 448 512">
        <path
          d="M135.2 17.7L128 32H32C14.3 32 0 46.3 0 64S14.3 96 32 96H416c17.7 0 32-14.3 32-32s-14.3-32-32-32H320l-7.2-14.3C307.4 6.8 296.3 0 284.2 0H163.8c-12.1 0-23.2 6.8-28.6 17.7zM416 128H32L53.2 467c1.6 25.3 22.6 45 47.9 45H346.9c25.3 0 46.3-19.7 47.9-45L416 128z">
        </path>
      </svg>
    </button>
    <button v-else-if="account" class="btn btn-primary position-absolute top-0 end-0 m-2"
      onclick="event.stopPropagation()">
      Favorite
    </button>

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

.delete-button {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  background-color: rgba(20, 20, 20, 0.6);
  border: none;
  font-weight: 600;
  display: flex;
  align-items: center;
  justify-content: center;
  box-shadow: 0px 0px 20px rgba(0, 0, 0, 0.164);
  cursor: pointer;
  transition-duration: 0.3s;
  overflow: hidden;
  position: relative;
}

.delete-svgIcon {
  width: 15px;
  transition-duration: 0.3s;
}

.delete-svgIcon path {
  fill: white;
}

.delete-button:hover {
  width: 90px;
  border-radius: 50px;
  transition-duration: 0.3s;
  background-color: rgb(255, 69, 69);
  align-items: center;
}

.delete-button:hover .delete-svgIcon {
  width: 20px;
  transition-duration: 0.3s;
  transform: translateY(60%);
  -webkit-transform: rotate(360deg);
  -moz-transform: rotate(360deg);
  -o-transform: rotate(360deg);
  -ms-transform: rotate(360deg);
  transform: rotate(360deg);
}

.delete-button::before {
  display: none;
  content: "Delete";
  color: white;
  transition-duration: 0.3s;
  font-size: 2px;
}

.delete-button:hover::before {
  display: block;
  padding-right: 10px;
  font-size: 13px;
  opacity: 1;
  transform: translateY(0px);
  transition-duration: 0.3s;
}
</style>
