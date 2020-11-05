import Mock from 'mockjs'
import '@/mock/user/current'
import '@/mock/user/routes'
import '@/mock/user/login'
import '@/mock/user/routes'
import '@/mock/project'
import '@/mock/workplace'

// 设置全局延时
Mock.setup({
  timeout: '300-600'
})
