<template>
  <a-col :span="weight">
    <div style="padding-bottom: 6px">
      <div class="example-2 card">
        <div
          class="wrapper"
          :style="`background: url(${
            articleImage + article.introductionImage
          }) center / cover no-repeat;`"
        >
          <div
            class="header"
            :style="`background-image: linear-gradient(to top, transparent, ${randomColor});`"
          >
            <div class="date">
              <!-- <span class="day">12</span>
          <span class="month">Aug</span> -->
              <span class="year">{{ generateTime(article.approveDate) }}</span>
            </div>
            <ul class="menu-content">
              <li v-if="article.pinned">
                <a href="#" class="fa fa-tag"></a>
              </li>
              <li v-if="article.cared">
                <a href="#" class="fa fa-heart-o"><span></span></a>
              </li>
               <li>
                <a href="#" class="fa fa-eye"
                  ><span>{{ article.totalViews }}</span></a
                >
              </li>
              <li>
                <a href="#" class="fa fa-comment-o"
                  ><span>{{ article.totalComment }}</span></a
                >
              </li>
            </ul>
          </div>
          <div class="data">
            <div
              class="content"
              :style="`background-image: linear-gradient(to bottom, transparent, ${randomColor});`"
            >
              <span class="author">{{ article.displayName }}</span>
              <a href="javascript:void(0)" @click="goToLandingByCategory()"><span class="category" :style="`background-color:#FC5730;`">{{ article.categoryName }}</span></a>
              <h5 class="title">
                <a
                  href="javascript:void(0);"
                  @click="goToArticleDetail(article.id)"
                  >{{ article.title }}</a
                >
              </h5>
              <p class="text">{{ article.sapo }}</p>
              <a
                href="javascript:void(0)"
                class="button"
                @click="goToArticleDetail(article.id)"
                >Read more</a
              >
            </div>
          </div>
        </div>
      </div>
    </div></a-col
  >
</template>
<script>
import {generateTime} from '../../api/date'
import CONFIG from "../../config/index";
export default {
  components: {},

  props: {
    article: {
      type: Object,
      default: null,
      description: "Html tag to use for the badge.",
    },
    weight: {
      type: Number,
      default: 6,
      description: "Html tag to use for the badge.",
    },
  },

  data() {
    return {
      articleImage: `${CONFIG.apiUrl}/image/getImageThumb/{get-image-thumb}?id=`,
      randomColor: "",
      colorCollection: [
        // " #f27021",
        // "#FC5730",
        "#000000ba",
        // "#fc9445",
        // "#fd1d1d",
        // "#833ab4"
        // "#5C6BC0",
        // "#717171",
        // "#c2ad70",
        // "#7793a3",
        // "#5B4B36",
        // "#38535D",
        // "#325369",
        // "#624245",
        // "#C6DAD2",
        // "#3462af",
        // "#2643e9",
        // "#03acca",
        // "#1aae6f",
        // "#f80031",
        // "#ff3709",
      ],
    };
  },
  created() {
    this.randomOverlayColor();
  },
  methods: {
    goToLandingByCategory() {
      this.$router.push(`/landing-by-category/${this.article.categoryID}`);
      if (window.location.href.includes("/landing-by-category")) {
        window.location.reload();
      }
    },
    randomOverlayColor() {
      this.randomColor =
        this.colorCollection[
          Math.floor(Math.random() * this.colorCollection.length)
        ];
    },
    generateTime(time) {
      return generateTime(time)
    },
    goToArticleDetail(id) {
      let routeData = this.$router.resolve({
        path: `/article-detail/${id}`,
      });
      window.open(routeData.href, "_blank");
    },
  },
};
</script>
<style lang="scss" scoped>
.ant-col {
  padding-left: 3px !important;
  padding-right: 3px !important;
}
$indigo: #5c6bc0;
$black: #717171;
@import url(https://fonts.googleapis.com/css?family=Open+Sans:300,400,700);
// Variables
$regal-blue: #034378;
$san-juan: #2d4e68;
$bermuda: #77d7b9;
$white: #fff;
$black: #000;
$open-sans: "Open Sans", sans-serif;
// clear-fix mixin
@mixin cf {
  &::before,
  &::after {
    content: "";
    display: table;
  }
  &::after {
    clear: both;
  }
}

* {
  box-sizing: border-box;
}

body {
  background-image: linear-gradient(to right, $regal-blue, $san-juan);
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  height: 100vh;
  font-family: $open-sans;
}

a {
  text-decoration: none;
}

h1 {
  font-family: $open-sans;
  font-weight: 300;
}

// Base styles
.card {
  // padding: 0 1.7rem;
  .menu-content {
    @include cf;
    margin: 0;
    padding: 0;
    list-style-type: none;
    li {
      display: inline-block;
    }
    a {
      color: $white;
    }
    span {
      position: absolute;
      left: 50%;
      top: 0;
      font-size: 10px;
      font-weight: 700;
      font-family: "Open Sans";
      transform: translate(-50%, 0);
    }
  }
  .wrapper {
    background-color: $white;
    min-height: 370px;
    position: relative;
    overflow: hidden;
    box-shadow: 0 19px 38px rgba($black, 0.3), 0 15px 12px rgba($black, 0.2);
    &:hover {
      .data {
        transform: translateY(0);
      }
    }
  }
  .data {
    background-image: -webkit-gradient(
      linear,
      left top,
      left bottom,
      from(transparent),
      to(#000)
    );
    position: absolute;
    bottom: 0;
    width: 100%;
    transform: translateY(calc(70px + 1em));
    transition: transform 0.3s;
    .content {
      // background-image: linear-gradient(to bottom, transparent, #000);
      padding: 1em;
      position: relative;
      z-index: 1;
    }
  }
  .category{
    margin-left: 10px;
    color: #fff;
    
    font-size: .6875rem;
    line-height: .825rem;
    line-height: 1;
    font-weight: 700;
    -webkit-border-radius: 19px;
    border-radius: 19px;
    padding: 4px 10px;
    max-width: fit-content;
  }
  .author {
        font-weight: 600;

    font-size: 12px;
  }
  .title {
    font-weight: 700;
    margin-top: 10px;
    overflow: hidden;
    text-overflow: ellipsis;
    display: -webkit-box;
    -webkit-line-clamp: 2;
    -webkit-box-orient: vertical;
    text-overflow: unset;
  }
  .text {
    height: 70px;
    margin: 0;
    overflow: hidden;
    text-overflow: ellipsis;
    display: -webkit-box;
    -webkit-line-clamp: 3;
    -webkit-box-orient: vertical;
    text-overflow: unset;
  }
  input[type="checkbox"] {
    display: none;
  }
  input[type="checkbox"]:checked + .menu-content {
    transform: translateY(-60px);
  }
}

// Second example styles
.example-2 {
  border-radius: 12px;
  -webkit-box-shadow: 0 4px 8px 0 #212529;
  -moz-box-shadow: 0 4px 8px 0 rgba(178, 178, 178, 0.26);
  -ms-box-shadow: 0 4px 8px 0 rgba(178, 178, 178, 0.26);
  box-shadow: 0 4px 8px 0 rgba(178, 178, 178, 0.26);
  .wrapper {
    border-radius: 12px;
    -webkit-box-shadow: 0 4px 8px 0 #212529;
    -moz-box-shadow: 0 4px 8px 0 rgba(178, 178, 178, 0.26);
    -ms-box-shadow: 0 4px 8px 0 rgba(178, 178, 178, 0.26);
    box-shadow: 0 4px 8px 0 rgba(178, 178, 178, 0.26);
    background: url(https://i1-kinhdoanh.vnecdn.net/2021/07/31/dien-2806-1627731698.jpg?w=1020&h=0&q=100&dpr=1&fit=crop&s=GnRDkRkFEpHr8qn4exsBAw)
      center / cover no-repeat;
    &:hover {
      .menu-content {
        span {
          transform: translate(-50%, -10px);
          opacity: 1;
        }
      }
    }
  }
  .header {
    // background-image: linear-gradient(to top, transparent, #000000ba);
    @include cf;
    color: $white;
    padding: 1em;
    .date {
      float: left;
      font-size: 12px;
    }
  }
  .menu-content {
    float: right;
    li {
      margin: 0 5px;
      position: relative;
    }
    span {
      transition: all 0.3s;
      opacity: 0;
    }
  }
  .data {
    color: $white;
    transform: translateY(calc(70px + 4em));
  }
  .title {
    a {
      color: $white;
    }
  }
  .button {
    display: block;
    width: 100px;
    margin: 2em auto 1em;
    text-align: center;
    font-size: 12px;
    color: $white;
    line-height: 1;
    position: relative;
    font-weight: 700;
    &::after {
      content: "\2192";
      opacity: 0;
      position: absolute;
      right: 0;
      top: 50%;
      transform: translate(0, -50%);
      transition: all 0.3s;
    }
    &:hover {
      &::after {
        transform: translate(5px, -50%);
        opacity: 1;
      }
    }
  }
}
</style>



