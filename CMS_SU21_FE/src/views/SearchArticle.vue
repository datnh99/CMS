<template>
  <loading
    v-if="loading"
    v-model="loading"
    :can-cancel="false"
    loader="dots"
    :is-full-page="true"
    color="#FC5730"
  ></loading>
  <div v-else>
    <div class="position-relative">
      <!-- shape Hero -->
      <section class="section search-cover  section-lg my-0">
        <div class="shape  shape-default">
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
          <div class="col px-0">
            <div class="row">
              <div class="col-lg-12 founded-searchkey">
                <h1 class="display-3 search-key-title text-white">
                  <span v-if="searchKey"
                    >We found {{ totalsSearch }} articles for
                    <b style="font-style: oblique;font-weight: 600">[{{ searchKey }}]</b>
                  </span>
                  <span v-else> We found {{ totalsSearch }} articles </span>
                </h1>
                <!-- <p class="lead text-white">
                  The design system comes with four pre-built pages to help you
                  get started faster. You can change the text and images and
                  you're good to go.
                </p> -->
                <!-- <div class="btn-wrapper">
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
                </div> -->
              </div>
            </div>
          </div>
        </div>
      </section>
      <!-- 1st Hero Variation -->
    </div>
    <section class="section section-lg pt-lg-0 mt--100">
      <div class="container container-custom">
        <a-row :gutter="24">
                <CardArticle
                v-for="item in listNewArticle" :key="item.id"
              :article="item"
              :weight="6"
            >
            </CardArticle>
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
import "vue-loading-overlay/dist/vue-loading.css";
import Loading from "vue-loading-overlay";
import ArticleRepo from "../api/article.js";
import CONFIG from "../config/index";
import CardArticle from "./components/CardArticle.vue"
export default {
  name: "home",
  components: {Loading,CardArticle},
  data() {
    return {
      listNewArticle: [],
      current: 0,
      relatedArticleImage: `${CONFIG.apiUrl}/image/getImage/{get-image}?id=`,
      loading: false,
      showLoadmore: true,
      categoryID: -1,
      searchKey: "",
      totalsSearch: 0,
      reloadIcon: "reload",
    };
  },
  watch: {
    "$route.params.searchKey": function () {
      this.searchKey = this.$route.params.searchKey;
      this.loadingMode();
    },
  },
  mounted() {},
  created() {
    // this.getListTopArticle();
    const { searchKey } = this.$route.params;
    if (searchKey) {
      this.searchKey = searchKey;
    }
    this.loadingMode();
  },
  watch: {},
  methods: {
    goToArticleDetail(id) {
      let routeData = this.$router.resolve({
        path: `/article-detail/${id}`,
      });
      window.open(routeData.href, "_blank");
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
    // getListTopArticle() {
    //   var data = {
    //     title: "",
    //     categoryID: this.categoryID,
    //     status: -1,
    //   };
    //   ArticleRepo.searchLandingArticles(data, 1).then((res) => {
    //     this.listTopArticle = res.data.data.items;
    //   });
    // },
    loadingMode() {
      this.current += 1;
      this.loading = true
      this.reloadIcon = "loading";
      ArticleRepo.fullTextSearchArticle(this.searchKey, this.current, 8).then(
        (res) => {
          this.loading = false
          this.reloadIcon = "reload";
          this.totalsSearch = res.data.data.totals;
          res.data.data.items.forEach((element) => {
            this.listNewArticle.push({
              id: element.id,
              content: element.content,
              approveDate: element.approveDate,
              account: element.account,
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
              updateDate: element.modifiedTime,
            });
          });
          if (this.current * 8 >= this.totalsSearch) {
            this.showLoadmore = false;
          }
        }
      );
    },
  },
};
</script>
<style scoped>
.search-key-title span{
  font-weight: 700;
}
.search-cover {
  background: linear-gradient(
    83deg,
    rgba(131, 58, 180, 1) 0%,
    rgba(253, 29, 29, 1) 41%,
    rgba(252, 148, 69, 1) 100%
  );
}
.founded-searchkey{
  padding-bottom: 40px;
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
</style>
