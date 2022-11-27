<template>
  <a-layout id="components-layout-demo-side" style="min-height: 100vh">
    <a-layout-sider v-model="collapsed" collapsible>
      <div class="logo" />

      <a-menu
        v-if="menuDisplayed"
        theme="light"
        :default-selected-keys="componentName"
        :default-open-keys="componentName"
        mode="vertical"
        v-model="componentName"
      >
        <a-menu-item key="1" @click="backToUserView()">
          <a-icon type="export" /> <span>Back to user's view</span>
        </a-menu-item>

        <a-menu-item
          v-if="checkDisplayMenu(screenCode.NEW_ARTICLE)"
          key="NewArticle"
        >
          <a-icon type="file-add" /> <span>New Article</span>
        </a-menu-item>
        <a-menu-item
          v-if="checkDisplayMenu(screenCode.MY_ARTICLE)"
          key="MyArticle"
        >
          <a-icon type="solution" /> <span>My Article</span>
        </a-menu-item>
        <a-menu-item
          v-if="checkDisplayMenu(screenCode.REVIEW_ARTICLE)"
          key="ReviewArticle"
        >
          <a-icon type="edit" /> <span>Review Article</span>
        </a-menu-item>
        <a-menu-item
          v-if="checkDisplayMenu(screenCode.ROLE_MANAGEMENT)"
          key="UserRoleManagerment"
        >
          <a-icon type="solution" /> <span>User Role Management</span>
        </a-menu-item>
        <a-menu-item
          v-if="checkDisplayMenu(screenCode.CATEGORY_MANAGEMENT)"
          key="Categories"
        >
          <a-icon type="appstore" /> <span>Category Management</span>
        </a-menu-item>
        <a-menu-item
          v-if="checkDisplayMenu(screenCode.ARTICLE_MANAGEMENT)"
          key="ArticleManagement"
        >
          <a-icon type="profile" /> <span>Article Management</span>
        </a-menu-item>
        <a-menu-item
          v-if="checkDisplayMenu(screenCode.DATA_REPORT)"
          key="DataReport"
        >
          <a-icon type="project" /> <span>Data Report</span>
        </a-menu-item>
        <a-menu-item
          v-if="checkDisplayMenu(screenCode.SYSTEM_CONFIG)"
          key="SystemConfig"
        >
          <a-icon type="tool" /> <span>System Configuration</span>
        </a-menu-item>
        <a-menu-item key="LogOut" @click="processLogout">
          <a-icon type="logout" /> <span>Log out</span>
        </a-menu-item>
      </a-menu>
    </a-layout-sider>
    <a-layout>
      <div :style="{ minHeight: '600px' }">
        <transition name="fade" mode="out-in" appear>
          <component
            v-if="
              menuDisplayed &&
              componentName[0] !== 'EditArticle' &&
              componentName[0] !== 'EditArticleByReview'
            "
            v-bind:is="componentName[0]"
          ></component>
        </transition>

        <NewArticle
          v-if="menuDisplayed && componentName[0] === 'EditArticle'"
          :id="$route.params.id"
          :artManage="artManage"
          @re-load="reLoad"
        />
        <EditArticleByReview
          v-if="menuDisplayed && componentName[0] === 'EditArticleByReview'"
          :id="$route.params.id"
          @re-load="reLoad"
        />
      </div>
      <a-layout-footer style="text-align: center">
        FUSU ©2021 Created by 89AE
      </a-layout-footer>
    </a-layout>
  </a-layout>
</template>
<script>
import CONFIG from "../config/index";
import Categories from "./Category.vue";
import ArticleManagement from "./ArticleManagement.vue";
import ReviewArticle from "./ReviewArticle.vue";
import UserRoleManagerment from "./UserRoleManagerment.vue";
import MyArticle from "./MyArticle.vue";
import NewArticle from "./NewArticle.vue";
import DataReport from "./DataReport.vue";
import EditArticleByReview from "./EditArticleByReview.vue";
import SystemConfig from "./SystemConfig.vue";
import { processLogout } from "../api/processLogin";
import { menuAccessPemission } from "../api/menu";
export default {
  components: {
    Categories: Categories,
    ReviewArticle: ReviewArticle,
    UserRoleManagerment: UserRoleManagerment,
    MyArticle: MyArticle,
    NewArticle: NewArticle,
    DataReport: DataReport,
    ArticleManagement: ArticleManagement,
    EditArticleByReview: EditArticleByReview,
    SystemConfig: SystemConfig,
  },
  name: "404",
  data() {
    return {
      clientUrl: CONFIG.CLIENT_URL,
      collapsed: false,
      componentName: [],
      menuDisplayed: true,
      screenCode: CONFIG.SCREEN_CODE,
      userRole: "",
      extendedComponent: false,
      artManage: false,
    };
  },
  created() {
    this.setDefaultAction();
  },
  watch: {
    componentName(value) {
      if (!this.componentName.includes("EditArticle")) {
        if (window.location.href.includes("/admin-action/EditArticle")) {
          window.location.href = `${CONFIG.CLIENT_URL}/#/admin-action`;
        }
      }
      if (this.componentName.includes("LogOut")) {
        this.componentName = [];
      }
    },
    "$route.params.defaultComponent": function () {
      if (window.location.href.includes("/admin-action/MyArticle")) {
        window.location.href = `${CONFIG.CLIENT_URL}/#/admin-action`;
        this.componentName = [];
        this.componentName.push("MyArticle");
      }
    },
  },
  methods: {
    reLoad(value) {
      console.log("value ===>", value);
      this.componentName = [];
      this.componentName[0] = value;
    },
    setDefaultAction() {
      let defaultComponent = this.$route.params.defaultComponent;
      if (!defaultComponent) {
        this.userRole = this.$cookies.get("roleCode");
        if (this.userRole === "super_admin") {
          defaultComponent = "UserRoleManagerment";
        } else if (this.userRole === "moderator") {
          defaultComponent = "ReviewArticle";
        } else if (this.userRole === "editor") {
          defaultComponent = "ArticleManagement";
        } else {
          defaultComponent = "NewArticle";
        }
      } else {
        this.extendedComponent = true;
        if (this.$route.params.artManage) {
          this.artManage = this.$route.params.artManage;
        }
      }
      this.componentName.push(defaultComponent);
    },
    processLogout() {
      processLogout();
      setTimeout(() => {
        this.$router.push("/login");
      }, 3000);
    },
    checkDisplayMenu(screenCode) {
      return menuAccessPemission(screenCode);
    },
    backToUserView() {
      this.menuDisplayed = false;
      this.extendedComponent = false;
      this.$router.push("/landing");
    },
  },
};
</script>

<style scoped>
.ant-menu-item-selected {
  background: #fc5730 !important;
}
.ant-layout-sider {
  background: #fff !important;
}
</style>
<style >
.ant-layout-sider-trigger {
  background: #fc5730 !important;
}
</style>