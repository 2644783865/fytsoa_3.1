<template>
  <el-dialog
    :title="dialog.title"
    :visible.sync="dialog.visible"
    width="1100px"
    @close="onClose"
  >
    <el-row>
      <el-col class="cur-left">
        <div class="select-img">
          <div class="bg-gray">
            <i class="el-icon-circle-plus"></i>
            <p>选择图片</p>
          </div>
        </div>
        <div class="user-else-info">
          <p class="last-login">上次登录：{{ formData.upLoginTime }}</p>
          <el-row>
            <el-col :span="8">
              <span>{{ formData.loginCount }}</span>
              <p>次数</p>
            </el-col>
            <el-col :span="8">
              <span><i class="el-icon-warning-outline"></i></span>
              <p>状态</p>
            </el-col>
            <el-col :span="8">
              <span>13</span>
              <p>消息</p>
            </el-col>
          </el-row>
        </div>
      </el-col>
      <el-col class="cur-right">
        <el-row :gutter="15">
          <el-form
            ref="elForm"
            :model="formData"
            :rules="rules"
            size="medium"
            label-width="100px"
          >
            <el-col :span="24">
              <el-form-item label="所属部门" prop="organize">
                <el-cascader
                  v-model="formData.organize"
                  :options="organizeOptions"
                  :props="organizeProps"
                  :style="{ width: '100%' }"
                  placeholder="请选择所属部门"
                  clearable
                ></el-cascader>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="登录账号" prop="loginAccount">
                <el-input
                  v-model="formData.loginAccount"
                  placeholder="请输入登录账号"
                  :maxlength="30"
                  show-word-limit
                  clearable
                  :style="{ width: '100%' }"
                ></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="所属角色" prop="role">
                <el-cascader
                  ref="roleRef"
                  v-model="formData.role"
                  :options="roleOptions"
                  :props="roleProps"
                  placeholder="请择选所属角色"
                  clearable
                  collapse-tags
                  :show-all-levels="false"
                  style="width: 100%"
                ></el-cascader>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="登录密码" prop="loginPassWord">
                <el-input
                  v-model="formData.loginPassWord"
                  placeholder="请输入登录密码"
                  :maxlength="30"
                  show-word-limit
                  clearable
                  show-password
                  :style="{ width: '100%' }"
                ></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="所属岗位" prop="post">
                <el-select
                  v-model="formData.post"
                  multiple
                  placeholder="请选择所属岗位"
                  clearable
                  collapse-tags
                  :style="{ width: '100%' }"
                  @change="$forceUpdate()"
                >
                  <el-option
                    v-for="(item, index) in postOptions"
                    :key="index"
                    :label="item.name"
                    :value="item.id"
                    :disabled="item.disabled"
                  ></el-option>
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="姓名" prop="fullName">
                <el-input
                  v-model="formData.fullName"
                  placeholder="请输入姓名"
                  :maxlength="20"
                  clearable
                  :style="{ width: '100%' }"
                ></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="手机号码" prop="mobile">
                <el-input
                  v-model="formData.mobile"
                  placeholder="请输入手机号码"
                  :maxlength="11"
                  clearable
                  :style="{ width: '100%' }"
                ></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="性别" prop="sex">
                <el-radio-group v-model="formData.sex" size="medium">
                  <el-radio
                    v-for="(item, index) in sexOptions"
                    :key="index"
                    :label="item.value"
                    :disabled="item.disabled"
                  >
                    {{ item.label }}
                  </el-radio>
                </el-radio-group>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="邮箱" prop="email">
                <el-input
                  v-model="formData.email"
                  placeholder="请输入邮箱"
                  :maxlength="50"
                  show-word-limit
                  clearable
                  :style="{ width: '100%' }"
                ></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <el-form-item label="状态" prop="status" required>
                <el-switch
                  v-model="formData.status"
                  active-text="关闭后，则冻结用户，无法进行登录"
                ></el-switch>
              </el-form-item>
            </el-col>
            <el-col :span="24">
              <el-form-item label="描述" prop="summary">
                <el-input
                  v-model="formData.summary"
                  type="textarea"
                  placeholder="请输入描述"
                  :maxlength="200"
                  show-word-limit
                  :autosize="{ minRows: 2, maxRows: 3 }"
                  :style="{ width: '100%' }"
                ></el-input>
              </el-form-item>
            </el-col>
          </el-form>
        </el-row>
        <div slot="footer">
          <el-button @click="close">取消</el-button>
          <el-button type="primary" @click="handelConfirm">确定</el-button>
        </div>
      </el-col>
    </el-row>
    <div slot="footer">
      <el-button @click="close">取消</el-button>
      <el-button type="primary" @click="handelConfirm">确定</el-button>
    </div>
  </el-dialog>
</template>
<script>
  import { add, update, organizeAll, roleAll, postAll } from '@/api/sys/admin'
  import { changeTree } from '@/utils/treeTool'
  import { deepClone } from '@/utils/index'
  export default {
    data() {
      return {
        dialog: {
          title: '添加用户',
          visible: false,
        },
        formData: {
          id: '',
          organize: [],
          loginAccount: '',
          role: [],
          loginPassWord: '',
          post: [],
          fullName: '',
          mobile: '',
          sex: '男',
          email: '',
          status: true,
          summary: '',
          loginCount: 0,
        },
        rules: {
          organize: [
            {
              required: true,
              type: 'array',
              message: '请至少选择一个所属部门',
              trigger: 'change',
            },
          ],
          loginAccount: [
            {
              required: true,
              message: '请输入登录账号',
              trigger: 'blur',
            },
          ],
          role: [
            {
              required: true,
              message: '请择选所属角色',
              trigger: 'change',
            },
          ],
          loginPassWord: [
            {
              required: true,
              message: '请输入登录密码',
              trigger: 'blur',
            },
          ],
          post: [
            {
              required: true,
              message: '请选择所属岗位',
              trigger: 'change',
            },
          ],
          fullName: [
            {
              required: true,
              message: '请输入姓名',
              trigger: 'blur',
            },
          ],
          mobile: [
            {
              required: true,
              message: '请输入手机号码',
              trigger: 'blur',
            },
          ],
          sex: [
            {
              required: true,
              message: '性别不能为空',
              trigger: 'change',
            },
          ],
          email: [],
          summary: [],
        },
        organizeOptions: [],
        roleOptions: [],
        postOptions: [],
        sexOptions: [
          {
            label: '男',
            value: '男',
          },
          {
            label: '女',
            value: '女',
          },
        ],
        roleProps: { multiple: true, expandTrigger: 'hover' },
        organizeProps: {
          multiple: false,
          label: 'label',
          value: 'value',
          children: 'children',
          checkStrictly: true,
          expandTrigger: 'hover',
        },
      }
    },
    methods: {
      async init() {
        const org = await organizeAll()
        let orgArr = []
        org.data.forEach(function (m, i) {
          orgArr.push({
            id: m.id,
            value: m.id,
            label: m.name,
            parentId: m.parentId,
          })
        })
        this.organizeOptions = changeTree(orgArr)
        const role = await roleAll()
        let roleArr = []
        role.data.forEach(function (m, i) {
          roleArr.push({
            id: m.id,
            value: m.id,
            label: m.name,
            parentId: m.parentId,
          })
        })
        this.roleOptions = changeTree(roleArr)
        const post = await postAll({ limit: 1000 })
        this.postOptions = post.data.items
      },
      onClose() {
        this.formData = {
          sex: '男',
          status: true,
        }
        this.$refs['elForm'].resetFields()
      },
      close() {
        this.formData = {
          sex: '男',
          status: true,
        }
        this.dialog.visible = false
        this.$emit('update:visible', false)
      },
      handleAdd() {
        this.init()
        this.formData.id = '0'
        this.dialog.title = '添加用户'
        this.dialog.visible = true
      },
      handelModify(record) {
        this.init()
        this.dialog.title = '编辑用户'
        this.dialog.visible = true
        this.formData = deepClone(record)
        this.formData.organize = this.formData.organizeIdList.split(',')
        this.formData.role = []
        this.formData.role = this.formData.roleGroupParent.split('|')
        this.formData.post = this.formData.postGroup.split(',')
        console.log(this.formData)
      },
      handelConfirm() {
        this.$refs['elForm'].validate(async (valid) => {
          if (!valid) return
          this.formData.organizeIdList = this.formData.organize.join(',')
          this.formData.organizeId = this.formData.organize.pop()
          this.formData.postGroup = this.formData.post.join(',')
          let roleNode = this.$refs.roleRef.getCheckedNodes()
          let roleArr = []
          roleNode.forEach(function (item, i) {
            if (item.children.length == 0) {
              roleArr.push(item.data.id)
            }
          })
          this.formData.roleGroup = roleArr.join(',')
          this.formData.organize = null
          this.formData.roleGroupParent = this.formData.role.join('|')
          console.log(this.formData.role)
          let tipName = '添加成功'
          let res = null
          if (this.formData.id == '0') {
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
<style lang="scss" scoped>
  .cur-left {
    width: 240px;
    position: absolute;
    .select-img {
      border-radius: 5px;
      border: 1px solid #e6e6e6;
      padding: 10px;
      text-align: center;
      .bg-gray {
        background-color: #f5f7fa;
        border-radius: 4px;
        padding: 55px 0;
        cursor: pointer;
        i {
          font-size: 40px;
          color: #ccd1d9;
        }
      }
    }
  }
  .user-else-info {
    background-color: #f6f9fd;
    text-align: center;
    padding: 5px 0;
    .last-login {
      padding: 10px 0;
    }
    p {
      margin: 5px 0;
    }
  }
  .user-pic {
    width: 100%;
    height: 200px !important;
  }
  .cur-right {
    padding-left: 240px;
  }
</style>
