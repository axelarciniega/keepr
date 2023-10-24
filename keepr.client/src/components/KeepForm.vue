<template>
    <div class="row">
            <h1>Create Keep Form</h1>
            <form @submit.prevent="createKeep">
                <div class="col-6">
                    <label for="Name">Name</label>
                    <input v-model="keepData.name" maxlength="40" type="text" class="form-control">
                </div>

                <div class="col-6">
                    <label for="Description">Description</label>
                    <textarea v-model="keepData.description" maxlength="500" id="description" rows="5" class="form-control"></textarea>
                </div>

                <div class="col-6">
                    <label for="Image">Image</label>
                    <input v-model="keepData.img" maxlenth="500" type="text" class="form-control">
                </div>

                <div class="text-center pt-4">
                    <button>Create <i class="mdi mdi-creation"></i></button>
                </div>
            </form>
    </div>
</template>

<script>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService';
import { Modal } from 'bootstrap';


export default {

setup() {
    const keepData = ref({});
    const router = useRouter();
    
    function resetForm(){
        keepData.value = {type: ''};
    }
  return {
    keepData,
    async createKeep(){
        try {
            await keepsService.createKeep(keepData.value)
            Pop.success('Created Keep')
            resetForm();
            Modal.getOrCreateInstance('#KeepModal').hide();

        } catch (error) {
            Pop.error();
        }
    }
  };
},
};
</script>


<style>
</style>