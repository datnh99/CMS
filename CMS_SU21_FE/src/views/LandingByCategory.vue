<template>
  <loading
    v-if="loading"
    v-model="loading"
    :can-cancel="true"
    loader="dots"
    :is-full-page="false"
    color="#FC5730"
  ></loading>
  <div v-else>
    <div class="position-relative profile-page">
      <!-- shape Hero -->
      <section class="section-shaped my-0">
        <div
          class="introduction-image bg-image"
          :style="`background-image: url('${categoryImage}')`"
        ></div>
        <!-- <span></span>
          <span></span>
          <span></span>
          <span></span>
          <span></span>
          <span></span>
          <span></span>
          <span></span>
          <span></span> -->

        <div class="container shape-container d-flex">
          <div class="col px-0">
            <div class="row">
              <div class="col-lg-6" style="margin-top: 100px">
                <h1 class="display-1 text-white category-name">
                  {{ category.categoryName }}
                </h1>
                <p class="lead text-white">
                  <!-- The design system comes with four pre-built pages to help you
                  get started faster. You can change the text and images and
                  you're good to go. -->
                </p>
              </div>
            </div>
          </div>
        </div>
      </section>
      <!-- 1st Hero Variation -->
    </div>
    <section class="section section-lg pt-lg-0 mt--200">
      <div class="container container-custom">
        <a-row :gutter="24" justify="space-between" align="bottom">
          <div v-for="(item, index) in listTopArticle" :key="item.id">
            <CardArticle
              :article="item"
              :weight="item.weight"
              :style="item.style"
            >
            </CardArticle>

            <a-col v-if="index === 1" :span="8" style="padding-top: 169px">
              <div
                class="border-0 m-b stories-wrapper"
                hover
                shadow
                style="height: 100%; background: white; border-radius: 12px"
              >
                <div class="stories-container">
                  <a-list
                    item-layout="vertical"
                    size="large"
                    :pagination="paginationArticle"
                    :data-source="listTopArticle"
                  >
                    <div style="border-left: 4px solid #fc5730" slot="header">
                      <h5
                        class="display-5"
                        style="padding-left: 9px; font-weight: 700"
                      >
                        TOP STORIES
                      </h5>
                    </div>

                    <a-list-item
                      v-for="item in listTopArticle"
                      slot="renderItem"
                      :key="item.id"
                      slot-scope="item, index"
                    >
                      <template slot="actions">
                        <span>
                          <a-icon
                            class="icon-flex"
                            type="eye"
                            style="margin-right: 8px"
                          />
                          {{ item.totalViews }}
                        </span>
                        <span>
                          <a-icon
                            class="icon-flex"
                            type="message"
                            style="margin-right: 8px"
                          />
                          {{ item.totalComment }}
                        </span>
                      </template>
                      <!-- <img
                          v-if="item.introductionImage"
                          slot="extra"
                          width="272"
                          alt="logo"
                          :src="articleImage + item.introductionImage"
                        /> -->
                      <!-- <img
                          v-else
                          slot="extra"
                          width="272"
                          alt="logo"
                          src="../../public/img/icons/common/default-image.jpg"
                        /> -->
                      <a-list-item-meta :description="item.sapo">
                        <a
                          style="font-weight: 700"
                          slot="title"
                          href="javascript:void(0)"
                          >{{ item.title }}</a
                        >
                        <!-- <a-avatar slot="avatar" :src="item.avatar" /> -->
                      </a-list-item-meta>
                      <!-- {{ item.content }} -->
                    </a-list-item>
                  </a-list>
                </div>
              </div>
            </a-col>
          </div>
        </a-row>
        <a-row :gutter="24">
          <CardArticle
            v-for="item in listNewArticle"
            :key="item.id"
            :article="item"
          />
        </a-row>
        <div class="row">
          <div class="col-12">
            <div class="load-more">
              <base-button
                rel="noopener"
                size="md"
                class="btn btn-neutral main-btn"
                v-if="showLoadmore"
                @click="loadingMode"
              >
                <a-icon :type="reloadIcon" style="display: inline-flex" />
                Load more
              </base-button>
            </div>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<script>
import CardArticle from "./components/CardArticle.vue";
import ArticleRepo from "../api/article.js";
import CategoryRepository from "../api/category";
import "vue-loading-overlay/dist/vue-loading.css";
import Loading from "vue-loading-overlay";
import CONFIG from "../config/index";
export default {
  name: "home",
  components: { Loading, CardArticle },
  data() {
    return {
      listTopArticle: [],
      listNewArticle: [],
      current: 1,
      relatedArticleImage: `${CONFIG.apiUrl}/image/getImage/{get-image}?id=`,
      loading: false,
      showLoadmore: true,
      categoryID: -1,
      category: {},
      categoryImage: `${CONFIG.apiUrl}/image/getImage/{get-image}?id=`,
      reloadIcon: "reload",
      lstArticleId: "",
      paginationArticle: {
        onChange: (page) => {},
        pageSize: 2,
      },
    };
  },
  props: {},
  watch: {
    "$route.params.categoryId": function () {
      this.categoryID = this.$route.params.categoryId;
      this.current = 1;
      this.listNewArticle = [];
      this.showLoadmore = true;
      this.categoryImage = `${CONFIG.apiUrl}/image/getImage/{get-image}?id=`;
      this.getCategoryById();
      this.getListTopArticle();
    },
  },
  mounted() {
    this.getListTopArticle();
  },
  created() {
    const { categoryId } = this.$route.params;
    if (categoryId) {
      this.categoryID = categoryId;
      this.getCategoryById();
    }
  },
  methods: {
    getCategoryById() {
      CategoryRepository.getCategoryById(this.categoryID).then((res) => {
        this.category = res.data.data;
        this.categoryImage += this.category.introductionImage;
      });
    },
    goToArticleDetail(id) {
      let routeData = this.$router.resolve({
        path: `/article-detail/${id}`,
      });
      window.open(routeData.href, "_blank");
    },
    offHome() {
      this.$emit("checkShowComponent", true);
    },
    doMath: function () {
      return index + 1;
    },
    generateTime(time) {
      switch (typeof time) {
        case "number":
          break;
        case "string":
          time = +new Date(time);
          break;
        case "object":
          if (time.constructor === Date) time = time.getTime();
          break;
        default:
          time = +new Date();
      }
      var time_formats = [
        [60, "seconds", 1], // 60
        [120, "1 minute ago", "1 minute from now"], // 60*2
        [3600, "minutes", 60], // 60*60, 60
        [7200, "1 hour ago", "1 hour from now"], // 60*60*2
        [86400, "hours", 3600], // 60*60*24, 60*60
        [172800, "Yesterday", "Tomorrow"], // 60*60*24*2
        [604800, "days", 86400], // 60*60*24*7, 60*60*24
        [1209600, "Last week", "Next week"], // 60*60*24*7*4*2
        [2419200, "weeks", 604800], // 60*60*24*7*4, 60*60*24*7
        [4838400, "Last month", "Next month"], // 60*60*24*7*4*2
        [29030400, "months", 2419200], // 60*60*24*7*4*12, 60*60*24*7*4
        [58060800, "Last year", "Next year"], // 60*60*24*7*4*12*2
        [2903040000, "years", 29030400], // 60*60*24*7*4*12*100, 60*60*24*7*4*12
        [5806080000, "Last century", "Next century"], // 60*60*24*7*4*12*100*2
        [58060800000, "centuries", 2903040000], // 60*60*24*7*4*12*100*20, 60*60*24*7*4*12*100
      ];
      var seconds = (+new Date() - time) / 1000,
        token = "ago",
        list_choice = 1;

      if (seconds == 0) {
        return "Just now";
      }
      if (seconds < 0) {
        seconds = Math.abs(seconds);
        token = "from now";
        list_choice = 2;
      }
      var i = 0,
        format;

      while ((format = time_formats[i++]))
        if (seconds < format[0]) {
          if (typeof format[2] == "string") return format[list_choice];
          else
            return (
              Math.floor(seconds / format[2]) + " " + format[1] + " " + token
            );
        }
      return time;
    },
    getListTopArticle() {
      this.listTopArticle = [];
      var data = {
        title: "",
        categoryID: this.categoryID,
        status: -1,
      };
      this.loading = true;
      ArticleRepo.searchLandingArticles(data, 1, 8).then((res) => {
        if (this.current * 8 >= res.data.data.totals) {
          this.showLoadmore = false;
        }
        let listTopArticle = res.data.data.items;
        if (listTopArticle) {
          this.lstArticleId = listTopArticle.map((art) => art.id).join();
          for (let index = 0; index < listTopArticle.length; index++) {
            let element = listTopArticle[index];
            let weight = 6;
            let style = "";
            switch (index) {
              case 0:
                weight = 6;
                style = "padding-top:296px";
                break;
              case 1:
                weight = 10;
                style = "padding-top:296px";
                break;
              case 2:
                weight = 12;
                break;
              case 3:
                weight = 6;
                break;
              case (4, 5, 6):
                weight = 6;
                break;
              case 7:
                weight = 12;
                break;
              case 8:
                weight = 12;
                break;
              default:
                break;
            }
            this.listTopArticle.push({
              id: element.id,
              content: element.content,
              approveDate: element.approveDate,
              author: element.author,
              categoryID: element.categoryID,
              categoryName: element.categoryName,
              createdTime: element.createdTime,
              deleteFlag: element.deleted,
              introductionImage: element.introductionImage,
              sapo: element.sapo,
              status: element.status,
              statusName: element.statusName,
              tagContent: element.tagContent,
              title: element.title,
              totalViews: element.totalViews,
              totalComment: element.totalComment,
              updateDate: element.modifiedTime,
              cared: element.followedUser,
              weight: weight,
              style: style,
            });
          }
        }
        this.loading = false;
      });
    },
    loadingMode() {
      this.current += 1;
      var data = {
        title: "",
        categoryID: this.categoryID,
        status: -1,
      };
      this.reloadIcon = "loading";
      ArticleRepo.searchLandingArticles(data, this.current, 8).then((res) => {
        if (this.current * 8 >= res.data.data.totals) {
          this.showLoadmore = false;
        }
        this.reloadIcon = "reload";
        res.data.data.items.forEach((element) => {
          this.listNewArticle.push({
            id: element.id,
            content: element.content,
            approveDate: element.approveDate,
            author: element.author,
            categoryID: element.categoryID,
            categoryName: element.categoryName,
            createdTime: element.createdTime,
            deleteFlag: element.deleted,
            introductionImage: element.introductionImage,
            sapo: element.sapo,
            status: element.status,
            statusName: element.statusName,
            tagContent: element.tagContent,
            title: element.title,
            totalViews: element.totalViews,
            totalComment: element.totalComment,
            modifiedTime: element.modifiedTime,
            cared: element.followedUser,
            displayName: element.firstName + " " + element.lastName,
          });
        });
      });
    },
  },
};
</script>
<style scoped>
.category-name {
  font-weight: 800;
}
.ant-list-item-meta-description {
  overflow: hidden;
  text-overflow: ellipsis;
  display: -webkit-box;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  text-overflow: unset;
}
.introduction-image {
  height: 500px;

  background: linear-gradient(
    83deg,
    rgba(131, 58, 180, 1) 0%,
    rgba(253, 29, 29, 1) 41%,
    rgba(252, 148, 69, 1) 100%
  );

  background-repeat: round;
}
.introduction-image:before {
  content: "";
  position: absolute;
  left: 0;
  top: 0;
  width: 100%;
  height: 500px;
  opacity: 0.7;
  background: 0 0;
  background: -moz-linear-gradient(
    legacy-direction(to left),
    transparent,
    #000
  );
  background: -webkit-linear-gradient(
    legacy-direction(to left),
    transparent,
    #000
  );
  background: linear-gradient(
    83deg,
    rgba(131, 58, 180, 1) 0%,
    rgba(253, 29, 29, 1) 41%,
    rgba(252, 148, 69, 1) 100%
  );
  z-index: 12;
}

.container-custom {
  max-width: 1600px !important;
}
.m-b {
  margin-bottom: 20px;
}
.load-more--button {
  cursor: pointer;
  border: none;
  outline: 0;
  -webkit-border-radius: 25px;
  border-radius: 25px;
  background-color: #22e6ba;
  color: #fff;
  padding: 12px 8px;
  min-width: 120px;
  text-align: center;
}
.load-more {
  text-align: center;
  margin: 40px 0 30px;
}

.ellip-line {
  display: inline-block;
  text-overflow: ellipsis;
  white-space: nowrap;
  word-wrap: normal;
  max-width: 100%;
  position: relative;
  overflow: hidden;
}
.stories-wrapper {
}
.stories-container {
  /* min-height: 666px; */
  -webkit-border-radius: 12px;
  border-radius: 12px;
  -webkit-box-shadow: 0 4px 8px 0 rgb(178 178 178 / 26%);
  -moz-box-shadow: 0 4px 8px 0 rgba(178, 178, 178, 0.26);
  -ms-box-shadow: 0 4px 8px 0 rgba(178, 178, 178, 0.26);
  box-shadow: 0 4px 8px 0 rgb(178 178 178 / 26%);
  border: 1px solid rgba(178, 178, 178, 0.1);
  /* margin-bottom: 25px; */
  padding: 16px;
}
.stories-container--list {
  font-weight: 300;
  line-height: 1.4;
  letter-spacing: -0.0135em;
  list-style: none;
}
.stories-container--item {
  padding: 41px 0 5px;
  display: block;
}
.stories-container--head-text {
  font-size: 1.125rem;
  line-height: 1.35rem;
  font-weight: 300;
  line-height: 1.28;
  letter-spacing: 5px;
  color: #000;
  text-transform: uppercase;
}
.stories-container--item-label {
  color: #000;
  font-size: 0.875rem;
  line-height: 1.05rem;
  line-height: 18px;
  font-weight: 600;
}
.stories-container--item-label:hover {
  color: #fc5730 !important;
}

.bg-image {
  pointer-events: none;
  width: 100%;
  /* height: 130%; */
  position: absolute;
  z-index: -1;
}
</style>
<style>
.card-article .card-body:hover {
  -webkit-box-shadow: 0 7px 12px 7px rgba(0, 0, 0, 0.2);
  -moz-box-shadow: 0 7px 12px 7px rgba(0, 0, 0, 0.2);
  -ms-box-shadow: 0 7px 12px 7px rgba(0, 0, 0, 0.2);
  box-shadow: 0 7px 12px 7px rgba(0, 0, 0, 0.2);
}
</style>