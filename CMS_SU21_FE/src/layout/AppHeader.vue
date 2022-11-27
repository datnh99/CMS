<template>
  <div>
    <div class="container"> <header class="header-global">
      <div class="container navigation-main">
        <div class="header-top">
          <div class="header-left">
            <div class="">
              <!-- <base-button @click="showDrawer" outline type="secondary">
                <a-icon :fontSize="40" type="menu" />
              </base-button> -->
            </div>
            <div class="logo">
              <router-link class="navbar-brand" to="/landing">
                <img src="../../public/img/brand/logo cms (1)-02 (1).png" alt="logo" />
              </router-link>
            </div>

            <div class="search">
              <a-input-search
                class="main-search"
                v-model="searchKey"
                placeholder="Search..."
                @keyup.enter="goToSearchArticle()"
                @search="goToSearchArticle()"
              />
            </div>
          </div>

          <div class="header-right">
            <div class="search">
              <a-input-search
                class="main-search"
                v-model="searchKey"
                placeholder="Search..."
                @keyup.enter="goToSearchArticle()"
                @search="goToSearchArticle()"
              />
            </div>
            <div class="article">
              <base-button
                rel="noopener"
                size="sm"
                class="btn btn-neutral btn-icon main-btn"
                v-if="checkDisplayMenu(screenCode.NEW_ARTICLE)"
                @click="$router.push({ name: 'NewArticle' })"
              >
                <span class="btn-inner--icon">
                  <i class="fa fa-cloud-upload mr-2"></i>
                </span>
                <span class="nav-link-inner--text">New Article</span>
              </base-button>
            </div>
            <!-- <div class="article">
              <base-button
                data-toggle="dropdown"
                size="sm"
                class="btn btn-neutral btn-icon main-btn"
                @click="$router.push('/profile')"
              >
                <i class="ni ni-collection d-lg-none"></i>
                <span class="btn-inner--icon">
                  <img
                    v-lazy="'img/theme/team-2-800x800.jpg'"
                    alt="Circle image"
                    class="img-fluid rounded-circle shadow"
                    style="width: 20px"
                  />
                </span>
                <span class="nav-link-inner--text">{{ username }}</span>
              </base-button>
            </div> -->
            <div class="article">
              <base-button
                rel="noopener"
                size="sm"
                class="btn btn-neutral btn-icon main-btn"
                v-if="checkDisplayMenu(screenCode.ADMIN_ACTION)"
                @click="$router.push({ name: 'AdminAction' })"
              >
                <span class="btn-inner--icon">
                  <i class="fa fa-address-card mr-2"></i>
                </span>
                <span class="nav-link-inner--text">ADMIN ACTION</span>
              </base-button>
            </div>
            <div class="article">
              <base-button
                rel="noopener"
                size="sm"
                class="btn btn-icon main-btn"
                @click="processLogOut()"
              >
        
                <span class="nav-link-inner--text"> <a-icon type="logout" style="display:inline-flex"/> Sign out</span>
              </base-button>
            </div>
                  <router-link class="navbar-brand" to="/landing">
                <img style="height:41px;padding-left:10px" src="../../public/img/icons/common/2020-FPTU-Eng.png" alt="logo" />
              </router-link>
          </div>
        </div>
      </div>
    </header></div>
   
    <a-affix :offset-top="0" >
      <div class="container" style="  max-width: 1550px;">
      <vue-navigation-bar :options="navbarOptions">
        <template v-if="listCategoryExpanded.length > 0" slot="custom-section">
          <div class="vnb__menu-options__option">
            <a
              href="javascript:void(0)"
              @click="openExpandCategory()"
              class="vnb__menu-options__option__link"
              aria-label="dasfqwdqscad"
              tabindex="0"
              >
              More
              <a-icon
                :style="{ fontSize: '20px', color: expanded }"
                type="caret-down"
              />
            </a
            >
          </div>
        
        </template>
      </vue-navigation-bar>
      </div>
      <div
        class="container mega-menu"
        v-if="mega"
        :class="{ displayMega: mega }"
        style="  max-width: 1550px;"
      >
        <div
          class="animate__animated animate__bounce"
          style="flex-wrap: nowrap"
        >
          <div
            class="vnb__menu-options vnb__menu-options--left"
            style="padding-bottom: 12px"
          >
            <div
              v-for="item in listCategoryExpanded"
              :key="item.id"
              class="vnb__menu-options__option"
            >
              <a
                :href="`#/landing-by-category/${item.id}`"
                :class="`vnb__menu-options__option__link`"
                aria-label="dasfqwdqscad"
                tabindex="0"
                >
                {{ item.categoryName }}
               </a
              >
            </div>
          </div>
        </div>
      </div>
    </a-affix>

    <!-- <a-drawer
      title="Categories"
      width="300"
      :closable="false"
      placement="left"
      :visible="visibleDrawer"
      @close="onClose"
    >
      <a
        v-for="item in listCategory"
        class="categories-item"
        :key="item.CategoryID"
        style="margin-top: 10px"
        href="javascript:void(0)"
        @click="goToLandingByCategory(item.CategoryID)"
      >
        <h5>{{ item.CategoryName }}</h5>
      </a>
    </a-drawer> -->
    <!-- <div
      :style="{
        position: 'absolute',
        bottom: 0,
        width: '100%',
        borderTop: '1px solid #e8e8e8',
        padding: '10px 16px',
        textAlign: 'right',
        left: 0,
        background: '#fff',
        borderRadius: '0 0 4px 4px',
      }"
    >
      <a-button style="marginright: 8px" @click="onClose"> Cancel </a-button>
      <a-button type="primary" @click="onClose"> Submit </a-button>
    </div> -->
  </div>
</template>
<script>
import "vue-navigation-bar/dist/vue-navigation-bar.css";

import Vue from "vue";
import BaseNav from "@/components/BaseNav";
import BaseDropdown from "@/components/BaseDropdown";
import CloseButton from "@/components/CloseButton";
import BaseButton from "../components/BaseButton.vue";
import CategoryRepository from "../api/category";
import { menuAccessPemission } from "../api/menu";
import { processLogout } from "../api/processLogin";
import CONFIG from "../config/index";
import "animate.css";
export default {
  components: {
    BaseNav,
    CloseButton,
    BaseDropdown,
    BaseButton,
  },
  data() {
    return {
      expanded: "#595959",
      mega: false,
      visibleDrawer: false,
      childrenDrawer: false,
      listCategory: [],
      listCategoryExpanded: [],

      username: Vue.$cookies.get("account"),
      screenCode: CONFIG.SCREEN_CODE,
      searchKey: "",
      navbarOptions: {
        elementId: "main-navbar",
        isUsingVueRouter: true,
        mobileBreakpoint: 992,
        brandImagePath: "./",
        // brandImage: require("../../public/img/brand/logo.png"),
        brandImageAltText: "brand-image",
        collapseButtonOpenColor: "#661c23",
        collapseButtonCloseColor: "#661c23",
        showBrandImageInMobilePopup: true,
        ariaLabelMainNav: "Main Navigation",
        tooltipAnimationType: "shift-away",
        tooltipPlacement: "bottom",
        menuOptionsLeft: [
          // {
          //   type: "link",
          //   text: "Why Dunder Mifflin",
          //   arrowColor: "#659CC8",
          //   subMenuOptions: [
          //     {
          //       isLinkAction: true,
          //       type: "link",
          //       text: "About",
          //       subText:
          //         "Stupid corporate wet blankets. Like booze ever killed anyone.",
          //       path: { name: "about" },
          //       iconLeft: '<i class="fa fa-star fa-fw"></i>',
          //     },
          //     {
          //       type: "hr",
          //     },
          //     {
          //       type: "link",
          //       text: "Locations",
          //       subText: "You're a presentation tool!",
          //       path: { name: "locations" },
          //       arrowColor: "#659CC8",
          //     },
          //     {
          //       type: "hr",
          //     },
          //     {
          //       type: "link",
          //       text: "Blog",
          //       subText:
          //         "I enjoy having breakfast in bed. I like waking up to the smell of bacon. Sue me.",
          //       path: { name: "blog" },
          //     },
          //   ],
          // },
          // {
          //   type: "link",
          //   text: "Contact",
          //   subMenuOptions: [
          //     {
          //       type: "link",
          //       text: "Customer Service",
          //       path: { name: "customer-service" },
          //     },
          //     {
          //       type: "link",
          //       text: "Accounting",
          //       path: { name: "accounting" },
          //     },
          //     {
          //       type: "hr",
          //     },
          //     {
          //       type: "link",
          //       text: "Reception",
          //       path: { name: "reception" },
          //       iconLeft:
          //         '<svg id="i-telephone" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 32 32" width="32" height="32" fill="none" stroke="currentcolor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"> <path d="M3 12 C3 5 10 5 16 5 22 5 29 5 29 12 29 20 22 11 22 11 L10 11 C10 11 3 20 3 12 Z M11 14 C11 14 6 19 6 28 L26 28 C26 19 21 14 21 14 L11 14 Z" /> <circle cx="16" cy="21" r="4" /> </svg>',
          //     },
          //   ],
          // },
          {
            type: "link",
            path: "/landing",
            iconRight: '<i class="fa fa-home fa-lg"></i>',
          },
        ],
        menuOptionsRight: [
          {
            text: Vue.$cookies.get("displayName").toUpperCase(),
            type: "link",
            path: "/profile",
            iconLeft: '<i class="fa fa-user fa-lg"></i>',
          },
        ],
      },
    };
  },
  created() {
    this.fetchCategory();
  },
  methods: {
    openExpandCategory() {
      if (this.mega) {
        this.expanded = "#595959";
        this.mega = false;
      } else {
        this.expanded = "#FC5730";
        this.mega = true;
      }
    },
    goToSearchArticle() {
      this.$router.push(`/search/${this.searchKey}`);
      // window.location.href = `${CONFIG.CLIENT_URL}/#/search/${this.searchKey}`
      if (window.location.href.includes("/search")) {
        window.location.reload();
      }
    },
    goToLandingByCategory(id) {
      this.$router.push(`/landing-by-category/${id}`);
      if (window.location.href.includes("/landing-by-category")) {
        window.location.reload();
      }
    },
    checkDisplayMenu(screenCode) {
      return menuAccessPemission(screenCode);
    },
    showDrawer() {
      this.visibleDrawer = true;
    },
    fetchCategory() {
      CategoryRepository.getAllCategory().then((res) => {
        this.listCategory = res.data.data.items.slice(0, 11);
        if (res.data.data.items.length > 11) {
          this.listCategoryExpanded = res.data.data.items.slice(
            11,
            res.data.data.items.length + 1
          );
        }
        this.listCategory.forEach((category) => {
          const menuItem = {
            type: "link",
            text: category.categoryName,
            path: `/landing-by-category/${category.id}`,
            class: "view-by-category",
            // iconRight: '<i class="fa fa-long-arrow-right fa-fw"></i>',
          };
          this.navbarOptions.menuOptionsLeft.push(menuItem);
        });
        // this.navbarOptions.menuOptionsLeft.push({
        //   type: "link",
        //   path: `landing`,
        //   iconRight:
        //     '<i style ="color: #595959" class="fa fa-server fa-lg"></i>',
        // });
      });
    },
    onClose() {
      this.visibleDrawer = false;
    },
    showChildrenDrawer() {
      this.childrenDrawer = true;
    },
    onChildrenDrawerClose() {
      this.childrenDrawer = false;
    },
    async processLogOut() {
      await processLogout();
      this.$router.push("/login");
    },
  },
};
</script>
<style lang="scss" scoped>

.container {
    max-width: 1300px;
}

$tl: 0.6s; // transition length
.mega-menu {
  background: #fff;
}
body {
  margin: 5%;
}
.main-search {
  width: 200px;
}

.search-box {
  font-family: Arial, Helvetica, sans-serif;
  float: right;
  transition: width $tl, border-radius $tl, background $tl, box-shadow $tl;
  width: 40px;
  height: 40px;
  border-radius: 20px;
  border: none;
  cursor: pointer;
  background: rgb(235, 235, 235);
  & + label .search-icon {
    color: black;
  }
  &:hover {
    color: white;
    background: rgb(200, 200, 200);
    box-shadow: 0 0 0 5px rgb(61, 71, 82);
    & + label .search-icon {
      color: white;
    }
  }
  &:focus {
    transition: width $tl cubic-bezier(0, 1.22, 0.66, 1.39), border-radius $tl,
      background $tl;
    border: none;
    outline: none;
    box-shadow: none;
    padding-left: 15px;
    cursor: text;
    width: 90%;
    border-radius: auto;
    background: rgb(235, 235, 235);
    color: black;
    & + label .search-icon {
      color: black;
    }
  }
  &:not(:focus) {
    text-indent: -5000px;
  } // for more-graceful falling back (:not browsers likely support indent)
}

#search-submit {
  position: relative;
  left: -5000px;
}

.search-icon {
  position: relative;
  left: -30px;
  color: white;
  cursor: pointer;
}
</style>
<style scoped>

.displayMega a:hover {
  text-decoration: none;
  padding-bottom: 0;
  border-bottom: 0;
}

.a:hover {
  color: white !important;
}
.categories-item h5 {
  padding-left: 0.1rem;
  /* border-left: 3px solid #5e72e4; */
  padding-bottom: 0 !important;
  border-bottom: 0;
}
.categories-item :hover {
  color: #FC5730;

  padding-left: 0.1rem;
  /* border-left: 3px solid #5e72e4; */
  padding-left: 5px !important;
  border-bottom: 0;
}
.header-top {
  /* background: #e6e6e6; */
  padding-top: 10px;
  margin-top: -11px;
  /* margin-bottom: 11px; */
  padding-bottom: 10px;
}
.navigation-main {
  padding-top: 15px;
  max-width: 100%;
  background-color: white;
  /* border-bottom: 1px solid #FC5730; */
}
</style>
<style>
.ant-affix{
background: #fff;
}
.main-btn {
  border-radius: 25px !important;
}
.view-by-category :hover {
  color: #595959 !important;
}
.view-more-category {
  color: #595959 !important;
}
.vnb__menu-options--left {
  margin-right: 20px;
  padding-left: 70px;
}
.vnb__menu-options__option :hover{
  color: #FC5730 !important;
}
.main-search > input {
  border-radius: 25px !important;
}
.ant-drawer-title {
  font-size: 24px !important;
}

.ant-menu-item-selected,
.ant-menu-item-active {
  color: white !important;
  background-color: #FC5730;
}
.router-link-active {
  color: #FC5730 !important;
}
.router-link-active:hover {
  color: white !important;
}
.ant-menu-submenu-selected {
  color: #aca2a2 !important;
}
.ant-menu-item > a:hover {
  color: white !important;
}
.ant-menu-submenu-active,
.ant-menu-submenu-title:hover {
  color: #FC5730 !important;
  border-bottom: none !important;
}
.ant-menu-inline .ant-menu-item::after {
  border-right: 3px solid #FC5730;
}
.submenu-title-wrapper {
  color: white;
}
.ant-menu-submenu-arrow::before {
  background: linear-gradient(to right, #FC5730, #FC5730) !important;
}
.ant-menu-submenu-arrow::after {
  background: linear-gradient(to right, #FC5730, #FC5730) !important;
}
.a {
  color: #FC5730;
}

/* header-top */
.header-top {
  display: inline-block;
  width: 100%;
  /* padding: 0 100px; */
}

.header-left {
  display: flex;
  float: left;
  width: 30%;
}
.header-left .search {
  display: none;
}

.header-right {
  padding-top: 15px;
  display: flex;
  float: right;
}
.logo {
}
.search {
  padding-top: 5px;
  height: 40px;
}

.search-box:focus {
  margin: 0px;
  height: 40px;
}

.article {
  margin: auto 0px auto 20px;
}

/* Extra small devices (portrait phones, less than 576px) */
@media (max-width: 575.98px) {
  .header-top {
    padding: 10px 10px 0 10px !important;
  }

  .header-left {
    width: 100%;
  }

  .header-left .search {
    display: flex;
  }

  .header-right {
    width: 100%;
  }
  .header-right .search {
    display: none;
  }

  .article {
    margin: auto 0px auto 50px;
  }

  .menu {
    margin-left: 20px;
  }
}
/* Small devices (landscape phones, 576px and up) */
@media (min-width: 576px) and (max-width: 767.98px) {
  .header-top {
    padding: 10px 10px 0 10px !important;
  }
}

@media (min-width: 768px) and (max-width: 991.98px) {
  .header-top {
    padding: 10px 10px 0 30px !important;
  }
}
/* Large devices (desktops, 992px and up) */
@media (min-width: 992px) and (max-width: 1199.98px) {
}

/* Extra large devices (large desktops, 1200px and up) */
@media (min-width: 1200px) {
}
</style>
