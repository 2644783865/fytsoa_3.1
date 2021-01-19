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
      <el-col class="code-right" :style="{ height: fullHeight + 'px' }">
        <div class="title">
          <i class="el-icon-document-copy"></i>
          菜单列表
        </div>
        <div class="menu-wall">
          <el-tree
            ref="tree"
            :data="menuTree"
            show-checkbox
            node-key="id"
            default-expand-all
            :render-content="renderContent"
          ></el-tree>
        </div>
        <div class="footer-btn">
          <el-button type="primary" round @click="saveAuthorize">
            保存授权
          </el-button>
        </div>
      </el-col>
    </el-row>
  </div>
</template>
<script>
  import { getList, getMenuList, addAuthorize } from '@/api/sys/role'
  import { changeTree } from '@/utils/treeTool'
  export default {
    data() {
      return {
        param: {
          key: '',
        },
        treeData: [],
        selectRole: {},
        menuTree: [],
        fullHeight: window.innerHeight - 125,
        menuArray: [],
      }
    },
    created() {},
    mounted() {
      this.init()
    },
    methods: {
      renderContent(h, { node, data, store }) {
        return (
          <span class="custom-tree-node">
            <span>{node.label}</span>
            <span class="btn-mi">
              {data.btns.map((item) => {
                return (
                  <el-checkbox
                    on-change={() => this.btnHander(data)}
                    checked={item.checked}
                  >
                    {item.name}
                  </el-checkbox>
                )
              })}
            </span>
          </span>
        )
      },
      btnHander(m) {
        //console.log(m)
      },
      handleCheckChange(data, checked, indeterminate) {
        //console.log(data, checked, indeterminate)  @check-change="handleCheckChange"
        let _this = this
        if (checked) {
          data.btns.forEach((item) => {
            item.checked = true
          })
          console.log(data)
          _this.$refs.tree.setCheckedNodes([data])
          // this.menuArray.forEach((item) => {
          //   if (item.id == data.id) {
          //     item.btns.forEach((row) => {
          //       row.checked = true
          //     })
          //   }
          // })
        } else {
          data.btns.forEach((item) => {
            item.checked = false
          })
          // this.menuArray.forEach((item) => {
          //   if (item.id == data.id) {
          //     item.btns.forEach((row) => {
          //       row.checked = true
          //     })
          //   }
          // })
        }
        //console.log(this.menuArray)
        //this.menuTree = changeTree(this.menuArray)
      },
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
        const menu = await getMenuList()
        let menuArr = []
        menu.data.forEach(function (m, i) {
          menuArr.push({
            id: m.id,
            pid: m.parentId,
            label: m.name,
            parentId: m.parentId,
            btns: m.btnFun,
          })
        })
        this.menuArray = menuArr
        this.menuTree = changeTree(menuArr)
      },
      async saveAuthorize() {
        var nodes = this.$refs.tree.getCheckedNodes()
        if (!this.selectRole.id) {
          this.$notify({
            message: '请选择要授权的角色信息~',
            type: 'warning',
          })
          return
        }
        if (nodes.length == 0) {
          this.$notify({
            message: '请勾选授权的菜单信息~',
            type: 'warning',
          })
          return
        }
        let _menus = []
        nodes.forEach((item) => {
          _menus.push({ menuId: item.id, btnFun: item.btns })
        })
        var data = { roleId: this.selectRole.id, menus: _menus }
        console.log(data)
        const res = await addAuthorize(data)
        this.loading = false
        if (res.code == 200) {
          this.$notify({
            message: '授权成功',
            type: 'success',
          })
        } else {
          this.$notify({
            message: res.message,
            type: 'error',
          })
        }
      },
      columnnode(data, node, e) {
        if (!data.children) {
          this.selectRole = data
        }
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
    padding: 0px 0px 10px 10px;
    .title {
      height: 50px;
      line-height: 50px;
      padding-left: 20px;
      font-weight: bold;
      background-color: #dee5ef;
    }
  }
  .menu-wall {
    padding: 10px;
  }
  .footer-btn {
    height: 45px;
    position: absolute;
    width: 100%;
    text-align: center;
    z-index: 10;
    bottom: 0px;
    border-top: 1px solid #e6e7e8;
    padding-top: 10px;
    background: #ffffff;
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
  .custom-tree-node .btn-mi {
    padding-left: 50px;
  }
  .custom-tree-node > span:first-child {
    display: inline-block;
    width: 100px;
  }
</style>
