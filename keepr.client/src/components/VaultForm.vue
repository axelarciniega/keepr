<template>
    <div class="row">
        <h1>Create Vault Form</h1>
        <form @submit.prevent="createVault">

            <div class="col-6">
                <label for="Name">Name</label>
                <input v-model="vaultData.name" maxlength="40" type="text" class="form-control">
            </div>

            <div class="col-6">
                <label for="description">Description</label>
                <textarea v-model="vaultData.description" maxlength="500" id="description" rows="6"></textarea>
            </div>

            <div class="col-6">
                <label for="image">Image</label>
                <input v-model="vaultData.img" maxlength="500" type="text" class="form-control">
            </div>

            <div class="col-4 pt-3">
                    <label for="isPrivate">Private??</label>
                    <input v-model="vaultData.isPrivate" type="checkbox" class="form-checkbox" id="isPrivateBox">
                </div>

                <div class="pt-3">
                    <button>Create Vault <i class="mdi mdi-creation"></i></button>
                </div>
        </form>
    </div>
</template>

<script>
import { ref } from 'vue';
import Pop from '../utils/Pop';
import { vaultsService } from '../services/VaultsService';
import { Modal } from 'bootstrap';

export default {
setup() {
    const vaultData = ref({});
    function resetForm(){
        vaultData.value = {type: ''};
    }
  return {
    vaultData,
    async createVault(){
        try {
            await vaultsService.createVault(vaultData.value)
            Pop.success('Created Vault')
            resetForm();
            Modal.getOrCreateInstance('#VaultModal').hide();
        } catch (error) {
            Pop.error(error);
        }
    }
  };
},
};
</script>


<style>
</style>