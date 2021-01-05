<template>
  <el-dialog :title="dialog.title" :visible.sync="dialogVisible" width="700px">
    <tree-transfer
      :open-all="transfer.openAll"
      :title="transfer.title"
      :from_data="fromData"
      :to_data="toData"
      :default-props="{ label: 'label' }"
      :mode="mode"
      height="300px"
      @add-btn="add"
      @remove-btn="remove"
    ></tree-transfer>
    <span slot="footer" class="dialog-footer">
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" :loading="btnLoading" @click="handelConfirm">
        确 定
      </el-button>
    </span>
  </el-dialog>
</template>
<script>
  import treeTransfer from 'el-tree-transfer'
  import { roleAll } from '@/api/sys/admin'
  import { changeTree } from '@/utils/treeTool'
  export default {
    components: { treeTransfer },
    data() {
      return {
        dialog: {
          title: '为用户授权角色',
        },
        transfer: {
          title: ['角色', '已选角色'],
          openAll: true,
        },
        mode: 'transfer',
        fromData: [
          {
            id: '1',
            pid: 0,
            label: '一级 1',
            children: [
              {
                id: '1-1',
                pid: '1',
                label: '二级 1-1',
                disabled: true,
                children: [],
              },
              {
                id: '1-2',
                pid: '1',
                label: '二级 1-2',
                children: [
                  {
                    id: '1-2-1',
                    pid: '1-2',
                    children: [],
                    label: '二级 1-2-1',
                  },
                  {
                    id: '1-2-2',
                    pid: '1-2',
                    children: [],
                    label: '二级 1-2-2',
                  },
                ],
              },
            ],
          },
        ],
        toData: [],
        dialogVisible: false,
        btnLoading: false,
      }
    },
    methods: {
      async initTree() {
        const role = await roleAll()
        let roleArr = []
        role.data.forEach(function (m, i) {
          roleArr.push({
            id: m.id,
            pid: m.parentId,
            label: m.name,
            parentId: m.parentId,
          })
        })

        this.fromData = changeTree(roleArr)
        console.log(this.fromData)
      },
      close() {
        this.dialogVisible = false
      },
      handleOpen(arr) {
        console.log(arr)
        this.initTree()
        this.dialogVisible = true
      },
      add(fromData, toData, obj) {
        // 树形穿梭框模式transfer时，返回参数为左侧树移动后数据、右侧树移动后数据、移动的{keys,nodes,halfKeys,halfNodes}对象
        // 通讯录模式addressList时，返回参数为右侧收件人列表、右侧抄送人列表、右侧密送人列表
        console.log('fromData:', fromData)
        console.log('toData:', toData)
        console.log('obj:', obj)
      },
      // 监听穿梭框组件移除
      remove(fromData, toData, obj) {
        // 树形穿梭框模式transfer时，返回参数为左侧树移动后数据、右侧树移动后数据、移动的{keys,nodes,halfKeys,halfNodes}对象
        // 通讯录模式addressList时，返回参数为右侧收件人列表、右侧抄送人列表、右侧密送人列表
        console.log('fromData:', fromData)
        console.log('toData:', toData)
        console.log('obj:', obj)
      },
      handelConfirm() {},
    },
  }
</script>
<style>
  .wl-transfer .transfer-title {
    margin-top: 0px;
    margin-bottom: 0px;
  }
</style>
