<template>
  <div class="profile-page">
    <section class="section-profile-cover section-shaped my-0">
      <div class="container-fluid">
        <h4 class="text-white screen-name">MY ARTICLE</h4>
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
                    <a-input
                      v-model="articleFormSearch.title"
                      placeholder="Input title..."
                    >
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
                    <label><b>Category: </b></label>
                    <br />
                    <a-select
                      v-model="articleFormSearch.categoryID"
                      show-arrow
                      allow-clear
                      show-search
                      :filter-option="false"
                      style="width: 100%"
                      placeholder="All"
                      @search="fetchCategory"
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
                    <label><b>Status: </b></label>
                    <br />
                    <a-select
                      v-model="articleFormSearch.status"
                      allow-clear
                      style="width: 100%"
                      default-value=""
                      placeholder="All"
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
                </a-col>

                <a-col
                  class="gutter-row mt-10"
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
                        type="primary"
                        size="md"
                        :disabled="loading"
                        class="mr-4 btn-neutral btn-filter main-btn"
                      >
                        <i class="fa fa-search"></i>
                        Search
                      </base-button>
                    </span>
                  </div>
                </a-col>

                <a-col
                  class="gutter-row mt-10"
                  :span="2"
                  :col="2"
                  :xs="12"
                  :sm="5"
                  :md="4"
                  :lg="3"
                >
                  <div class="gutter-box">
                    <br />
                    <span @click="resetFilter()">
                      <base-button
                        type="primary"
                        size="md"
                        :disabled="loading"
                        class="mr-4 btn-neutral btn-filter main-btn"
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
                  :data-source="articleListTable"
                  :scroll="{ x: 1000 }"
                  :pagination="false"
                >
                  <template #titleCustom="item">
                    <router-link
                      :to="{
                        name: 'ArticleDetail',
                        params: {
                          id: item.id,
                        },
                      }"
                    >
                      {{ item.title }}
                    </router-link>
                  </template>

                  <template #manager="item">
                    <a-popover title="Manager list" trigger="hover">
                      <template slot="content">
                        <p>{{ item.account }}</p>
                        <p>Content</p>
                      </template>
                      <a-button type="primary"> Hover me </a-button>
                    </a-popover>
                  </template>

                  <template #updateDateCustom="item">
                    <span>
                      {{ generateTime(item.modifiedTime) }}
                    </span>
                  </template>

                  <template #originalArticleCustom="item">
                    <a-button
                      v-if="item.originalArticleID != 0"
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
                  </template>

                  <!-- <a slot="action" slot-scope="text">action</a> -->
                  <template #action="item">
                    <a-dropdown :disabled="checkShowBtnAction(item)">
                      <a-menu slot="overlay">
                        <a-menu-item
                          key="1"
                          @click="handleEditItemBtnClick(item)"
                        >
                          Edit
                        </a-menu-item>

                        <a-menu-item key="2">
                          <a-popconfirm
                            placement="topLeft"
                            ok-text="Yes"
                            cancel-text="No"
                            @confirm="deleteSubItemBtnClick(item)"
                          >
                            <template slot="title">
                              <p>Are you sure delete article</p>
                              <p>"{{ item.title }}"</p>
                            </template>
                            Delete
                          </a-popconfirm>
                        </a-menu-item>
                      </a-menu>
                      <a-button style="margin-left: 8px">
                        Action <a-icon type="down" />
                      </a-button>
                    </a-dropdown>
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
          </div>
        </card>
      </div>
    </section>
  </div>
</template>
<script>
import Category from "../api/category.js";
import ArticleRepo from "../api/article.js";
import moment from "moment";
import MasterDataRepo from "../api/masterData.js";
import User from "../api/user.js";
import config from "../config/index";
import Constant from "../config/constant";

const requiredError = "This field can't blank";

const defaultForm = {
  CategoryID: "",
  CategoryName: "",
  account: "",
  ActiveFlag: "",
};
const defaultSearchForm = {
  title: undefined,
  categoryID: undefined,
  status: undefined,
};

export default {
  name: "my-article",
  data() {
    return {
      data: [],
      selectedItem: null,
      viewData: [],
      articleFormSearch: { ...defaultSearchForm },
      categoryNameSearch: "",
      managerCategorySearch: "",
      editForm: { ...defaultForm },
      statusList: [],
      categoryList: [],
      loading: false,
      totals: 0,
      current: 1,
      contentId: "",
      paginationRange: [0, 0],
      pageSize: 10,
      articleListTable: [],
      errors: {
        categoryName: "",
      },
      routes: [
        {
          path: "",
          breadcrumbName: "Home",
        },
        {
          path: "category-management",
          breadcrumbName: "Category Management",
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
          title: "Title",
          width: 180,
          // dataIndex: "title",
          scopedSlots: { customRender: "titleCustom" },
          key: "title",
        },
        {
          title: "Type",
          width: 100,
          dataIndex: "categoryName",
          key: "categoryName",
        },
        {
          title: "Status",
          width: 100,
          key: "statusName",
          dataIndex: "statusName",
        },
        {
          title: "Modified Time",
          width: 120,
          scopedSlots: { customRender: "updateDateCustom" },
          key: "updateDate",
        },
        {
          title: "Modified By",
          width: 120,
          key: "modifiedBy",
          dataIndex: "modifiedBy",
        },
        {
          title: "Original Article",
          width: 120,
          scopedSlots: { customRender: "originalArticleCustom" },
          key: "originalArticleID",
        },

        {
          title: "Action",
          key: "operation",
          fixed: "right",
          width: 120,
          scopedSlots: { customRender: "action" },
        },
      ],
      username: "",
    };
  },
  beforeCreate() {
    this.form = this.$form.createForm(this, { name: "validate_other" });
  },
  created() {
    this.username = this.$cookies.get("account")
  },
  mounted() {
    this.fetchCategory("");
    this.fetchStatus();
  },
  methods: {
    checkShowBtnAction(item) {
      if(item.statusName === 'Draft' || item.statusName === 'Rejected') {
        return false;
      }
      if (item.statusName === 'Editing') {
        if(item.modifiedBy === this.username) {
          return false;
        } else {
          return true;
        }
      } 
      return true;
    },
    generateTime(dateNumber) {
      const date = new Date(dateNumber);
      const m = moment(date);
      return m.isValid() ? m.format("DD-MM-YYYY") : "";
    },
    fetchStatus() {
      MasterDataRepo.searchByType(Constant.MASTER_DATA_ARTICLE_STATUS, 1).then(
        (res) => {
          this.statusList = res.data.data.items;
          // this.totals = res.data.data.totals;
          this.loading = false;
        }
      );
    },
    redirectToOriginalArticle(originalArticleID) {
      this.loading = true;
      let routeData = this.$router.resolve({
        path: `/original-article-detail/${originalArticleID}`,
      });
      window.open(routeData.href, "_blank");
      this.loading = false;
    },
    async handleEditItemBtnClick(item) {
      this.selectedItem = item;
      this.$router.push("/admin-action/EditArticle/" + item.id);
      if (window.location.href.includes("/admin-action")) {
        window.location.reload();
      }
      // this.$router.push("edit-article/" + item.id);
    },
    async deleteSubItemBtnClick(item) {
      this.selectedItem = item;
      await ArticleRepo.deleteMyArticle(item.id)
        .then((res) => {
          if (res.data.data === true) {
            this.$notification.success({
              message: "Delete Article successful",
            });
            this.paginate();
          } else {
            this.$notification.error({
              message: "Delete Article fail",
            });
          }
        })
        .catch((e) => {
          this.$notification.error({
            message: e.response.data.message,
          });
        });
    },
    fetchCategory(value) {
      Category.searchCategory(value, 1).then((res) => {
        this.categoryList = res.data.data.items;
        // this.totals = res.data.data.totals;
        this.loading = false;
      });
    },
    validate() {
      let isValid = true;
      if (
        this.editForm.categoryName == "" ||
        this.editForm.categoryName == null
      ) {
        this.errors.categoryName = requiredError;
        isValid = false;
      }
      return isValid;
    },

    submitForm() {
      this.loading = true;
      this.current = 1;
      this.articleFormSearch.title =
        this.articleFormSearch.title === "" ||
        this.articleFormSearch.title === undefined ||
        this.articleFormSearch.title === null
          ? ""
          : this.articleFormSearch.title.replace(/\s+/g, " ").trim()
      var data = {
        title:
          this.articleFormSearch.title === "" ||
          this.articleFormSearch.title === undefined
            ? ""
            : this.articleFormSearch.title,
        categoryID:
          this.articleFormSearch.categoryID === "" ||
          this.articleFormSearch.categoryID === undefined
            ? -1
            : this.articleFormSearch.categoryID,
        status:
          this.articleFormSearch.status === "" ||
          this.articleFormSearch.status === undefined
            ? -1
            : this.articleFormSearch.status,
        account: this.$cookies.get("account"),
      };
      ArticleRepo.searchMyArticle(data, this.current).then((res) => {
        this.articleListTable = res.data.data.items;
        this.totals = res.data.data.totals;
        this.loading = false;
      });
      this.resetsearchForm();
    },
    resetFilter() {
      this.loading = true;
      this.current = 1;
      this.articleFormSearch.title = undefined;
      this.articleFormSearch.categoryID = undefined;
      this.articleFormSearch.status = undefined;
      var data = {
        title: "",
        categoryID: -1,
        status: -1,
        account: this.$cookies.get("account"),
      };
      ArticleRepo.searchMyArticle(data, this.current).then((res) => {
        this.articleListTable = res.data.data.items;
        this.totals = res.data.data.totals;
        this.loading = false;
      });
    },
    resetsearchForm() {
      this.articleFormSearch.title === "" ||
      this.articleFormSearch.title === undefined
        ? undefined
        : this.articleFormSearch.title;

      this.articleFormSearch.categoryID === "" ||
      this.articleFormSearch.categoryID === undefined
        ? undefined
        : this.articleFormSearch.categoryID;

      this.articleFormSearch.status === "" ||
      this.articleFormSearch.status === undefined
        ? undefined
        : this.articleFormSearch.status;
    },
    paginate(current = 1) {
      this.current = current;
      this.loading = true;
      this.articleFormSearch.title =
        this.articleFormSearch.title === "" ||
        this.articleFormSearch.title === undefined ||
        this.articleFormSearch.title === null
          ? ""
          : this.articleFormSearch.title.replace(/\s+/g, " ").trim()
      var data = {
        title:
          this.articleFormSearch.title === "" ||
          this.articleFormSearch.title === undefined
            ? ""
            : this.articleFormSearch.title,
        categoryID:
          this.articleFormSearch.categoryID === "" ||
          this.articleFormSearch.categoryID === undefined
            ? -1
            : this.articleFormSearch.categoryID,
        status:
          this.articleFormSearch.status === "" ||
          this.articleFormSearch.status === undefined
            ? -1
            : this.articleFormSearch.status,
        account: this.$cookies.get("account"),
      };
      ArticleRepo.searchMyArticle(data, this.current).then((res) => {
        this.articleListTable = res.data.data.items;
        this.totals = res.data.data.totals;
        this.loading = false;
      });
    },
  },
};
</script>
<style scoped>
.section-profile-cover {
  background: linear-gradient(
    83deg,
    rgba(131, 58, 180, 1) 0%,
    rgba(253, 29, 29, 1) 41%,
    rgba(252, 148, 69, 1) 100%
  );
}
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

.section-skew {
  margin-top: -15rem;
}
.ant-table-header-column {
  text-transform: uppercase;
}
</style>