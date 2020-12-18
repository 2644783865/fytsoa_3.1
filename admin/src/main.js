import Vue from 'vue'
import App from './App'
import store from './store'
import router from './router'
import './plugins'
import '@/layouts/export'
import iconPicker from 'e-icon-picker'
import 'e-icon-picker/dist/index.css'
Vue.use(iconPicker, { FontAwesome: false, ElementUI: true })
/**
 * @description 生产环境默认都使用mock，如果正式用于生产环境时，记得去掉
 */

if (process.env.NODE_ENV === 'production') {
  const { mockXHR } = require('@/utils/static')
  mockXHR()
}

Vue.config.productionTip = false

new Vue({
  el: '#vue-admin-beautiful',
  router,
  store,
  render: (h) => h(App),
})
