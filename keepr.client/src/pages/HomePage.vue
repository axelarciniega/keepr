<template>
  <div class="">

    
    
    <div class="masonry-container mt-5">
      <div v-for="keep in keeps" :key="keep.id">
        <KeepCard :keep="keep"/>
      </div>
    </div>
    
    
    
    
    
    
  </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService';
import { AppState } from '../AppState';

export default {
  setup() {
    onMounted(()=> {
      getKeeps();
    })

    async function getKeeps(){
      try {
        await keepsService.getKeeps();
      } catch (error) {
        Pop.error(error)
      }
    }

    return {
      keeps: computed(()=> AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">

.masonry-container{
    columns: 4 150px;
    column-gap: 40px;
// padding: 5em;
}



// .home {
//   display: grid;
//   height: 80vh;
//   place-content: center;
//   text-align: center;
//   user-select: none;

//   .home-card {
//     width: 50vw;

//     >img {
//       height: 200px;
//       max-width: 200px;
//       width: 100%;
//       object-fit: contain;
//       object-position: center;
//     }
//   }
// }


@media(max-width: 768px){
  .masonry-container{
     $gap: 1.25em;
    columns: 2 100px;
    column-gap: $gap;
  }
}

</style>
