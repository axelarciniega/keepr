<template>
  <div class="container m-5">
    <h3>Edit Your Account</h3>
    <form @submit.prevent="editAccount">

      <div class="col-6">
        <label for="name">Name</label>
        <input maxlength="40" v-model="editable.name" type="text" class="form-control">
      </div>

      <div class="col-6">
        <label for="Profile Picture">Profile Picture</label>
        <input maxlength="400" v-model="editable.picture" type="text" class="form-control">
         <img class="pt-2" :src="account.picture" :alt="account.name">
      </div>

      <div class="col-6">
        <label for="Cover Image">Cover Image</label>
        <input maxlength="400" v-model="editable.coverImage" type="text" class="form-control">
        <img class="pt-2" :src="account.coverImage" :alt="account.name">
       
      </div>

      <div class="col-11 -col-md-3 m-0 pt-3">
        <button>Save Changes<i class="mdi mdi-check"></i></button>
      </div>


    </form>

  </div>
</template>

<script>
import { computed, ref, watchEffect } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { accountService } from '../services/AccountService';
export default {
  setup() {
    const editable = ref({})
    watchEffect(() => {
      editable.value = AppState.account;
    });
    return {
      editable,
      account: computed(() => AppState.account),

      async editAccount(){
        try {
          await accountService.editAccount(editable.value);
          Pop.success('Changes Saved');
        } catch (error) {
          Pop.error(error);
        }
      }
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
