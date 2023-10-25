<template>
    <div v-if="profile" class="container">
      <div >
        <section class="row">
          <div class="mt-3 justify-content-center d-flex">
            <img class="coverImage" :src="profile.coverImage" :alt="profile.name">
          </div>
        </section>
        
        <section class="row">
              <div class="justify-content-center d-flex">
              <img class="profile-pic " :src="profile.picture" :alt="profile.name">
            </div>
            <h1 class="text-center">{{ profile.name }}</h1>
            <i class="text-center">{{ keeps.length }} Keeps | {{ vaults.length }} Vaults  </i>
          </section>

          <section class="row mt-4">
            <h3>Vaults</h3>
            <div class="col-3 col-md-2 m-1 " v-for="v in vaults" :key="v.id">
                <router-link :to="{name: 'Vault', params: {vaultId: v.id}}">
                  <div class="position-relative">
                    <img class="imgBack" :src="v.img" alt="">
                    <i v-if="v.isPrivate" class="mdi mdi-lock locker"></i>
                    <p class="absoluteImg">{{ v.name }}</p>
                  </div>
                </router-link>
            </div>
          </section>

          <section class="masonry-container mt-5">
            <h3>Keeps</h3> 
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
import { profilesService } from '../services/ProfilesService';
import { computed, onUnmounted, watchEffect } from 'vue';
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService';
import { vaultsService } from '../services/VaultsService';
import { clearService } from '../services/ClearService';


export default {
setup() {
  const route = useRoute({});
  const router = useRouter({})

  onUnmounted(()=> {
    clearAppstate();
  })

  watchEffect(() => {
    getProfileById();
    keepProfiles();
    vaultProfile();
  })

  async function keepProfiles(){
    try {
      const profileId = route.params.profileId
      await keepsService.keepProfile(profileId)
    } catch (error) {
      Pop.error(error)
    }
  }

  async function vaultProfile(){
    try {
      const profileId = route.params.profileId
      await vaultsService.vaultProfile(profileId)
    } catch (error) {
      Pop.error(error);
    }
  }

  async function getProfileById(){
    try {
      const profileId = route.params.profileId
      await profilesService.getProfileById(profileId)
    } catch (error) {
      Pop.error(error)
    }
  }

  function clearAppstate(){
    try {
      clearService.clearAppstate()
    } catch (error) {
      Pop.error(error);
    }
  }

  return {
    profile: computed(() => AppState.activeProfile),
    keeps: computed(() => AppState.keeps),
    vaults: computed(() => AppState.vaults),
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

.absoluteImg{
  position: absolute;
  bottom: 10px;
  color: white;
  background-color: rgba(0, 0, 0, 0.393);
}
.locker{
  position: absolute;
  left: 5px;
  z-index: 2;
}

.imgBack{
  width: 15vh;
  height: 15vh;
  border-radius: 20px;
  z-index: 1;
}

.profile-pic{
  width: 10vh;
  height: 10vh;
  border-radius: 50%;
  
}

.coverImage{
  background-position: center;
  background-size: cover;
  width: 50vw;
  height: 40vh;
}

@media(max-width: 768px){
 .coverImage{
   background-position: center;
  background-size: cover;
  width: 90vw;
  height: 20vh;
 }

 .imgBack{
  width: 12vh;
  height: 12vh;
  border-radius: 20px;
  z-index: 1;
 }

 .locker{
  position: absolute;
  left: 5px;
  z-index: 2;
}



}

</style>