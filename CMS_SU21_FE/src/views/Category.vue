<template>
   <div class="profile-page">
    <section class="section-profile-cover section-shaped my-0">
      <div class="container-fluid">
        <h4 class="text-white screen-name" >CATEGORY MANAGEMENT</h4>
        </div>
    </section>
    <section class="section section-skew">
      <div class="container ">
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
                :span="8"
           
              >
                <div class="gutter-box">
                  <label><b>Name: </b></label>
                  <a-input 
                    placeholder="Enter category name"
                    v-model="categoryNameSearch"
                    size="default"
                  >
                  </a-input>
                </div>
              </a-col>


              <a-col  :span="3" >   
                <br />
                <button @click="submitForm()" 
                  class="btn btn-neutral  btn-filter main-btn"
                  size="sm"
                  :disabled="loading"
                >
                  <i class="fa fa-search"></i>
                  Search
                </button>
              
              </a-col>

              <a-col  :span="3" > 
                <br />
                <button 
                  class="btn btn-neutral  btn-filter main-btn"
                  :disabled="loading"
                  @click="openAddForm(true)"
                >
                <i class="fa fa-plus-circle"></i>
                  Add
                </button>
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
            <div  :style="{ background: '#fff', minHeight: '280px' }">
              <a-table
                :columns="columns"
                :data-source="categoryListTable"
                :scroll="{ x: 1000}"
                :pagination="false"
              >
                <template #categoryNameCustom="item">
                  <router-link
                      :to="{
                        name: 'LandingByCategory',
                        params: {
                          categoryId: item.id,
                        },
                      }"
                    >
                      {{ item.categoryName }}
                    </router-link>
                </template>


                <template #ActiveFlag="item">
                  <a-button
                    disabled
                    class="cus-active-button"
                    v-if="item.deleted == 0"
                    type="primary"
                    shape="round"
                    size="small"
                  >
                    Active
                  </a-button>
                  <a-button
                    disabled
                    v-else
                    type="danger"
                    shape="round"
                    size="small"
                  >
                    InActive
                  </a-button>
                </template>

                <template #createDateCustom="item">
                  <span>{{ generateTime(item.createdTime) }}</span>
                </template>

                <template #updateDateCustom="item">
                  <span>{{ generateTime(item.modifiedTime) }}</span>
                </template>

                <!-- <a slot="action" slot-scope="text">action</a> -->
                <template #action="item">
                  <a-dropdown>
                    <a-menu slot="overlay" >
                      <a-menu-item 
                        key="1"
                        @click="handleEditItemBtnClick(item)"
                      > 
                        Edit
                      </a-menu-item>

                      <a-menu-item 
                        key="2"
                        v-if="articleListTable && item.totalArticle == 0"
                      > 
                        <a-popconfirm placement="leftBottom" ok-text="Yes" cancel-text="No" @confirm="deleteSubItemBtnClick(item)">
                          <template slot="title">
                            <span>Are you sure delete category</span><br>
                            <span>"{{ item.categoryName }}"</span>
                          </template>
                          Delete
                        </a-popconfirm>
                      </a-menu-item>
                    </a-menu>
                    <a-button> Action <a-icon type="down" /> </a-button>
                  </a-dropdown>
                </template>


                <!-- <template #action="item">
                  <a-button
                    type="default"
                    shape="round"
                    size="default"
                    @click="handleEditItemBtnClick(item)"
                  >
                    <a-icon @click="editCategoryBtn" type="edit" />
                  </a-button>
                </template> -->
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
              :width="700"
              note: formValue.note
              title="Add Category"
              v-model="showModal.add"
              @ok="submitCreateForm"
              :maskClosable="false"
              @cancel="closeAddForm()"
            >
              <a-form
                :form="form"
              >
                <a-form-item label="Category Name:" :label-col="{span: 8}" :wrapper-col="{ span: 14 }">
                  <a-input
                    v-decorator="['categoryName', { rules: [{ required: true, message: 'Please input this field!' }] }]"
                    style="width: 100%"
                  />
                </a-form-item>

                <!-- <a-form-item label="Category Code:" :label-col="{span: 8}" :wrapper-col="{ span: 14 }">
                  <a-input
                    v-decorator="['categoryCode', { rules: [{ required: true, message: 'Please input this field!' }] }]"
                    style="width: 100%"
                  />
                </a-form-item> -->

                <a-form-item label="Is Active:" :label-col="{span: 8}" :wrapper-col="{ span: 14 }">
                  <a-select
                    v-decorator="['deleted', { rules: [{ required: true, message: 'Please input this field!' }] }]"
                    style="width: 100%"
                    class="filter-select"
                  >
                    <a-select-option :value="0">
                      Active
                    </a-select-option>
                    <a-select-option :value="1">
                      InActive
                    </a-select-option>
                  </a-select>
                </a-form-item>

                <!-- <a-form-item label="Upload Avatar" :label-col="{span: 8}" :wrapper-col="{ span: 14 }">
                  <div class="dropbox">
                    <a-upload-dragger
                      v-decorator="[
                        'avatarImage',
                        {
                          valuePropName: 'fileList',
                          getValueFromEvent: normFile,
                          rules: [
                            {
                              required: false,
                              message: 'Please upload avatar!',
                            },
                          ],
                        },
                      ]"
                      name="file"
                      :action="introductionImageUpload"
                      @change="handleImageUploadChange"
                      :headers="headers"
                      accept="image/*"
                      :multiple="false"
                    >
                      <p class="ant-upload-drag-icon">
                        <a-icon type="inbox" />
                      </p>
                      <p class="ant-upload-text">
                        Click or drag file to this area to upload
                      </p>
                      <p class="ant-upload-hint">
                        Support for a single or bulk upload.
                      </p>
                    </a-upload-dragger>
                  </div>
                </a-form-item> -->

                <a-form-item label="Upload Background" :label-col="{span: 8}" :wrapper-col="{ span: 14 }">
                  <div class="dropbox">
                    <a-upload-dragger
                      v-decorator="[
                        'introductionImage',
                        {
                          valuePropName: 'fileList',
                          getValueFromEvent: normFile,
                          rules: [
                            {
                              required: true,
                              message: 'Please upload avatar!',
                            },
                          ],
                        },
                      ]"
                      name="file"
                      :action="avatarImageUpload"
                      @change="handleImageUploadChange"
                      :headers="headers"
                      accept="image/*"
                      :multiple="false"
                    >
                      <p class="ant-upload-drag-icon">
                        <a-icon type="inbox" />
                      </p>
                      <p class="ant-upload-text">
                        Click or drag file to this area to upload
                      </p>
                      <p class="ant-upload-hint">
                        Support for a single or bulk upload.
                      </p>
                    </a-upload-dragger>
                  </div>
                </a-form-item>

              </a-form>
            </a-modal>
          <!-- /add modal -->

          <!-- edit modal -->
            <a-modal
              title="Edit Category"
              v-model="showModal.edit"
              @ok="saveCategory"
              @cancel="closeEditForm()"
            >
              <a-row :gutter="[24,16]">
                <a-col :span="8">Category Name
                  <span class="red">*</span>
                </a-col>
                <a-col :span="16">
                  <a-textarea
                    v-model="editForm.categoryName"
                    :auto-size="{ minRows: 1, maxRows: 5 }"
                    :min="0"
                    class="full-width--i"
                  />
                  <span
                    v-if="errors.categoryName"
                    class="red"
                  >
                    {{ errors.categoryName }}
                  </span>
                </a-col>
              </a-row>

              <!-- <a-row :gutter="[24,16]">
                <a-col :span="8">Category Code
                  <span class="red">*</span>
                </a-col>
                <a-col :span="16">
                  <a-textarea
                    v-model="editForm.categoryCode"
                    :auto-size="{ minRows: 1, maxRows: 1 }"
                    :min="0"
                    class="full-width--i"
                  />
                  <span
                    v-if="errors.categoryCode"
                    class="red"
                  >
                    {{ errors.categoryCode }}
                  </span>
                </a-col>
              </a-row> -->

              <a-row :gutter="[24, 16]">
                <a-col :span="8">Upload Background:
                  <span class="red">*</span>
                </a-col>
                <a-col :span="16">
                  <a-upload
                    v-model="editForm.introductionImage"
                    name="file"
                    list-type="picture-card"
                    :action="introductionImageUpload"
                    :fileList="fileList"
                    @change="handleImageUploadChangeEdit"
                    @preview="handlePreviewUploadFile"
                    :headers="headers"
                    accept="image/*"
                    :multiple="false"
                  >
                    <div v-if="fileList.length < 1">
                      <a-icon type="plus" />
                      <div class="ant-upload-text">Upload</div>
                    </div>
                  </a-upload>
                  <span
                    v-if="errors.introductionImage"
                    class="red"
                  >
                    {{ errors.introductionImage }}
                  </span>
                  <span
                    v-if="errors.notEnoughQuality"
                    class="red"
                  >
                    {{ errors.notEnoughQuality }}
                  </span>
                  <a-modal
                    :visible="previewVisible"
                    :footer="null"
                    @cancel="handleCancel"
                  >
                    <img alt="example" style="width: 100%" :src="previewImage" />
                  </a-modal>
                </a-col>
              </a-row>

              <a-row :gutter="[24,16]">
                <a-col :span="8">
                  Status
                  <span class="red">*</span>
                </a-col>
                <a-col :span="16">
                  <a-select
                    v-model="editForm.deleted"
                    class="filter-select"
                    style="width: 100%"
                  >
                    <a-select-option
                      v-for="item in statusList"
                      :key="item.key"
                      :value="item.value"
                    >
                      {{ item.key }}
                    </a-select-option>
                  </a-select>
                </a-col>
              </a-row>
            </a-modal>
          </div>
        </card>
      </div>
    </section>
  </div>
</template>
<script>
import Category from "../api/category.js";
import ArticleRepository from "../api/article.js";
import moment from "moment";
import User from "../api/user.js";
import CONFIG from "../config/index";
import vue2Dropzone from "vue2-dropzone";
import Constant from "../config/constant";

const defaultModalState = {
  add: false,
  edit: false,
};
const requiredError = "This field can't blank";
const defaultInputErrors = {
  categoryName: "",
  // categoryCode: "",
  introductionImage: "",
  notEnoughQuality: "",
};

const defaultForm = {
  id: "",
  categoryName: "",
  // categoryCode: "",
  deleted: "",
};

export default {
  components: {
    vueDropzone: vue2Dropzone,
  },
  data() {
    return {
      data: [],
      selectedItem: null,
      viewData: [],
      categoryNameSearch: "",
      editForm: { ...defaultForm },
      statusList: [
        {
          key: "Active",
          value: 0,
        },
        {
          key: "InActive",
          value: 1,
        },
      ],

      introductionImageUpload: "",
      avatarImageUpload: "",
      editor: window.ClassicEditor,
      articleListTable: [],
      totalsArticle: 0,
      managerCategoryList: [],
      showModal: { ...defaultModalState },
      loading: false,
      totals: 1,
      current: 1,
      contentId: "",
      paginationRange: [0, 0],
      pageSize: 10,
      categoryList: [],
      categoryListTable: [],
      errors: {
        categoryName: "",
        // categoryCode: "",
        introductionImage: "",
        notEnoughQuality: "",
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
          title: "Category Name",
          width: 160,
          key: "categoryName",
          scopedSlots: { customRender: "categoryNameCustom" },
        },
        {
          title: "Status",
          width: 100,
          key: "deleted",
          scopedSlots: { customRender: "ActiveFlag" },
        },
        {
          title: "Create By",
          width: 140,
          dataIndex: "createdBy",
          key: "createdBy",
        },
        {
          title: "Create Date",
          width: 120,
          // dataIndex: "CreateDate",
          scopedSlots: { customRender: "createDateCustom" },
          key: "createdTime",
        },
        {
          title: "Modified By",
          width: 140,
          dataIndex: "modifiedBy",
          key: "modifiedBy",
        },
        {
          title: "Update Date",
          width: 120,
          // dataIndex: "UpdateDate",
          scopedSlots: { customRender: "updateDateCustom" },
          key: "modifiedTime",
        },
        {
          title: "Action",
          key: "operation",
          fixed: "right",
          width: 120,
          scopedSlots: { customRender: "action" },
        },
      ],
      fileList: [],
      previewImage: "",
      categoryIntroductionImage: `${CONFIG.apiUrl}/image/getImageThumb/{get-image-thumb}?id=`,
      previewVisible: false,
    };
  },
  beforeCreate() {
    this.form = this.$form.createForm(this, { name: "validate_other" });
    this.headers = {
      authorization: "Bearer " + this.$cookies.get("accessToken"),
      "X-Requested-With": "XMLHttpRequest",
    };
  },
  created() {
    this.introductionImageUpload = `${CONFIG.apiUrl}/image/uploadImage/{upload-image}?introductionImage=true`;
    this.avatarImageUpload = `${CONFIG.apiUrl}/image/uploadImage/{upload-image}?introductionImage=false`;
  },
  mounted() {},
  methods: {
    generateTime(dateNumber) {
      const date = new Date(dateNumber);
      const m = moment(date);
      return m.isValid() ? m.format("DD-MM-YYYY") : "";
    },
    deleteSubItemBtnClick(item) {
      this.selectedItem = item;
      Category.deleteCategory(item.id)
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
    async handleEditItemBtnClick(item) {
      this.selectedItem = item;
      this.viewData = {
        file: item.fileInfor,
        data: [
          {
            label: "Category Name",
            value: item.categoryName,
          },
          // {
          //   label: "Category Code",
          //   value: item.categoryCode,
          // },
          {
            label: "Category Manager",
            value: item.requester,
          },
          {
            label: "Status",
            value: item.deleted,
          },
        ],
      };
      this.editForm.id = item.id;
      this.editForm.categoryName = item.categoryName;
      // this.editForm.categoryCode = item.categoryCode;
      // this.editForm.account = item.account;
      this.editForm.deleted = item.deleted;
      this.editForm.introductionImage = item.introductionImage
      this.fileList = [
        {
          uid: item.introductionImage,
          name: "image.png",
          status: "done",
          url: this.categoryIntroductionImage + item.introductionImage,
        },
      ];
      this.showModal = {
        edit: true,
      };
    },
    async handlePreviewUploadFile(file) {
      this.previewImage = file.thumbUrl;
      if (this.article) {
        this.previewImage =
          this.categoryIntroductionImage + this.selectedItem.introductionImage;
      }
      this.previewVisible = true;
    },
    
    handleCancel() {
      this.previewVisible = false;
    },
    handleImageUploadChangeEdit({ fileList }) {
      this.errors.introductionImage = "";
      this.errors.notEnoughQuality = "";
      this.fileList = [];
      this.editForm.introductionImage = null;
      this.fileList = fileList;
      if (this.fileList && this.fileList.length > 0 && this.fileList[0].status === "done") {
        this.editForm.introductionImage = this.fileList[0].response;
        this.$message.success(`File uploaded successfully.`);
      } else if (this.fileList.length > 0 && this.fileList[0].status === "error") {
        this.fileList = [];
        this.editForm.introductionImage = null
        this.$message.error(`File upload failed.`);
        this.errors.notEnoughQuality = "Please upload high quality image(upgrade 500x1000 pixel)!";
      }
    },
    handleImageUploadChange(info) {
      const status = info.file.status;
      if (status !== "uploading") {
      }
      if (status === "done") {
        this.$message.success(`${info.file.name} file uploaded successfully.`);
      } else if (status === "error") {
        this.$message.error(`${info.file.name} file upload failed.`);
      }
    },
    normFile(e) {
      if (Array.isArray(e)) {
        return e;
      }
      return e && e.fileList;
    },
    editCategoryBtn() {
      var formEditData = {
        id: this.editForm.id,
        categoryName: this.editForm.categoryName,
        introductionImage: this.editForm.introductionImage,
        // categoryCode: this.editForm.categoryCode,
        // Account: this.editForm.account,
        deleted: this.editForm.deleted,
      };
    },
    submitCreateForm(e) {
      e.preventDefault();
      let errors;
      let formValue;
      this.form.validateFields((err, values) => {
        errors = err;
        formValue = values;
      });
      if (errors) return;
      var formAddData = {
        categoryName: formValue.categoryName,
        // categoryCode: formValue.categoryCode,
        // Account: formValue.categoryManager,
        deleted: formValue.deleted,
        introductionImage: formValue.introductionImage[0].response,
      };
      // if (formValue.avatarImage) {
      //   formAddData.avatarImage = formValue.avatarImage[0].response;
      // }
      Category.addCategory(formAddData)
        .then((response) => {
          if (response.data.data === 1) {
            this.$notification.success({
              message: "Add new record successful",
            });
            this.paginate();
            this.form.resetFields();
            this.closeModal();
          } else {
            this.$notification.error({
              message: response.data.message,
            });
          }
        })
        .catch((e) => {
          this.$notification.error({
            message: e.response.data.message[0].msg,
          });
          this.closeModal();
        });
    },
    saveCategory() {
      const validation = this.validate();
      if (!validation) {
        return;
      }
      var formEditData = {
        id: this.editForm.id,
        categoryName: this.editForm.categoryName,
        introductionImage: this.editForm.introductionImage,
        // categoryCode: this.editForm.categoryCode,
        // Account: this.editForm.account,
        deleted: this.editForm.deleted,
      };
      Category.editCategory(formEditData)
        .then((response) => {
          if (response.data.data === true) {
            this.$notification.success({
              message: "Edit successfully!",
            });
            this.paginate();
            this.closeModal();
          } else {
            this.$notification.error({
              message: "Edit failed!",
            });
          }
        })
        .catch((error) => {
          this.$notification.error({
            message: "Edit failed!",
          });
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
      if (
        this.editForm.introductionImage == "" ||
        this.editForm.introductionImage == null
      ) {
        this.errors.introductionImage = requiredError;
        isValid = false;
      }
      // if (
      //   this.editForm.categoryCode == "" ||
      //   this.editForm.categoryCode == null
      // ) {
      //   this.errors.categoryCode = requiredError;
      //   isValid = false;
      // }
      return isValid;
    },
    openAddForm(isShow = true) {
      this.contentId = "";
      this.showModal = {
        ...defaultModalState,
        add: isShow,
      };
    },
    closeAddForm() {
      this.contentId = "";
      this.form = this.$form.createForm(this, { name: "validate_other" });
    },
    closeEditForm() {
      this.contentId = "";
      this.editForm.id = "";
      this.editForm.categoryName = "";
      
      // this.editForm.categoryCode = "";
      // this.editForm.account = "";
      this.editForm.deleted = "";

      this.errors.introductionImage = "";
      this.errors.notEnoughQuality = "";
      this.showModal = {
        ...defaultModalState,
        edit: false,
      };
    },
    closeModal() {
      this.showModal = { ...defaultModalState };
      this.errors = { ...defaultInputErrors };
      this.editForm = { ...defaultForm };
    },
    submitForm() {
      this.loading = true;
      (this.categoryNameSearch =
        this.categoryNameSearch === "" ||
        this.categoryNameSearch === undefined ||
        this.categoryNameSearch === null
          ? ""
          : this.categoryNameSearch.replace(/\s+/g, " ").trim()),
        (this.current = 1),
        Category.searchCategory(this.categoryNameSearch, this.current).then(
          (res) => {
            this.totals = res.data.data.totals;
            this.categoryListTable = res.data.data.items;
            this.loading = false;
          }
        );
    },
    resetFilter() {
      (this.categoryNameSearch = ""),
        (this.current = 1),
        Category.searchCategory(this.categoryNameSearch, this.current).then(
          (res) => {
            this.categoryListTable = res.data.data.items;
            this.totals = res.data.data.totals;
            this.loading = false;
          }
        );
    },
    fetchCategory() {
      Category.searchCategory(this.categoryNameSearch, this.current).then(
        (res) => {
          this.categoryListTable = res.data.data;
          this.totals = res.data.datatotals;
          this.loading = false;
        }
      );
    },

    paginate(current = 1) {
      this.current = current;
      this.loading = true;
      Category.searchCategory(this.categoryNameSearch, this.current).then(
        (res) => {
          this.categoryListTable = res.data.data.items;
          this.totals = res.data.data.totals;
          this.loading = false;
        }
      );
    },
  },
};
</script>
<style scoped>
.section-profile-cover {
/* background: rgb(131,58,180); */
background: linear-gradient(83deg, rgba(131,58,180,1) 0%, rgba(253,29,29,1) 41%, rgba(252,148,69,1) 100%);
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

.ant-page-header {
  padding: 20px 0px;
}
</style>

<style >
.btn-filter {
  min-width: 80%;
  height: 40px;
}
.ant-page-header-heading-title {
  font-size: 33px !important;
}
.ant-page-header-heading {
  padding-top: 10px;
}
.pagnigation-custom {
  float: right;
}
.cus-active-button {
  color: white !important;
}

.page-header {
  -webkit-transition: all 0.15s ease;
  transition: all 0.15s ease;
  will-change: transform;
  letter-spacing: 0.025em;
  background: #FC5730;
  display: inline-block;
  height: 42px;
  -webkit-box-shadow: 0 4px 8px 0 rgb(178 178 178 / 26%);
  -moz-box-shadow: 0 4px 8px 0 rgba(178, 178, 178, 0.26);
  -ms-box-shadow: 0 4px 8px 0 rgba(178, 178, 178, 0.26);
  box-shadow: 0 4px 8px 0 rgb(178 178 178 / 26%);
  border: 1px solid rgba(178, 178, 178, 0.1);
  padding: 3px 8px;
  border-bottom-right-radius: 6px;
  border-bottom-left-radius: 6px;
}
.page-header h2 {
  color: #fff;
}
.screen-name {
  opacity: 0.8;
  text-transform: uppercase;
  padding: 10px 10px;
}
</style>