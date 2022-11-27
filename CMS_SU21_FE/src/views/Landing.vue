<template>
  <!-- <a-spin :spinning="loading"  size="large"> -->

  <!-- <a-icon slot="indicator" type="loading" style="font-size: 30px" spin />
    <div v-if="loading" class="spin-content"></div> -->
  <loading
    v-if="loading"
    v-model="loading"
    :can-cancel="true"
    loader="dots"
    :is-full-page="false"
    color="#FC5730"
  ></loading>

  <div v-else class="vld-parent">
    <div class="position-relative">
      <!-- shape Hero -->
      <section class="section section-shaped section-lg my-0">
        <div class="shape shape-style-1 bg-gradient-custom">
          <!-- <img src="../../public/img/theme/48pz1hd6ktp51.png" alt="" /> -->
          <span></span>
          <span></span>
          <span></span>
          <span></span>
          <span></span>
          <span></span>
          <span></span>
          <span></span>
          <span></span>
        </div>
        <div class="container shape-container d-flex">
          <!-- <div class="col px-0">
            <div class="row">
              <div class="col-lg-6">
                <h1 class="display-3 text-white">
                  A beautiful Design System
                  <span>completed with examples</span>
                </h1>
                <p class="lead text-white">
                  The design system comes with four pre-built pages to help you
                  get started faster. You can change the text and images and
                  you're good to go.
                </p>
                <div class="btn-wrapper">
                  <base-button
                    tag="a"
                    href="https://demos.creative-tim.com/argon-design-system/docs/components/alerts.html"
                    class="mb-3 mb-sm-0"
                    type="info"
                    icon="fa fa-code"
                  >
                    Components
                  </base-button>
                  <base-button
                    tag="a"
                    href="https://www.creative-tim.com/product/argon-design-system"
                    class="mb-3 mb-sm-0"
                    type="white"
                    icon="ni ni-cloud-download-95"
                  >
                    Download HTML
                  </base-button>
                </div>
              </div>
            </div> -->
          <!-- </div> -->
        </div>
      </section>
      <!-- 1st Hero Variation -->
    </div>
    <section class="section section-lg pt-lg-0 mt--200">
      <div class="card-herder">
        <card class="border-0 m-b landing card-article">
          <div class="box-image">
            <!-- <a
              href="javascript:void(0)"
              @click="goToArticleDetail(listTopArticle[0].id)"
            >
              <img
                :src="relatedArticleImage + listTopArticle[0].introductionImage"
              />
            </a> -->
            <ArticleCarousel
              :list="listMostViewedArticle"
              :preview="preview"
              @previewClicked="previewClicked"
            />
          </div>
          <div class="box-title">
            <div style="display: flex">
              <div class="description mb-3 mr-3 card-channelInfo--label">
                <!-- {{ listTopArticle[0].categoryName }} -->
              </div>
            </div>
            <div class="text-uppercase card-title">
              <a href="javascript:void(0)">
                <h2 style="color: #fff" class="union">
                  FPT UNIVERSITY STUDENT UNION
                </h2>
              </a>
            </div>
          </div>
        </card>
      </div>

      <div class="container container-custom">
        <a-row :gutter="24" justify="space-between" align="bottom">
          <div
            v-for="(item, index) in listNewestArticleByCategory"
            :key="item.id"
          >
            <CardArticle
              :article="item"
              :weight="item.weight"
              :style="item.style"
            >
            </CardArticle>

            <a-col v-if="index === 1" :span="8">
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
                    :data-source="listArticleByTags"
                  >
                    <div style="border-left: 4px solid #fc5730" slot="header">
                      <h5
                        class="display-5"
                        style="padding-left: 9px; font-weight: 700"
                      >
                        POPULAR TAGS
                      </h5>
                    </div>
                    <template slot="header">
                      <a
                        href="javascript:void(0)"
                        style="padding-left: 12px"
                        v-for="(tag, index) in listPopularTags"
                        :key="index"
                        @click="getArticleByPopularTag(tag.tagContent)"
                      >
                        <badge :type="tag.color" class="popular-tag"
                          >{{ tag.tagContent }}
                          <span style="font-weight: 800">{{
                            " | " + tag.totalArticle
                          }}</span>
                        </badge>
                      </a>
                       <a
                        href="javascript:void(0)"
                        style="padding-left: 12px"
                        @click="getArticleByPopularTag()"
                      >
                        <badge type="default" class="popular-tag"
                          >Show all
                          <!-- <span style="font-weight: 800">{{
                            " | " + listPopularTags.length
                          }}</span> -->
                        </badge>
                      </a>

                      <!-- 
                      <badge type="primary">Primary</badge>

                      <badge type="secondary">Secondary</badge>

                      <badge type="info">Info</badge>

                      <badge type="success">Success</badge>

                      <badge type="danger">Danger</badge>

                      <badge type="warning">Warning</badge> -->
                    </template>
                    <a-list-item
                      v-for="item in listArticleByTags"
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
        <div class="recommend-header">
          <h5 class="display-5 recommend-title">Recommended For You</h5>
        </div>
        <a-row :gutter="24" justify="space-between" align="bottom">
          <div v-for="item in listTopArticle" :key="item.id">
            <CardArticle
              :article="item"
              :weight="item.weight"
              :style="item.style"
            >
            </CardArticle>
          </div>
        </a-row>
        <div class="recommend-header">
          <h5 class="display-5 recommend-title">Explore All</h5>
        </div>
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
              <!-- <a-button
                type="danger"
                shape="round"
                size="large"
                :loading="loading"
                v-if="showLoadmore"
                @click="loadingMode"
              >
                Load more
              </a-button> -->
            </div>
          </div>
        </div>
      </div>
    </section>
  </div>
  <!-- </a-spin> -->
</template>

<script>
import CardArticle from "./components/CardArticle.vue";
import "vue-loading-overlay/dist/vue-loading.css";
import Loading from "vue-loading-overlay";
import ArticleRepo from "../api/article.js";
import ArticleCarousel from "./components/ArticleCarousel.vue";
import DataReportRepository from "../api/dataReport";
import HashtagRepository from "../api/hashtag";
import anime from "animejs/lib/anime.es.js";
export default {
  name: "home",
  components: { Loading, CardArticle, ArticleCarousel },
  data() {
    return {
      preview: false,
      listPopularTags: [],
      listArticleByTags: [],
      listTopArticle: [],
      listNewArticle: [],
      listNewestArticleByCategory: [],
      listMostViewedArticle: [],
      current: 0,
      loading: true,
      showLoadmore: true,
      categoryID: -1,
      lstArticleId: "",
      reloadIcon: "reload",
      paginationArticle: {
        onChange: (page) => {},
        pageSize: 3,
      },
      listTagColor: [
        "default",
        "primary",
        "info",
        "success",
        "danger",
        "warning",
      ],
    };
  },
  props: {
    previewArticle: {
      type: Object,
      required: false,
      default: null,
      description: "This object is preview version of article!",
    },
  },
  created() {
    if (this.previewArticle) {
      this.listMostViewedArticle = [this.previewArticle];
      this.preview = true;
    } else {
      this.getMostViewedArticle();
    }
    this.getNewestArticleOfCategory();
    this.getListTopArticle();
    this.getPopularTags();
  },
  mounted() {
    // setTimeout(() => {
    // }, 5000);
  },
  updated() {
    this.$nextTick(function () {
      let header = document.querySelector(".union");
      header.innerHTML = header.innerText
        .split("")
        .map(function (char) {
          if (char == " ") {
            char = "&nbsp";
          }
          return "<span>" + char + "</span>";
        })
        .join("");
      anime.timeline({ loop: true }).add({
        targets: ".union span",
        scale: [5, 1],
        duration: 10000,
        // translateY:["50px",0],
        opacity: [0, 1],
        delay: function (element, i) {
          return i * 50;
        },
      });
    });
  },
  methods: {
    randomOverlayColor() {
      const color =
        this.listTagColor[Math.floor(Math.random() * this.listTagColor.length)];
      return color;
    },
    getPopularTags() {
      HashtagRepository.getPopularTags().then((res) => {
        let listPopularTags = res.data.data;
        if (listPopularTags) {
          listPopularTags.forEach((el) => {
            this.listPopularTags.push({
              tagContent: el.tagContent,
              totalArticle: el.totalArticle,
              color: this.randomOverlayColor(),
            });
          });
          this.getArticleByPopularTag(null);
        }
      });
    },
    getArticleByPopularTag(tag) {
      let hashtag = [];
      if (!tag) {
        hashtag = this.listPopularTags.map((tag) => tag.tagContent);
      } else {
        hashtag.push(tag);
      }
      ArticleRepo.getArticleByPopularTags(hashtag).then((res) => {
        this.listArticleByTags = res.data.data;
      });
    },
    getMostViewedArticle() {
      DataReportRepository.getTopArticles({
        fromDate: "",
        toDate: "",
      }).then((res) => {
        this.listMostViewedArticle = res.data.data;
      });
    },
    goToArticleDetail(id) {
      let routeData = this.$router.resolve({
        path: `/article-detail/${id}`,
      });
      window.open(routeData.href, "_blank");
    },
    previewClicked(data) {
      this.offHome();
    },
    offHome() {
      this.$emit("checkShowComponent", true);
    },
    doMath: function () {
      return index + 1;
    },
    getListTopArticle() {
      var data = {
        title: "",
        categoryID: this.categoryID,
        status: -1,
      };
      this.loading = true;
      ArticleRepo.getTopArticles(data).then((res) => {
        let listTopArticle = res.data.data.items;
        if (listTopArticle) {
          this.lstArticleId += listTopArticle.map((art) => art.id).join();
          this.loadingMode();
          for (let index = 0; index < listTopArticle.length; index++) {
            let element = listTopArticle[index];
            let weight = 6;
            let style = "";
            // switch (index) {
            //   case 0:
            //     weight = 6;
            //     break;
            //   case 1:
            //     weight = 6;
            //     break;
            //   case 2:
            //     weight = 12;
            //     break;
            //   case 3:
            //     weight = 6;
            //     break;
            //   case (4, 5, 6):
            //     weight = 6;
            //     break;
            //   case 7:
            //     weight = 12;
            //     break;
            //   case 8:
            //     weight = 12;
            //     break;
            //   default:
            //     break;
            // }
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
              displayName: element.firstName + " " + element.lastName,
              pinned: element.pinned,
            });
          }
        }
        this.loading = false;
      });
    },
    getNewestArticleOfCategory() {
      this.loading = true;
      ArticleRepo.getNewestArticleOfCategory().then((res) => {
        let listNewestArticleByCategory = res.data.data;
        if (listNewestArticleByCategory) {
          this.lstArticleId = listNewestArticleByCategory
            .map((art) => art.id)
            .join();
          let layoutIndex = 0;

          for (
            let index = 0;
            index < listNewestArticleByCategory.length;
            index++
          ) {
            let element = listNewestArticleByCategory[index];
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
              // case 2:
              //   weight = 12;
              //   break;
              // case 3:
              //   weight = 6;
              //   break;
              // case (4, 5, 6):
              //   weight = 6;
              //   break;
              // case 7:
              //   weight = 12;
              //   break;
              // case 8:
              //   weight = 12;
              //   break;
              default:
                break;
            }
            if (index >= 2) {
              if (layoutIndex == 0) {
                weight = 12;
              } else if (layoutIndex == 5) {
                weight = 12;
                layoutIndex = -1;
              } else {
                weight = 6;
              }
              layoutIndex++;
            }

            this.listNewestArticleByCategory.push({
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
              displayName: element.firstName + " " + element.lastName,
              pinned: element.pinned,
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
        lstArticlesId: this.lstArticleId,
      };
      this.reloadIcon = "loading";
      ArticleRepo.searchLandingArticles(data, this.current, 8).then((res) => {
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
        // if (this.current * 8 >= res.data.data.totals) {
        //   this.showLoadmore = false;
        // }
        if (
          this.listNewArticle.length === res.data.data.totals ||
          this.listNewArticle.length > res.data.data.totals
        ) {
          this.showLoadmore = false;
        }
      });
    },
  },
};
</script>
<style scoped>
.recommend-header {
  margin-top: 50px;
  border-left: 4px solid #fc5730;
  margin-bottom: 15px;
}
.recommend-title {
  padding: 4px 4px;
  text-transform: uppercase;
  line-height: 1.25vw;
  font-size: 1.4vw;
  font-weight: 700;
}
.icon-flex {
  display: inline-flex;
}
.bg-gradient-custom {
  /* background: rgb(13, 12, 14); */
  background: linear-gradient(
    83deg,
    rgba(131, 58, 180, 1) 0%,
    rgba(253, 29, 29, 1) 41%,
    rgba(252, 148, 69, 1) 100%
  );
  /* background: rgb(242,112,33); */
  /* background: linear-gradient(208deg, rgba(242,112,33,1) 0%, rgba(243,112,34,1) 28%, rgba(244,112,32,0.30325633671437324) 88%); */
}
.ant-list-item-meta-description {
  overflow: hidden;
  text-overflow: ellipsis;
  display: -webkit-box;
  -webkit-line-clamp: 1;
  -webkit-box-orient: vertical;
  text-overflow: unset;
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

.stories-container {
  min-height: 666px;
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
</style>
<style>
.popular-tag:hover {
  transform: translateY(-0.25em);
}
.card-article .card-body:hover {
  -webkit-box-shadow: 0 7px 12px 7px rgba(0, 0, 0, 0.2);
  -moz-box-shadow: 0 7px 12px 7px rgba(0, 0, 0, 0.2);
  -ms-box-shadow: 0 7px 12px 7px rgba(0, 0, 0, 0.2);
  box-shadow: 0 7px 12px 7px rgba(0, 0, 0, 0.2);
}

.landing .card-body {
  background-image: linear-gradient(to bottom, transparent, #000);
  border-radius: 8px;
}
.landing {
  height: 330px;
  background-repeat: round;
}
.card-title {
  line-height: 1;
  letter-spacing: 3px;
  height: 25%;
  align-items: center;
  font-size: 38px;
  color: #fff;
  margin-bottom: 10px;
  overflow: hidden;
  text-overflow: ellipsis;
  display: -webkit-box;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  text-overflow: unset;
}

.card-article h2 {
  text-transform: uppercase;
  font-size: 38px;
  font-weight: 900;
}
/* @keyframes shine {
  0% {
    background-position-x: -500%;
  }
  100% {
    background-position-x: 500%;
  }
} */
.card-channelInfo--label {
  color: #fff;
  font-weight: 700;
  -webkit-border-radius: 19px;
  border-radius: 19px;
  padding: 4px 10px;
  width: fit-content;
}
.load-more .ant-btn:hover,
.ant-btn:focus {
  color: #fff;
  background-color: #fff;
  border-color: #fff;
}

.card-herder {
  padding-left: 12px;
  width: 100%;
  height: 100px;
  margin-top: 37px;
  /* margin-bottom: -100px; */
}
.card-herder .card-article {
  background-color: transparent;
}

.card-herder .card-body:hover {
  box-shadow: none !important;
}

.card-herder .card-body {
  display: flex;
  border-radius: 0 !important;
  background: none !important;
  padding: 0 !important;
}
.card-herder .box-image {
  display: inline-block;
  width: calc(100% / 2 + 246px);
}
.card-herder .box-image img {
  width: 100%;
  height: 420px;
  border-top-right-radius: 8px;
  border-bottom-right-radius: 8px;
  margin-top: -100px;
}
.card-herder .box-title {
  display: inline-block;
  width: 370px;
  margin-left: 20px;
  margin-top: -50px;
}
.union span {
  display: inline-block;
}
.card-article .time-icon {
  margin-top: 4px;
}
.card-article .time-icon img {
  width: 20px;
  height: 20px;
  margin-top: -4px;
  margin-right: 3px;
}
.spin-content {
  border: 1px solid #575e63;
  background-color: #e6f7ff;
  padding: 30px;
  height: 1000px;
}
.vld-overlay.is-active {
  display: flex !important;
}
.article-sapo {
  height: 30%;
  color: #fff;
  overflow: hidden;
  text-overflow: ellipsis;
  display: -webkit-box;
  -webkit-line-clamp: 3;
  -webkit-box-orient: vertical;
  text-overflow: unset;
}
.ant-pagination-item {
  font-weight: 700 !important;
  color: #fc5730;
  background-color: fff;
  border: 1px solid #fc5730 !important;
  border-radius: 25px !important;
}
.ant-pagination-item-active {
  font-weight: 700 !important;
  color: #fff;
  background-color: #fc5730 !important;
  border: 1px solid #fc5730 !important;
  border-radius: 25px !important;
}
.ant-pagination-item:focus a,
.ant-pagination-item:hover a {
  font-weight: 700 !important;
  color: #fff !important;
  background-color: #fc5730 !important;
  border: 1px solid #fc5730 !important;
  border-radius: 25px !important;
}
.ant-pagination-prev .ant-pagination-item-link,
.ant-pagination-next .ant-pagination-item-link {
  border: none !important;
  font-weight: bold !important;

  font-size: 14px !important;
}
.ant-pagination-item-active:hover {
  font-weight: 700 !important;
  color: #fff;
  background-color: #fc5730 !important;
  border: 1px solid #fc5730 !important;
  border-radius: 25px !important;
}
</style>