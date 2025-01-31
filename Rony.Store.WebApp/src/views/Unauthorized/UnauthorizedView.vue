<template>
  <div class="not-authorized">
    <h1>Not authorized</h1>
    <div class="not-authorized-icon"></div>
    <p>It seems you do not have the needed permissions to access this. Please contact your administrator or log in with a user that have all permissions needed.</p>
  </div>
</template>

<script setup lang="ts">
</script>

<style scoped>
.not-authorized{
  color: red;
  display: flex;
  flex-direction: column;
  align-items: center;
  max-width: 600px;
  margin: auto;
}

.not-authorized-icon {
  position: relative;
  width: 100px;
  height: 100px;
  border: 10px solid red;
  border-radius: 50%;
  background-color: white;
}

.not-authorized-icon::before {
  content: '';
  position: absolute;
  width: 100%;
  height: 10px;
  background-color: red;
  top: 50%;
  left: 0;
  transform: translateY(-50%) rotate(45deg);
}

</style>
