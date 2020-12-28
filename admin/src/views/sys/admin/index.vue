<template>
  <div>
    <el-row class="fyt-search">
      <el-col :span="24">
        <el-form :inline="true" :model="param" class="demo-form-inline">
          <el-form-item label="关键字">
            <el-input
              v-model="param.key"
              clearable
              placeholder="可根据姓名、手机搜索"
            ></el-input>
          </el-form-item>
          <el-form-item label="用户状态">
            <el-select v-model="param.status" clearable placeholder="用户状态">
              <el-option
                v-for="item in options"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" icon="el-icon-zoom-in" @click="onSubmit">
              查询
            </el-button>
          </el-form-item>
        </el-form>
      </el-col>
    </el-row>
    <el-row style="padding: 15px">
      <el-col :span="24" class="fyt-tools">
        <el-button
          type="primary"
          icon="el-icon-plus"
          @click="$refs.modify.handleAdd()"
        >
          添加
        </el-button>
        <el-button type="danger" icon="el-icon-delete" @click="delSelect">
          删除
        </el-button>
      </el-col>
      <el-col :span="24">
        <el-table
          ref="multipleTable"
          v-loading="loading"
          :data="tableData.items"
          row-key="id"
          :height="tableAttr.height"
        >
          <el-table-column
            type="selection"
            fixed="left"
            width="55"
          ></el-table-column>
          <el-table-column
            type="index"
            fixed="left"
            width="80"
            label="序号"
          ></el-table-column>
          <el-table-column
            prop="fullName"
            fixed="left"
            width="100"
            label="姓名"
          ></el-table-column>
          <el-table-column
            prop="loginAccount"
            label="登录账号"
            width="120"
          ></el-table-column>
          <el-table-column
            prop="organizeName"
            label="所属部门"
            width="120"
          ></el-table-column>
          <el-table-column prop="sex" label="性别" width="80"></el-table-column>
          <el-table-column
            prop="mobile"
            label="手机号码"
            width="180"
          ></el-table-column>
          <el-table-column prop="status" label="状态" width="180">
            <template slot-scope="scope">
              <el-link
                :underline="false"
                :type="scope.row.status ? 'primary' : 'warning'"
              >
                {{ scope.row.status ? '正常' : '停用' }}
              </el-link>
            </template>
          </el-table-column>
          <el-table-column
            prop="createTime"
            label="创建时间"
            width="180"
          ></el-table-column>
          <el-table-column fixed="right" label="操作" width="140">
            <template slot-scope="scope">
              <el-link
                icon="el-icon-edit"
                :underline="false"
                type="primary"
                @click="$refs.modify.handelModify(scope.row)"
              >
                修改
              </el-link>
              <el-link
                icon="el-icon-delete"
                :underline="false"
                type="danger"
                style="margin-left: 15px"
                @click="deleteRow(scope.row)"
              >
                删除
              </el-link>
            </template>
          </el-table-column>
        </el-table>
        <el-pagination
          background
          :current-page="tableData.currentPage"
          :page-sizes="[10, 20, 50, 100, 200, 500, 1000]"
          :page-size="10"
          layout="total, sizes, prev, pager, next, jumper"
          :total="tableData.totalItems"
          style="text-align: right"
          @size-change="handleSizeChange"
          @current-change="handleCurrentChange"
        ></el-pagination>
      </el-col>
      <modify ref="modify" @complete="onComplete"></modify>
    </el-row>
  </div>
</template>
<script>
  import { getList, deletes } from '@/api/sys/admin'
  import modify from './modify'
  export default {
    components: {
      modify,
    },
    data() {
      return {
        param: {
          key: '',
          status: '',
          limit: 10,
          page: 1,
        },
        tableAttr: {
          height: window.innerHeight - 325,
        },
        tableData: [],
        options: [
          {
            value: 1,
            label: '正常',
          },
          {
            value: 0,
            label: '停用',
          },
        ],
        loading: true,
      }
    },
    created() {},
    mounted() {
      this.init()
    },
    methods: {
      async init() {
        const t = await getList(this.param)
        this.tableData = t.data
        this.loading = false
      },
      onSubmit() {
        this.init()
      },
      deleteRow(rows) {
        this.delSubmit(rows.id)
      },
      delSelect() {
        const _selectData = this.$refs.multipleTable.selection
        let ids = []
        _selectData.forEach((element) => {
          ids.push(element.id)
        })
        if (ids.length == 0) {
          this.$notify({
            message: '请选择要删除的项~',
            type: 'warning',
          })
          return
        }
        this.delSubmit(ids.join(','))
      },
      delSubmit(parm) {
        var _this = this
        this.$confirm('确认删除选中用户信息吗？', '提示', {
          type: 'warning',
        }).then(async () => {
          this.loading = true
          const res = await deletes(parm)
          this.loading = false
          if (res.code == 200) {
            this.$notify({
              message: '删除成功',
              type: 'success',
            })
            _this.init()
          } else {
            this.$notify({
              message: res.message,
              type: 'error',
            })
          }
        })
      },
      handleSizeChange(val) {
        this.param.page = 1
        this.param.limit = val
        this.init()
      },
      handleCurrentChange(val) {
        this.param.page = val
        this.init()
      },
      onComplete() {
        this.init()
      },
    },
  }
</script>
<style lang="scss" scoped></style>
