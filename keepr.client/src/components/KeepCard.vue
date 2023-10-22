<template>
    <div class="container">
        <!-- STUB Keep Card -->
        <section class="row">
            <div @click="getKeepDetails()" class="col-12 col-md-5 relative" data-bs-toggle="modal" data-bs-target="#DetailModal">
                <div class="img-s" :style="`background-image: url(${keep.img})`">
                    <!-- <img class="img-s" :src="keep.img"> -->
                    <p class="absolute">{{ keep.name }}</p>
                    <img class="profile-pic " :src="keep.creator.picture" alt="">
                </div>
            </div>
        </section>


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
                                <i class="mdi mdi-alpha-k-box"></i> {{ activeKeep.views }}
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



    </div>
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

    .relative{
        position: relative;
    }

    .absolute{
        position: absolute;
        top: 43vh;
        left: 60px;
        background-color: rgba(0, 0, 0, 0.255);
        backdrop-filter: blur(20px);
        min-width: 70px;
        color:white;
    }

  
    .profile-pic{
        position: absolute;
        left: 240px;
        top: 40vh;
        width: 8vh;
        height: 8vhs;
        border-radius: 50%;
    }



    .img-s{
        border-radius: 15px;
        background-position: center;
        background-size: cover;
        width: 40vh;
        height: 50vh;
        $gap: 1.25em;
        column-gap: $gap;
        margin: $gap;
    }
    .img-s:hover{
        cursor: pointer;
    }

    @media(max-width: 768px){
       

        .profile-pic{
        position: absolute;
        left: 5px;
        top: 40vh;
        width: 8vh;
        height: 8vhs;
        border-radius: 50%;
        }

        .absolute{
            position: absolute;

        }

        .img-s{
        border-radius: 15px;
        background-position: center;
        background-size: cover;
        width: 15vh;
        height: 30vh;
        $gap: 1.25em;
        column-gap: $gap;
        margin: $gap;
        }
    }


</style>