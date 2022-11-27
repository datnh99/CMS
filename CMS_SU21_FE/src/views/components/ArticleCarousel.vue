<template>
  <div class="carousel">
    <div class="carousel__nav">
      <span id="moveLeft" class="carousel__arrow" @click="prevBtnClick">
        <svg class="carousel__icon" width="24" height="24" viewBox="0 0 24 24">
          <path
            d="M20,11V13H8L13.5,18.5L12.08,19.92L4.16,12L12.08,4.08L13.5,5.5L8,11H20Z"
          ></path>
        </svg>
      </span>
      <span id="moveRight" class="carousel__arrow" @click="nextBtnClick">
        <svg class="carousel__icon" width="24" height="24" viewBox="0 0 24 24">
          <path
            d="M4,11V13H16L10.5,18.5L11.92,19.92L19.84,12L11.92,4.08L10.5,5.5L16,11H4Z"
          ></path>
        </svg>
      </span>
    </div>
    <div class="carousel-item active">
      <div
        class="carousel-item__image"
        @click="goToArticleDetail(list[0].id)"
        :style="`background-image: url(${
          articleImage + list[0].introductionImage
        })`"
      ></div>
      <div class="carousel-item__info">
        <div class="carousel-item__container">
          <h2 class="carousel-item__subtitle">{{ list[0].categoryName }}</h2>
          <a href="javascript:void(0)">
            <h1
              class="carousel-item__title"
              @click="goToArticleDetail(list[0].id)"
            >
              {{ list[0].title }}
            </h1></a
          >
          <p class="carousel-item__description">
            {{ list[0].sapo }}
          </p>
          <a
            href="javascript:void(0)"
            class="carousel-item__btn"
            @click="goToArticleDetail(list[0].id)"
            >Read More</a
          >
        </div>
      </div>
    </div>
    <div
      v-for="item in list.slice(1, list.length)"
      :key="item.id"
      class="carousel-item"
    >
      <div
        class="carousel-item__image"
        @click="goToArticleDetail(item.id)"
        :style="`background-image: url(${
          articleImage + item.introductionImage
        }) `"
      ></div>
      <div class="carousel-item__info">
        <div class="carousel-item__container">
          <h2 class="carousel-item__subtitle">{{ item.categoryName }}</h2>
          <a href="javascript:void(0)">
            <h1
              class="carousel-item__title"
              @click="goToArticleDetail(item.id)"
            >
              {{ item.title }}
            </h1></a
          >
          <p class="carousel-item__description">
            {{ item.sapo }}
          </p>
          <a
            href="javascript:void(0)"
            class="carousel-item__btn"
            @click="goToArticleDetail(item.id)"
            >Read More</a
          >
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import CONFIG from "../../config/index";

export default {
  data() {
    return {
      next: 0,
      prev: 0,
      slide: 0,
      current: 0,
      listCarousel: [],
      total: 0,
      articleImage: `${CONFIG.apiUrl}/image/getImageThumb/{get-image-thumb}?id=`,
    };
  },
  props: {
    list: {
      type: Array,
      default: null,
      description: "Html tag to use for the badge.",
    },
    preview: {
      type: Boolean,
      default: false,
      description: "Html tag to use for the badge.",
    },
  },
  mounted() {
    setTimeout(() => {
      this.listCarousel = document.querySelectorAll(".carousel-item");
      this.listCarousel[0].classList.add("active");
      this.total = this.listCarousel.length;
      window.setInterval(() => {
        this.nextBtnClick();
      }, 5000);
    }, 10000);
  },
  methods: {
    goToArticleDetail(id) {
      if (!this.preview) {
        let routeData = this.$router.resolve({
          path: `/article-detail/${id}`,
        });
        window.open(routeData.href, "_blank");
      } else {
        this.$emit("previewClicked", true);
      }
    },
    setSlide(prev, next) {
      this.slide = this.current;
      if (next > this.total - 1) {
        this.slide = 0;
        this.current = 0;
      }
      if (next < 0) {
        this.slide = this.total - 1;
        this.current = this.total - 1;
      }
      this.listCarousel[prev].classList.remove("active");
      this.listCarousel[this.slide].classList.add("active");
      setTimeout(function () {}, 800);
    },
    nextBtnClick() {
      this.next = this.current;
      this.current = this.current + 1;
      this.setSlide(this.next, this.current);
    },
    prevBtnClick() {
      this.prev = this.current;
      this.current = this.current - 1;
      this.setSlide(this.prev, this.current);
    },
  },
};
</script>
<style  scoped>
* {
  box-sizing: border-box;
}

/* html,
body {
  margin: 0;
  padding: 0;
  width: 100%;
  height: 100%;
  display: flex;
  display: -webkit-flex;
  justify-content: center;
  -webkit-justify-content: center;
  align-items: center;
  -webkit-align-items: center;
} */

body {
  background-color: #eaeaea;
}

.carousel {
  box-shadow: 0 19px 38px rgba(#000, 0.3), 0 15px 12px rgba(#000, 0.2);
  border-radius: 12px;
  -webkit-box-shadow: 0 4px 8px 0 #21252959;
  -moz-box-shadow: 0 4px 8px 0 rgba(178, 178, 178, 0.26);
  -ms-box-shadow: 0 4px 8px 0 rgba(178, 178, 178, 0.26);
  width: 100%;
  height: 116%;
  display: flex;
  max-height: 600px;
  overflow: hidden;
  position: relative;
}
.carousel-item.active {
  display: flex;
}
.carousel-item {
  visibility: visible;
  display: inherit;
  width: 100%;
  height: 100%;
  align-items: center;
  justify-content: flex-end;
  -webkit-align-items: center;
  -webkit-justify-content: flex-end;
  position: relative;
  background-color: #fff;
  flex-shrink: 0;
  -webkit-flex-shrink: 0;
  position: absolute;
  z-index: 0;
  transition: 0.6s all linear;
}

.carousel-item__info {
  height: 100%;
  display: flex;
  justify-content: center;
  flex-direction: column;
  display: -webkit-flex;
  -webkit-justify-content: center;
  -webkit-flex-direction: column;
  order: 4;
  left: 0;
  margin: auto;
  padding: 0 40px;
  width: 40%;
}

.carousel-item__image {
  width: 60%;
  height: 100%;
  order: 3;
  align-self: flex-end;
  flex-basis: 60%;
  -webkit-order: 2;
  -webkit-align-self: flex-end;
  -webkit-flex-basis: 60%;
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  position: relative;
  transform: translateX(100%);
  transition: 0.6s all ease-in-out;
}

.carousel-item__subtitle {
  font-size: 10px;
  text-transform: uppercase;
  margin: 0;
  color: #7e7e7e;
  font-weight: 700;
  transform: translateY(25%);
  opacity: 0;
  visibility: hidden;
  transition: 0.4s all ease-in-out;
}

.carousel-item__title {
  overflow: hidden;
  text-overflow: ellipsis;
  display: -webkit-box;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  text-overflow: unset;
  margin: 15px 0 0 0;
  font-size: 24px;
  line-height: 45px;
  font-weight: 700;
  color: #2c2c2c;
  transform: translateY(25%);
  opacity: 0;
  visibility: hidden;
  transition: 0.6s all ease-in-out;
}

.carousel-item__description {
  transform: translateY(25%);
  opacity: 0;
  visibility: hidden;
  transition: 0.6s all ease-in-out;
  margin-top: 35px;
  font-size: 13px;
  color: #7e7e7e;
  line-height: 22px;
  margin-bottom: 35px;
  overflow: hidden;
  text-overflow: ellipsis;
  display: -webkit-box;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  text-overflow: unset;
}

/* .carousel-item .carousel-item__image {
  background-image: url("../../../../public/img/icons/common/unnamed.jpg");
} */

.carousel-item__container {
}

.carousel-item__btn {
  width: 35%;
  color: #2c2c2c;
  font-size: 11px;
  text-transform: uppercase;
  margin: 0;
  width: 35%;
  font-weight: 700;
  text-decoration: none;
  transform: translateY(25%);
  opacity: 0;
  visibility: hidden;
  transition: 0.6s all ease-in-out;
}

.carousel__nav {
  position: absolute;
  right: 0;
  z-index: 2;
  background-color: #fff;
  bottom: 0;
}

.carousel__icon {
  display: inline-block;
  vertical-align: middle;
  width: 16px;
  fill: #5d5d5d;
}

.carousel__arrow {
  cursor: pointer;
  display: inline-block;
  padding: 11px 15px;
  position: relative;
}

.carousel__arrow:nth-child(1):after {
  content: "";
  right: -3px;
  position: absolute;
  width: 1px;
  background-color: #b0b0b0;
  height: 14px;
  top: 50%;
  margin-top: -7px;
}

.active {
  z-index: 1;
  display: flex;
  visibility: visible;
}

.active .carousel-item__subtitle,
.active .carousel-item__title,
.active .carousel-item__description,
.active .carousel-item__btn {
  transform: translateY(0);
  opacity: 1;
  transition: 0.6s all ease-in-out;
  visibility: visible;
}

.active .carousel-item__image {
  transition: 0.6s all ease-in-out;
  transform: translateX(0);
}
</style>
