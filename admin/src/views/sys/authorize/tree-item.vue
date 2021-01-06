<template>
  <div class="menu-wall soa-cur" style="padding-bottom: 0px">
    <ul v-for="(it, index) in treeData" :key="index" class="layui-tree-cus">
      <tree-item class="item" :item="it" @make-folder="makeFolder"></tree-item>
    </ul>
  </div>
</template>
<script>
  export default {
    name: 'TreeItem',
    // template: '#item-template',
    // props: {
    //   node: Array,
    // },
    data: function () {
      return {
        treeData: [],
        isOpen: true,
      }
    },
    computed: {
      isFolder: function () {
        return this.item.children && this.item.children.length
      },
    },
    methods: {
      toggle: function () {
        if (this.isFolder) {
          this.isOpen = !this.isOpen
        }
      },
      makeFolder: function () {
        if (!this.isFolder) {
          this.$emit('make-folder', this.item)
          this.isOpen = true
        }
      },
      selectCbk: function (m, e) {
        var that = this
        this.selectParentCbk(m, e.target.checked)
        _tempDate.some((item) => {
          if (item.parentGuidList.indexOf(m.guid) > 0) {
            if (e.target.checked) {
              item.isChecked = true
              if (item.btnFun) {
                item.btnFun.some((m) => {
                  m.status = true
                })
              }
            } else {
              item.isChecked = false
              if (item.btnFun) {
                item.btnFun.some((m) => {
                  m.status = false
                })
              }
            }
          }
        })
      },
      selectBtnCbk(m, pm, e) {
        if (e.target.checked) {
          _tempDate.some((item) => {
            if (item.guid == pm.guid && !item.isChecked) {
              item.isChecked = true
            }
          })
        } else {
          var isLayerCbk = false
          pm.btnFun.some((item) => {
            if (item.status && item.guid != m.guid) {
              isLayerCbk = true
              return true
            }
          })
          if (!isLayerCbk) {
            pm.isChecked = false
          }
        }
      },
      selectParentCbk(m, cbk_status) {
        var that = this
        var arr = m.parentGuidList.split(',').filter((item) => item != '')
        arr.some((item) => {
          if (item != m.guid) {
            _tempDate.some((row) => {
              if (row.guid === item && m.layer > row.layer) {
                if (cbk_status) {
                  row.isChecked = true
                } else {
                  if (!that.childrenIsSelect(m.parentGuid)) {
                    row.isChecked = false
                  }
                }
              }
            })
          }
        })
      },
      childrenIsSelect(pid) {
        var isRes = false
        _tempDate.some((item) => {
          if (item.parentGuid === pid && item.isChecked) {
            isRes = true
            return true
          }
        })
        return isRes
      },
    },
  }
</script>
<style>
  .menu-wall .layui-tree-cus {
    overflow: auto;
  }

  .menu-wall .layui-tree-cus li {
    padding: 4px 0;
  }

  .menu-wall .layui-tree-cus ul li {
    padding-left: 35px;
    line-height: 30px;
  }

  .menu-wall .layui-tree-cus span {
    cursor: pointer;
  }

  .menu-wall {
    padding: 15px;
  }

  .menu-wall .layui-tree-cus .btnfun {
    display: inline-block;
    margin-left: 50px;
    text-align: right;
  }

  .menu-wall .layui-tree-cus .btnfun label {
    margin-left: 25px;
  }

  .menu-save {
    padding: 10px;
    text-align: center;
  }
</style>
