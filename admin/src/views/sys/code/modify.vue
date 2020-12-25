<template>
  <el-dialog
    :title="dialog.title"
    :visible.sync="dialogVisible"
    width="700px"
    @close="onClose"
  >
    <el-form ref="elForm" :model="formData" :rules="rules" label-width="100px">
      <el-form-item label="字典名称" prop="name">
        <el-input
          v-model="formData.name"
          placeholder="请输入字典名称"
          :maxlength="30"
          show-word-limit
          clearable
          :style="{ width: '100%' }"
        ></el-input>
      </el-form-item>
      <el-form-item label="字典阔值" prop="codeValues">
        <el-input
          v-model="formData.codeValues"
          placeholder="请输入字典阔值，如A、B"
          :maxlength="30"
          show-word-limit
          clearable
          :style="{ width: '100%' }"
        ></el-input>
      </el-form-item>
      <el-form-item label="状态" prop="status" required>
        <el-switch v-model="formData.status" active-text="是否启用"></el-switch>
      </el-form-item>
      <el-form-item label="排序" prop="sort" required>
        <el-slider v-model="formData.sort" :max="100" :step="1"></el-slider>
      </el-form-item>
      <el-form-item label="备注">
        <el-input
          v-model="formData.summary"
          type="textarea"
          placeholder="请输入备注"
          :maxlength="100"
          show-word-limit
          :autosize="{ minRows: 2, maxRows: 4 }"
          :style="{ width: '100%' }"
        ></el-input>
      </el-form-item>
    </el-form>
    <span slot="footer" class="dialog-footer">
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" :loading="btnLoading" @click="handelConfirm">
        确 定
      </el-button>
    </span>
  </el-dialog>
</template>
<script>
  import { getCodeList, add, update } from '@/api/sys/code'
  import { changeTree } from '@/utils/treeTool'
  import { deepClone } from '@/utils/index'
  export default {
    data() {
      return {
        dialog: {
          title: '添加字典',
        },
        formData: {
          id: undefined,
          name: undefined,
          codeValues: undefined,
          status: true,
          sort: 1,
          summary: undefined,
        },
        rules: {
          name: [
            {
              required: true,
              message: '请输入字典名称',
              trigger: 'blur',
            },
          ],
          codeValues: [
            {
              required: true,
              message: '请输入字典阔值，如A、B',
              trigger: 'blur',
            },
          ],
        },
        dialogVisible: false,
        btnLoading: false,
      }
    },
    methods: {
      onClose() {
        this.$refs['elForm'].resetFields()
      },
      close() {
        this.dialogVisible = false
        this.$emit('update:visible', false)
      },
      handleAdd(m) {
        this.formData.id = '0'
        this.formData.typeId = m.id
        this.dialog.title = '添加字典'
        this.dialogVisible = true
      },
      handelModify(record) {
        this.dialog.title = '编辑字典'
        this.dialogVisible = true
        this.formData = deepClone(record)
      },
      handelConfirm() {
        this.$refs['elForm'].validate(async (valid) => {
          if (!valid) return
          let tipName = '添加成功'
          let res = null
          if (this.formData.id == '0') {
            this.formData.id = ''
            res = await add(this.formData)
          } else {
            tipName = '修改成功'
            res = await update(this.formData)
          }
          if (res.code == 200) {
            this.close()
            this.$emit('complete')
            this.$notify({
              message: tipName,
              type: 'success',
            })
          } else {
            this.$notify({
              message: message,
              type: 'error',
            })
          }
        })
      },
    },
  }
</script>
