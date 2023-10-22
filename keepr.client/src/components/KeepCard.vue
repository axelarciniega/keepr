<template>
    <div class="container-fluid">

        <!-- <section class="row"> -->
            <div class="col-12 col-md-5">
                <div class="">
                    <img class="img-s" :src="keep.img">
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



    .img-s{
        border-radius: 15px;
        object-fit: cover;
        object-position: center;
        width: 40vh;
        height: 100%;
        $gap: 1.25em;
        column-gap: $gap;
        margin: $gap;
    }

    @media(max-width: 768px){
        .img-s{
        border-radius: 15px;
        object-fit: cover;
        object-position: center;
        width: 20vh;
        height: 100%;
        $gap: 1.25em;
        column-gap: $gap;
        margin: $gap;
        }
    }


</style>