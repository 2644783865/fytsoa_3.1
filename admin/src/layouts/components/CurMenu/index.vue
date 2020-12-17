<template>
  <el-scrollbar class="gallery-bar-container">
    <div :class="'logo-container-' + layout">
      <router-link to="/">
        <!-- 这里是logo变更的位置 -->
        <span v-if="logo && layout == 'gallery'" class="logo">
          <vab-remix-icon :icon-class="logo" />
        </span>
        <span
          class="title"
          :class="{ 'hidden-xs-only': layout === 'horizontal' }"
          :title="title"
        >
          {{ title }}
        </span>
      </router-link>
    </div>
    <el-tabs tab-position="left">
      <el-tab-pane label="用户管理">
        <div slot="label" class="gallery-grid gallery-grid-card">
          <i class="el-icon-date"></i>
          用户
        </div>
      </el-tab-pane>
      <el-tab-pane label="配置管理">
        <div slot="label" class="gallery-grid gallery-grid-card">
          <i class="el-icon-date"></i>
          配置
        </div>
      </el-tab-pane>
      <el-tab-pane label="角色管理">
        <div slot="label" class="gallery-grid gallery-grid-card">
          <i class="el-icon-date"></i>
          角色
        </div>
      </el-tab-pane>
      <el-tab-pane label="定时任务">
        <div slot="label" class="gallery-grid gallery-grid-card">
          <i class="el-icon-date"></i>
          定时
        </div>
      </el-tab-pane>
    </el-tabs>
    <el-menu
      default-active="1-4-1"
      class="el-menu-vertical-demo"
      :collapse="isCollapse"
    >
      <el-submenu index="1">
        <template slot="title">
          <i class="el-icon-location"></i>
          <span slot="title">导航一</span>
        </template>
        <el-menu-item-group>
          <span slot="title">分组一</span>
          <el-menu-item index="1-1">选项1</el-menu-item>
          <el-menu-item index="1-2">选项2</el-menu-item>
        </el-menu-item-group>
        <el-menu-item-group title="分组2">
          <el-menu-item index="1-3">选项3</el-menu-item>
        </el-menu-item-group>
        <el-submenu index="1-4">
          <span slot="title">选项4</span>
          <el-menu-item index="1-4-1">选项1</el-menu-item>
        </el-submenu>
      </el-submenu>
      <el-menu-item index="2">
        <i class="el-icon-menu"></i>
        <span slot="title">导航二</span>
      </el-menu-item>
      <el-menu-item index="3" disabled>
        <i class="el-icon-document"></i>
        <span slot="title">导航三</span>
      </el-menu-item>
      <el-menu-item index="4">
        <i class="el-icon-setting"></i>
        <span slot="title">导航四</span>
      </el-menu-item>
    </el-menu>
  </el-scrollbar>
</template>

<script>
  import { mapGetters } from 'vuex'
  import { logo } from '@/config'

  export default {
    name: 'CurMenu',
    data() {
      return {
        title: this.$baseTitle,
        isCollapse: false,
      }
    },
    computed: {
      ...mapGetters({
        logo: 'settings/logo',
        layout: 'settings/layout',
      }),
    },
  }
</script>
<style lang="scss">
  .gallery-bar-container {
    position: fixed !important;
    top: 0;
    bottom: 0;
    left: 0;
    width: 256px;
    height: 100vh;
    overflow: hidden;
    background: #fff;
    box-shadow: 0 1px 4px rgba(0, 21, 41, 0.08);

    .el-tabs {
      position: fixed;
      color: #515a6e !important;
      background: #f7faff !important;
      width: 65px;
      height: 100%;
      .el-tabs__item {
        padding: 5px !important;
        height: auto;
      }
      .el-tabs__item.is-left {
        text-align: center;
      }
      .el-tabs__item.is-active {
        .gallery-grid {
          background: #1890ff;
          color: #ffffff;
        }
      }
    }
    .el-tabs__nav-wrap::after,
    .el-tabs__active-bar {
      display: none;
    }
    .el-tabs + .el-menu {
      left: 75px;
      width: 168px;
    }
    .gallery-grid {
      width: 55px !important;
      height: 50px !important;
      border-radius: 5px;
      padding-top: 10px;
      line-height: 25px;
    }

    .el-menu {
      border-right: none;
      padding: 0 10px 0 0;
    }
    .el-menu-item {
      border-radius: 5px;
      height: 50px;
      line-height: 50px;
    }
    .el-submenu .el-menu-item {
      min-width: 165px;
    }
    .el-submenu__title i {
      color: #909399 !important;
    }
    .el-menu-item:hover {
      border-radius: 5px;
      color: #1890ff !important;
      background-color: rgba(24, 144, 255, 0.1) !important;
    }
    .el-menu-item.is-active {
      color: #1890ff;
      background-color: rgba(24, 144, 255, 0.1) !important;
    }
    .el-tabs__item i {
      display: block;
    }
  }
  html {
    body {
      submenu__title i {
        color: #909399;
      }
    }
  }
  .el-submenu__title {
    border-radius: 5px !important;
    height: 50px !important;
    line-height: 50px !important;
  }
</style>
