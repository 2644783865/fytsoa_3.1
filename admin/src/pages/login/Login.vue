<template>
  <common-layout>
    <!-- bg effect -->
    <div id="bg">
      <canvas id="c1"></canvas>
      <canvas id="c2"></canvas>
      <canvas id="c3"></canvas>
    </div>
    <!-- //bg effect -->
    <div class="sub-main-w3">
      <a-form @submit="onSubmit" :form="form">
        <h2>登录FytSoa后台管理系统
          <i class="fas fa-level-down-alt"></i>
        </h2>
        <a-alert type="error" :closable="true" v-show="error" :message="error" showIcon style="margin-bottom: 24px;" />
        <div class="form-style-agile">
          <label>
            登录账号
          </label>
          <a-form-item>
            <a-input autocomplete="autocomplete" size="large" placeholder="admin"
              v-decorator="['name', {rules: [{ required: true, message: '请输入账户名', whitespace: true}]}]">
              <a-icon slot="prefix" type="user" />
            </a-input>
          </a-form-item>
        </div>
        <div class="form-style-agile">
          <label>
            登录密码
          </label>
          <a-form-item>
            <a-input size="large" placeholder="888888" autocomplete="autocomplete" type="password"
              v-decorator="['password', {rules: [{ required: true, message: '请输入密码', whitespace: true}]}]">
              <a-icon slot="prefix" type="lock" />
            </a-input>
          </a-form-item>
        </div>
        <a-button :loading="logging" style="width: 100%;margin-top: 15px" size="large" htmlType="submit" type="danger">
          登 录</a-button>
      </a-form>
    </div>
    <div class="footer">
      <p>Copyright &copy; 2020.Company FytSoa All rights reserved.</p>
    </div>
  </common-layout>
</template>

<script>
  import CommonLayout from '@/layouts/CommonLayout'
  import { login, getRoutesConfig } from '@/services/user'
  import { setAuthorization } from '@/utils/request'
  import { loadRoutes } from '@/utils/routerUtil'
  import { bgInit } from '@/utils/login-bg'
  import { mapMutations } from 'vuex'

  export default {
    name: 'Login',
    components: { CommonLayout },
    data() {
      return {
        logging: false,
        error: '',
        form: this.$form.createForm(this)
      }
    },
    computed: {
      systemName() {
        return this.$store.state.setting.systemName
      }
    },
    mounted: function () {
      bgInit();
      window.onresize = () => {
        bgInit();
      }
    },
    methods: {
      ...mapMutations('account', ['setUser', 'setPermissions', 'setRoles']),
      onSubmit(e) {
        e.preventDefault()
        this.form.validateFields((err) => {
          if (!err) {
            this.logging = true
            const name = this.form.getFieldValue('name')
            const password = this.form.getFieldValue('password')
            login(name, password).then(this.afterLogin)
          }
        })
      },
      afterLogin(res) {
        this.logging = false
        const loginRes = res.data
        if (loginRes.code >= 0) {
          const { user, permissions, roles } = loginRes.data
          this.setUser(user)
          this.setPermissions(permissions)
          this.setRoles(roles)
          setAuthorization({ token: loginRes.data.token, expireAt: new Date(loginRes.data.expireAt) })
          // 获取路由配置
          getRoutesConfig().then(result => {
            const routesConfig = result.data.data
            console.log(routesConfig);
            loadRoutes(routesConfig)
            this.$router.push('/dashboard/workplace')
            this.$message.success(loginRes.message, 3)
          })
        } else {
          this.error = loginRes.message
        }
      }
    }
  }
</script>

<style lang="less" scoped>
  .common-layout {
    background: #2196F3;

    .sub-main-w3 {
      display: -webkit-flex;
      display: -webkit-box;
      display: -moz-flex;
      display: -moz-box;
      display: -ms-flexbox;
      display: flex;
      align-items: center;
      -webkit-box-pack: center;
      -moz-box-pack: center;
      -ms-flex-pack: center;
      -webkit-justify-content: center;
      justify-content: center;
    }

    .sub-main-w3 form {
      max-width: 600px;
      margin: 0 5vw;
      background: rgba(10, 10, 10, 0.17);
      padding: 3.5vw;
      box-sizing: border-box;
      display: -webkit-flex;
      display: flex;
      flex-wrap: wrap;
      justify-content: center;
      border-bottom: 8px solid #f7296f;
      border-radius: 30px 30px 50px 50px;
      z-index: 1;
    }

    h2 {
      color: #fff;
      font-size: 22px;
      font-weight: 500;
      letter-spacing: 1px;
      text-transform: capitalize;
      margin-bottom: 1em;
    }

    .form-style-agile {
      margin-bottom: 1em;
      flex-basis: 100%;
      -webkit-flex-basis: 100%;
    }

    .sub-main-w3 label {
      font-size: 14px;
      color: #fff;
      display: inline-block;
      font-weight: 500;
      margin-bottom: 12px;
      text-transform: capitalize;
      letter-spacing: 1px;
    }

    .sub-main-w3 label i {
      font-size: 15px;
      margin-left: 5px;
      color: #f7296f;
      border-radius: 50%;
      line-height: 1.9;
      text-align: center;
    }

    .form-style-agile input[type="text"],
    .form-style-agile input[type="password"] {
      width: 100%;
      color: #000;
      outline: none;
      font-size: 14px;
      letter-spacing: 1px;
      padding: 15px 15px;
      box-sizing: border-box;
      border: none;
      border: 1px solid #000;
      background: #fff;
    }

    .sub-main-w3 input[type="submit"] {
      color: #fff;
      background: #f7296f;
      border: none;
      padding: 13px 0;
      margin-top: 30px;
      outline: none;
      width: 36%;
      font-size: 16px;
      cursor: pointer;
      letter-spacing: 2px;
      -webkit-transition: 0.5s all;
      -o-transition: 0.5s all;
      -moz-transition: 0.5s all;
      -ms-transition: 0.5s all;
      transition: 0.5s all;
      box-shadow: 2px 2px 6px rgba(0, 0, 0, 0.49);
    }

    .sub-main-w3 input[type="submit"]:hover {
      background: #000;
      -webkit-transition: 0.5s all;
      -o-transition: 0.5s all;
      -moz-transition: 0.5s all;
      -ms-transition: 0.5s all;
      transition: 0.5s all;
    }

    .wthree-text {
      width: 100%;
    }

    .wthree-text ul li:nth-child(1) {
      float: left;
    }

    .wthree-text ul li:nth-child(2) {
      float: right;
    }

    .wthree-text ul li {
      display: inline-block;
    }

    .wthree-text ul li a {
      color: #fff;
      font-size: 14px;
      letter-spacing: 1px;
      font-weight: 500;
    }

    /*-- checkbox --*/

    .wthree-text label {
      font-size: 15px;
      color: #fff;
      cursor: pointer;
      position: relative;
    }

    .wthree-text {
      text-align: center;
    }



    @keyframes rippling {
      50% {
        border-left-color: #fff;
      }

      100% {
        border-bottom-color: #fff;
        border-left-color: #fff;
      }
    }



    /*-- copyright --*/

    .footer {
      margin: 4vw .3vw 2vw;
      position: relative;
    }

    .footer p {
      font-size: 14px;
      color: #fff;
      letter-spacing: 2px;
      text-align: center;
      line-height: 1.8;
    }

    .footer p a {
      color: #fff;
      -webkit-transition: 0.5s all;
      -o-transition: 0.5s all;
      -moz-transition: 0.5s all;
      -ms-transition: 0.5s all;
      transition: 0.5s all;
    }

    .footer p a:hover {
      color: #f7296f;
      -webkit-transition: 0.5s all;
      -o-transition: 0.5s all;
      -moz-transition: 0.5s all;
      -ms-transition: 0.5s all;
      transition: 0.5s all;
    }

    /*-- //copyright --*/

    /*-- bg effect --*/

    #bg {
      position: fixed;
      top: 0;
      left: 0;
      width: 100%;
      height: 100%;
      z-index: 0;
    }

    #bg canvas {
      position: absolute;
      top: 0;
      left: 0;
      width: 100%;
      height: 100%;
    }

    /*-- //bg effect --*/

    /*--responsive--*/

    @media(max-width:1920px) {
      h1 {
        font-size: 3.5vw;
      }
    }

    @media(max-width:1024px) {
      h1 {
        font-size: 4.5vw;
      }
    }

    @media(max-width:800px) {
      h1 {
        font-size: 2.6em;
      }
    }

    @media(max-width:480px) {
      h1 {
        font-size: 2.3em;
        letter-spacing: 1px;
      }

      .sub-main-w3 form {
        padding: 7.5vw;
      }

      .footer p {
        letter-spacing: 1px;
      }
    }

    @media(max-width:414px) {

      .form-style-agile input[type="text"],
      .form-style-agile input[type="password"] {
        font-size: 13px;
        padding: 13px 15px;
      }

      .wthree-text ul li:nth-child(1),
      .wthree-text ul li:nth-child(2) {
        float: none;
      }

      .wthree-text ul li:nth-child(2) {
        margin-top: 10px;
      }

      .sub-main-w3 input[type="submit"] {
        width: 56%;
      }

      .wthree-text ul li {
        display: block;
      }
    }

    @media(max-width:320px) {
      h1 {
        font-size: 1.8em;
        margin: 5vw 1vw;
      }

      .sub-main-w3 form {
        padding: 25px 14px;
      }
    }

    /*--//responsive--*/
  }
</style>