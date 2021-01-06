<template>
  <div>
    <el-row class="fyt-search">
      <el-col :span="24">
        <el-form :inline="true" :model="param" class="demo-form-inline">
          <el-form-item label="机构名称">
            <el-input
              v-model="param.key"
              clearable
              placeholder="根据角色名称搜索"
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
      </el-col>
      <el-col :span="24">
        <el-table
          :data="tableData"
          :height="tableAttr.height"
          row-key="id"
          default-expand-all
          :tree-props="{ children: 'children', hasChildren: 'hasChildren' }"
        >
          <el-table-column fixed prop="name" label="角色名称"></el-table-column>
          <el-table-column prop="isSystem" label="是否超管" width="140">
            <template slot-scope="scope">
              <el-tag
                :type="scope.row.isSystem ? 'success' : 'danger'"
                disable-transitions
              >
                {{ scope.row.isSystem ? '是' : '否' }}
              </el-tag>
            </template>
          </el-table-column>

          <el-table-column
            prop="sort"
            label="排序"
            width="100"
          ></el-table-column>
          <el-table-column prop="status" label="状态" width="100">
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
          <el-table-column fixed="right" label="操作" width="260">
            <template slot-scope="scope">
              <el-link
                icon="el-icon-finished"
                :underline="false"
                type="primary"
              >
                分配权限
              </el-link>
              <el-link
                icon="el-icon-edit"
                :underline="false"
                type="primary"
                style="margin-left: 15px"
                @click.native.prevent="$refs.modify.handelModify(scope.row)"
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
      </el-col>
    </el-row>
    <modify ref="modify" @complete="onComplete"></modify>
  </div>
</template>
<script>
  import { getList, deletes } from '@/api/sys/role'
  import { changeTree } from '@/utils/treeTool'
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
        },
        tableAttr: {
          height: 300,
        },
        tableData: [],
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
        const t = await getList(this.param)
        this.tableData = changeTree(t.data)
      },
      onSubmit() {
        this.init()
      },
      deleteRow(rows) {
        var _this = this
        this.$confirm(
          '确认删除选中记录吗？如果下面有子级，将子级一并删除！',
          '提示',
          {
            type: 'warning',
          }
        ).then(async () => {
          this.loading = true
          const res = await deletes(rows.id)
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
      onComplete() {
        this.init()
      },
    },
  }
</script>
<style lang="scss" scoped></style>
