<template>
    <div class="container">
        <div v-if="activeVault">
            <section class="row">
                <div class="mt-4 text-center position-relative">
                    <img class="backgroundImg" :src="activeVault.img" :alt="activeVault.name">
                    <h1 class=""> By {{ activeVault.creator?.name }}</h1>
                </div> 
            </section>
            
            <div class="text-center">
                <i> {{ keeps.length }} keeps</i>
            </div>

            <div v-if="account.id == activeVault.creatorId" class="text-center">
                <button @click="removeVault(activeVault.id)">Delete Vault <i class="mdi mdi-trash-can"></i></button>
            </div>

            <section class="masonry-container">
                <div class="" v-for="k in keeps" :key="k.id">
                    <KeepCard :keep="k"/>
                </div>
            </section>

        </div>
    </div>
</template>

<script>
import { useRoute, useRouter } from 'vue-router';
import Pop from '../utils/Pop';
import { vaultsService } from '../services/VaultsService';
import { computed, onMounted, onUnmounted } from 'vue';
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService';
import { clearService } from '../services/ClearService';

export default {
    
setup() {
    onMounted(()=> {
        vaultDetails();
        getKeepsByVaultId();
    })

    onUnmounted(()=> {
        clearAppstate();
    })

    const route = useRoute({});
    const router = useRouter({})

    async function getKeepsByVaultId(){
        try {
            const vaultId = route.params.vaultId
            await keepsService.getKeepsByVaultId(vaultId)
        } catch (error) {
            Pop.error(error)
        }
    }

    async function vaultDetails(){
        try {
            const vaultId = route.params.vaultId
            await vaultsService.vaultDetails(vaultId)
        } catch (error) {
            Pop.error(error)
        }
    }

    function clearAppstate(){
        try {
            clearService.clearVaultAppstate()
        } catch (error) {
            Pop.error(error);
        }
    }
  return {
    activeVault: computed(()=> AppState.activeVault),
    keeps: computed(()=> AppState.keeps),
    account: computed(() => AppState.account),

    async removeVault(activeVaultId){
        try {
            if(await Pop.confirm()){
                vaultsService.removeVault(activeVaultId)
                Pop.success('Delete Vault')
                router.push({name: 'Home'})
            }
        } catch (error) {
            Pop.error(error);
        }
    }
  };
},
};
</script>


<style>

.masonry-container{
  columns: 4 200px;
  width: 100%;
  column-gap: 10px;

}

.title{
    position: absolute;
    bottom: 10px;
}

.backgroundImg{
    width: 60vh;
    height: 40vh;
}

</style>