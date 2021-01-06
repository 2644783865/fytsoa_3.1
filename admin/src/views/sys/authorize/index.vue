<template>
  <div>
    <el-row>
      <el-col class="code-left">
        <div class="title">
          <i class="el-icon-c-scale-to-original"></i>
          角色组
        </div>
        <div class="cur-tree">
          <el-tree
            :data="treeData"
            node-key="id"
            default-expand-all
            :expand-on-click-node="false"
            @node-click="columnnode"
          ></el-tree>
        </div>
      </el-col>
      <el-col class="code-right"></el-col>
    </el-row>
  </div>
</template>
<script>
  import { getList, getMenuList } from '@/api/sys/role'
  import { changeTree } from '@/utils/treeTool'
  export default {
    data() {
      return {
        param: {
          key: '',
        },
        treeData: [],
      }
    },
    created() {},
    mounted() {
      this.init()
    },
    methods: {
      async init() {
        const role = await getList(this.param)
        let roleArr = []
        role.data.forEach(function (m, i) {
          roleArr.push({
            id: m.id,
            pid: m.parentId,
            label: m.name,
            parentId: m.parentId,
          })
        })
        this.treeData = changeTree(roleArr)
      },
      columnnode(data, node, e) {
        console.log(data)
      },
      onSubmit() {
        console.log('submit!')
      },
    },
  }
</script>
<style lang="scss" scoped>
  .code-left {
    width: 260px !important;
    position: absolute;
    top: 0;
    bottom: 0;
    float: inherit;
    background-color: #edf5ff;
    .title {
      height: 50px;
      line-height: 50px;
      padding-left: 20px;
      font-weight: bold;
      background-color: #dee5ef;
    }
  }
  .code-left::after {
    content: '';
    position: absolute;
    height: 1px;
    width: 100%;
    left: -19px;
    top: 0px;
    background-color: #f6f8f9;
  }
  .code-right {
    left: 260px;
    position: relative;
    width: calc(100% - 260px);
    top: 0;
    bottom: 0;
    right: 0;
    float: inherit;
  }
</style>
<style>
  .cur-tree .custom-tree-node {
    flex: 1;
    display: flex;
    align-items: center;
    justify-content: space-between;
    font-size: 14px;
    padding-right: 8px;
  }
  .cur-tree .el-tree {
    background: transparent;
  }
  .el-tree-node__content {
    height: 35px;
  }
  .cur-tree .tool {
    display: inline-block;
    padding-right: 10px;
    position: relative;
    z-index: 10;
  }
  .cur-tree .tool button {
    margin-left: 10px;
    font-size: 16px;
  }
  .el-icon-c-scale-to-original {
    font-size: 18px;
    position: relative;
    top: 2px;
  }
</style>
