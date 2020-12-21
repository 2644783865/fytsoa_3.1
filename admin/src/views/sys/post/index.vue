<template>
  <div>
    <el-row class="fyt-search">
      <el-col :span="24">
        <el-form :inline="true" :model="param" class="demo-form-inline">
          <el-form-item label="机构名称">
            <el-input
              v-model="param.key"
              clearable
              placeholder="根据岗位名称搜索"
            ></el-input>
          </el-form-item>
          <el-form-item label="状态">
            <el-select
              v-model="param.status"
              clearable
              placeholder="根据状态搜索"
            >
              <el-option label="正常" value="1"></el-option>
              <el-option label="停用" value="0"></el-option>
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
          :height="tableAttr.height"
          row-key="id"
        >
          <el-table-column type="selection" width="55"></el-table-column>
          <el-table-column
            type="index"
            width="80"
            label="序号"
          ></el-table-column>
          <el-table-column
            prop="number"
            label="岗位编号"
            width="180"
          ></el-table-column>
          <el-table-column prop="name" label="岗位名称"></el-table-column>
          <el-table-column
            prop="sort"
            label="排序"
            width="180"
          ></el-table-column>
          <el-table-column prop="status" label="状态" width="180">
            <template slot-scope="scope">
              <el-tag
                :type="scope.row.status ? 'success' : 'danger'"
                disable-transitions
              >
                {{ scope.row.status ? '正常' : '停用' }}
              </el-tag>
            </template>
          </el-table-column>
          <el-table-column
            prop="createTime"
            label="创建时间"
            width="180"
          ></el-table-column>
          <el-table-column fixed="right" label="操作" width="160">
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
                @click.native.prevent="deleteRow(scope.row)"
              >
                删除
              </el-link>
            </template>
          </el-table-column>
        </el-table>
        <el-pagination
          style="text-align: right"
          :current-page="tableData.currentPage"
          :page-sizes="[10, 20, 50, 100, 200, 500, 1000]"
          :page-size="10"
          layout="total, sizes, prev, pager, next, jumper"
          :total="tableData.totalItems"
          @size-change="handleSizeChange"
          @current-change="handleCurrentChange"
        ></el-pagination>
      </el-col>
    </el-row>
    <modify ref="modify" @complete="onComplete"></modify>
  </div>
</template>
<script>
  import { getList, deletes } from '@/api/sys/post'
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
        loading: true,
        tableAttr: {
          height: 300,
        },
        tableData: {},
      }
    },
    created() {
      this.tableAttr.height = window.innerHeight - 325
    },
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
        this.$confirm('确认要删除当前的岗位吗？', '提示', {
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
