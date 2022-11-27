<template>
  <div class="profile-page">
    <section class="section-profile-cover section-shaped my-0">
      <div class="container-fluid">
        <h4 class="text-white screen-name">Article Management</h4>
      </div>
    </section>
    <section class="section section-skew">
      <div class="container">
        <card shadow class="card-profile mt--300" no-body>
          <div class="px-4">
            <div class="gutter-example">
              <!-- <a-page-header
                style="border: 1px solid rgb(235, 237, 240); margin-top: 10px"
                title="Article Management"
                :breadcrumb="{ props: { routes } }"
                sub-title="This is a subtitle"
              /> -->
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
                  :lg="4"
                >
                  <div class="gutter-box">
                    <label><b>Title: </b></label>
                    <a-input v-model="articleFormSearch.title"> </a-input>
                  </div>
                </a-col>

                <a-col
                  class="gutter-row mt-10"
                  :span="4"
                  :col="4"
                  :xs="24"
                  :sm="12"
                  :md="12"
                  :lg="4"
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
                  :lg="4"
                >
                  <div class="gutter-box">
                    <label><b>Author: </b></label>
                    <a-input v-model="articleFormSearch.author"> </a-input>
                  </div>
                </a-col>

                <a-col
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
                    <span @click="resetFilter()">
                      <base-button 
                        class="btn btn-neutral btn-filter main-btn mr-4"
                        type="primary" 
                        size="md" 
                        :disabled="loading"
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
                  </template>

                  <template #numberReportCustom="item">
                    <a-button
                      v-if="
                        item.numberReport !== 0 &&
                        item.numberReport !== null &&
                        item.numberReport !== undefined
                      "
                      type="primary"
                      shape="round"
                      size="small"
                      :loading="loading"
                      @click="showReportModal(item)"
                    >
                      <span v-if="item.numberReport > 1"> See {{ item.numberReport }} reports </span>
                      <span v-else> See {{ item.numberReport }} report </span>
                    </a-button>
                    <a-button
                      v-else
                      type="danger"
                      shape="round"
                      size="small"
                      disabled
                    >
                      No report
                    </a-button>
                  </template>

                  <!-- <a slot="action" slot-scope="text">action</a> -->
                  <template #action="item">
                    <a-dropdown :trigger="['click']" :disabled="checkShowActionBtn(item) ">
                      <a-button style="margin-left: 8px" @click="e => e.preventDefault()">
                        Action <a-icon type="down" />
                      </a-button>
                      
                      <a-menu slot="overlay">
                        <a-menu-item
                          key="1"
                          @click="handleEditItemBtnClick(item)"
                        >
                          Edit
                        </a-menu-item>

                        <a-menu-item key="2" v-if="checkShowEditorAction">
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

                        <a-menu-item key="3" v-if="item.statusName === 'Posted' && checkShowEditorAction">
                          <a-popconfirm
                            placement="topLeft"
                            ok-text="Yes"
                            cancel-text="No"
                            @confirm="hideItemBtnClick(item)"
                          >
                            <template slot="title">
                              <p>Are you sure hide article</p>
                              <p>"{{ item.title }}"</p>
                            </template>
                            Hide
                          </a-popconfirm>
                        </a-menu-item>

                        <a-menu-item key="3" v-if="item.statusName === 'Hiding' && checkShowEditorAction">
                          <a-popconfirm
                            placement="topLeft"
                            ok-text="Yes"
                            cancel-text="No"
                            @confirm="unhideItemBtnClick(item)"
                          >
                            <template slot="title">
                              <p>Are you sure unhide article</p>
                              <p>"{{ item.title }}"</p>
                            </template>
                            Unhide
                          </a-popconfirm>
                        </a-menu-item>

                        <a-menu-item key="4" v-if="item.statusName === 'Posted' && checkShowEditorAction && !item.pinned">
                          <a-popconfirm
                            placement="topLeft"
                            ok-text="Yes"
                            cancel-text="No"
                            @confirm="pinItemBtnClick(item)"
                          >
                            <template slot="title">
                              <span>
                                <p>Are you sure pin this article <br>
                                  on top in Home page</p>
                              </span> 
                            </template>
                            Pin
                          </a-popconfirm>
                        </a-menu-item>

                        <a-menu-item key="5" v-if="item.pinned">
                          <a-popconfirm
                            placement="topLeft"
                            ok-text="Yes"
                            cancel-text="No"
                            @confirm="unpinItemBtnClick(item)"
                          >
                            <template slot="title">
                              <p>Are you sure to unpin this article </p>
                            </template>
                            Unpin
                          </a-popconfirm>
                        </a-menu-item>
                      </a-menu>
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
      <!-- Modal Report  -->
      <a-modal
        :width="1000"
        v-model="reportModal"
        title="Report List"
        :footer="null"
        :maskClosable="false"
        @cancel="closeAddForm()"
      >
        <a-spin :spinning="loading">
          <a-icon
            type="loading"
            slot="indicator"
            style="font-size: 24px"
            spin
          />
          <div :style="{ background: '#fff' }">
            <a-table
              :columns="columnReport"
              :data-source="reasonReportList"
              :scroll="{ x: 1000 }"
              :pagination="false"
            >

              <template #createdTimeCustom="item">
                <span>
                  {{ generateTime(item.modifiedTime) }}
                </span>
              </template>
              <template #reasonNameCustom="item">
                <span v-if="item.reasonName == 'Other' && item.reasonOther">
                  {{ item.reasonOther }}
                </span> 
                <span v-else>
                  {{ item.reasonName }}
                </span> 
              </template>
            </a-table>
            
            <div class="pagging-in-modal">
              <a-pagination
                show-quick-jumper
                :default-current="1"
                :current="currentReason"
                :total="totalReasonReport"
                @change="paginateReason"
              />
            </div>
          </div>
        </a-spin>
      </a-modal>
    </section>
  </div>
</template>
<script>
import Category from "../api/category.js";
import ArticleRepo from "../api/article.js";
import DenounceArticleRepository from "../api/denounce.js";
import moment from "moment";
import MasterDataRepo from "../api/masterData.js";
import Constant from "../config/constant";

const requiredError = "This field can't blank";

const defaultForm = {
  CategoryID: "",
  CategoryName: "",
  account: "",
  ActiveFlag: "",
};
const defaultSearchForm = {
  title: "",
  categoryID: undefined,
  status: undefined,
};

export default {
  name: "Article Management",
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
          path: "article-management",
          breadcrumbName: "Article Management",
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
          title: "Author",
          width: 100,
          dataIndex: "author",
          key: "author",
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
          title: "Total Report",
          width: 80,
          key: "numberReport",
          scopedSlots: { customRender: "numberReportCustom" },
        },
        {
          title: "Action",
          key: "operation",
          fixed: "right",
          width: 120,
          scopedSlots: { customRender: "action" },
        },
      ],
      columnReport: [
        {
          title: "Index",
          width: 80,
          dataIndex: "index",
          key: "index",
          fixed: "left",
        },
        {
          title: "Reporter",
          width: 80,
          dataIndex: "createdBy",
          key: "createdBy",
        },
        {
          title: "Created Time",
          width: 80,
          key: "createdTime",
          scopedSlots: { customRender: "createdTimeCustom" },
        },
        {
          title: "Reason Report",
          width: 140,
          key: "reasonName",
          scopedSlots: { customRender: "reasonNameCustom" },
        },
      ],
      reportModal: false,
      reasonReportList: [],
      totalReasonReport: 0,
      currentReason: 1,
      selectedItemID: null,
      checkShowEditorAction: false,
      pinnedArticle: [],
      isPinned: false,
      confirmText: '',
    };
  },
  beforeCreate() {
    this.form = this.$form.createForm(this, { name: "validate_other" });
  },
  created() {
    
    for (let i = 0; i < 100; i++) {
      this.data.push({
        key: i,
        name: `Edrward ${i}`,
        age: 32,
        address: `London Park no. ${i}`,
      });
    }
    
    this.checkEditorAction();
  },
  mounted() {
    this.fetchCategory("");
    this.fetchStatus();
    // this.checkHavePinArticle();
  },
  methods: {
    // async checkHavePinArticle() {
    //   await ArticleRepo.getPinnedArticle().then(res => {
    //     if(res.data.data.items) {
    //       this.pinnedArticle = res.data.data.items
    //     }
    //   })
    // },
    // async getPinnedArticle(item) {
    //   await ArticleRepo.getPinnedArticleByCategory(item.categoryID).then(res => {
    //     if(res.data.data.items) {
    //       this.pinnedArticle = res.data.data.items
    //       if(this.pinnedArticle.length === 0) {
    //         this.confirmText = 'Are you sure pin article ' + item.title + ' on top in Home page'
    //       } else {
    //         this.confirmText = 'Are you sure unpin article ' + this.pinnedArticle[0].title + ' and pin article ' + item.title + ' on top in Home page'
    //       }
    //     }
    //   })
    // },
    checkShowActionBtn(item) {
      const username = this.$cookies.get("account");
      console.log('item editing ===> ', item);
      console.log('username editing ===> ', username);
      if(item.statusName === 'Editing') {
        if(item.modifiedBy === username) {
          return false;
        } else {
          return true;
        }
      } 
      if(item.statusName === 'Reviewing') {
        if(item.modifiedBy === username) {
          return false;
        } else {
          return true;
        }
      }
      if(item.statusName === 'Deleted') {
        return true;
      }
      return false;
    },
    checkEditorAction() {
      if(this.$cookies.get("roleCode") === "editor") {
        this.checkShowEditorAction = true
      }
    },
    generateTime(dateNumber) {
      const date = new Date(dateNumber);
      const m = moment(date);
      return m.isValid() ? m.format("DD-MM-YYYY") : "";
    },
    fetchStatus() {
      MasterDataRepo.searchByType(Constant.MASTER_DATA_ARTICLE_STATUS, 1).then(
        (res) => {
          if(this.$cookies.get("roleCode") === "moderator") {
            res.data.data.items.map(element => {
              if(element.lowerName === 'rejected' || element.lowerName === 'posted') {
                this.statusList.push(element);
              }
            });
          } else {
            res.data.data.items.map(element => {
              if(element.lowerName !== 'draft') {
                this.statusList.push(element);
              }
            })
          }
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
      this.loading = true;
      this.selectedItem = item;
      this.$router.push("/admin-action/EditArticle/" + item.id + "/true");
          if (window.location.href.includes("/admin-action")) {
        window.location.reload();
      }
      // this.$router.push("/edit-article/" + item.id + "/true");
    },
    async deleteSubItemBtnClick(item) {
      this.loading = true;
      this.selectedItem = item;
      await ArticleRepo.deleteMyArticle(item.id)
        .then((res) => {
          this.loading = false;
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
    async pinItemBtnClick(item) {
      this.loading = true;
      this.selectedItem = item;
      await ArticleRepo.updateStatusPinArticle(item.id, item.categoryID, true).then((res) => {
        if (res.data.data === true) {
          this.$notification.success({
            message: "Pin Article successful",
          });
          this.paginate();
          // this.checkHavePinArticle();
        } else {
          this.$notification.error({
            message: "Pin Article fail",
          });
          this.loading = false;
        }
      }).catch((e) => {
        this.$notification.error({
          message: e.response.data.message,
        });
      });
    },
    async unpinItemBtnClick(item) {
      this.loading = true;
      this.selectedItem = item;
      await ArticleRepo.updateStatusPinArticle(item.id, item.categoryID, false).then((res) => {
        if (res.data.data === true) {
          this.$notification.success({
            message: "Unpin Article successful",
          });
          this.paginate();
          // this.checkHavePinArticle();
        } else {
          this.$notification.error({
            message: "Unpin Article fail",
          });
          this.loading = false;
        }
      }).catch((e) => {
        this.$notification.error({
          message: e.response.data.message,
        });
      });
    },
    async hideItemBtnClick(item) {
      this.loading = true;
      this.selectedItem = item;
      await ArticleRepo.hideArticle(item.id)
        .then((res) => {
          this.loading = false;
          if (res.data.data === true) {
            this.$notification.success({
              message: "Hide Article successful",
            });
            this.paginate();
          } else {
            this.$notification.error({
              message: "Hide Article fail",
            });
          }
        })
        .catch((e) => {
          this.$notification.error({
            message: e.response.data.message,
          });
        });
    },
    async unhideItemBtnClick(item) {
      this.loading = true;
      this.selectedItem = item;
      await ArticleRepo.unhideArticle(item.id)
        .then((res) => {
          this.loading = false;
          if (res.data.data === true) {
            this.$notification.success({
              message: "Unhide Article successful",
            });
            this.paginate();
          } else {
            this.$notification.error({
              message: "Unhide Article fail",
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
      const roleName = this.$cookies.get("roleCode");
      const account = this.$cookies.get("account");
      if(roleName === 'editor') {
        Category.searchCategory(value, 1).then((res) => {
          this.categoryList = res.data.data.items;
          this.loading = false;
        });
      } else {
        Category.searchCategoryAndManager(value, account, 1).then(
          (res) => {
            this.categoryList = res.data.data.items;
            this.loading = false;
          }
        );
      }
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

    async submitForm() {
      this.loading = true;
      this.current = 1;
      this.articleFormSearch.title = this.articleFormSearch.title === "" ||
        this.articleFormSearch.title === undefined
          ? ""
          : this.articleFormSearch.title.replace(/\s+/g, " ").trim()
      this.articleFormSearch.author = this.articleFormSearch.author === "" ||
        this.articleFormSearch.author === undefined
          ? ""
          : this.articleFormSearch.author.replace(/\s+/g, " ").trim()

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
        account:
          this.articleFormSearch.author === "" ||
          this.articleFormSearch.author === undefined
            ? ""
            : this.articleFormSearch.author,
      };
      await ArticleRepo.searchArticleManagement(data, this.current).then((res) => {
        this.articleListTable = res.data.data.items;
        this.totals = res.data.data.totals;
        this.loading = false;
      }).catch((e) => {
        this.$notification.error({
          message: "Load data fail!",
        });
        this.loading = false;
      });
    },
    resetFilter() {
      this.loading = true;
      this.current = 1;
      this.articleFormSearch.title = "";
      this.articleFormSearch.categoryID = undefined;
      this.articleFormSearch.status = undefined;
      this.articleFormSearch.author = "";
      var data = {
        title: "",
        categoryID: -1,
        status: -1,
        author: "",
      };
      ArticleRepo.searchArticleManagement(data, this.current).then((res) => {
        this.articleListTable = res.data.data.items;
        this.totals = res.data.data.totals;
        this.loading = false;
      }).catch((e) => {
        this.$notification.error({
          message: e.response.data.message,
        });
        this.loading = false;
      });
    },

    paginate(current = 1) {
      this.loading = true;
      this.current = current;
      this.articleFormSearch.title = this.articleFormSearch.title === "" ||
        this.articleFormSearch.title === undefined
          ? ""
          : this.articleFormSearch.title.replace(/\s+/g, " ").trim()
      this.articleFormSearch.author = this.articleFormSearch.author === "" ||
        this.articleFormSearch.author === undefined
          ? ""
          : this.articleFormSearch.author.replace(/\s+/g, " ").trim()
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
        account:
          this.articleFormSearch.author === "" ||
          this.articleFormSearch.author === undefined
            ? ""
            : this.articleFormSearch.author,
      };
      ArticleRepo.searchArticleManagement(data, this.current).then((res) => {
        this.articleListTable = res.data.data.items;
        this.totals = res.data.data.totals;
        this.loading = false;
      }).catch((e) => {
        this.$notification.error({
          message: e.response.data.message,
        });
        this.loading = false;
      });
    },
    async showReportModal(item) {
      this.selectedItem = item;
      await this.getResonReportListOfArticle(item.id);
      this.reportModal = true;
    },
    getResonReportListOfArticle(id) {
      this.loading = true;
      this.selectedItemID = id;
      this.currentReason = 1;
      DenounceArticleRepository.getByArticleId(id, this.currentReason).then(res=>{
        this.reasonReportList = res.data.data.items;
        this.totalReasonReport = res.data.data.totals;
        this.loading = false;
      })
    },
    paginateReason(currentReason = 1) {
      this.loading = true;
      this.currentReason = currentReason;
      DenounceArticleRepository.getByArticleId(this.selectedItemID, this.currentReason).then((res) => {
        this.reasonReportList = res.data.data.items;
        this.totalReasonReport = res.data.data.totals;
        this.loading = false;
      });
    },
    closeAddForm() {
      this.reportModal = false;
      this.reasonReportList = [];
      this.totalReasonReport = 0;
    }
  },
};
</script>
<style scoped>
.section-profile-cover {
background: linear-gradient(83deg, rgba(131,58,180,1) 0%, rgba(253,29,29,1) 41%, rgba(252,148,69,1) 100%);
/* background: linear-gradient(208deg, rgba(242,112,33,1) 0%, rgba(243,112,34,1) 28%, rgba(244,112,32,0.30325633671437324) 88%); */
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
  border-top: 4px solid #FC5730;
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

.pagging-in-modal {
  margin-top: 20px;
  display: flex;
  justify-content: flex-end;
}

.group-button-custom {
  position: absolute;
  right: 0;
  padding-right: 0px !important;
}
</style>