/**
 * @copyright chuzhixin 1204505056@qq.com
 * @description router全局配置，如有必要可分文件抽离
 */

import Vue from 'vue'
import VueRouter from 'vue-router'
import Layout from '@/layouts'
import EmptyLayout from '@/layouts/EmptyLayout'
import { publicPath, routerMode } from '@/config'

Vue.use(VueRouter)

export const constantRoutes = [
  {
    path: '/login',
    component: () => import('@/views/login/index'),
    hidden: true,
  },
  {
    path: '/register',
    component: () => import('@/views/register/index'),
    hidden: true,
  },
  {
    path: '/401',
    name: '401',
    component: () => import('@/views/401'),
    hidden: true,
  },
  {
    path: '/404',
    name: '404',
    component: () => import('@/views/404'),
    hidden: true,
  },
]

/*当settings.js里authentication配置的是intelligence时，views引入交给前端配置*/
export const asyncRoutes = [
  {
    path: '/',
    component: Layout,
    redirect: '/index',
    children: [
      {
        path: '/index',
        name: 'Index',
        component: () => import('@/views/index/index'),
        meta: {
          title: '首页',
          icon: 'home',
          affix: true,
          noKeepAlive: true,
        },
      },
    ],
  },
  {
    path: '/sys',
    component: Layout,
    redirect: 'noRedirect',
    name: 'Sys',
    alwaysShow: true,
    meta: {
      title: '系统管理',
      icon: 'shopping-cart',
    },
    children: [
      {
        path: 'menu',
        name: 'menu',
        component: () => import('@/views/sys/menu/index'),
        meta: {
          title: '菜单管理',
          noKeepAlive: true,
        },
        children: null,
      },
      {
        path: 'organize',
        name: 'organize',
        component: () => import('@/views/sys/organize/index'),
        meta: {
          title: '机构管理',
          noKeepAlive: true,
        },
        children: null,
      },
      {
        path: 'role',
        name: 'role',
        component: () => import('@/views/sys/role/index'),
        meta: {
          title: '角色管理',
          noKeepAlive: true,
        },
        children: null,
      },
      {
        path: 'post',
        name: 'post',
        component: () => import('@/views/sys/post/index'),
        meta: {
          title: '岗位管理',
          noKeepAlive: true,
        },
        children: null,
      },
      {
        path: 'admin',
        name: 'admin',
        component: () => import('@/views/sys/admin/index'),
        meta: {
          title: '用户管理',
          noKeepAlive: true,
        },
        children: null,
      },
      {
        path: 'authorize',
        name: 'authorize',
        component: () => import('@/views/sys/authorize/index'),
        meta: {
          title: '权限管理',
          noKeepAlive: true,
        },
        children: null,
      },
      {
        path: 'code',
        name: 'code',
        component: () => import('@/views/sys/code/index'),
        meta: {
          title: '字典管理',
          noKeepAlive: true,
        },
        children: null,
      },
      {
        path: 'logs',
        name: 'logs',
        component: () => import('@/views/sys/logs/index'),
        meta: {
          title: '日志管理',
          noKeepAlive: true,
        },
        children: null,
      },
    ],
  },
  {
    path: '*',
    redirect: '/404',
    hidden: true,
  },
]

const router = new VueRouter({
  base: routerMode === 'history' ? publicPath : '',
  mode: routerMode,
  scrollBehavior: () => ({
    y: 0,
  }),
  routes: constantRoutes,
})
//注释的地方是允许路由重复点击，如果你觉得框架路由跳转规范太过严格可选择放开
/* const originalPush = VueRouter.prototype.push;
VueRouter.prototype.push = function push(location, onResolve, onReject) {
  if (onResolve || onReject)
    return originalPush.call(this, location, onResolve, onReject);
  return originalPush.call(this, location).catch((err) => err);
}; */

export function resetRouter() {
  router.matcher = new VueRouter({
    base: routerMode === 'history' ? publicPath : '',
    mode: routerMode,
    scrollBehavior: () => ({
      y: 0,
    }),
    routes: constantRoutes,
  }).matcher
}

export default router
