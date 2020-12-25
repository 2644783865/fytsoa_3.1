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
              icon="el-icon-edit"
              @click="$refs.modifycolumn.handleAdd()"
            >
              添加字典栏目
            </el-button>
            <el-button type="primary" icon="el-icon-edit" @click="addCode">
              添加值
            </el-button>
            <el-button type="danger" icon="el-icon-edit">删除值</el-button>
          </el-col>
          <el-col :span="24">
            <el-table :data="tableData" :height="tableAttr.height">
              <el-table-column
                fixed
                prop="date"
                label="日期"
                width="200"
              ></el-table-column>
              <el-table-column
                prop="name"
                label="姓名"
                width="120"
              ></el-table-column>
              <el-table-column
                prop="province"
                label="省份"
                width="120"
              ></el-table-column>
              <el-table-column
                prop="city"
                label="市区"
                width="120"
              ></el-table-column>
              <el-table-column prop="address" label="地址"></el-table-column>
              <el-table-column
                prop="zip"
                label="邮编"
                width="120"
              ></el-table-column>
              <el-table-column fixed="right" label="操作" width="120">
                <template slot-scope="scope">
                  <el-button
                    type="text"
                    size="small"
                    @click.native.prevent="deleteRow(scope.$index, tableData)"
                  >
                    移除
                  </el-button>
                </template>
              </el-table-column>
            </el-table>
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
        },
        selectColumn: {},
        tableAttr: {
          height: 300,
        },
        tableData: [],
        treeData: [],
      }
    },
    created() {
      this.tableAttr.height = window.innerHeight - 275
    },
    mounted() {
      this.init()
    },
    methods: {
      async init() {
        const t = await columnList()
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
        console.log(this.treeData)
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
      onColumnComplete() {
        this.init()
      },
      onComplete() {},
      deleteRow(index, rows) {
        rows.splice(index, 1)
      },
      columnnode(data, node, e) {
        this.selectColumn = data
      },
      append(data, e) {
        e.cancelBubble = true
        $refs.modifytype.handelModify(data)
      },
      remove(node, data, e) {
        alert('delete')
      },
      renderContent(h, { node, data, store }) {
        return (
          <span class="custom-tree-node">
            <span>{node.label}</span>
            <span class="tool">
              <el-button
                icon="el-icon-edit"
                type="text"
                on-click={() => this.append(data, $event)}
              ></el-button>
              <el-button
                icon="el-icon-delete"
                type="text"
                on-click={() => this.remove(node, data, $event)}
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
    width: auto;
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
