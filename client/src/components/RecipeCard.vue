<script setup>
import { Recipe } from '@/models/Recipe';

defineProps({
  recipe: { type: Recipe, required: true }
});
</script>

<template>
  <div
    class="cover-img p-2 d-flex flex-column justify-content-end border border-secondary rounded shadow-lg position-relative overflow-hidden"
    :style="{ backgroundImage: `url(${recipe.img})` }" role="button" tabindex="0">
    <div class="glass-card text-light p-3 d-flex justify-content-between align-items-center">
      <div>
        <span class="mb-1"><b>{{ recipe.title }}</b></span>
        <div v-if="recipe.creator">
          <img :src="recipe.creator.picture" :alt="recipe.creator.name"
            class="creator-img rounded-circle border border-light me-2" />
          <span>{{ recipe.creator.name }}</span>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.cover-img {
  min-height: 30dvh;
  background-size: cover;
  background-position: center;
  transition: transform 0.3s ease, box-shadow 0.3s ease;
}

.cover-img:hover {
  transform: scale(1.03);
  box-shadow: 0 6px 16px rgba(0, 0, 0, 0.2);
  border-color: #6c63ff;
}

.glass-card {
  background: rgba(0, 0, 0, 0.6);
  border-radius: 8px;
}

.creator-img {
  height: 2rem;
  width: 2rem;
  object-fit: cover;
}
</style>
