<template>
  <div class="profile-page">
    <section class="section-profile-cover section-shaped my-0">
      <div class="container-fluid">
        <h4 class="text-white screen-name">User Role Management</h4>
      </div>
    </section>
    <section class="section section-skew">
      <div class="container">
        <card shadow class="card-profile mt--300" no-body>
          <div class="px-4">
            <div class="gutter-example">
              <!-- <a-page-header
                style="border: 1px solid rgb(235, 237, 240); margin-top: 10px;"
                title="User Role Management "
                sub-title="This is a subtitle"
              /> -->
            </div>

            <div class="gutter-example lg-md">
              <a-row :gutter="36">
                <a-col :span="6">
                  <label><b>Account: </b></label>
                  <a-input
                    v-model="formSearch.account"
                    placeholder="Enter account "
                    :allowClear="true"
                  />
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
                    <label><b>Role: </b></label>
                    <a-select
                      v-model="formSearch.roleCode"
                      class="filter-select"
                      style="width: 100%"
                      show-arrow
                      allow-clear
                      show-search
                      :filter-option="false"
                      placeholder="All"
                    >
                      <a-select-option
                        v-for="item in roleListToAdd"
                        :key="item.roleCode"
                        :value="item.roleCode"
                      >
                        {{ item.roleName }}
                      </a-select-option>
                    </a-select>
                  </div>
                </a-col>
                <a-col :span="3">
                  <br />
                  <button
                    class="btn btn-neutral btn-filter main-btn"
                    :disabled="loading"
                    @click="searchUsersWithForm()"
                  >
                    <i class="fa fa-search"></i>
                    Search
                  </button>
                </a-col>
                <!-- <a-col 
                class="gutter-row mt-10 group-button-custom "
                :span="2"
                :col="2"
                :xs="12"
                :sm="5"
                :md="4"
                :lg="3"
              >
                <div class="gutter-box">
                  <br />
                  <button
                    class="btn btn-primary  btn-filter main-btn"
                    @click="searchUsers()"
                  >
                    <i class="tim-icons icon-gift-2"></i> Search
                  </button>
                </div>
              </a-col> -->

                <a-col :span="3">
                  <br />
                  <button
                    class="btn btn-neutral btn-round btn-filter main-btn"
                    :disabled="loading"
                    @click="showModalAddUser()"
                  >
                    <i class="fa fa-user-plus"></i>
                    Add User
                  </button>
                </a-col>

                <a-col :span="3">
                  <br />
                  <button
                    class="btn btn-neutral btn-round btn-filter main-btn"
                    :disabled="loading"
                    @click="showModalAddUserRole()"
                  >
                    <i class="fa fa-plus"></i>
                    Assign Role
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
              <div :style="{ background: '#fff', minHeight: '280px' }">
                <a-table
                  :columns="columnsUser"
                  :data-source="listUser"
                  :scroll="{ x: 1000 }"
                  :pagination="false"
                >
                  
                  <template #createdTimeCustom="item">
                    <span>{{ generateTime(item.createdTime) }}</span>
                  </template>

                  <template #action="item">
                    <a-dropdown>
                      <a-menu slot="overlay">
                        <a-menu-item
                          key="1"
                          @click="handleEditItemBtnClick(item)"
                          v-if="item.roleCode === 'moderator'"
                        >
                          Change Categories
                        </a-menu-item>

                        <a-menu-item key="2">
                          <a-popconfirm
                            placement="leftBottom"
                            ok-text="Yes"
                            cancel-text="No"
                            @confirm="deleteSubItemBtnClick(item)"
                          >
                            <template slot="title">
                              <span>Are you sure delete </span><br />
                              <span
                                >"role {{ item.roleName }} of account
                                {{ item.account }}"</span
                              >
                            </template>
                            Delete
                          </a-popconfirm>
                        </a-menu-item>
                      </a-menu>
                      <a-button> Action <a-icon type="down" /> </a-button>
                    </a-dropdown>
                  </template>
                </a-table>

                <div class="gutter-example pt-md pagnigation-custom">
                  <a-pagination
                    show-quick-jumper
                    :default-current="1"
                    :total="totalUsers"
                    @change="onChange"
                  />
                </div>
              </div>
            </a-spin>


          <!-- Add User Role -->
            <a-modal
              v-model="showModal.modalAddUserRole"
              title="Assign User Role"
              :width="500"
              :maskClosable="false"
              :destroyOnClose="true"
              :closable="false"
            >
              <a-row :gutter="[24, 16]">
                <a-col :span="4">Account: <span class="red">*</span> </a-col>
                <a-col :span="12">
                  <a-input v-model="formAddUserRole.account" />
                  <span v-if="errors.account" class="red">
                    {{ errors.account }}
                  </span>
                </a-col>
                <a-col :span="6"><p>@fpt.edu.vn</p></a-col>
              </a-row>

              <a-row :gutter="[24, 16]">
                <a-col :span="4">Role: <span class="red">*</span> </a-col>
                <a-col :span="12">
                  <a-select
                    v-model="formAddUserRole.roleCode"
                    class="filter-select"
                    style="width: 100%"
                  >
                    <a-select-option
                      v-for="item in roleListToAdd"
                      :key="item.roleCode"
                      :value="item.roleCode"
                    >
                      {{ item.roleName }}
                    </a-select-option>
                  </a-select>
                  <span v-if="errors.roleCode" class="red">
                    {{ errors.roleCode }}
                  </span>
                </a-col>
              </a-row>

              <a-row :gutter="[24, 16]" v-if="checkShowCategory()">
                <a-col :span="8"
                  >Category Management:
                  <!-- <span class="red">*</span> -->
                </a-col>
                <a-col :span="16">
                  <a-select
                    mode="multiple"
                    v-model="formAddUserRole.categoryID"
                    class="filter-select"
                    style="width: 100%"
                    @search="getCategoryList"
                  >
                    <a-select-option
                      v-for="item in categoryList"
                      :key="item.id"
                      :value="item.id"
                    >
                      {{ item.categoryName }}
                    </a-select-option>
                  </a-select>
                </a-col>
              </a-row>

              <template slot="footer">
                <a-button
                  key="submit"
                  type="primary"
                  :loading="loading"
                  @click="handleOkAddUserRole"
                >
                  Submit
                </a-button>
                <a-button
                  key="cancel"
                  type="secondary"
                  :loading="loading"
                  @click="handleCancelAddUserRole"
                >
                  Cancel
                </a-button>
              </template>
            </a-modal>



          <!-- Add user -->
            <a-modal
              v-model="showModal.modalAddUser"
              title="Add User Information"
              :width="450"
              :maskClosable="false"
              :destroyOnClose="true"
              :closable="false"
            >
              <a-row :gutter="[24, 16]">
                <a-col :span="6">First name: (<span class="red">*</span>) </a-col>
                <a-col :span="18">
                  <a-input v-model="formAddUser.firstName" />
                  <span v-if="errors.firstName" class="red">
                    {{ errors.firstName }}
                  </span>
                </a-col>
              </a-row>

              <a-row :gutter="[24, 16]">
                <a-col :span="6">Last name: (<span class="red">*</span>) </a-col>
                <a-col :span="18">
                  <a-input v-model="formAddUser.lastName" />
                  <span v-if="errors.lastName" class="red">
                    {{ errors.lastName }}
                  </span>
                </a-col>
              </a-row>

              <a-row :gutter="[24, 16]">
                <a-col :span="6">Avatar: </a-col>
                <a-col :span="18">
                    <!-- :fileList="fileList" -->
                  <a-upload
                    v-model="formAddUser.avatar"
                    name="file"
                    list-type="picture-card"
                    :action="introductionImageUpload"
                    @change="handleImageUploadChange"
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
                  <a-modal
                    :visible="previewVisible"
                    :footer="null"
                    @cancel="handleCancel"
                  >
                    <img
                      alt="example"
                      style="width: 100%"
                      :src="previewImage"
                    />
                  </a-modal>
                </a-col>
              </a-row>

              <a-row :gutter="[24, 16]">
                <a-col :span="6">Account: (<span class="red">*</span>) </a-col>
                <a-col :span="18">
                  <a-input v-model="formAddUser.account" />
                  <span v-if="errors.account" class="red">
                    {{ errors.account }}
                  </span>
                </a-col>
              </a-row>
              
              <a-row :gutter="[24, 16]">
                <a-col :span="6">Address: (<span class="red">*</span>) </a-col>
                <a-col :span="18">
                  <a-input v-model="formAddUser.address" />
                  <span v-if="errors.address" class="red">
                    {{ errors.address }}
                  </span>
                </a-col>
              </a-row>

              <a-row :gutter="[24, 16]">
                <a-col :span="6">Gender: (<span class="red">*</span>) </a-col>
                <a-col :span="18">
                  <a-radio-group 
                    v-model="formAddUser.gender" 
                    name="radioGroup" 
                    :default-value="1"
                  >
                    <a-radio :value="1">
                      Male
                    </a-radio>
                    <a-radio :value="0">
                      Female
                    </a-radio>
                  </a-radio-group>
                  <span v-if="errors.gender" class="red">
                    {{ errors.gender }}
                  </span>
                </a-col>
              </a-row>

              <a-row :gutter="[24, 16]">
                <a-col :span="6">Phone Number: (<span class="red">*</span>) </a-col>
                <a-col :span="18">
                  <a-input v-model="formAddUser.phoneNumber" />
                  <span v-if="errors.phoneNumber" class="red">
                    {{ errors.phoneNumber }}
                  </span>
                </a-col>
              </a-row>

              <a-row :gutter="[24, 16]">
                <a-col :span="6">Date of birth: (<span class="red">*</span>) </a-col>
                <a-col :span="18">
                  <a-date-picker v-model="formAddUser.dateOfBirth">
                    <template slot="renderExtraFooter">
                    </template>
                  </a-date-picker>
                  <span v-if="errors.dateOfBirth" class="red">
                    {{ errors.dateOfBirth }}
                  </span>
                </a-col>
              </a-row>

              <a-row :gutter="[24, 16]">
                <a-col :span="6">Role: (<span class="red">*</span>) </a-col>
                <a-col :span="18">
                  <a-select
                    v-model="formAddUser.roleCode"
                    class="filter-select"
                    style="width: 100%"
                  >
                    <a-select-option
                      v-for="item in roleListToAdd"
                      :key="item.roleCode"
                      :value="item.roleCode"
                    >
                      {{ item.roleName }}
                    </a-select-option>
                  </a-select>
                  <span v-if="errors.roleCode" class="red">
                    {{ errors.roleCode }}
                  </span>
                </a-col>
              </a-row>

              <a-row :gutter="[24, 16]" v-if="checkShowCategoryAddUser()">
                <a-col :span="8"
                  >Category Management:
                  <!-- <span class="red">*</span> -->
                </a-col>
                <a-col :span="16">
                  <a-select
                    mode="multiple"
                    v-model="formAddUser.categoryID"
                    class="filter-select"
                    style="width: 100%"
                    @search="getCategoryList"
                  >
                    <a-select-option
                      v-for="item in categoryList"
                      :key="item.id"
                      :value="item.id"
                    >
                      {{ item.categoryName }}
                    </a-select-option>
                  </a-select>
                </a-col>
              </a-row>

              <template slot="footer">
                <a-button
                  key="submit"
                  type="primary"
                  :loading="loading"
                  @click="handleOkAddUser"
                >
                  Submit
                </a-button>
                <a-button
                  key="cancel"
                  type="secondary"
                  :loading="loading"
                  @click="handleCancelAddUser"
                >
                  Cancel
                </a-button>
              </template>
            </a-modal>

            <!-- edit modal -->
            <a-modal
              title="Change Categories"
              v-model="showModal.edit"
              :maskClosable="false"
              :destroyOnClose="true"
              :closable="false"
            >
              <a-row :gutter="[24, 16]" v-if="checkShowEditCategory">
                <a-col :span="8"> Select Category: </a-col>
                <a-col :span="16">
                  <a-select
                    mode="multiple"
                    v-model="editForm.categoryID"
                    class="filter-select"
                    style="width: 100%"
                  >
                    <a-select-option
                      v-for="item in categoryListToEdit"
                      :key="item.id"
                      :value="item.id"
                    >
                      {{ item.categoryName }}
                    </a-select-option>
                  </a-select>
                </a-col>
              </a-row>

              <template slot="footer">
                <a-button
                  key="submit"
                  type="primary"
                  :loading="loading"
                  @click="saveUserRole"
                >
                  Update
                </a-button>
                <a-button
                  key="cancel"
                  type="secondary"
                  :loading="loading"
                  @click="closeEditForm"
                >
                  Cancel
                </a-button>
              </template>
            </a-modal>
          </div>
        </card>
      </div>
    </section>
  </div>
</template>
<script>
import Vue from "vue";
import RoleRepository from "../api/role";
import CategoryRepository from "../api/category";
import UserRepository from "../api/user";
import UserRoleRepository from "../api/userRole";
import moment from "moment";
import CONFIG from "../config/index";
const defaultFormSearh = {
  account: "",
  roleCode: undefined,
};
const requiredError = "This field can't blank";
const defaultInputErrors = {
  roleCode: "",
  account: "",
};
const defaultFormAddUserRole = {
  account: "",
  roleCode: "",
};
const defaultFormAddUser = {
  account: "",
  roleCode: "",
  firstName: "",
  lastName: "",
  avatar: "",
  address: "",
  gender: 1,
  phoneNumber: "",
  dateOfBirth: "1970-01-01",
};
const defaultShowForm = {
  modalAddUserRole: false,
  modalAddUser: false,
  edit: false,
};
const defaultForm = {
  id: "",
  account: "",
  roleCode: "",
  categoryID: [],
};

export default {
  name: "user-role-managerment",
  data() {
    return {
      roleListToAdd: [],
      categoryList: [],
      categoryListToEdit: [],
      listCategoryOfManagerDefault: [],
      categoryOfManager: [],
      checkShowEditCategory: false,
      loading: false,
      roleList: [],
      roleOfUser: Vue.$cookies.get("roleCode"),
      listUser: [],
      // listUserToAddRole: [],
      currentUserPage: 1,
      selectedItem: null,
      viewData: [],
      totalUsers: 0,
      errors: {
        roleCode: "",
        account: "",
        category: "",
        firstName: "",
        lastName: "",
        address: "",
        gender: "",
        phoneNumber: "",
        dateOfBirth: "",
      },
      editForm: { ...defaultForm },
      tempRole: {},
      showModal: { ...defaultShowForm },
      formSearch: { ...defaultFormSearh },
      formAddUserRole: { ...defaultFormAddUserRole },
      formAddUser: { ...defaultFormAddUser },
      routes: [
        {
          path: "index",
          breadcrumbName: "First-level Menu",
        },
        {
          path: "first",
          breadcrumbName: "Second-level Menu",
        },
        {
          path: "second",
          breadcrumbName: "Third-level Menu",
        },
      ],
      columnsUser: [
        {
          title: "NO",
          width: 40,
          dataIndex: "index",
          key: "index",
        },
        {
          title: "Account",
          width: 120,
          dataIndex: "account",
          key: "account",
        },
        {
          title: "Role Code",
          width: 100,
          dataIndex: "roleCode",
          key: "roleCode",
        },
        {
          title: "Created By",
          width: 120,
          dataIndex: "createdBy",
          key: "createdBy",
        },
        {
          title: "Created Time",
          width: 100,
          key: "createdTime",
          scopedSlots: { customRender: "createdTimeCustom" },
        },
        {
          title: "Action",
          key: "operation",
          fixed: "right",
          width: 100,
          scopedSlots: { customRender: "action" },
        },
      ],
      fileList: [],
      previewImage: "",
      headers: "",
      previewVisible: false,
      introductionImageUpload: "",
    };
  },
  created() {
    this.introductionImageUpload = `${CONFIG.apiUrl}/image/uploadImage/{upload-image}?introductionImage=false`;
    // this.searchUsers();
    // this.getUserExceptRoleAndAccount();
    this.searchRoles();
    this.getCategoryList("");
  },
  methods: {
    handleCancel() {
      this.previewVisible = false;
    },
    handleImageUploadChange(info) {
      this.fileList = [];
      const status = info.file.status;
      if (status !== "uploading") {
      }
      if (status === "done") {
        this.fileList = info.fileList;
        this.formAddUser.avatar = info.fileList[0].response;
        this.$message.success(`${info.file.name} file uploaded successfully.`);
      } else if (status === "error") {
        this.fileList = [];
        this.$message.error(info.file.response, 5);
      }
    },
    async handlePreviewUploadFile(file) {
      this.previewImage = file.thumbUrl;
      // if (this.article) {
      //   this.previewImage =
      //     this.relatedArticleImage + this.article.introductionImage;
      // }
      this.previewVisible = true;
    },
    generateTime(dateNumber) {
      const date = new Date(dateNumber);
      const m = moment(date);
      return m.isValid() ? m.format("DD-MM-YYYY") : "";
    },
    checkShowCategory() {
      if (this.formAddUserRole.roleCode === "moderator") {
        this.getCategoryList("");
        return true;
      }
      return false;
    },
    checkShowCategoryAddUser() {
      if (this.formAddUser.roleCode === "moderator") {
        this.getCategoryList("");
        return true;
      }
      return false;
    },
    getCategoryList(value) {
      CategoryRepository.searchCategoryToAddManager(value, 1).then((res) => {
        this.categoryList = res.data.data.items;
      });
    },
    searchCategoryManagement(account, isDelete) {
      this.loading = true;
      CategoryRepository.getByManager(account).then((res) => {
        this.categoryOfManager = res.data.data.items;
        if (!isDelete) {
          this.categoryOfManager.map((element) => {
            if (this.checkCategoryDuplicate(element)) {
              this.categoryListToEdit.unshift(element);
            }
          });
          this.categoryList.map((element) => {
            if (this.checkCategoryDuplicate(element)) {
              this.categoryListToEdit.unshift(element);
            }
          });
          this.editForm.categoryID = this.categoryOfManager.map(
            (element) => element.id
          );
          this.listCategoryOfManagerDefault = this.editForm.categoryID.map(
            (element) => element
          );
        } else {
          let categoryIDs;
          if (
            this.selectedItem.roleCode === "moderator" &&
            this.categoryOfManager.length > 0
          ) {
            categoryIDs = this.categoryOfManager.map((element) => element.id);
          } else {
            categoryIDs = null;
          }

          var formDelete = {
            Id: this.selectedItem.id,
            RoleCode: this.selectedItem.roleCode,
            categoryID: categoryIDs,
          };
          UserRoleRepository.deleteUserRole(formDelete)
            .then((res) => {
              if (res.data.data === true) {
                this.$notification.success({
                  message: "Delete user role successful",
                });
                this.loading = false;
                this.onChange();
              } else {
                this.$notification.error({
                  message: "Delete user role fail",
                });
                this.loading = false;
              }
            })
            .catch((e) => {
              this.$notification.error({
                message: e.response.data.message,
              });
              this.loading = false;
            });
        }
      });
    },
    checkCategoryDuplicate(category) {
      for (let i = 0; i < this.categoryListToEdit.length; i++) {
        if (this.categoryListToEdit[i].id === category.id) {
          return false;
        }
      }
      return true;
    },
    async handleEditItemBtnClick(item) {
      this.selectedItem = item;
      this.viewData = {
        data: [
          {
            label: "Role Code",
            value: item.roleCode,
          },
        ],
      };
      this.editForm.id = item.id;
      this.editForm.account = item.account;
      this.editForm.roleCode = item.roleCode;
      if (item.roleCode === "moderator") {
        await this.getCategoryList("");
        this.checkShowEditCategory = true;
        await this.searchCategoryManagement(item.account, false);
      }
      this.showModal = {
        edit: true,
      };
      this.loading = false
    },
    showModalAddUserRole(roleCode) {
      this.showModal.modalAddUserRole = true;
      let roleForm;
      switch (roleCode) {
        case "super_admin":
          roleForm = {
            roleCode: "super_admin",
            roleName: "Super Admin",
          };
          break;
        case "editor":
          roleForm = {
            roleCode: "editor",
            roleName: "Editor",
          };
          break;
        case "moderator":
          roleForm = {
            roleCode: "moderator",
            roleName: "Moderator",
          };
          break;
        case "writer":
          roleForm = {
            roleCode: "writer",
            roleName: "Writer",
          };
          break;
        default:
      }
      this.tempRole = roleForm;
    },
    showModalAddUser(roleCode) {
      this.headers = {
        authorization: "Bearer " + this.$cookies.get("accessToken"),
        "X-Requested-With": "XMLHttpRequest",
      };
      this.showModal.modalAddUser = true;
      let roleForm;
      switch (roleCode) {
        case "super_admin":
          roleForm = {
            roleCode: "super_admin",
            roleName: "Super Admin",
          };
          break;
        case "editor":
          roleForm = {
            roleCode: "editor",
            roleName: "Editor",
          };
          break;
        case "moderator":
          roleForm = {
            roleCode: "moderator",
            roleName: "Moderator",
          };
          break;
        case "writer":
          roleForm = {
            roleCode: "writer",
            roleName: "Writer",
          };
          break;
        default:
      }
      this.tempRole = roleForm;
    },
    handleOkAddUserRole(e) {
      this.loading = true;
      e.preventDefault();
      const validation = this.validateAdd();
      if (!validation) {
        return;
      }
      UserRepository.searchUserByAccount(this.formAddUserRole.account).then(res => {
        var listUser = res.data.data.items;

        let existRole = false;
        if(listUser != null && listUser.length > 0) { 
          for(let i = 0; i < listUser.length; i++) {
            if(listUser[i].roleCode === this.formAddUserRole.roleCode) {
              this.$notification.error({
                message: "User " + this.formAddUserRole.account + " have already exist role " + this.formAddUserRole.roleCode,
              });
              existRole = true;
              this.loading = false;
            }
          }
        }

        if(!existRole) {
          if(listUser != null && listUser.length > 0 && listUser[0].account === this.formAddUserRole.account ) {
            this.addUserRole();
          } else {
            this.$notification.error({
              message: "That user don't exist. Please add user before add role!",
            });
            this.loading = false;
          }
        }
      })
    },
    handleOkAddUser(e) {
      e.preventDefault();
      const validation = this.validateAddUser();
      if (!validation) {
        return;
      }
      UserRepository.searchUserByAccount(this.formAddUser.account).then(res => {
        var listUser = res.data.data.items;

        let existAccount = false;
        if(listUser != null && listUser.length > 0) { 
          for(let i = 0; i < listUser.length; i++) {
            if(listUser[i].account === this.formAddUser.account) {
              this.$notification.error({
                message: "User with account '" + this.formAddUser.account + "' have already exist!",
              });
              existAccount = true;
              break
            }
          }
        }

        if(!existAccount) {
          this.addUser();
        }
      })
    },
    handleCancelAddUserRole() {
      this.formAddUserRole = { ...defaultFormAddUserRole };
      // this.listUserToAddRole = [];
      this.showModal.modalAddUserRole = false;
      this.errors.account = "";
      this.errors.roleCode = "";
    },
    handleCancelAddUser() {
      this.formAddUser = { ...defaultFormAddUser };
      // this.listUserToAddRole = [];
      this.showModal.modalAddUser = false;

      this.errors.account = "";
      this.errors.roleCode = "";
      this.errors.firstName = "";
      this.errors.lastName = "";
      this.errors.address = "";
      this.errors.gender = "";
      this.errors.phoneNumber = "";
      this.errors.dateOfBirth = "";
    },
    addUserRole() {
      this.loading = true;
      const validation = this.validateAdd();
      if (!validation) {
        this.loading = false;
        return;
      }
      UserRoleRepository.addUserRole(this.formAddUserRole)
        .then((res) => {
          if (res.data.data) {
            this.searchUsers();
            this.$notification["success"]({
              message: "Add user role succeed !",
            });
            this.loading = false;
          } else {
            this.$notification["error"]({
              message: res.data.message,
            });
            this.loading = false;
          }
          this.showModal.modalAddUserRole = false;
          this.formAddUserRole = { ...defaultFormAddUserRole };
          this.errors.account = "";
          this.errors.roleCode = "";
          this.errors.category = "";
        })
        .catch((err) => {
          this.$notification["error"]({
            message: "Add user role failed !",
          });
          this.loading = false;
        });
    },
    addUser() {
      this.loading = true;
      const validation = this.validateAddUser();
      if (!validation) {
        this.loading = false;
        return;
      }
      UserRepository.addUser(this.formAddUser)
        .then((res) => {
          if (res.data.data) {
            this.searchUsers();
            this.$notification["success"]({
              message: "Add user succeed !",
            });
          } else {
            this.$notification["error"]({
              message: res.data.message,
            });
          }
          this.showModal.modalAddUser = false;
          this.formAddUser = { ...defaultFormAddUser };
          this.errors.account = "";
          this.errors.roleCode = "";
          this.errors.category = "";
          this.errors.firstName = "";
          this.errors.lastName = "";
          this.errors.address = "";
          this.errors.gender = "";
          this.errors.phoneNumber = "";
          this.errors.dateOfBirth = "";
        })
        .catch((err) => {
          this.$notification["error"]({
            message: "Add user failed !",
          });
        });
      this.loading = false;
    },
    closeEditForm() {
      this.editForm.id = "";
      this.editForm.account = "";
      this.editForm.roleCode = "";
      this.editForm.categoryID = [];
      this.categoryListToEdit = [];
      this.categoryOfManager = [];
      this.showModal = {
        ...defaultShowForm,
        edit: false,
      };
    },
    validateAdd() {
      let isValid = true;
      if (
        this.formAddUserRole.account == "" ||
        this.formAddUserRole.account == null
      ) {
        this.errors.account = requiredError;
        isValid = false;
      }
      if (
        this.formAddUserRole.roleCode == "" ||
        this.formAddUserRole.roleCode == null
      ) {
        this.errors.roleCode = requiredError;
        isValid = false;
      }
      return isValid;
    },
    validateAddUser() {
      let isValid = true;
      if (
        this.formAddUser.account == "" ||
        this.formAddUser.account == null
      ) {
        this.errors.account = requiredError;
        isValid = false;
      }
      if (
        this.formAddUser.roleCode == "" ||
        this.formAddUser.roleCode == null
      ) {
        this.errors.roleCode = requiredError;
        isValid = false;
      }
      if (
        this.formAddUser.firstName == "" ||
        this.formAddUser.firstName == null
      ) {
        this.errors.firstName = requiredError;
        isValid = false;
      }
      if (
        this.formAddUser.lastName == "" ||
        this.formAddUser.lastName == null
      ) {
        this.errors.lastName = requiredError;
        isValid = false;
      }
      if (
        this.formAddUser.address == "" ||
        this.formAddUser.address == null
      ) {
        this.errors.address = requiredError;
        isValid = false;
      }
      if (
        this.formAddUser.gender == "" ||
        this.formAddUser.gender == null
      ) {
        this.errors.gender = requiredError;
        isValid = false;
      }
      if (
        this.formAddUser.phoneNumber == "" ||
        this.formAddUser.phoneNumber == null
      ) {
        this.errors.phoneNumber = requiredError;
        isValid = false;
      }
      if (
        this.formAddUser.dateOfBirth == "" ||
        this.formAddUser.dateOfBirth == null
      ) {
        this.errors.dateOfBirth = requiredError;
        isValid = false;
      }
      return isValid;
    },
    validate() {
      let isValid = true;
      if (this.editForm.roleCode == "" || this.editForm.roleCode == null) {
        this.errors.roleCode = requiredError;
        isValid = false;
      }
      return isValid;
    },
    checkDisplayUpdateBtn(array) {
      // if the other array is a falsy value, return
      if (!array) {
        return false;
      }

      // compare lengths - can save a lot of time
      if (this.listCategoryOfManagerDefault.length != array.length) {
        return false;
      }

      // sort 2 array to compare
      this.listCategoryOfManagerDefault =
        this.listCategoryOfManagerDefault.sort(function (a, b) {
          return a - b;
        });
      array = array.sort(function (a, b) {
        return a - b;
      });

      for (var i = 0; i < this.listCategoryOfManagerDefault.length; i++) {
        if (this.listCategoryOfManagerDefault[i] != array[i]) {
          return false;
        }
      }
      return true;
    },
    saveUserRole() {
      this.loading = true;
      const validation = this.validate();
      if (!validation) {
        return;
      }
      var formEditData = {
        Id: this.editForm.id,
        Account: this.editForm.account,
        RoleCode: this.editForm.roleCode,
        categoryID: this.editForm.categoryID,
        oldCategoryID: this.listCategoryOfManagerDefault,
      };
      let xxx = this.editForm.categoryID.map((element) => element);
      if (this.checkDisplayUpdateBtn(xxx)) {
        this.$notification.success({
          message: "Edit successfully!",
        });
        this.onChange();
        this.closeModal();
        this.loading = false;
      } else {
        UserRoleRepository.updateRoleOfUser(formEditData)
          .then((response) => {
            if (response.data.data === true) {
              this.$notification.success({
                message: "Edit successfully!",
              });
              this.onChange();
              this.closeModal();
              this.loading = false;
            } else {
              this.$notification.error({
                message: data.message,
              });
            this.loading = false;
            }
          })
          .catch((error) => {
            this.$notification.error({
              message: "Edit fail!",
            });
            this.loading = false;
          });
      }
    },
    deleteSubItemBtnClick(item) {
      this.selectedItem = item;
      this.searchCategoryManagement(item.account, true);
    },
    closeModal() {
      this.showModal = { ...defaultShowForm };
      this.errors = { ...defaultInputErrors };
      this.editForm = { ...defaultForm };
    },
    searchUsers() {
      this.loading = true;
      this.formSearch.account =
        this.formSearch.account === "" ||
        this.formSearch.account === null ||
        this.formSearch.account === undefined
          ? ""
          : this.formSearch.account.replace(/\s+/g, " ").trim();

      var roleCode = this.$cookies.get("roleCode");
      let RoleCodeOfUser = [];
      switch (roleCode) {
        case "super_admin":
          RoleCodeOfUser.push("super_admin");
          RoleCodeOfUser.push("editor");
          RoleCodeOfUser.push("moderator");
          RoleCodeOfUser.push("writer");
          break;
        case "editor":
          RoleCodeOfUser.push("moderator");
          RoleCodeOfUser.push("writer");
          break;
        case "moderator":
          RoleCodeOfUser.push("writer");
          break;
        default:
      }
      if (
        this.formSearch.roleCode !== "" &&
        this.formSearch.roleCode !== undefined
      ) {
        if (RoleCodeOfUser.includes(this.formSearch.roleCode)) {
          RoleCodeOfUser = [];
          RoleCodeOfUser.push(this.formSearch.roleCode);
        } else {
          this.listUser = [];
          this.totalUsers = 0;
        }
      }
      var formDelete = {
        Account: this.formSearch.account,
        RoleCodeOfUser: RoleCodeOfUser,
      };
      UserRoleRepository.searchUser(formDelete, this.currentUserPage).then(
        (res) => {
          this.listUser = res.data.data.items;
          this.totalUsers = res.data.data.total;
          this.loading = false;
        }
      );
    },
    searchUsersWithForm() {
      this.loading = true;
      this.formSearch.account =
        this.formSearch.account === "" ||
        this.formSearch.account === null ||
        this.formSearch.account === undefined
          ? ""
          : this.formSearch.account.replace(/\s+/g, " ").trim();

      var roleCode = this.$cookies.get("roleCode");
      let RoleCodeOfUser = [];
      switch (roleCode) {
        case "super_admin":
          RoleCodeOfUser.push("super_admin");
          RoleCodeOfUser.push("editor");
          RoleCodeOfUser.push("moderator");
          RoleCodeOfUser.push("writer");
          break;
        case "editor":
          RoleCodeOfUser.push("moderator");
          RoleCodeOfUser.push("writer");
          break;
        case "moderator":
          RoleCodeOfUser.push("writer");
          break;
        default:
      }
      if (
        this.formSearch.roleCode !== "" &&
        this.formSearch.roleCode !== undefined
      ) {
        if (RoleCodeOfUser.includes(this.formSearch.roleCode)) {
          RoleCodeOfUser = [];
          RoleCodeOfUser.push(this.formSearch.roleCode);
        } else {
          this.listUser = [];
          this.totalUsers = 0;
        }
      }
      var formDelete = {
        Account: this.formSearch.account,
        RoleCodeOfUser: RoleCodeOfUser,
      };
      UserRoleRepository.searchUser(formDelete, 1).then(
        (res) => {
          this.listUser = res.data.data.items;
          this.totalUsers = res.data.data.total;
          this.loading = false;
        }
      );
    },
    searchRoles() {
      RoleRepository.getAllRole().then((res) => {
        this.roleList = res.data.data;
        this.checkListRoleToAdd();
      });
    },
    checkListRoleToAdd() {
      switch (this.roleOfUser) {
        case "super_admin":
          this.roleListToAdd = this.roleList;
          break;
        case "editor":
          this.roleList.map((element) => {
            if (
              element.roleCode === "moderator" ||
              element.roleCode === "writer"
            ) {
              this.roleListToAdd.push(element);
            }
          });
          break;
        case "moderator":
          this.roleList.map((element) => {
            if (element.roleCode === "writer") {
              this.roleListToAdd.push(element);
            }
          });
          break;
        default:
      }
    },
    onChange(current = 1) {
      this.currentUserPage = current;
      this.searchUsers();
    },
  },
};
</script>
<style scoped>
.section-profile-cover {
  background: linear-gradient(83deg, rgba(131,58,180,1) 0%, rgba(253,29,29,1) 41%, rgba(252,148,69,1) 100%);
}
.container {
  max-width: 1430px;
}
.cancel-btn {
  background-color: white !important;
  border-color: #FC5730 !important;
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
.main-content {
  float: right;
}


.filter-select {
  width: 200px;
}

.ant-page-header {
  padding: 20px 0px;
}
/* .ant-input-search{
  width: 100%;
} */
/* .ant-input-search-button{
  background-color: #FC5730  !important;
    border-color: #FC5730;
} */
.red {
  color: red;
}
</style>

<style >
.column-btn-filter {
  padding-top: 4.5px;
}
/* .ant-input-search-button {
  background-color: #FC5730 !important;
  border-color: #FC5730 !important;
} */
.ant-page-header-heading-title {
  font-size: 33px !important;
  line-height: 51px;
}
.ant-page-header-heading {
  padding-top: 10px;
}
.pagnigation-custom {
  float: right;
}
.ant-btn-primary {
  background-color: #FC5730 !important;
  border-color: #FC5730 !important;
}
.ant-btn:hover {
  color: #FC5730 !important;
  border-color: #FC5730 !important;
}
</style>