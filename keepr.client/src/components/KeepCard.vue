<template>
    <!-- <div class="container"> -->
        <!-- STUB Keep Card -->
        <!-- <section class="row"> -->
            <div @click="getKeepDetails()" class="position-relative" data-bs-toggle="modal" data-bs-target="#DetailModal">
                <!-- <div class="img-s" :style="`background-image: url(${keep.img})`"> -->
                    <img class="img-s" :src="keep.img" :alt="keep.name">
                    <!-- <img class="img-s" :src="keep.img"> -->
                    <div class="justify-content-between d-flex keep-details">

                        <p class=" col-5 col-md-5 absolute text-center m-3">{{ keep.name }}</p>
                            <img :title="keep.creator.name" class="profile-pic col-12 col-md-4" :src="keep.creator.picture" :alt="keep.creator.name">
                    </div>
            </div>
    </template>

<script>
import { computed, onMounted, ref, onUnmounted, watchEffect } from 'vue';
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
    props: {keep: {type: Keep, required: true}},
setup(props) {
    const router = useRouter({})
    const route = useRoute({})
    const formData = ref({})

    onUnmounted(()=> {
        clearAppstate();
    })



    function clearAppstate(){
        try {
            clearService.clearAppstate()
        } catch (error) {
            Pop.error(error);
        }
    }

  return {
    formData,
    keepss: computed(()=> AppState.keeps),
    activeKeep: computed(()=> AppState.activeKeep),
    account: computed(() => AppState.account),
    activeVault: computed(() => AppState.activeVault),
    keepCoverImg: computed(() => `url(${props.keep.img})`),
    // selectedVault: computed (()=> AppState.myVaults.find(v => v.id == formData.value.vaultId)),
   

    // STAYS
    async getKeepDetails(){
        try {
            logger.log('active keep to be', props.keep)
            logger.log(props.keep.id)
            const keep = props.keep

            await keepsService.getKeepDetails(keep)
            AppState.activeKeep.views++
        } catch (error) {
            Pop.error(error)
        }
    },
  };
},
};
</script>


<style lang="scss" scoped>

    .activeImage{
        width: 100%;
        height: 100%;
        border-radius: 10px;
    }

    .keep-coverImg {
        background-image: v-bind(keep-coverImg)
    }

   

    .absolute{
        border-radius: px;
        backdrop-filter: blur(20px);
        color:white;
        max-height: 40px;
    }

  
    .profile-pic{
        
     
        width: 8vh;
        height: 8vhs;
        border-radius: 50%;
    }


.keep-details{
    position: absolute;
    bottom: 40px;
    width: 100%;
    padding-right: 10px;
    padding-bottom: 20px;
}
    .img-s{
        border-radius: 15px;
        object-fit: cover;
        object-position: center;
        width: 100%;
        $gap: 10px;
        column-gap: $gap;
        margin-bottom: 30px;
    }
    .img-s:hover{
        cursor: pointer;
    }

    @media(max-width: 768px){
       

        .profile-pic{
        width: 30px;
        height: 30px;
        border-radius: 50px;
        margin: 8px;
        }

.keep-details{
    position: absolute;
}

      

        .img-s{
        border-radius: 15px;
        object-fit: cover;
        object-position: center;
        width: 100%;
        height: 30vh;
        $gap: 1.25em;
        margin-bottom: 30px;
        }
    }


</style>