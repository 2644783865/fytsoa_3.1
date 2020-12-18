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
          <el-form-item label="上级菜单" prop="parent">
            <el-cascader
              v-model="formData.parent"
              :options="parentIdOptions"
              :props="parentIdProps"
              :style="{ width: '100%' }"
              placeholder="请选择所属上级菜单"
              clearable
              filterable
            ></el-cascader>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="菜单类型" prop="types">
            <el-radio-group v-model="formData.types" size="medium">
              <el-radio
                v-for="(item, index) in typesOptions"
                :key="index"
                :label="item.value"
                :disabled="item.disabled"
                @change="typesChange"
              >
                {{ item.label }}
              </el-radio>
            </el-radio-group>
          </el-form-item>
        </el-col>
        <el-col :span="12">
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
        </el-col>
        <el-col v-show="formData.types == 1" :span="24">
          <el-form-item
            label="菜单图标"
            prop="icon"
            :required="formData.types == 1"
          >
            <e-icon-picker v-model="formData.icon" />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="菜单名称" prop="name">
            <el-input
              v-model="formData.name"
              placeholder="请输入菜单名称"
              :maxlength="30"
              show-word-limit
              clearable
              :style="{ width: '100%' }"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="权限标识" prop="code">
            <el-input
              v-model="formData.code"
              placeholder="请输入权限标识"
              :maxlength="30"
              show-word-limit
              clearable
              :style="{ width: '100%' }"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col v-show="formData.types == 2" :span="24">
          <el-form-item
            label="路由地址"
            prop="urls"
            :required="formData.types == 2"
          >
            <el-input
              v-model="formData.urls"
              placeholder="请输入路由地址"
              :maxlength="100"
              show-word-limit
              clearable
              suffix-icon="el-icon-link"
              :style="{ width: '100%' }"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="24">
          <el-form-item label="排序" prop="sort" required>
            <el-slider v-model="formData.sort" :max="100" :step="1"></el-slider>
          </el-form-item>
        </el-col>
        <el-col v-show="formData.types == 2" :span="24">
          <el-form-item label="按钮权限" :required="formData.types == 2">
            <el-checkbox-group v-model="formData.btnFun" size="medium">
              <el-checkbox
                v-for="(item, index) in btnFunOptions"
                :key="index"
                :label="item.id"
                :disabled="item.disabled"
              >
                {{ item.name }}
              </el-checkbox>
            </el-checkbox-group>
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
  import { getList, add, update } from '@/api/sys/menu'
  import { getCodeList } from '@/api/sys/code'
  import { changeTree } from '@/utils/treeTool'
  import { deepClone } from '@/utils/index'
  export default {
    data() {
      let validateBtnFun = (rule, value, callback) => {
        if (this.formData.btnFun.length == 0 && this.formData.types == 2) {
          callback(new Error('请选择按钮权限'))
        } else {
          callback()
        }
      }
      let validateUrls = (rule, value, callback) => {
        if (!this.formData.urls && this.formData.types == 2) {
          callback(new Error('请输入路由地址'))
        } else {
          callback()
        }
      }
      let validateIcon = (rule, value, callback) => {
        if (!this.formData.icon && this.formData.types == 1) {
          callback(new Error('请选择目录图标'))
        } else {
          callback()
        }
      }
      return {
        dialog: {
          title: '添加菜单',
        },
        formData: {
          id: '0',
          parent: [],
          types: 1,
          status: true,
          icon: undefined,
          name: undefined,
          code: undefined,
          urls: undefined,
          sort: 1,
          btnFun: [],
        },
        isVisbtnFun: true,
        rules: {
          parent: [
            {
              required: true,
              type: 'array',
              message: '请选择上级菜单',
              trigger: 'change',
            },
          ],
          types: [
            {
              required: true,
              message: '菜单类型不能为空',
              trigger: 'change',
            },
          ],
          status: [
            {
              required: true,
              message: '状态不能为空',
              trigger: 'change',
            },
          ],
          icon: [{ validator: validateIcon }],
          name: [
            {
              required: true,
              message: '请输入菜单名称',
              trigger: 'blur',
            },
          ],
          code: [
            {
              required: true,
              message: '请输入权限标识',
              trigger: 'blur',
            },
          ],
          urls: [{ validator: validateUrls }],
          btnFun: [{ validator: validateBtnFun }],
        },
        parentIdOptions: [],
        parentIdProps: {
          multiple: false,
          checkStrictly: true,
          expandTrigger: 'hover',
        },
        typesOptions: [
          {
            label: '目录',
            value: 1,
          },
          {
            label: '菜单',
            value: 2,
          },
        ],
        statusOptions: [
          {
            label: '显示',
            value: true,
          },
          {
            label: '隐藏',
            value: false,
          },
        ],
        iconOptions: [],
        btnFunOptions: [],
        dialogVisible: false,
        btnLoading: false,
        btnData: [],
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
        const _code = await getCodeList({ id: '1258647245457330176' })
        this.btnFunOptions = _code.data.items
      },
      typesChange(m) {},
      onClose() {
        this.$refs['elForm'].resetFields()
      },
      close() {
        this.dialogVisible = false
        this.$emit('update:visible', false)
      },
      handleAdd() {
        this.formData.id = '0'
        this.dialog.title = '添加菜单'
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
        this.dialog.title = '编辑菜单'
        this.dialogVisible = true
        this.formData = deepClone(record)
      },
      handelConfirm() {
        this.$refs['elForm'].validate(async (valid) => {
          if (!valid) return
          const _parentList = this.formData.parent.join(',')
          this.formData.parentId = this.formData.parent.pop()
          this.formData.parentIdList = _parentList
          console.log(this.formData)
          //return
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
