<template>
    <a-row style="margin-top: 0" :gutter="[24, 24]">
        <a-col :sm="24" :md="24" :xl="24">
            <a-card>
                <a-button type="primary" icon="plus" @click="$refs.modify.handelModify()">添加</a-button>
                <a-button type="danger" icon="delete">添加</a-button>
            </a-card>
            <a-card>
                <div>
                    <div class="operator">

                    </div>
                    <a-table :columns="columns" rowKey="id" :data-source="data" :row-selection="rowSelection"
                        defaultExpandAllRows v-if="!loading" :pagination="pageStatus" :loading="loading"
                        :scroll="{ x: 1500 }">
                        <template slot="btnFun" slot-scope="btnFun">
                            <span>
                                <a-tag v-for="(tag,index) in btnFun" :key="index" color="blue">{{tag.name}}</a-tag>
                            </span>
                        </template>
                        <span slot="sort" slot-scope="text,record">
                            <a-tag color="cyan">
                                <a-icon type="arrow-up" @click="curSort(1,record)" />
                            </a-tag>
                            <a-tag color="cyan">
                                <a-icon type="arrow-down" @click="curSort(2,record)" />
                            </a-tag>
                        </span>
                        <span slot="id" slot-scope="text,record">
                            <a @click="$refs.modify.handelModify(record)">添加子级</a>
                            <a-divider type="vertical" />
                            <a @click="$refs.modify.handelModify(record)">修改</a>
                        </span>
                    </a-table>
                </div>
            </a-card>
        </a-col>
        <modify ref="modify" @complete="onComplete"></modify>
    </a-row>
</template>

<script>
    import { request, METHOD } from '@/utils/request'
    import modify from './modify'
    const columns = [
        {
            title: '菜单名称',
            dataIndex: 'name',
            key: 'name',
            fixed: 'left',
            width: '200px'
        },
        {
            title: '别名',
            dataIndex: 'code',
            width: '200px',
        },
        {
            title: 'Url',
            dataIndex: 'urls',
            width: '300px'
        },
        {
            title: '排序',
            dataIndex: 'sort',
            width: '100px',
            scopedSlots: { customRender: 'sort' }
        },
        {
            title: '按钮权限',
            dataIndex: 'btnFun',
            width: '300px',
            scopedSlots: { customRender: 'btnFun' }
        },
        {
            title: '操作',
            dataIndex: 'id',
            width: '140px',
            fixed: 'right',
            scopedSlots: { customRender: 'id' }
        },
    ];

    const rowSelection = {
        onChange: (selectedRowKeys, selectedRows) => {
            console.log(`selectedRowKeys: ${selectedRowKeys}`, 'selectedRows: ', selectedRows);
        },
        onSelect: (record, selected, selectedRows) => {
            console.log(record, selected, selectedRows);
        },
        onSelectAll: (selected, selectedRows, changeRows) => {
            console.log(selected, selectedRows, changeRows);
        },
    };

    export default {
        components: {
            modify
        },
        data() {
            return {
                advanced: true,
                columns: columns,
                selectedRows: [],
                rowSelection,
                data: [],
                pageStatus: false,
                loading: true
            }
        },
        mounted() {
            this.init()
        },
        methods: {
            init() {
                request('http://192.168.1.11:5005/api/Menu', METHOD.GET).then(res => {
                    var result = res.data;
                    console.log(result);
                    this.data = this.changeTree(result.data);
                    this.loading = false;
                    this.pageStatus = false;
                })
            },
            curSort(mold, m) {
                console.log(m);
            },
            onComplete() {
                alert('11111');
            },
            add(m) {

            },
            edit(m) {

            },
            changeTree: function (paramData) {
                if (paramData.length > 0) {
                    paramData.forEach(item => {
                        const parentId = item.parentId;
                        if (parentId) {
                            paramData.forEach(ele => {
                                if (ele.id === parentId) {
                                    let childArray = ele.children;
                                    if (!childArray) {
                                        childArray = [];
                                    }

                                    childArray.push(item);
                                    ele.children = childArray;
                                }
                            });
                        }
                    });
                }
                return paramData.filter(item => item.parentId === '0');
            },
        }
    }
</script>

<style lang="less" scoped>
.ant-btn {
    margin-right: 15px;
}
</style>