<template>
    <div class="container-fluid">

        <!-- <section class="row"> -->
            <div class="col-12 col-md-5">
                <div class="masonry-container">
                    <img :src="keep.img">
                </div>
            </div>
        <!-- </section> -->
    </div>
    </template>

<script>
import { computed } from 'vue';
import { Keep } from '../models/Keep';
import { AppState } from '../AppState';

export default {
    props: {keep: {type: Keep, required: true}},
setup() {
  return {
  };
},
};
</script>


<style lang="scss" scoped>


.masonry-container{
    $gap: 1.25em;
    display: grid;
    columns: 4 400px;
    column-gap: 1rem;
    img{
        border: 10px;
        width: 100%;
        margin-bottom: $gap;
    }
}


</style>