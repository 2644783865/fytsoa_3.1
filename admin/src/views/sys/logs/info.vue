<template>
  <el-dialog :title="dialog.title" :visible.sync="dialogVisible" width="850px">
    <el-form ref="form">
      <el-form-item label="请求地址：">
        <div v-html="model.address"></div>
      </el-form-item>
      <el-form-item label="请求参数：">
        <div v-html="model.parameters"></div>
      </el-form-item>
      <el-form-item label="返回结果：">
        <div
          style="height: 300px; overflow: auto; line-height: 26px"
          v-html="model.returnValue"
        ></div>
      </el-form-item>
    </el-form>
    <div slot="footer">
      <el-button @click="close">取消</el-button>
    </div>
  </el-dialog>
</template>
<script>
  import { getModel } from '@/api/sys/logs'
  export default {
    data() {
      return {
        dialog: {
          title: '日志详情',
        },
        model: {},
        dialogVisible: false,
        btnLoading: false,
      }
    },
    methods: {
      async init(id) {
        const t = await getModel(id)
        this.model = t.data
      },
      close() {
        this.dialogVisible = false
      },
      handelInfo(record) {
        this.dialogVisible = true
        this.init(record.id)
      },
    },
  }
</script>
