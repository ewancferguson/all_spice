<script setup>
import { AppState } from '@/AppState';
import CreateRecipe from '@/components/CreateRecipe.vue';
import Login from '@/components/Login.vue';
import RecipeCard from '@/components/RecipeCard.vue';
import RecipeModal from '@/components/RecipeModal.vue';
import { accountService } from '@/services/AccountService';
import { recipesService } from '@/services/RecipesService';
import Pop from '@/utils/Pop';
import { computed, onMounted, ref } from 'vue';



const recipes = computed(() => {
  if (activeFilterCategory.value == 'home') return AppState.recipes
  if (activeFilterCategory.value == 'myRecipes') return AppState.recipes.filter(recipe => recipe.creatorId == account.value.id)
  return AppState.favoriteRecipes
}
)



const activeFilterCategory = ref('home')


const activeRecipe = computed(() =>
  AppState.activeRecipe
)

const account = computed(() =>
  AppState.account
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

async function getFavoriteRecipes() {
  try {
    await accountService.getFavoriteRecipes()
  }
  catch (error) {
    console.error('NOT LOGGED IN LOG IN TO SEE FAVORITE RECIPES', error);

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

    <nav v-if="account" class="navbar navbar-expand-lg navbar-custom">
      <div class="container position-relative">
        <div class="row justify-content-center w-100 spill-container">
          <div class="col-md-6 spill-over">
            <ul class="navbar-nav d-flex justify-content-center shadow p-2 rounded">
              <li @click="activeFilterCategory = 'home'" class="nav-item">
                <a class="nav-link" href="#">Home</a>
              </li>
              <li @click="activeFilterCategory = 'myRecipes'" class="nav-item">
                <a class="nav-link" href="#">My Recipes</a>
              </li>
              <li @click="activeFilterCategory = 'hello'" class="nav-item">
                <a class="nav-link" href="#">Favorites</a>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </nav>

    <div class="container">
      <section class="row justify-content-evenly">
        <div class="col-md-4 my-4" v-for="recipe in recipes" :key="recipe.id">
          <RecipeCard :recipe="recipe" />
        </div>
      </section>
    </div>

    <CreateRecipe />

    <button v-if="account" data-bs-toggle="modal" data-bs-target="#createModal"
      class="btn btn-primary fixed-bottom-right">
      Create Recipe
    </button>
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

.fixed-bottom-right {
  position: fixed;
  bottom: 20px;
  right: 20px;
  z-index: 1000;
  background-color: #007b5e;
  color: white;
  padding: 15px;
  border-radius: 50%;
  font-size: 1.5rem;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
  border: none;
  transition: background-color 0.3s ease;
}

.fixed-bottom-right:hover {
  background-color: #00573e;
}
</style>
