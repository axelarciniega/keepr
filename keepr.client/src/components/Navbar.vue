<template>
  <nav class="navbar navbar-expand-lg px-3 nav-color">

      <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
        <div class="d-flex flex-column align-items-center">
        <h2 class="text-black"><span class="text-secondary">K</span>eep<span class="text-secondary">r</span></h2>
      </div>
    </router-link>

      <!-- <button  class="navbar-toggle m-1" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button> -->
    <div class="collapse navbar-collapse m-2" id="navbarText">
      <button v-show="account.id" b- type="button" class="btn btn-secondary m-4" data-bs-toggle="modal" data-bs-target="#KeepModal">
        Create Keep
      </button>  
      <button v-show="account.id" type="button" class="btn btn-secondary m-4" data-bs-toggle="modal" data-bs-target="#VaultModal">
        Create Vault
      </button>

      
      
      <!-- <ul class="navbar-nav me-auto">
        <li>
          <router-link :to="{ name: 'About' }" class="btn text-success lighten-30 selectable text-uppercase">
            About
          </router-link>
        </li>
      </ul> -->
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
  
  <!-- STUB CREATE keep modal -->
<div class="modal fade" id="KeepModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="modal-header">
        <!-- <h1 class="modal-title fs-5" id="exampleModalLabel">Modal title</h1> -->
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <KeepForm/>
      </div>
      <!-- <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div> -->
    </div>
  </div>
</div>


<!-- STUB CREATE vault modal -->
<div class="modal fade" id="VaultModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <!-- <h1 class="modal-title fs-5" id="exampleModalLabel">Modal title</h1> -->
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <VaultForm/>
      </div>
      <!-- <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div> -->
    </div>
  </div>
</div>




</template>

<script>
import { AppState } from '../AppState';
import { computed, onMounted, onUnmounted } from 'vue';

import Login from './Login.vue';
export default {
  setup() {
    return {
      account: computed(() => AppState.account),
    }
  },
  components: { Login }
}
</script>

<style scoped>

.nav-color{
  background-color: rgb(255, 250, 239);
}

a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
