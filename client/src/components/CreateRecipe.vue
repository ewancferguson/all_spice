<script setup>
import { recipesService } from '@/services/RecipesService';
import Pop from '@/utils/Pop';
import { ref } from 'vue';
import RecipeModal from './RecipeModal.vue';
import { Modal } from 'bootstrap';



const editableRecipeData = ref({
  title: '',
  instructions: '',
  img: '',
  category: ''
})


async function createRecipe() {
  try {
    await recipesService.createRecipe(editableRecipeData.value)

    editableRecipeData.value =
    {
      title: '',
      instructions: '',
      img: '',
      category: ''
    }
    Pop.success("Recipe Created")
    Modal.getInstance('#createModal').hide()
    Modal.getOrCreateInstance('#recipeModal').show()
  }
  catch (error) {
    Pop.error(error);
  }
}





</script>


<template>
  <div>
    <div class="modal fade" id="createModal" tabindex="-1" aria-labelledby="createModalLabel" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="createModalLabel">Create Recipe</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <form @submit.prevent="createRecipe()">
              <div class="mb-3">
                <label for="recipeTitle" class="form-label">Title</label>
                <input v-model="editableRecipeData.title" type="text" id="recipeTitle" class="form-control" required>
              </div>
              <div class="mb-3">
                <label for="recipeImgUrl" class="form-label">Image URL</label>
                <input v-model="editableRecipeData.img" type="url" id="recipeImgUrl" class="form-control" required>
              </div>
              <div class="mb-3">
                <label for="recipeInstructions" class="form-label">Instructions</label>
                <textarea v-model="editableRecipeData.instructions" id="recipeInstructions" class="form-control"
                  rows="4" required></textarea>
              </div>
              <div class="mb-3">
                <label for="recipeCategory" class="form-label">Category</label>
                <select v-model="editableRecipeData.category" id="recipeCategory" class="form-select" required>
                  <option value="" disabled selected>Select category</option>
                  <option value="breakfast">Breakfast</option>
                  <option value="lunch">Lunch</option>
                  <option value="dinner">Dinner</option>
                  <option value="snack">Snack</option>
                  <option value="dessert">Dessert</option>
                </select>
              </div>
              <button type="submit" class="btn btn-primary w-100">Submit</button>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>


  <RecipeModal />

</template>

<style scoped>
.modal {
  background-color: rgba(0, 0, 0, 0.5);
}

.modal-dialog {
  max-width: 500px;
  width: 100%;
}

.modal-content {
  border-radius: 10px;
  overflow: hidden;
  animation: slide-down 0.3s ease-out;
}

@keyframes slide-down {
  from {
    transform: translateY(-50px);
    opacity: 0;
  }

  to {
    transform: translateY(0);
    opacity: 1;
  }
}
</style>