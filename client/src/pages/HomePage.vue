<script setup>
import { AppState } from '@/AppState';
import Login from '@/components/Login.vue';
import RecipeCard from '@/components/RecipeCard.vue';
import RecipeModal from '@/components/RecipeModal.vue';
import { recipesService } from '@/services/RecipesService';
import Pop from '@/utils/Pop';
import { computed, onMounted } from 'vue';



const recipes = computed(() =>
  AppState.recipes
)

const activeRecipe = computed(() =>
  AppState.activeRecipe
)


onMounted(() => {
  getRecipes()
})


async function getRecipes() {
  try {
    await recipesService.getRecipes()
  }
  catch (error) {
    Pop.error(error);
  }
}



</script>

<template>
  <div>
    <div>
      <div class="position-relative">
        <img src="https://images.unsplash.com/photo-1473093226795-af9932fe5856?w=2000&auto=format&fit=crop&q=80"
          alt="Header Image" class="header-image">
        <div class="header-content text-center">
          <h1 class="display-4">All Spice</h1>
          <p class="lead">Cherish Your Family<br>And Their Cooking</p>
        </div>
        <div class="header-actions">
          <form class="d-flex search-bar">
            <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search" />
            <button class="btn btn-outline-success" type="submit">Search</button>
          </form>
          <Login />
        </div>
      </div>
    </div>

    <nav class="navbar navbar-expand-lg navbar-custom">
      <div class="container position-relative">
        <div class="row justify-content-center w-100 spill-container">
          <div class="col-md-6 spill-over">
            <ul class="navbar-nav d-flex justify-content-center shadow p-2 rounded">
              <li class="nav-item">
                <a class="nav-link" href="#">Home</a>
              </li>
              <li class="nav-item">
                <a class="nav-link" href="#">My Recipes</a>
              </li>
              <li class="nav-item">
                <a class="nav-link" href="#">Favorites</a>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </nav>
  </div>

  <div class="container">
    <section class="row justify-content-evenly">
      <div class="col-md-4 my-4" v-for="recipe in recipes" :key="recipe.id">
        <RecipeCard :recipe="recipe" />
      </div>
    </section>
  </div>



</template>

<style scoped>
.header-image {
  height: 300px;
  object-fit: cover;
  width: 100%;
}

.header-content {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  color: white;
}

.spill-container {
  margin-top: -35px;
}

.spill-over {
  position: relative;
  z-index: 10;
}

.spill-over ul {
  background-color: white;
}

.navbar-custom {
  border-radius: 8px;
}

.navbar-nav .nav-link {
  font-size: 1.1rem;
  font-weight: bold;
  color: #007b5e;
}

.navbar-nav .nav-link:hover {
  color: #00573e;
}

.shadow {
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.header-actions {
  position: absolute;
  top: 10%;
  right: 5%;
  display: flex;
  align-items: center;
  gap: 1rem;
}

.search-bar input {
  width: 200px;
}

.search-bar button {
  white-space: nowrap;
}

.login-btn {
  background-color: #007bff;
  border: none;
  color: white;
}

.login-btn:hover {
  background-color: #0056b3;
}
</style>
