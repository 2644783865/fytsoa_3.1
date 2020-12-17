<template>
  <el-dialog
    :title="dialog.title"
    :visible.sync="dialogVisible"
    width="850px"
    @close="onClose"
  >
    <el-row :gutter="15">
      <el-form
        ref="elForm"
        :model="formData"
        :rules="rules"
        label-width="100px"
      >
        <el-col :span="24">
          <el-form-item label="所属机构" prop="parent">
            <el-cascader
              v-model="formData.parent"
              :options="parentIdOptions"
              :props="parentIdProps"
              :style="{ width: '100%' }"
              placeholder="请选择所属机构"
              clearable
              filterable
            ></el-cascader>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="机构名称" prop="name">
            <el-input
              v-model="formData.name"
              placeholder="请输入机构名称"
              clearable
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="机构编号">
            <el-input
              v-model="formData.number"
              placeholder="请输入机构编号"
              clearable
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="机构负责人" prop="leaderUser">
            <el-input
              v-model="formData.leaderUser"
              placeholder="请输入机构负责人"
              clearable
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="联系电话" prop="leaderMobile">
            <el-input
              v-model="formData.leaderMobile"
              placeholder="请输入联系电话"
              clearable
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="联系邮箱" prop="leaderEmail">
            <el-input
              v-model="formData.leaderEmail"
              placeholder="请输入联系邮箱"
              clearable
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="状态" prop="sort" required>
            <el-switch v-model="formData.status"></el-switch>
          </el-form-item>
        </el-col>
        <el-col :span="24">
          <el-form-item label="排序" prop="sort" required>
            <el-slider
              v-model="formData.sort"
              :max="500"
              :step="1"
              show-input
            ></el-slider>
          </el-form-item>
        </el-col>
      </el-form>
    </el-row>
    <span slot="footer" class="dialog-footer">
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" :loading="btnLoading" @click="handelConfirm">
        确 定
      </el-button>
    </span>
  </el-dialog>
</template>
<script>
  import { getList, add, update } from '@/api/sys/organize'
  import { changeTree } from '@/utils/treeTool'
  import { deepClone } from '@/utils/index'
  export default {
    data() {
      return {
        dialog: {
          title: '添加机构',
        },
        formData: {
          id: '0',
          parent: [],
          name: '',
          leaderUser: '',
          leaderMobile: '',
          leaderEmail: '',
          status: true,
          sort: 1,
        },
        rules: {
          parent: [
            {
              required: true,
              type: 'array',
              message: '请至少选择一个所属机构',
              trigger: 'change',
            },
          ],
          name: [
            {
              required: true,
              message: '请输入机构名称',
              trigger: 'blur',
            },
          ],
          leaderUser: [
            {
              required: true,
              message: '请输入机构负责人',
              trigger: 'blur',
            },
          ],
          leaderMobile: [
            {
              required: true,
              message: '请输入联系电话',
              trigger: 'blur',
            },
          ],
          leaderEmail: [],
        },
        parentIdOptions: [],
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
        const t = await getList()
        let _tree = [{ id: '1', value: '0', label: '根目录', parentId: '0' }]
        t.data.some((m) => {
          _tree.push({
            id: m.id,
            value: m.id,
            label: m.name,
            parentId: m.parentId,
          })
        })
        this.parentIdOptions = changeTree(_tree)
      },
      onClose() {
        this.formData = {
          id: '0',
          parent: [],
          name: '',
          leaderUser: '',
          leaderMobile: '',
          leaderEmail: '',
          status: true,
          sort: 1,
        }
        this.$refs['elForm'].resetFields()
      },
      close() {
        this.dialogVisible = false
        this.$emit('update:visible', false)
      },
      handleAdd() {
        this.formData.id = '0'
        this.dialog.title = '添加机构'
        this.dialogVisible = true
        this.initTree()
      },
      handelModify(record) {
        this.initTree()
        record.parent = []
        var str = record.parentIdList.split(',')
        str.forEach(function (item, i) {
          if (item != record.id && item) {
            record.parent.push(item)
          }
          if (record.parentId == '0' && item) {
            record.parent.push('0')
          }
        })
        this.dialog.title = '编辑机构'
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
