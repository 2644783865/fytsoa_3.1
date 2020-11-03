import TabsView from '@/layouts/tabs/TabsView'
import BlankView from '@/layouts/BlankView'
import PageView from '@/layouts/PageView'

// 路由配置
const options = {
    routes: [{
            path: '/login',
            name: '后台管理系统',
            component: () =>
                import ('@/pages/login')
        },
        {
            path: '*',
            name: '404',
            component: () =>
                import ('@/pages/exception/404'),
        },
        {
            path: '/403',
            name: '403',
            component: () =>
                import ('@/pages/exception/403'),
        },
        {
            path: '/',
            name: '首页',
            component: TabsView,
            redirect: '/login',
            children: [{
                    path: 'dashboard',
                    name: '控制台',
                    meta: {
                        icon: 'file-ppt'
                    },
                    children: [{
                        path: 'workplace',
                        name: '工作台',
                        component: () =>
                            import ('@/pages/demo'),
                    }, {
                        path: 'datas',
                        name: '数据中心',
                        component: () =>
                            import ('@/pages/demo'),
                    }]
                },
                {
                    path: 'parent1',
                    name: '父级路由1',
                    meta: {
                        icon: 'dashboard',
                    },
                    component: BlankView,
                    children: [{
                        path: 'demo1',
                        name: '演示页面1',
                        component: () =>
                            import ('@/pages/demo'),
                    }]
                },
                {
                    path: 'parent2',
                    name: '父级路由2',
                    meta: {
                        icon: 'form'
                    },
                    component: PageView,
                    children: [{
                        path: 'demo2',
                        name: '演示页面2',
                        component: () =>
                            import ('@/pages/demo'),
                    }]
                },
                {
                    path: 'sys',
                    name: '系统管理',
                    meta: {
                        icon: 'setting'
                    },
                    component: PageView,
                    children: [{
                        path: 'or',
                        name: '机构管理',
                        component: () =>
                            import ('@/pages/demo'),
                    }, {
                        path: 'role',
                        name: '角色管理',
                        component: () =>
                            import ('@/pages/demo'),
                    }, {
                        path: 'post',
                        name: '岗位管理',
                        component: () =>
                            import ('@/pages/demo'),
                    }, {
                        path: 'menu',
                        name: '菜单管理',
                        component: () =>
                            import ('@/pages/demo'),
                    }, {
                        path: 'admin',
                        name: '管理员管理',
                        component: () =>
                            import ('@/pages/demo'),
                    }, {
                        path: 'qx',
                        name: '权限管理',
                        component: () =>
                            import ('@/pages/demo'),
                    }, {
                        path: 'log',
                        name: '日志管理',
                        component: () =>
                            import ('@/pages/log'),
                    }]
                },
                {
                    path: 'exception',
                    name: '异常页',
                    meta: {
                        icon: 'warning',
                    },
                    component: BlankView,
                    children: [{
                            path: '404',
                            name: 'Exp404',
                            component: () =>
                                import ('@/pages/exception/404')
                        },
                        {
                            path: '403',
                            name: 'Exp403',
                            component: () =>
                                import ('@/pages/exception/403')
                        },
                        {
                            path: '500',
                            name: 'Exp500',
                            component: () =>
                                import ('@/pages/exception/500')
                        }
                    ]
                },
                {
                    name: '验权页面',
                    path: 'auth/demo',
                    meta: {
                        icon: 'file-ppt',
                        authority: {
                            permission: 'form',
                            role: 'manager'
                        },
                        component: () =>
                            import ('@/pages/demo')
                    }
                }
            ]
        }
    ]
}

export default options