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

const account = computed(() => AppState.account
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


async function deleteIngredient(ingredientId) {
  try {
    await ingredientsService.deleteIngredient(ingredientId)
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
            <li v-for="ingredient in ingredients" :key="ingredient.id"
              class="list-group-item d-flex align-items-center">
              <button v-if="recipe.creatorId == account.id" @click="deleteIngredient(ingredient.id)" class="btn">
                <svg viewBox="0 0 15 17.5" height="17.5" width="15" xmlns="http://www.w3.org/2000/svg" class="icon">
                  <path transform="translate(-2.5 -1.25)"
                    d="M15,18.75H5A1.251,1.251,0,0,1,3.75,17.5V5H2.5V3.75h15V5H16.25V17.5A1.251,1.251,0,0,1,15,18.75ZM5,5V17.5H15V5Zm7.5,10H11.25V7.5H12.5V15ZM8.75,15H7.5V7.5H8.75V15ZM12.5,2.5h-5V1.25h5V2.5Z"
                    id="Fill"></path>
                </svg>
              </button>
              {{ ingredient.quantity }} {{ ingredient.name }}
            </li>
          </ul>

          <form v-if="recipe.creatorId == account.id" @submit="createIngredient()">
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


<style lang="scss" scoped>
.btn {
  background-color: transparent;
  position: relative;
  border: none;
}

.btn::after {
  position: absolute;
  top: -130%;
  left: 50%;
  transform: translateX(-50%);
  width: fit-content;
  height: fit-content;
  background-color: rgb(168, 7, 7);
  padding: 4px 8px;
  border-radius: 5px;
  transition: .2s linear;
  transition-delay: .2s;
  color: white;
  text-transform: uppercase;
  font-size: 12px;
  opacity: 0;
  visibility: hidden;
}

.icon {
  transform: scale(1.2);
  transition: .2s linear;
}

.btn:hover>.icon {
  transform: scale(1.5);
}

.btn:hover>.icon path {
  fill: rgb(168, 7, 7);
}

.btn:hover::after {
  visibility: visible;
  opacity: 1;
  top: -160%;
}
</style>