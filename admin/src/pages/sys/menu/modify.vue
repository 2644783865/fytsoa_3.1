<template>
    <div>
        <a-modal :title="modal.title" :visible="modal.visible" :confirm-loading="modal.confirmLoading" @ok="handleOk"
            @cancel="handleCancel" :width="modal.width">
            <a-form :form="form" :label-col="{ span: 3 }" :wrapper-col="{ span: 21 }" @submit="handleSubmit">
                <a-row :gutter="20">
                    <a-col :span="24">
                        <a-form-item label="父级菜单">
                            <a-tree-select v-model="value" :dropdown-style="{ maxHeight: '400px', overflow: 'auto' }"
                                :tree-data="treeData" placeholder="请选择上级菜单" tree-default-expand-all>
                            </a-tree-select>
                        </a-form-item>
                    </a-col>
                    <a-col :span="12">
                        <a-form-item label="菜单类型" :label-col="{ span: 6 }" :wrapper-col="{ span: 18 }">
                            <a-radio-group name="radioGroup" :default-value="types" @change="onChange">
                                <a-radio :value="1">
                                    目录
                                </a-radio>
                                <a-radio :value="2">
                                    菜单
                                </a-radio>
                            </a-radio-group>
                        </a-form-item>
                    </a-col>
                    <a-col :span="12">
                        <a-form-item label="状态" :label-col="{ span: 6 }" :wrapper-col="{ span: 18 }">
                            <a-radio-group name="radioGroup" :default-value="1">
                                <a-radio :value="1">
                                    显示
                                </a-radio>
                                <a-radio :value="2">
                                    隐藏
                                </a-radio>
                            </a-radio-group>
                        </a-form-item>
                    </a-col>
                    <a-col :span="12">
                        <a-form-item label="菜单名称" :label-col="{ span: 6 }" :wrapper-col="{ span: 18 }">
                            <a-input maxLength="10" allowClear
                                v-decorator="['name', { rules: [{ required: true, message: 'Please input your note!' }] }]" />
                        </a-form-item>
                    </a-col>
                    <a-col :span="12">
                        <a-form-item label="权限标识" :label-col="{ span: 6 }" :wrapper-col="{ span: 18 }">
                            <a-input
                                v-decorator="['code', { rules: [{ required: true, message: 'Please input your note!' }] }]" />
                        </a-form-item>
                    </a-col>
                    <a-col :span="24">
                        <a-form-item label="菜单图标">
                            <a-input allowClear />
                        </a-form-item>
                    </a-col>
                    <a-col :span="24" v-show="types==2">
                        <a-form-item label="路由地址">
                            <a-input allowClear
                                v-decorator="['code', { rules: [{ required: true, message: 'Please input your note!' }] }]" />
                        </a-form-item>
                    </a-col>
                    <a-col :span="24" v-show="types==2">
                        <a-form-item label="权限标识">
                            <a-checkbox-group :options="plainOptions" />
                        </a-form-item>
                    </a-col>
                </a-row>
            </a-form>
        </a-modal>
    </div>
</template>
<script>
    const plainOptions = ['添加', '修改', '删除'];
    export default {
        data() {
            return {
                form: this.$form.createForm(this, { name: 'coordinated' }),
                treeData: [],
                modal: {
                    visible: false,
                    title: '增加菜单',
                    width: 780,
                    confirmLoading: false,
                },
                types: 1,
                plainOptions
            }

        }, methods: {
            handelModify(record) {
                this.modal.title = !record ? '增加菜单' : '修改菜单'
                this.modal.visible = true;
            },
            handleOk(e) {
                this.modal.confirmLoading = true;
                this.form.validateFields((err, values) => {
                    if (!err) {
                        console.log('Received values of form: ', values);
                        setTimeout(() => {
                            this.modal.visible = false;
                            this.modal.confirmLoading = false;
                        }, 2000);
                    } else {
                        this.modal.confirmLoading = false;
                    }
                });

            },
            handleCancel(e) {
                this.modal.visible = false;
            },
            onChange(e) {
                this.types = e.target.value;
            }
        }
    }
</script>