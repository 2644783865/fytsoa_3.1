<template>
  <div>
    <el-row class="fyt-search">
      <el-col :span="24">
        <el-form :inline="true" :model="param" class="demo-form-inline">
          <el-form-item label="菜单名称">
            <el-input
              v-model="param.key"
              clearable
              placeholder="菜单名称"
            ></el-input>
          </el-form-item>
          <el-form-item label="状态">
            <el-select v-model="param.status" clearable placeholder="状态">
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
        <el-button type="success" icon="el-icon-finished" @click="openAll">
          展开所有
        </el-button>
        <el-button
          type="warning"
          icon="el-icon-c-scale-to-original"
          @click="closeAll"
        >
          合并所有
        </el-button>
      </el-col>
      <el-col :span="24">
        <el-table
          v-if="tableAttr.refresh"
          v-loading="loading"
          :data="tableData"
          :height="tableAttr.height"
          row-key="id"
          :default-expand-all="tableAttr.expand"
          :tree-props="{ children: 'children', hasChildren: 'hasChildren' }"
        >
          <el-table-column prop="name" label="菜单名称"></el-table-column>
          <el-table-column
            prop="code"
            label="权限标识"
            width="150"
          ></el-table-column>
          <el-table-column
            prop="urls"
            label="路由"
            width="220"
          ></el-table-column>
          <el-table-column prop="sort" label="排序" width="80">
            <template slot-scope="scope">
              <el-tooltip
                class="item"
                effect="dark"
                content="向上升一级"
                placement="top"
              >
                <el-link
                  :underline="false"
                  style="color: #5cd29d"
                  icon="el-icon-upload2"
                  @click="
                    sort({
                      parent: scope.row.parentId,
                      id: scope.row.id,
                      type: 0,
                    })
                  "
                ></el-link>
              </el-tooltip>
              <el-tooltip
                class="item"
                effect="dark"
                content="向下降一级"
                placement="top"
              >
                <el-link
                  :underline="false"
                  style="margin-left: 10px; color: #5cd29d"
                  icon="el-icon-download"
                  @click="
                    sort({
                      parent: scope.row.parentId,
                      id: scope.row.id,
                      type: 1,
                    })
                  "
                ></el-link>
              </el-tooltip>
            </template>
          </el-table-column>
          <el-table-column prop="status" label="状态" width="80">
            <template slot-scope="scope">
              <el-tag
                :type="scope.row.status ? 'success' : 'danger'"
                disable-transitions
              >
                {{ scope.row.status ? '显示' : '隐藏' }}
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
                @click="deleteRow(scope.row)"
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
  import { getList, deletes, colSort } from '@/api/sys/menu'
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
          expand: true,
          refresh: true,
        },
        loading: true,
        tableData: [],
        options: [
          {
            value: 1,
            label: '显示',
          },
          {
            value: 2,
            label: '隐藏',
          },
        ],
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
        this.loading = false
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
      openAll() {
        this.tableAttr.refresh = false
        this.tableAttr.expand = true
        this.$nextTick(() => {
          this.tableAttr.refresh = true
        })
      },
      closeAll() {
        this.tableAttr.refresh = false
        this.tableAttr.expand = false
        this.$nextTick(() => {
          this.tableAttr.refresh = true
        })
      },
      async sort(m) {
        await colSort(m)
        this.init()
      },
      onComplete() {
        this.init()
      },
    },
  }
</script>
<style lang="scss" scoped></style>
