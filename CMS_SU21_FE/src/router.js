import Vue from "vue";
import Router from "vue-router";
import AppHeader from "./layout/AppHeader";
import AppFooter from "./layout/AppFooter";
import Components from "./views/Components.vue";
import Landing from "./views/Landing.vue";
import Login from "./views/Login.vue";
import Register from "./views/Register.vue";
import Profile from "./views/Profile.vue";
import MyArticle from "./views/MyArticle.vue";
import ArticleManagement from "./views/ArticleManagement.vue";
import ReviewArticle from "./views/ReviewArticle.vue";
import Category from "./views/Category.vue";
import UserRoleManagerment from "./views/UserRoleManagerment.vue";
import NewArticle from "./views/NewArticle.vue";
import EditArticleByReviewer from "./views/EditArticleByReview.vue";
import ArticleDetail from "./views/ArticleDetail.vue";
import OriginalArticleDetail from "./views/OriginalArticleDetail.vue";
import Forbidden from "./views/403.vue";
import NotFound from "./views/404.vue";
import AdminAction from "./views/AdminAction.vue";
import SearchArticle from "./views/SearchArticle.vue";
import LandingByCategory from "./views/LandingByCategory.vue"
import { checkLogin, checkPermission } from "./api/processLogin";
import CONFIG from "./config/index";
import { preloaderFinished } from "./config/preloader";
import VueScrollTo from "vue-scrollto";

import { includes } from "lodash";

Vue.use(Router);

const router = new Router({
  linkExactActiveClass: "active",
  routes: [
    // {
    //   path: "/",
    //   name: "components",
    //   components: {
    //     header: AppHeader,
    //     default: Components,
    //     footer: AppFooter,
    //   },
    // },
    {
      path: "/landing",
      name: "landing",
      components: {
        header: AppHeader,
        default: Landing,
        footer: AppFooter,
      },
      beforeEnter: async (to, from, next) => {
        await checkLogin(next);
      },
    },
    {
      path: "/landing-by-category/:categoryId",
      name: "LandingByCategory",
      components: {
        header: AppHeader,
        default: LandingByCategory,
        footer: AppFooter,
      },
      beforeEnter: async (to, from, next) => {
        await checkLogin(next);
      },
    },
    {
      path: "/search/:searchKey?",
      name: "search",
      components: {
        header: AppHeader,
        default: SearchArticle,
        footer: AppFooter,
      },
      beforeEnter: async (to, from, next) => {
        await checkLogin(next);
      },
    },
  
    {
      path: "/login/:shareId?",
      name: "login",
      components: {
        // header: AppHeader,
        default: Login,
      },
    },
    {
      path: "/register",
      name: "Register",
      components: {
        // header: AppHeader,
        default: Register,
        footer: AppFooter,
      },
    },
    {
      path: "/",
      name: "Landingdf",
      components: {
        header: AppHeader,
        default: Landing,
        footer: AppFooter,
      },
      beforeEnter: async (to, from, next) => {
        await checkLogin(next);
      },
    },
    {
      path: "/profile",
      name: "profile",
      components: {
        header: AppHeader,
        default: Profile,
        footer: AppFooter,
      },
      beforeEnter: async (to, from, next) => {
        await checkLogin(next);
      },
    },
    {
      path: "/my-article",
      name: "MyArticle",
      components: {
        header: AppHeader,
        default: MyArticle,
        footer: AppFooter,
      },
      beforeEnter: async (to, from, next) => {
        await checkLogin(next);
      },
    },
    {
      path: "/article-management",
      name: "ArticleManagement",
      components: {
        header: AppHeader,
        default: ArticleManagement,
        footer: AppFooter,
      },
      beforeEnter: async (to, from, next) => {
        await checkLogin(next);
      },
    },
    {
      path: "/review-article",
      name: "ReviewArticle",
      components: {
        header: AppHeader,
        default: ReviewArticle,
        footer: AppFooter,
      },
      beforeEnter: async (to, from, next) => {
        await checkLogin(next, CONFIG.SCREEN_CODE.REVIEW_ARTICLE);
      },
    },
    {
      path: "/category-management",
      name: "Category",
      components: {
        header: AppHeader,
        default: Category,
        footer: AppFooter,
      },
      beforeEnter: async (to, from, next) => {
        await checkLogin(next, CONFIG.SCREEN_CODE.CATEGORY_MANAGEMENT);
      },
    },
    {
      path: "/role-management",
      name: "UserRoleManagerment",
      components: {
        header: AppHeader,
        default: UserRoleManagerment,
        footer: AppFooter,
      },
      beforeEnter: async (to, from, next) => {
        await checkLogin(next, CONFIG.SCREEN_CODE.ROLE_MANAGEMENT);
      },
    },
    {
      path: "/new-article",
      name: "NewArticle",
      components: {
        header: AppHeader,
        default: NewArticle,
        footer: AppFooter,
      },
      props: { showSelectSpace: false },
      beforeEnter: async (to, from, next) => {
        await checkLogin(next, CONFIG.SCREEN_CODE.NEW_ARTICLE);
      },
    },
    {
      path: "/edit-article/:id/:artManagement?",
      name: "EditArticle",
      components: {
        header: AppHeader,
        default: NewArticle,
        footer: AppFooter,
      },
      props: { showSelectSpace: false },
      beforeEnter: async (to, from, next) => {
        await checkLogin(next, CONFIG.SCREEN_CODE.NEW_ARTICLE);
      },
    },
    {
      path: "/edit-article-by-reviewer/:id",
      name: "EditArticleByReviewer",
      components: {
        header: AppHeader,
        default: EditArticleByReviewer,
        footer: AppFooter,
      },
      props: { showSelectSpace: false },
      beforeEnter: async (to, from, next) => {
        await checkLogin(next, CONFIG.SCREEN_CODE.REVIEW_ARTICLE);
      },
    },
    {
      path: "/article-detail/:id",
      name: "ArticleDetail",
      components: {
        header: AppHeader,
        default: ArticleDetail,
        footer: AppFooter,
      },
      beforeEnter: async (to, from, next) => {
        await checkLogin(next);
      },
    },
    
    {
      path: "/original-article-detail/:id",
      name: "OriginalArticleDetail",
      components: {
        header: AppHeader,
        default: OriginalArticleDetail,
        footer: AppFooter,
      },
      beforeEnter: async (to, from, next) => {
        await checkLogin(next, CONFIG.SCREEN_CODE.ORIGINAL_ARTICLE_DETAIL);
      },
    },
    {
      path: "/forbidden",
      name: "Forbidden",
      components: {
        default: Forbidden,
      },
    },
    {
      path: "/admin-action/:defaultComponent?/:id?/:artManage?",
      name: "AdminAction",
      components: {
        default: AdminAction,
      },
      beforeEnter: async (to, from, next) => {
        await checkLogin(next, CONFIG.SCREEN_CODE.ADMIN_ACTION);
      },
    },
    {
      path: "/not-found",
      name: "NotFound",
      components: {
        default: NotFound,
      },
    },
  ],
  scrollBehavior: (to) => {
    if (to.hash) {
      return { selector: to.hash };
    } else {
      return { x: 0, y: 0 };
    }
  },
});

const notAuthRoutes = ["login", "Forbidden", "NotFound"];
router.afterEach((to) => {
  preloaderFinished();

  // return
  if (to) {
    const token = Vue.$cookies.isKey("accessToken");
    if (!includes(notAuthRoutes, to.name) && !token) {
      // return window.location.href = `${CONFIG.LOGIN_URL}${encodeURIComponent(window.location.href)}`;
      return (window.location.href = `${CONFIG.LOGIN_URL}/#/login`);
    }else if(to.name === "login" && token && to.params.shareId){
      return (window.location.href = `${CONFIG.CLIENT_URL}/#/article-detail/${window.atob(to.params.shareId)}`);

      // return router.push('/article-detail'+to.params.shareId);
    } else if (to.name === "login" && token) {
      return router.push({ name: "landing" });
    }

    if (CONFIG["PERMISSION_SCREEN_MAP"][to.name]) {
      const objectTypeCode = CONFIG["PERMISSION_SCREEN_MAP"][to.name];
      if (!checkPermission(objectTypeCode, "VIEW")) {
        return router.push({ name: "Forbidden" });
      }
    }
  }
  VueScrollTo.scrollTo("body");
});

export default router;
