<template>
    <div class="container">
        <div v-if="activeVault">
            {{ activeVault.name }}
        </div>
    </div>
</template>

<script>
import { useRoute } from 'vue-router';
import Pop from '../utils/Pop';
import { vaultsService } from '../services/VaultsService';
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState';

export default {
    
setup() {
    onMounted(()=> {
        vaultDetails();
    })
    const route = useRoute({});
    async function vaultDetails(){
        try {
            const vaultId = route.params.vaultId
            await vaultsService.vaultDetails(vaultId)
        } catch (error) {
            Pop.error(error)
        }
    }
  return {
    activeVault: computed(()=> AppState.activeVault)
  };
},
};
</script>


<style>
</style>