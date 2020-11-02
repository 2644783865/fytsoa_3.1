<template>
  <div class="common-layout">
    <div class="content">
      <slot></slot>
    </div>
  </div>
</template>

<script>
  import { mapState } from 'vuex'

  export default {
    name: 'CommonLayout',
    computed: {
      ...mapState('setting')
    }
  }
</script>

<style scoped lang="less">
  .common-layout {
    display: flex;
    flex-direction: column;
    height: 100vh;
    overflow: auto;
    background-color: @layout-body-background;

    .content {
      padding: 32px 0;
      flex: 1;

      @media (min-width: 768px) {

        padding: 112px 0 24px;
      }
    }
  }
</style>