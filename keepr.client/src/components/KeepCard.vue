<template>
    <!-- <div class="container"> -->
        <!-- STUB Keep Card -->
        <!-- <section class="row"> -->
            <div @click="getKeepDetails()" class="position-relative" data-bs-toggle="modal" data-bs-target="#DetailModal">
                <!-- <div class="img-s" :style="`background-image: url(${keep.img})`"> -->
                    <img class="img-s" :src="keep.img" alt="">
                    <!-- <img class="img-s" :src="keep.img"> -->
                    <div class="justify-content-between d-flex keep-details">

                        <p class=" col-5 col-md-5 absolute text-center m-3">{{ keep.name }}</p>
                            <img class="profile-pic col-12 col-md-4" :src="keep.creator.picture" alt="">
                    </div>
                <!-- </div> -->
            </div>
        <!-- </section> -->


        <!-- STUB modal -->
        <div class="modal fade" id="DetailModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div  class="modal-dialog modal-xl">
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
                            <div class="row">
                                <div class="col-6">
                                    <button>Save to vault</button>
                                </div>
                                <div class="col-6">
                                    <select name="vault-picker" id="vault-picker" class="form-control">
                                        <option value="" disabled selected>select a vault</option>
                                        <option v-for="keep in keepss" :key="keep.id" select="+keepss.id" value="keepss.id">{{ keepss.name }}</option>
                                    </select>
                                </div>
                                <div>
                        <router-link :to="{name: 'Profile', params: {profileId: keep.creatorId}}">

                                    <img class="profile-pic" :src="activeKeep.creator.picture" alt="">
                        </router-link>

                                </div>

                            </div>
                        </div>
                    </div>
                </section>
            </div>
            <!-- <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                <button  type="button" class="btn btn-primary">Save changes</button>
            </div> -->
            </div>
        </div>
        </div>



    <!-- </div> -->
    </template>

<script>
import { computed } from 'vue';
import { Keep } from '../models/Keep';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService';
import { logger } from '../utils/Logger';

export default {
    props: {keep: {type: Keep, required: true}},
setup(props) {
  return {
    keepss: computed(()=> AppState.keeps),
    activeKeep: computed(()=> AppState.activeKeep),
    async getKeepDetails(){
        try {
            logger.log(props.keep.id)
            const keepId = props.keep.id
            await keepsService.getKeepDetails(keepId)
        } catch (error) {
            Pop.error(error)
        }
    }
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

   

    .absolute{
        border-radius: px;
        background-color: rgba(0, 0, 0, 0.255);
        backdrop-filter: blur(20px);
        color:white;
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