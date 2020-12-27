<template>
  <div>
    <el-row>
      <el-col class="code-left">
        <div class="title">
          <i class="el-icon-c-scale-to-original"></i>
          字典栏目
        </div>
        <div class="cur-tree">
          <el-tree
            :data="treeData"
            node-key="id"
            default-expand-all
            :expand-on-click-node="false"
            :render-content="renderContent"
            @node-click="columnnode"
          ></el-tree>
        </div>
      </el-col>
      <el-col class="code-right">
        <el-row class="fyt-search">
          <el-col :span="24">
            <el-form :inline="true" :model="param" class="demo-form-inline">
              <el-form-item label="字典值">
                <el-input
                  v-model="param.key"
                  clearable
                  placeholder="根据字典名称+值搜索"
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
                <el-button
                  type="primary"
                  icon="el-icon-zoom-in"
                  @click="onSubmit"
                >
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
              @click="$refs.modifycolumn.handleAdd()"
            >
              添加字典栏目
            </el-button>
            <el-button
              type="primary"
              icon="el-icon-plus"
              :disabled="!vname"
              @click="addCode"
            >
              添加{{ vname ? '【' + vname + '】' : '' }}值
            </el-button>
            <el-button type="danger" icon="el-icon-edit" @click="delSelect">
              删除值
            </el-button>
          </el-col>
          <el-col :span="24">
            <el-table
              ref="multipleTable"
              v-loading="tableAttr.loading"
              :data="tableData.items"
              :height="tableAttr.height"
            >
              <el-table-column type="selection" width="55"></el-table-column>
              <el-table-column
                type="index"
                width="80"
                label="序号"
              ></el-table-column>
              <el-table-column prop="name" label="字典名称"></el-table-column>
              <el-table-column
                prop="codeValues"
                label="字典阔值"
                width="180"
              ></el-table-column>
              <el-table-column
                prop="sort"
                label="排序"
                width="80"
              ></el-table-column>
              <el-table-column prop="status" label="状态" width="100">
                <template slot-scope="scope">
                  <el-link
                    :underline="false"
                    :type="scope.row.status ? 'primary' : 'warning'"
                  >
                    {{ scope.row.status ? '启用' : '停用' }}
                  </el-link>
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
                    @click="deletes(scope.row)"
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
      </el-col>
    </el-row>
    <modifycolumn
      ref="modifycolumn"
      @complete="onColumnComplete"
    ></modifycolumn>
    <modify ref="modify" @complete="onComplete"></modify>
  </div>
</template>
<script>
  import modifycolumn from './modifycolumn'
  import modify from './modify'
  import {
    getCodeList,
    deletes,
    columnList,
    deletesColumn,
  } from '@/api/sys/code'
  import { changeTree } from '@/utils/treeTool'
  export default {
    components: {
      modifycolumn,
      modify,
    },
    data() {
      return {
        param: {
          key: '',
          status: '',
          limit: 10,
          page: 1,
          id: '0',
        },
        selectColumn: {},
        tableAttr: {
          height: 300,
          loading: true,
        },
        sourceData: [],
        tableData: [],
        treeData: [],
        vname: '',
      }
    },
    created() {
      this.tableAttr.height = window.innerHeight - 325
    },
    mounted() {
      this.init()
      this.initCode()
    },
    methods: {
      async init() {
        const t = await columnList()
        this.sourceData = t.data
        let _tree = []
        t.data.some((m) => {
          _tree.push({
            id: m.id,
            value: m.id,
            label: m.name,
            parentId: m.parentId,
          })
        })
        this.treeData = changeTree(_tree)
        //console.log(this.sourceData)
      },
      async initCode() {
        this.tableAttr.loading = true
        if (this.selectColumn && this.selectColumn.id) {
          this.param.id = this.selectColumn.id
        }
        const t = await getCodeList(this.param)
        this.tableData = t.data
        this.tableAttr.loading = false
      },
      onSubmit() {
        this.init()
      },
      addCode() {
        if (!this.selectColumn.id) {
          this.$notify({
            message: '请选择字典栏目，在添加字典值~',
            type: 'warning',
          })
          return
        }
        if (
          this.selectColumn.children &&
          this.selectColumn.children.length > 0
        ) {
          this.$notify({
            message: '请选择字典栏目子级~',
            type: 'warning',
          })
          return
        }
        this.$refs.modify.handleAdd(this.selectColumn)
      },
      handleSizeChange(val) {
        this.param.page = 1
        this.param.limit = val
        this.initCode()
      },
      handleCurrentChange(val) {
        this.param.page = val
        this.initCode()
      },
      onColumnComplete() {
        this.init()
      },
      onComplete() {
        this.initCode()
      },
      deleteRow(index, rows) {
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
        this.$confirm('确认要删除字典值吗？', '提示', {
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
            _this.initCode()
          } else {
            this.$notify({
              message: res.message,
              type: 'error',
            })
          }
        })
      },
      columnnode(data, node, e) {
        this.selectColumn = data
        if (!data.children || data.children.length == 0) {
          this.vname = data.label
        } else {
          this.vname = ''
        }
        this.initCode()
      },
      append(data) {
        var _model = {}
        this.sourceData.forEach((item) => {
          if (item.id == data.id) {
            _model = item
          }
        })
        this.$refs.modifycolumn.handelModify(_model)
      },
      deletes(data) {
        this.delSubmit(data.id)
      },
      remove(node, data) {
        var _this = this
        this.$confirm('确认要删除当前的字典栏目吗？', '提示', {
          type: 'warning',
        }).then(async () => {
          this.loading = true
          const res = await deletesColumn(data.id)
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
      renderContent(h, { node, data, store }) {
        return (
          <span class="custom-tree-node">
            <span>{node.label}</span>
            <span class="tool">
              <el-button
                icon="el-icon-edit"
                type="text"
                on-click={() => this.append(data)}
              ></el-button>
              <el-button
                icon="el-icon-delete"
                type="text"
                on-click={() => this.remove(node, data)}
              ></el-button>
            </span>
          </span>
        )
      },
    },
  }
</script>
<style lang="scss" scoped>
  .code-left {
    width: 260px !important;
    position: absolute;
    top: 0;
    bottom: 0;
    float: inherit;
    background-color: #edf5ff;
    .title {
      height: 50px;
      line-height: 50px;
      padding-left: 20px;
      font-weight: bold;
      background-color: #dee5ef;
    }
  }
  .code-left::after {
    content: '';
    position: absolute;
    height: 1px;
    width: 100%;
    left: -19px;
    top: 0px;
    background-color: #f6f8f9;
  }
  .code-right {
    left: 260px;
    position: relative;
    width: calc(100% - 260px);
    top: 0;
    bottom: 0;
    right: 0;
    float: inherit;
  }
</style>
<style lang="scss">
  .cur-tree .custom-tree-node {
    flex: 1;
    display: flex;
    align-items: center;
    justify-content: space-between;
    font-size: 14px;
    padding-right: 8px;
  }
  .cur-tree .el-tree {
    background: transparent;
  }
  .el-tree-node__content {
    height: 33px;
  }
  .cur-tree .tool {
    display: inline-block;
    padding-right: 10px;
    position: relative;
    z-index: 10;
  }
  .cur-tree .tool button {
    margin-left: 10px;
    font-size: 16px;
  }
  .el-icon-c-scale-to-original {
    font-size: 18px;
    position: relative;
    top: 2px;
  }
</style>
