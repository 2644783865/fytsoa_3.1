<template>
  <el-dialog
    :title="dialog.title"
    :visible.sync="dialogVisible"
    width="850px"
    @close="onClose"
  >
    <el-form
      ref="elForm"
      :model="formData"
      :rules="rules"
      size="medium"
      label-width="100px"
    >
      <el-form-item label="岗位编号" prop="number">
        <el-input
          v-model="formData.number"
          placeholder="请输入岗位编号,例如：10001"
          :maxlength="6"
          show-word-limit
          clearable
          :style="{ width: '100%' }"
        ></el-input>
      </el-form-item>
      <el-form-item label="岗位名称" prop="name">
        <el-input
          v-model="formData.name"
          placeholder="请输入岗位名称"
          :maxlength="25"
          show-word-limit
          clearable
          :style="{ width: '100%' }"
        ></el-input>
      </el-form-item>
      <el-form-item label="排序" prop="sort" required>
        <el-slider v-model="formData.sort" :max="100" :step="1"></el-slider>
      </el-form-item>
      <el-form-item label="状态" prop="status">
        <el-radio-group v-model="formData.status" size="medium">
          <el-radio
            v-for="(item, index) in statusOptions"
            :key="index"
            :label="item.value"
            :disabled="item.disabled"
          >
            {{ item.label }}
          </el-radio>
        </el-radio-group>
      </el-form-item>
      <el-form-item label="备注" prop="summary">
        <el-input
          v-model="formData.summary"
          type="textarea"
          placeholder="请输入备注"
          :maxlength="500"
          show-word-limit
          :autosize="{ minRows: 2, maxRows: 3 }"
          :style="{ width: '100%' }"
        ></el-input>
      </el-form-item>
    </el-form>
    <div slot="footer">
      <el-button @click="close">取消</el-button>
      <el-button type="primary" @click="handelConfirm">确定</el-button>
    </div>
  </el-dialog>
</template>
<script>
  import { getList, add, update } from '@/api/sys/post'
  import { changeTree } from '@/utils/treeTool'
  import { deepClone } from '@/utils/index'
  export default {
    data() {
      return {
        dialog: {
          title: '添加岗位',
        },
        formData: {
          id: '0',
          number: undefined,
          name: undefined,
          sort: 1,
          status: true,
          summary: undefined,
        },
        rules: {
          number: [
            {
              required: true,
              message: '请输入岗位编号',
              trigger: 'blur',
            },
          ],
          name: [
            {
              required: true,
              message: '请输入岗位名称',
              trigger: 'blur',
            },
          ],
          status: [
            {
              required: true,
              message: '状态不能为空',
              trigger: 'change',
            },
          ],
          summary: [
            {
              required: false,
              message: '请输入备注',
              trigger: 'blur',
            },
          ],
        },
        statusOptions: [
          {
            label: '正常',
            value: true,
          },
          {
            label: '停用',
            value: false,
          },
        ],
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
      handleAdd() {
        this.formData.id = '0'
        this.dialog.title = '添加岗位'
        this.dialogVisible = true
      },
      handelModify(record) {
        this.dialog.title = '编辑岗位'
        this.dialogVisible = true
        this.$nextTick(() => {
          this.formData = deepClone(record)
        })
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
