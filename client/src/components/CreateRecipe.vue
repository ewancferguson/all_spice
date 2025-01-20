<script setup>

</script>


<template>
  <div>

    <div class="modal fade show" id="createModal" tabindex="-1">
      <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Create Recipe</h5>
            <button type="button" class="btn-close" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <form>
              <div class="mb-3">
                <label for="recipeTitle" class="form-label">Title</label>
                <input type="text" id="recipeTitle" class="form-control" required>
              </div>
              <div class="mb-3">
                <label for="recipeImgUrl" class="form-label">Image URL</label>
                <input type="url" id="recipeImgUrl" class="form-control" required>
              </div>
              <div class="mb-3">
                <label for="recipeInstructions" class="form-label">Instructions</label>
                <textarea id="recipeInstructions" class="form-control" rows="4" required></textarea>
              </div>
              <button type="submit" class="btn btn-primary w-100">Submit</button>
            </form>
          </div>
        </div>
      </div>
      <div class="modal-backdrop fade show"></div>
    </div>
  </div>
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