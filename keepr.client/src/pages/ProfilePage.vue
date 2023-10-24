<template>
    <div class="container">
      <div v-if="profile">
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
          
          
      </div>
    </div>
</template>

<script>
import { useRoute } from 'vue-router';
import Pop from '../utils/Pop';
import { profilesService } from '../services/ProfilesService';
import { computed, watchEffect } from 'vue';
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService';
import { vaultsService } from '../services/VaultsService';


export default {
setup() {
  const route = useRoute({});

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
  return {
    profile: computed(() => AppState.activeProfile),
    keeps: computed(() => AppState.keeps),
    vaults: computed(() => AppState.vaults)
  };
},
};
</script>


<style>

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
}

</style>