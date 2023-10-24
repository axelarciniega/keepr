<template>
    <div class="container">
        <div v-if="activeVault">
            <section class="row">
                <div class="mt-4 text-center position-relative">
                    <img class="backgroundImg" :src="activeVault.img" :alt="activeVault.name">
                    <h1 class=""> By {{ activeVault.creator.name }}</h1>
                </div> 
            </section>
            
            <div class="text-center">
                <i> {{ keeps.length }} keeps</i>
            </div>

            <section class="masonry-container">
                <div class="col-3 col-md-4 m-1" v-for="k in keeps" :key="k.id">
                    <KeepCard :keep="k"/>
                </div>
            </section>

        </div>
    </div>
</template>

<script>
import { useRoute } from 'vue-router';
import Pop from '../utils/Pop';
import { vaultsService } from '../services/VaultsService';
import { computed, onMounted, onUnmounted } from 'vue';
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService';

export default {
    
setup() {
    onMounted(()=> {
        vaultDetails();
        getKeepsByVaultId();
    })

    onUnmounted(()=> {
        
    })

    const route = useRoute({});

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
  return {
    activeVault: computed(()=> AppState.activeVault),
    keeps: computed(()=> AppState.keeps)
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