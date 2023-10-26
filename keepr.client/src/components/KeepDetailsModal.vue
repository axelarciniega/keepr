<template>
       <div class="modal fade" id="DetailModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div  class="modal-dialog modal-lg">
            <div v-if="activeKeep" class="modal-content">
            <div class="modal-header">
                <h1 class="modal-title fs-5" id="exampleModalLabel">{{ activeKeep.name }}</h1>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <section class="row">
                    <div class="col-12 col-md-6">
                        <img class="activeImage" :src="activeKeep.img" alt="">
                    </div>
                    <div class="col-12 col-md-6 d-flex justify-content-between">
                        <div class="row">
                            <div class="col-6">
                                <i class="mdi mdi-eye"></i> {{ activeKeep.views }}
                            </div>
                            <div class="col-6">
                                <i class="mdi mdi-alpha-k-box"></i> {{ activeKeep.kept }}
                            </div>
                            <div class="row">
                                <div>
                                    <h1 class="text-center">{{ activeKeep.name }}</h1>
                                </div>
                                <div>
                                    <p class="text-center">{{ activeKeep.description }}</p>
                                </div>
                            </div>
                            <div  class="row">
                                <form v-if="account.id" @submit.prevent="createVaultKeep(vaultId)">
                                <div class="col-6">
                                    <select required v-model="formData.vaultId" name="vault-picker" id="vault-picker" class="form-control">
                                        <option value="" disabled selected>select a vault</option>
                                        <option v-for="vault in vaults" :key="'select-'+vault.id" :value="vault.id">{{ vault.name }}</option>
                                    </select>
                                </div>
                                <div  class="col-6">
                                    <button>Save to vault</button>
                                </div>
                            </form>

                            <div>
                                    <button @click="removeVaultKeep(activeKeep.vaultKeepId)">Remove From vault</button>
                            </div>
                                <div>
                                    <img @click="goToAccount" class="profile-pic selectable" :src="activeKeep.creatorPic" alt="">
                                    <p>{{ activeKeep.creator.name }}</p>
                                    <div class="pt-3" v-if="account.id == activeKeep.creatorId">
                                        <button @click="removeKeep(activeKeep.id)">Remove Keep</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
            </div>
        </div>
        </div>
</template>

<script>
import { computed, onMounted, ref, onUnmounted } from 'vue';
import { Keep } from '../models/Keep';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService';
import { logger } from '../utils/Logger';
import { useRoute, useRouter } from 'vue-router';
import { Modal } from 'bootstrap';
import { vaultsService } from '../services/VaultsService';
import { vaultKeepsService } from '../services/VaultKeepsService.js';
import { clearService } from '../services/ClearService';

export default {
    
    
setup() {

    

    const router = useRouter({})
    const route = useRoute({})
    const formData = ref({})

     function resetForm(){
        formData.value = {vaultId: ''}
    }

  return {
    formData,
    vaults: computed(() => AppState.myVaults),
    keepss: computed(()=> AppState.keeps),
    activeKeep: computed(()=> AppState.activeKeep),
    account: computed(() => AppState.account),
    activeVault: computed(() => AppState.activeVault),
     async createVaultKeep(){
        try {
            const keepId = AppState.activeKeep.id
            formData.value.keepId = keepId
            await vaultKeepsService.createVaultKeep(formData.value)
            AppState.activeKeep.kept++
            Pop.success('Saved')
            resetForm();
        } catch (error) {
            Pop.error(error);
        }
    },
    
    async removeVaultKeep(vaultKeepId){
        try {
            if(await Pop.confirm()){
                await vaultKeepsService.removeVaultKeep(vaultKeepId)
                Modal.getOrCreateInstance('#DetailModal').hide();
            }
        } catch (error) {
            Pop.error(error)
        }
    },
     async removeKeep(keepId){
        try {
            if(await Pop.confirm()){
                await keepsService.removeKeep(keepId)
                Pop.success('Deleted Keep')
                Modal.getOrCreateInstance('#DetailModal').hide();
            }
        } catch (error) {
            Pop.error(error);
        }
    },

    // MOVE
    async goToAccount(){
        try {
            router.push({name: 'Profile', params: {profileId: AppState.activeKeep.creatorId}})
            Modal.getOrCreateInstance('#DetailModal').hide();
        } catch (error) {
            Pop.error(error);
        }
    }
  };
},
};
</script>


<style>
 .activeImage{
        width: 100%;
        height: 100%;
        border-radius: 10px;
    }

     .profile-pic{
        
     
        width: 8vh;
        height: 8vhs;
        border-radius: 50%;
    }

     @media(max-width: 768px){
       

        .profile-pic{
        width: 30px;
        height: 30px;
        border-radius: 50px;
        margin: 8px;
        }
     }
</style>