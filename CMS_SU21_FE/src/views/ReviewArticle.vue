<template>
  <div class="profile-page">
    <section class="section-profile-cover section-shaped my-0">
      <div class="container-fluid">
        <h4 class="text-white screen-name">REVIEW ARTICLE</h4>
      </div>
    </section>
    <section class="section section-skew">
      <div class="container">
        <card shadow class="card-profile mt--300" no-body>
          <div class="px-4">
            <div class="gutter-example">
              <!-- <div class="page-header">
                <h2 class="display-4">Category Management</h2>
              </div> -->
            </div>

            <div class="gutter-example lg-md">
              <a-row :gutter="36">
                <a-col
                  class="gutter-row mt-10"
                  :span="4"
                  :col="4"
                  :xs="24"
                  :sm="12"
                  :md="12"
                  :lg="6"
                >
                  <div class="gutter-box">
                    <label><b>Title: </b></label>
                    <a-input v-model="articleTitleSearch" placeholder="Title">
                    </a-input>
                  </div>
                </a-col>

                <a-col
                  class="gutter-row mt-10"
                  :span="4"
                  :col="4"
                  :xs="24"
                  :sm="12"
                  :md="12"
                  :lg="6"
                >
                  <div class="gutter-box">
                    <label class="form-control-label"
                      ><b>Author name: </b></label
                    >
                    <a-input v-model="articleAuthorSearch" placeholder="Author">
                    </a-input>
                  </div>
                </a-col>

                <a-col
                  class="gutter-row mt-10"
                  :span="4"
                  :col="4"
                  :xs="24"
                  :sm="12"
                  :md="12"
                  :lg="6"
                >
                  <div class="gutter-box">
                    <label><b>Category name: </b></label>
                    <br />
                    <a-select
                      v-model="articleCategorySearch"
                      show-search
                      style="width: 100%"
                      placeholder="All"
                      @search="fetchCategoryList"
                    >
                      <a-select-option
                        v-for="item in categoryList"
                        :key="item.id"
                        :value="item.id"
                      >
                        {{ item.categoryName }}
                      </a-select-option>
                    </a-select>
                  </div>
                </a-col>

                <!-- <a-col
                  class="gutter-row mt-10"
                  :span="4"
                  :col="4"
                  :xs="24"
                  :sm="12"
                  :md="12"
                  :lg="4"
                >
                  <div class="gutter-box">
                    <label><b>Status: </b></label>
                    <br />
                    <a-select
                      v-model="articleStatusSearch"
                      show-search
                      style="width: 100%"
                      placeholder="All"
                      @search="fetchStatus"
                    >
                      <a-select-option
                        v-for="item in statusList"
                        :key="item.id"
                        :value="item.id"
                      >
                        {{ item.name }}
                      </a-select-option>
                    </a-select>
                  </div>
                </a-col> -->

                <a-col
                  class="gutter-row mt-10 group-button-custom"
                  :span="2"
                  :col="2"
                  :xs="12"
                  :sm="5"
                  :md="4"
                  :lg="3"
                >
                  <div class="gutter-box">
                    <br />
                    <span @click="submitForm()">
                      <base-button
                        class="btn btn-neutral btn-filter main-btn mr-4"
                        type="primary"
                        size="md"
                        :disabled="loading"
                      >
                        <i class="fa fa-search"></i>

                        Search
                      </base-button>
                    </span>
                  </div>
                </a-col>

                <a-col
                  class="gutter-row mt-10 group-button-custom"
                  :span="2"
                  :col="2"
                  :xs="12"
                  :sm="5"
                  :md="4"
                  :lg="3"
                >
                  <div class="gutter-box">
                    <br />
                    <span @click="clearAllDataSearch()">
                      <base-button
                        type="primary"
                        size="md"
                        :disabled="loading"
                        class="btn btn-neutral btn-filter main-btn mr-4"
                      >
                        <i class="fa fa-ban"></i>

                        Clear
                      </base-button>
                    </span>
                  </div>
                </a-col>
              </a-row>
            </div>
            <a-spin :spinning="loading">
              <a-icon
                type="loading"
                slot="indicator"
                style="font-size: 24px"
                spin
              />
              <div :style="{ background: '#fff', minHeight: '280px' }">
                <a-table
                  :columns="columns"
                  :data-source="reviewArticleListTable"
                  :scroll="{ x: 1000 }"
                  :pagination="false"
                >
                  <template #manager="item">
                    <a-popover title="Manager list" trigger="hover">
                      <template slot="content">
                        <p v-for="it in item.account" :key="it">
                          {{ it }}
                        </p>
                      </template>
                      <a-button type="primary"> Hover me </a-button>
                    </a-popover>
                  </template>

                  <!-- <template #deleteFlag="item">
                    <a-button
                      v-if="item.deleteFlag == 0"
                      type="danger"
                      shape="round"
                      size="small"
                    >
                      Active
                    </a-button>
                    <a-button v-else type="primary" shape="round" size="small">
                      InActive
                    </a-button>
                  </template> -->

                  <!-- <template #createDateCustom="item">
                    <span>{{ generateTime(item.createDate) }}</span>
                  </template>

                  <template #updateDateCustom="item">
                    <span>{{ generateTime(item.updateDate) }}</span>
                  </template> -->

                  <!-- <a slot="action" slot-scope="text">action</a> -->

                  <!-- <template #originalArticleCustom="item">
                    <a-button
                      v-if="
                        item.originalArticleID !== 0 &&
                        item.originalArticleID !== null &&
                        item.originalArticleID !== undefined
                      "
                      type="primary"
                      shape="round"
                      size="small"
                      :loading="loading"
                      @click="redirectToOriginalArticle(item.originalArticleID)"
                    >
                      See Original Article
                    </a-button>
                    <a-button
                      v-else
                      type="danger"
                      shape="round"
                      size="small"
                      disabled
                    >
                      Is Original Article
                    </a-button>
                  </template> -->

                  <template #action="item">
                    <a-button
                      type="primary"
                      shape="round"
                      icon="form"
                      size="default"
                      @click="handleReviewItemBtnClick(item)"
                      :disabled="checkDisableBtnReview(item)"
                      :loading="loading"
                      >Review
                    </a-button>
                  </template>
                </a-table>

                <div class="gutter-example pt-md pagnigation-custom">
                  <a-pagination
                    v-model="current"
                    show-quick-jumper
                    :default-current="1"
                    :total="totals"
                    @change="paginate"
                  />
                </div>
              </div>
            </a-spin>

            <!-- add modal -->
            <a-modal
              title="Review Article"
              v-model="showModal.add"
              cancelText="Reject"
              okText="Approve"
              @ok="submitReviewForm(true)"
              @cancel="submitReviewForm(false)"
            >
              <a-form :form="formReview">
                <a-form-item
                  label="Note:"
                  :label-col="{ span: 4 }"
                  :wrapper-col="{ span: 18 }"
                >
                  <a-textarea
                    :auto-size="{ minRows: 2, maxRows: 6 }"
                    v-decorator="[
                      'note',
                      {
                        rules: [
                          {
                            required: true,
                            message: 'Please input this field!',
                          },
                        ],
                      },
                    ]"
                    style="width: 100%"
                  />
                </a-form-item>
              </a-form>
            </a-modal>
          </div>
        </card>
      </div>
    </section>
  </div>
</template>
<script>
import Vue from "vue";
import Category from "../api/category.js";
import MasterDataRepo from "../api/masterData.js";
import ReviewArticleRepo from "../api/reviewArticle.js";
import ArticleRepository from "../api/article.js";
import moment from "moment";
import User from "../api/user.js";
import config from "../config/index";
import Constant from "../config/constant";

const defaultModalState = {
  add: false,
};
const requiredError = "This field can't blank";
const defaultInputErrors = {
  CategoryName: "",
  categoryCode: "",
};

export default {
  name: "review-article",
  data() {
    return {
      data: [],
      selectedItem: null,
      checkReviewed: false,
      viewData: [],
      categoryList: [],
      showModal: { ...defaultModalState },
      reviewArticleListTable: [],
      statusList: [],
      articleTitleSearch: "",
      articleCategorySearch: undefined,
      articleAuthorSearch: "",
      articleStatusSearch: undefined,
      loading: false,
      totals: 0,
      current: 1,
      contentId: "",
      paginationRange: [0, 0],
      pageSize: 10,
      errors: {
        approve: "",
        note: "",
      },

      routes: [
        {
          path: "",
          breadcrumbName: "Home",
        },
        {
          path: "review-category",
          breadcrumbName: "Review Category",
        },
      ],
      columns: [
        {
          title: "NO",
          width: 100,
          dataIndex: "index",
          key: "index",
          fixed: "left",
        },
        {
          title: "TITLE",
          width: 260,
          dataIndex: "title",
          key: "title",
        },
        {
          title: "CATEGORY NAME",
          width: 100,
          dataIndex: "categoryName",
          key: "categoryName",
        },
        {
          title: "AUTHOR",
          width: 100,
          key: "account",
          dataIndex: "account",
        },
        // {
        //   title: "CREATED DATE",
        //   width: 120,
        //   // dataIndex: "CreateDate",
        //   scopedSlots: { customRender: "createDateCustom" },
        //   key: "createDate",
        // },
        // {
        //   title: "UPDATED DATE",
        //   width: 120,
        //   // dataIndex: "UpdateDate",
        //   scopedSlots: { customRender: "updateDateCustom" },
        //   key: "updateDate",
        // },
        {
          title: "Status",
          width: 60,
          key: "statusName",
          dataIndex: "statusName",
        },
        {
          title: "Review by",
          width: 100,
          key: "reviewer",
          dataIndex: "reviewer",
        },
        // {
        //   title: "ACTIVE",
        //   width: 70,
        //   key: "deleteFlag",
        //   scopedSlots: { customRender: "deleteFlag" },
        // },
        // {
        //   title: "Original Article",
        //   width: 120,
        //   scopedSlots: { customRender: "originalArticleCustom" },
        //   key: "originalArticleID",
        // },
        {
          title: "ACTION",
          key: "operation",
          fixed: "right",
          width: 120,
          scopedSlots: { customRender: "action" },
        },
      ],
    };
  },
  beforeCreate() {
    this.formReview = this.$form.createForm(this, { name: "validate_other" });
  },
  created() {
    this.fetchStatus();
    this.fetchCategoryList();
  },
  methods: {
    redirectToOriginalArticle(originalArticleID) {
      this.loading = true;
      let routeData = this.$router.resolve({
        path: `/original-article-detail/${originalArticleID}`,
      });
      window.open(routeData.href, "_blank");
      this.loading = false;
    },
    checkDisableBtnReview(item) {
      if (
        item.reviewer === this.$cookies.get("account") ||
        item.reviewer === null
      ) {
        return false;
      } else {
        return true;
      }
    },
    async handleReviewItemBtnClick(item) {
      this.loading = true;
      this.selectedItem = item;
      this.formReview.note = "";
      const account = Vue.$cookies.get("account");
      var formAdd = {
        articleId: item.newID,
        account: account,
      };

      ReviewArticleRepo.getArticleReviewed(item.newID).then((res) => {
        if (res.data.success) {
          if (res.data.data.items[0].reviewer === account) {
            ArticleRepository.updateStatusArticle(item.newID, "reviewing", account).then(
              (res) => {
                if (res.data.data) {
                  ReviewArticleRepo.addReviewArticle(formAdd).then(
                    (response) => {
                      if (response.data.success) {
                        this.$router.push(
                          "/admin-action/EditArticleByReview/" + item.newID
                        );
                        if (window.location.href.includes("/admin-action")) {
                          window.location.reload();
                        }
                        // this.$router.push("/edit-article-by-reviewer/" + item.newID);
                      } else {
                        this.$notification.error({
                          message: "This article has been reviewed!",
                        });
                        this.loading = false;
                      }
                    }
                  );
                } else {
                  this.$notification.error({
                    message: res.data.message,
                  });
                  this.loading = false;
                }
              }
            );
          } else {
            this.$notification.error({
              message: "This article has been reviewed!",
            });
            this.loading = false;
          }
        } else {
          ArticleRepository.updateStatusArticle(item.newID, "reviewing", account).then(
            (res) => {
              if (res.data.data) {
                ReviewArticleRepo.addReviewArticle(formAdd).then((response) => {
                  if (response.data.success) {
                    this.$router.push(
                      "/admin-action/EditArticleByReview/" + item.newID
                    );
                    if (window.location.href.includes("/admin-action")) {
                      window.location.reload();
                    }
                    // this.$router.push("/edit-article-by-reviewer/" + item.newID);
                  } else {
                    this.$notification.error({
                      message: "This article has been reviewed!",
                    });
                    this.loading = false;
                  }
                });
              } else {
                this.$notification.error({
                  message: res.data.message,
                });
                this.loading = false;
              }
            }
          );
        }
      });

      // this.showModal = {
      //   ...defaultModalState,
      //   add: true,
      // };
    },
    validate() {
      let isValid = true;
      if (this.formReview.approve == "" || this.formReview.approve == null) {
        this.errors.approve = requiredError;
        isValid = false;
      }
      if (this.formReview.note == "" || this.formReview.note == null) {
        this.errors.note = requiredError;
        isValid = false;
      }
      return isValid;
    },
    closeReviewForm() {
      this.contentId = "";
      this.formReview.note = "";
      this.showModal = {
        ...defaultModalState,
        add: false,
      };
    },
    submitReviewForm(confirm) {
      this.loading = true;
      let errors;
      let formValue;
      let confirms = confirm;
      this.formReview.validateFields((err, values) => {
        errors = err;
        formValue = values;
      });
      if (errors) return;
      var formAdd = {
        articleId: this.selectedItem.id,
        account: Vue.$cookies.get("account"),
        confirm: confirm,
        note: formValue.note,
      };
      ReviewArticleRepo.addReviewArticle(formAdd).then((response) => {
        if (response.data.success === true) {
          if (confirms === true || confirms === "true") {
            this.$notification.success({
              message: "Approve successfully!",
            });
          } else {
            this.$notification.success({
              message: "Reject successfully!",
            });
          }
        } else {
          if (response.data.message !== null || response.data.message !== "") {
            this.$notification.error({
              message: response.data.message,
            });
            this.loading = false;
          } else {
            this.$notification.error({
              message: "Review fail!",
            });
            this.loading = false;
          }
        }
        this.formReview.resetFields();
        this.closeReviewForm();
      });
    },
    fetchStatus() {
      MasterDataRepo.searchByType(Constant.MASTER_DATA_ARTICLE_STATUS, 1).then(
        (res) => {
          this.statusList = res.data.data.items;

          for (var i = 0; i < this.statusList.length; i++) {
            if (
              this.statusList[i].code === "DRAFT" ||
              this.statusList[i].code === "DELETED"
            ) {
              this.statusList.splice(i, 1);
            }
          }

          this.loading = false;
        }
      );
    },
    fetchCategoryList() {
      const categoryName =
        this.articleCategorySearch === null ||
        this.articleCategorySearch === undefined
          ? ""
          : this.articleCategorySearch;
      let account = Vue.$cookies.get("account");
      const rolename = Vue.$cookies.get("roleCode");
      if(rolename === 'editor') {
        account = ''
      }
      Category.searchCategoryAndManager(categoryName, account, 1).then(
        (res) => {
          this.categoryList = res.data.data.items;
          this.loading = false;
        }
      );
    },
    generateTime(dateNumber) {
      const date = new Date(dateNumber);
      const m = moment(date);
      return m.isValid() ? m.format("DD-MM-YYYY") : "";
    },
    clearAllDataSearch() {
      this.articleTitleSearch = null;
      this.articleCategorySearch = undefined;
      this.articleAuthorSearch = null;
      this.articleStatusSearch = undefined;
      this.submitForm();
    },
    submitForm() {
      this.loading = true;
      this.current = 1;

      this.articleTitleSearch = 
      this.articleTitleSearch === "" ||
          this.articleTitleSearch === null ||
          this.articleTitleSearch === undefined
            ? ""
            : this.articleTitleSearch.replace(/\s+/g, " ").trim()
      this.articleAuthorSearch = this.articleAuthorSearch === "" ||
          this.articleAuthorSearch === null ||
          this.articleAuthorSearch === undefined
            ? ""
            : this.articleAuthorSearch.replace(/\s+/g, " ").trim()
      var formSearch = {
        title:
          this.articleTitleSearch === "" ||
          this.articleTitleSearch === null ||
          this.articleTitleSearch === undefined
            ? ""
            : this.articleTitleSearch,
        categoryID:
          this.articleCategorySearch === "" ||
          this.articleCategorySearch === null ||
          this.articleCategorySearch === undefined
            ? 0
            : this.articleCategorySearch,
        account:
          this.articleAuthorSearch === "" ||
          this.articleAuthorSearch === null ||
          this.articleAuthorSearch === undefined
            ? ""
            : this.articleAuthorSearch,
        reviewer: Vue.$cookies.get("account"),
        statusID:
          this.articleStatusSearch === "" ||
          this.articleStatusSearch === null ||
          this.articleStatusSearch === undefined
            ? 0
            : this.articleStatusSearch,
        roleCode: this.$cookies.get("roleCode"),
      };
      ReviewArticleRepo.searchReviewArticle(formSearch, this.current).then(
        (res) => {
          if (res.data.data !== null) {
            this.reviewArticleListTable = res.data.data.items;
            this.totals = res.data.data.totals;
            this.loading = false;
          } else {
            this.reviewArticleListTable = null;
            this.totals = 0;
            this.loading = false;
          }
        }
      );
    },

    paginate(current = 1) {
      this.current = current;
      this.loading = true;
      this.articleTitleSearch = 
      this.articleTitleSearch === "" ||
          this.articleTitleSearch === null ||
          this.articleTitleSearch === undefined
            ? ""
            : this.articleTitleSearch.replace(/\s+/g, " ").trim()
      this.articleAuthorSearch = this.articleAuthorSearch === "" ||
          this.articleAuthorSearch === null ||
          this.articleAuthorSearch === undefined
            ? ""
            : this.articleAuthorSearch.replace(/\s+/g, " ").trim()
      var formSearch = {
        title:
          this.articleTitleSearch === null ||
          this.articleTitleSearch === undefined
            ? ""
            : this.articleTitleSearch,
        categoryID:
          this.articleCategorySearch === null ||
          this.articleCategorySearch === undefined
            ? -1
            : this.articleCategorySearch,
        account:
          this.articleAuthorSearch === null ||
          this.articleAuthorSearch === undefined
            ? ""
            : this.articleAuthorSearch,
        statusID:
          this.articleStatusSearch === "" ||
          this.articleStatusSearch === undefined
            ? -1
            : this.articleStatusSearch,
        roleCode: this.$cookies.get("roleCode"),
      };
      ReviewArticleRepo.searchReviewArticle(formSearch, this.current).then(
        (res) => {
          this.reviewArticleListTable = res.data.data.items;
          this.totals = res.data.data.totals;
          this.loading = false;
        }
      );
    },
  },
};
</script>
<style scoped>
.container {
  max-width: 1430px;
}
#components-layout-demo-top .logo {
  width: 120px;
  height: 31px;
  background: rgba(255, 255, 255, 0.2);
  margin: 16px 24px 16px 0;
  float: left;
}
.gutter-example {
  padding-bottom: 40px;
}
.screen-header {
  border-top: 4px solid #fc5730;
}

.filter-select {
  width: 200px;
}

.ant-page-header {
  padding: 20px 0px;
}
.section-profile-cover {
  background: linear-gradient(
    83deg,
    rgba(131, 58, 180, 1) 0%,
    rgba(253, 29, 29, 1) 41%,
    rgba(252, 148, 69, 1) 100%
  );
}
</style>

<style >
.ant-page-header-heading-title {
  font-size: 33px !important;
}
.ant-page-header-heading {
  padding-top: 10px;
}
.pagnigation-custom {
  float: right;
}
.group-button-custom {
  position: absolute;
  right: 0;
  padding-right: 0px !important;
}
.group-button-custom2 {
  position: absolute;
  right: 0;
  padding-right: 0px !important;
  padding-left: 0px !important;
}
.ant-select-selection {
  box-shadow: 0 2px 3px rgb(50 50 93 / 15%), 0 1px 0 rgb(0 0 0 / 2%) !important;
  border-color: transparent !important;
  border-radius: 20px !important;

  transition: box-shadow 0.3s ease !important;
}
.ant-select-selection:focus {
  box-shadow: 0 2px 3px rgb(50 50 93 / 43%), 0 1px 0 rgb(0 0 0 / 2%) !important;
  -webkit-box-shadow: 0 2px 3px rgb(50 50 93 / 43%), 0 1px 0 rgb(0 0 0 / 2%) !important;
  border-color: transparent !important;
}
.ant-select-selection:hover {
  box-shadow: 0 2px 3px rgb(50 50 93 / 43%), 0 1px 0 rgb(0 0 0 / 2%) !important;
  -webkit-box-shadow: 0 2px 3px rgb(50 50 93 / 43%), 0 1px 0 rgb(0 0 0 / 2%) !important;
  border-color: transparent !important;
}
.ant-input {
  box-shadow: 0 2px 3px rgb(50 50 93 / 15%), 0 1px 0 rgb(0 0 0 / 2%) !important;
  border-color: transparent !important;
  border-radius: 20px !important;

  transition: box-shadow 0.3s ease !important;
}
.ant-input:focus {
  box-shadow: 0 2px 3px rgb(50 50 93 / 43%), 0 1px 0 rgb(0 0 0 / 2%) !important;
  -webkit-box-shadow: 0 2px 3px rgb(50 50 93 / 43%), 0 1px 0 rgb(0 0 0 / 2%) !important;
  border-color: transparent !important ;
}
.ant-input:hover {
  box-shadow: 0 2px 3px rgb(50 50 93 / 43%), 0 1px 0 rgb(0 0 0 / 2%);
  -webkit-box-shadow: 0 2px 3px rgb(50 50 93 / 43%), 0 1px 0 rgb(0 0 0 / 2%) !important;
  border-color: transparent !important;
}
</style>