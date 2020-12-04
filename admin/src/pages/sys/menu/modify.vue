<template>
    <div>
        <a-modal :title="title" :visible="visible" :confirm-loading="confirmLoading" @ok="handleOk"
            @cancel="handleCancel">
            <a-form :form="form" :label-col="{ span: 5 }" :wrapper-col="{ span: 18 }" @submit="handleSubmit">
                <a-form-item label="父级菜单">
                    <a-tree-select v-model="value" style="width: 100%"
                        :dropdown-style="{ maxHeight: '400px', overflow: 'auto' }" :tree-data="treeData"
                        placeholder="请选择上级菜单" tree-default-expand-all>
                    </a-tree-select>
                </a-form-item>
                <a-form-item label="Note">
                    <a-input
                        v-decorator="['note', { rules: [{ required: true, message: 'Please input your note!' }] }]" />
                </a-form-item>
                <a-form-item label="Gender">
                    <a-select v-decorator="[
                      'gender',
                      { rules: [{ required: true, message: 'Please select your gender!' }] },
                    ]" placeholder="Select a option and change input text above">
                        <a-select-option value="male">
                            male
                        </a-select-option>
                        <a-select-option value="female">
                            female
                        </a-select-option>
                    </a-select>
                </a-form-item>
            </a-form>
        </a-modal>
    </div>
</template>
<script>
    export default {
        data() {
            return {
                visible: false,
                title: '增加菜单',
                confirmLoading: false,
                form: this.$form.createForm(this, { name: 'coordinated' }),
                treeData: []
            }

        }, methods: {
            handelModify(record) {
                this.title = !record ? '增加菜单' : '修改菜单'
                this.visible = true;
            },
            handleOk(e) {
                this.confirmLoading = true;
                this.form.validateFields((err, values) => {
                    if (!err) {
                        console.log('Received values of form: ', values);
                        setTimeout(() => {
                            this.visible = false;
                            this.confirmLoading = false;
                        }, 2000);
                    } else {
                        this.confirmLoading = false;
                    }
                });

            },
            handleCancel(e) {
                this.visible = false;
            }
        }
    }
</script>