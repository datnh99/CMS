<template>
  <div>
    <div class="position-relative profile-page image-container">
      <section>
        <div class="image-container">
          <div class="section-profile-cover section-shaped my-0 bg-image"/>
        </div>
      </section>
      <section class="section section-skew">
        <div class="container">
          <div class="row justify-content-center">
            <div class="col-6">
              <span class="display-4">
                <span class="tag-version"> Original Version </span> <br>
                {{ article.title }} 
              </span>
              <card shadow class="card-profile" no-body>
                <div class="px-4">
                  <div class="mt-5 main-content">
                    <div v-html="article.content"></div>
                    <div class="authored">
                      <b>Written by:</b>
                      <strong class="description" style="font-style: italic">
                        {{ authorDetail.firstName + " " + authorDetail.lastName }}
                      </strong>
                    </div>
                  </div>
                </div>
              </card>
            </div>
            <div class="col-6">
              <span class="display-4">
                <span class="tag-version"> Optimized Version </span> <br>
                {{ articleOptimize.title }}
              </span>
              <card shadow class="card-profile" no-body>
                <div class="px-4">
                  <div class="mt-5 main-content">
                    <div v-html="articleOptimize.content"></div>
                    <div class="authored">
                      <b>Written by:</b>
                      <strong class="description" style="font-style: italic">
                        {{ authorDetail.firstName + " " + authorDetail.lastName }}
                      </strong>
                    </div>
                  </div>
                </div>
              </card>
            </div>
          </div>
        </div>
      </section>
    </div>
  </div>
</template>

<script>
import CONFIG from "../config/index";
import ArticleRepository from "../api/article";
import UserRepository from "../api/user";

import moment from "moment";

const defaultCommentForm = {
  id: -1,
  articleID: -1,
  content: "",
  parentID: -1,
};

export default {
  name: "OriginalArticleDetail",
  components: {
  },
  data() {
    return {
      imageApiUrl: `${CONFIG.apiUrl}/image/getImage/{get-image}?id=`,
      relatedArticleImage: `${CONFIG.apiUrl}/image/getImage/{get-image}?id=`,
      articleId: 0,
      article: {},
      articleOptimize: {},
      moment,
      introductionImageUrl: "",
      listRelated: [],
      loading: false,
      username: this.$cookies.get("account"),
      authorDetail: {},
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
  // updated(){

    
  // },
  created() {
    this.articleId = this.$route.params.id;
    this.getArticleById(this.articleId);
    this.getArticleOptimizeByOldId(this.articleId);
    setTimeout(() => {
      if (this.article.relatedArticle) {
        const relatedIds = this.article.relatedArticle.split(",");
        this.getRelatedArticles(relatedIds);
      }
    }, 500);
  },
  methods: {
    commentContentChange(value) {},
    removeElement(array, elem) {
      var index = array.indexOf(elem);
      if (index > -1) {
        array.splice(index, 1);
      }
    },
    goToArticleDetail(id) {
      let routeData = this.$router.resolve({
        path: `/article-detail/${id}`,
      });
      window.open(routeData.href, "_blank");
    },
    getAuthorInfor() {
      UserRepository.getUserByAccount(this.article.author).then((res) => {
        this.authorDetail = res.data.data;
      });
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
    getRelatedArticles(relatedArticles) {
      ArticleRepository.getRelatedArticlesByListId(relatedArticles).then(
        (res) => {
          if (res.data.data) {
            let listRelated = res.data.data;
            if (listRelated) {
              var i,
                j,
                temporary,
                chunk = 3;
              for (i = 0, j = listRelated.length; i < j; i += chunk) {
                temporary = listRelated.slice(i, i + chunk);
                this.listRelated.push(temporary);
              }
            }
          }
        }
      );
    },
    getArticleById(id) {
      if (this.previewArticle) {
        this.article = this.previewArticle;
        this.introductionImageUrl = `${CONFIG.apiUrl}/image/getImage/{get-image}?id=${this.article.introductionImage}`;
      } else {
        ArticleRepository.getOriginalArticleById(id).then((res) => {
          this.article = res.data.data;
          if (!res.data.success) {
            this.$router.push("/not-found");
          }
          this.getAuthorInfor();
          this.introductionImageUrl = `${CONFIG.apiUrl}/image/getImage/{get-image}?id=${this.article.introductionImage}`;
        });
      }
    },
    getArticleOptimizeByOldId(id) {
      ArticleRepository.getArticleOptimizeByOldId(id).then((res) => {
        this.articleOptimize = res.data.data;
      });
    },
  },
};
</script>
<style scoped>
.attachments-content {
  padding: 10px 10px 10px 10px;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}
.card-profile {
  margin-top: 1% !important;
  /* background-color: transparent */
}
.container {
  max-width: 100%;
}
.image-container {
  background: linear-gradient(
    360deg,
    rgba(2, 0, 36, 1) 0%,
    rgba(0, 0, 0, 1) 0%,
    rgba(0, 0, 0, 0) 40%
  );
}

.section-profile-cover {
  background: linear-gradient(
    83deg,
    rgba(131, 58, 180, 1) 0%,
    rgba(253, 29, 29, 1) 41%,
    rgba(252, 148, 69, 1) 100%
  );
}
.article-action {
  transition: transform 250ms;

  font-size: 24px;
}
.article-action:hover {
  transform: translateY(-5px);
}
.follow-user-btn :hover {
  color: #eb2f96;
}
.author-info {
  display: grid;
}
.btn-unfollow :hover {
  color: #fff !important;
  border-color: #fff !important;
}
.ant-comment-content-author > a,
.ant-comment-content-author > span {
  font-size: 14px !important;
  font-weight: 500;
}
.authored {
  float: right;
}

.ant-carousel >>> .slick-slide {
  height: 500px;
}

.detail-image {
  display: inline-block;
  overflow: hidden;
  position: relative;
  width: 100%;
  /* background-image: url("../../public/img/theme/48pz1hd6ktp51.png"); */
}
.bg-image {
  pointer-events: none;
  position: relative;
  width: 100%;
  object-fit: cover;
  height: 700px;
  z-index: -1;
}
.lead {
  margin-bottom: 10%;
}
.title {
  position: absolute;
  z-index: 99;
  display: block;
  width: 100%;
  max-width: 979px;
  left: 47%;
  -webkit-transform: translate(-50%, 0);
  -moz-transform: translate(-50%, 0);
  -o-transform: translate(-50%, 0);
  -ms-transform: translate(-50%, 0);
  transform: translate(-50%, 0);
  font-size: 1.5rem;
  line-height: 1.8rem;
  letter-spacing: 1.3px;
  font-weight: 700;
  max-height: 100px;
  overflow: hidden;
  padding: 0 16px;
  bottom: 18px;
  margin-bottom: 4%;
}
.title > span {
  line-height: 48px;
  color: #fff;
  padding: 2px 0;
}

.created-date {
  font-size: 0.875rem;
  color: #adb5bd;
}
.anticon {
  vertical-align: 0em;
}
.related-topic {
  padding-bottom: 40px;
}

.related-topic-text {
  font-size: 1rem;
  line-height: 1.2rem;
  font-weight: 600;
  letter-spacing: 0.15px;
  padding-bottom: 8px;
  text-transform: uppercase;
  position: relative;
  display: inline-block;
  margin-bottom: 30px;
  border-bottom: 3px solid #fc5730;
}
.display-4 {
  color: #ffffff
}
.vl {
  border-left: 6px solid green;
  height: 500px;
}
.tag-version {
  font-size: 30px;
  font-weight: 600;
  line-height: 2.3;
}
</style>
<style >
.media {
  display: block !important;
}
.ant-carousel .slick-dots li button {
  background: #fc5730 !important;
}
.main-content .image img {
  max-width: 80%;
  max-height: 100%;
  display: block;
  margin-left: auto;
  margin-right: auto;
}
.main-content .image {
  display: block;
  margin-left: auto;
  margin-right: auto;
}
</style>