<template>
  <el-dialog
    :title="dialog.title"
    :visible.sync="dialogVisible"
    width="700px"
    @close="onClose"
  >
    <el-form ref="elForm" :model="formData" :rules="rules" label-width="100px">
      <el-form-item label="上级栏目" prop="parent">
        <el-cascader
          v-model="formData.parent"
          :options="parentOptions"
          :props="parentIdProps"
          :style="{ width: '100%' }"
          placeholder="请选择所属机构"
          clearable
          filterable
        ></el-cascader>
      </el-form-item>
      <el-form-item label="栏目名称" prop="name">
        <el-input
          v-model="formData.name"
          placeholder="请输入栏目名称"
          :maxlength="30"
          show-word-limit
          clearable
          :style="{ width: '100%' }"
        ></el-input>
      </el-form-item>
      <el-form-item label="状态" prop="isSystem" required>
        <el-switch
          v-model="formData.isSystem"
          active-text="是否为系统内置集成，如果为是，则不允许删除"
          active-color="#EB5B02"
        ></el-switch>
      </el-form-item>
      <el-form-item label="栏目排序" prop="sort" required>
        <el-slider
          v-model="formData.sort"
          show-input
          :max="100"
          :step="1"
        ></el-slider>
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
  import { columnList, addColumn, updateColumn } from '@/api/sys/code'
  import { changeTree } from '@/utils/treeTool'
  import { deepClone } from '@/utils/index'
  export default {
    data() {
      return {
        dialog: {
          title: '添加字典栏目',
        },
        formData: {
          id: undefined,
          parent: [],
          parentId: undefined,
          name: undefined,
          isSystem: false,
          sort: 1,
        },
        rules: {
          parent: [
            {
              required: true,
              type: 'array',
              message: '请选择上级栏目',
              trigger: 'change',
            },
          ],
          name: [
            {
              required: true,
              message: '请输入栏位名称',
              trigger: 'blur',
            },
          ],
        },

        parentOptions: [],
        parentIdProps: {
          multiple: false,
          checkStrictly: true,
          expandTrigger: 'hover',
        },
        dialogVisible: false,
        btnLoading: false,
      }
    },
    methods: {
      async initTree() {
        const t = await columnList()
        let _tree = [{ id: '1', value: '0', label: '一级栏目', parentId: '0' }]
        t.data.some((m) => {
          _tree.push({
            id: m.id,
            value: m.id,
            label: m.name,
            parentId: m.parentId,
          })
        })
        this.parentOptions = changeTree(_tree)
      },
      onClose() {
        this.$refs['elForm'].resetFields()
      },
      close() {
        this.dialogVisible = false
        this.$emit('update:visible', false)
      },
      handleAdd() {
        this.formData.id = '0'
        this.dialog.title = '添加字典栏目'
        this.dialogVisible = true
        this.initTree()
      },
      handelModify(record) {
        this.initTree()
        record.parent = []
        console.log(record)
        var str = record.parentIdList.split(',')
        str.forEach(function (item, i) {
          if (item != record.id && item) {
            record.parent.push(item)
          }
          if (record.parentId == '0' && item) {
            record.parent.push('0')
          }
        })
        this.dialog.title = '编辑字典栏目'
        this.dialogVisible = true
        this.$nextTick(() => {
          this.formData = deepClone(record)
        })
      },
      handelConfirm() {
        this.$refs['elForm'].validate(async (valid) => {
          if (!valid) return
          const _parentList = this.formData.parent.join(',')
          this.formData.parentId = this.formData.parent.pop()
          this.formData.parentIdList = _parentList
          let tipName = '添加成功'
          let res = null
          if (this.formData.id == '0') {
            this.formData.id = ''
            res = await addColumn(this.formData)
          } else {
            tipName = '修改成功'
            res = await updateColumn(this.formData)
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
