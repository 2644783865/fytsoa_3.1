<template>
  <div>
    <el-row class="fyt-search">
      <el-col :span="24">
        <el-form :inline="true" :model="param" class="demo-form-inline">
          <el-form-item label="关键字搜索">
            <el-input
              v-model="param.key"
              clearable
              placeholder="根据关键字搜索"
            ></el-input>
          </el-form-item>
          <el-form-item label="请求类型">
            <el-select v-model="param.status" clearable placeholder="根据类型">
              <el-option label="操作" value="1"></el-option>
              <el-option label="登录" value="2"></el-option>
              <el-option label="异常" value="3"></el-option>
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
          <el-table-column type="expand">
            <template slot-scope="props">
              <el-form label-position="left" inline class="cur-table-expand">
                <el-form-item label="请求参数：">
                  <span>{{ props.row.parameters }}</span>
                </el-form-item>
                <el-form-item label="异常信息：">
                  <span>{{ props.row.message }}</span>
                </el-form-item>
              </el-form>
            </template>
          </el-table-column>
          <el-table-column type="selection" width="55"></el-table-column>

          <el-table-column
            prop="operateUser"
            label="操作人"
            width="180"
          ></el-table-column>
          <el-table-column
            prop="address"
            label="请求地址"
            :show-overflow-tooltip="true"
            width="260"
          ></el-table-column>
          <el-table-column prop="ip" label="Ip" width="180"></el-table-column>
          <el-table-column
            prop="executionDuration"
            label="请求耗时(ms)"
            width="130"
          ></el-table-column>
          <el-table-column label="请求结果" width="100" align="center">
            <template slot-scope="scope">
              <el-link
                icon="el-icon-document"
                :underline="false"
                type="primary"
                @click="$refs.info.handelInfo(scope.row)"
              ></el-link>
            </template>
          </el-table-column>
          <el-table-column
            prop="browser"
            label="浏览器信息"
            :show-overflow-tooltip="true"
            width="280"
          ></el-table-column>
          <el-table-column
            prop="operateTime"
            label="日志时间"
            width="180"
          ></el-table-column>
          <el-table-column fixed="right" label="操作" width="100">
            <template slot-scope="scope">
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
    <info ref="info"></info>
  </div>
</template>
<script>
  import { getList, deletes } from '@/api/sys/logs'
  import info from './info'
  export default {
    components: {
      info,
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
        this.loading = true
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
    },
  }
</script>
<style>
  .cur-table-expand {
    font-size: 0;
  }
  .cur-table-expand label {
    width: 90px;
    color: #99a9bf;
  }
  .cur-table-expand .el-form-item {
    margin-right: 0;
    margin-bottom: 0;
    width: 100%;
  }
</style>
<style lang="scss" scoped></style>
