<template>
  <div class="profile-page">
    <section class="section-profile-cover section-shaped my-0">
      <div class="shape shape-style-1  alpha-4">
        <span></span>
        <span></span>
        <span></span>
        <span></span>
        <span></span>
        <span></span>
        <span></span>
      </div>
    </section>
    <section class="section section-skew">
      <div class="container">
        <card shadow class="card-profile mt--300" no-body>
          <div class="px-4">
            <a-spin :spinning="loading">
              <a-icon
                type="loading"
                slot="indicator"
                style="font-size: 24px"
                spin
              />
              <div class="row justify-content-center">
                <div class="col-lg-3 order-lg-2">
                  <div class="card-profile-image">
                    <a href="#">
                        <!-- v-show="imageLoaded" -->
                      <img
                        style="width: 800px; height: 200px"
                        v-lazy="avatarImageUrl"
                        class="rounded-circle"
                      />
                        <!-- :on-success="imageLoaded = false" -->
                      <!-- <img
                        v-show="!imageLoaded"
                        style="width: 800px; height: 200px"
                        src="../../public/img/icons/common/default-avatar.png"
                        class="rounded-circle"
                      /> -->
                    </a>
                  </div>
                </div>
                <div
                  class="col-lg-4 order-lg-3 text-lg-right align-self-lg-center"
                >
                  <div class="card-profile-actions py-4 mt-lg-0">
                    <base-button
                      type="info"
                      size="sm"
                      class="mr-4"
                      v-if="roleOfUser"
                      @click="$router.push('/admin-action/MyArticle')"
                    >
                      <a-icon type="solution" />
                      <span>My Article</span></base-button
                    >
                    <base-button
                      rel="noopener"
                      size="sm"
                      class="mr-4"
                      @click="showModalEditUser"
                    >
                      <span class="btn-inner--icon">
                        <i class="fa fa-edit"></i>
                      </span>
                      <span class="nav-link-inner--text">EDIT PROFILE</span>
                    </base-button>
                  </div>
                </div>
                <div class="col-lg-4 order-lg-1">
                  <div class="card-profile-stats d-flex justify-content-center">
                    <div>
                      <span class="heading">{{ totalPostedArticle }}</span>
                      <span class="description">Posted Articles</span>
                    </div>
                    <div>
                      <span class="heading">{{ totalContributedArticle }}</span>
                      <span class="description">Contributed Articles</span>
                    </div>
                    <div>
                      <span class="heading">{{ totalComments }}</span>
                      <span class="description">Comments</span>
                    </div>
                  </div>
                </div>
              </div>
              <div class="text-center mt-5">
                <h3>
                  {{ profileOfUser.lastName + " " + profileOfUser.firstName }}
                </h3>
                <a-descriptions title="" bordered :column="1">
                  <a-descriptions-item label="First Name: " >
                    {{ profileOfUser.firstName }}
                  </a-descriptions-item>
                  <a-descriptions-item label="Last Name: ">
                    {{ profileOfUser.lastName }}
                  </a-descriptions-item>
                  <a-descriptions-item label="Gender: ">
                    <span v-if="profileOfUser.gender == true"> Male </span>
                    <span v-else> Female </span>
                  </a-descriptions-item>
                  <a-descriptions-item label="Date of Birth: ">
                    {{ generateTime(profileOfUser.dateOfBirth) }}
                  </a-descriptions-item>
                  <a-descriptions-item label="Address: ">
                    {{ profileOfUser.address }}
                  </a-descriptions-item>
                  <a-descriptions-item label="Phone number: ">
                    {{ profileOfUser.phoneNumber }}
                  </a-descriptions-item>
                </a-descriptions>
              </div>
            </a-spin>
            <div class="mt-5 py-5 border-top text-center">
              <div class="row justify-content-center">
                <div class="col-lg-9">
                </div>
              </div>
            </div>
          </div>
        </card>

        <!-- Edit user infor -->
        <a-modal
          v-model="showModal.modalEditUser"
          title="Edit Profile"
          :width="450"
          :maskClosable="false"
          :destroyOnClose="true"
          :closable="false"
        >
          <a-row :gutter="[24, 16]">
            <a-col :span="6">First name: (<span class="red">*</span>) </a-col>
            <a-col :span="18">
              <a-input v-model="formEditUser.firstName" />
              <span v-if="errors.firstName" class="red">
                {{ errors.firstName }}
              </span>
            </a-col>
          </a-row>

          <a-row :gutter="[24, 16]">
            <a-col :span="6">Last name: (<span class="red">*</span>) </a-col>
            <a-col :span="18">
              <a-input v-model="formEditUser.lastName" />
              <span v-if="errors.lastName" class="red">
                {{ errors.lastName }}
              </span>
            </a-col>
          </a-row>

          <a-row :gutter="[24, 16]">
            <a-col :span="6">Avatar: </a-col>
            <a-col :span="18">
              <a-upload
                v-model="formEditUser.avatar"
                name="file"
                list-type="picture-card"
                :action="introductionImageUpload"
                @change="handleImageUploadChange"
                @preview="handlePreviewUploadFile"
                :headers="headers"
                :fileList="fileList"
                accept=".png, .jpg, .jpeg"
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
            <a-col :span="6">Address: (<span class="red">*</span>) </a-col>
            <a-col :span="18">
              <a-input v-model="formEditUser.address" />
              <span v-if="errors.address" class="red">
                {{ errors.address }}
              </span>
            </a-col>
          </a-row>

          <a-row :gutter="[24, 16]">
            <a-col :span="6">Gender: (<span class="red">*</span>) </a-col>
            <a-col :span="18">
              <a-radio-group 
                v-model="formEditUser.gender" 
                name="radioGroup" 
                :default-value="false"
              >
                <a-radio :value="true">
                  Male
                </a-radio>
                <a-radio :value="false">
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
              <a-input v-model="formEditUser.phoneNumber" />
              <span v-if="errors.phoneNumber" class="red">
                {{ errors.phoneNumber }}
              </span>
            </a-col>
          </a-row>

          <a-row :gutter="[24, 16]">
            <a-col :span="6">Date of birth: (<span class="red">*</span>) </a-col>
            <a-col :span="18">
              <a-date-picker v-model="formEditUser.dateOfBirth" :valueFormat="'YYYY-MM-DD'" format="YYYY-MM-DD">
                <template slot="renderExtraFooter">
                </template>
              </a-date-picker>
              <span v-if="errors.dateOfBirth" class="red">
                {{ errors.dateOfBirth }}
              </span>
            </a-col>
          </a-row>

          <template slot="footer">
            <a-button
              key="submit"
              type="primary"
              :loading="loading"
              @click="handleOkEditUser"
            >
              Update
            </a-button>
            <a-button
              key="cancel"
              type="secondary"
              :loading="loading"
              @click="handleCancelEditUser"
            >
              Cancel
            </a-button>
          </template>
        </a-modal>
      </div>
    </section>
  </div>
</template>
<script>

import Vue from "vue";
import ProfileRepository from "../api/profile";
import UserRepository from "../api/user";
import CONFIG from "../config/index";
import moment from "moment";
 import locale from 'ant-design-vue/es/date-picker/locale/zh_CN';

const defaultShowForm = {
  modalEditUser: false,
};

const requiredError = "This field can't blank";

const defaultFormEditUser = {
  account: "",
  firstName: "",
  lastName: "",
  avatar: "",
  address: "",
  gender: 0,
  phoneNumber: "",
  dateOfBirth: "1970-01-01",
};

export default {
  data() {
    return {
      articleImage: `${CONFIG.apiUrl}/image/getImage/{get-image}?id=`,
      // avatar: this.$cookies.get("avatar"),
      displayName: this.$cookies.get("displayName"),
      totalPostedArticle: 0,
      totalContributedArticle: 0,
      totalComments: 0,
      profileOfUser: {},
      imageLoaded: true,
      avatarImageUrl: "",
      loading: false,
      showModal: { ...defaultShowForm },
      formEditUser: { ...defaultFormEditUser },
      fileList: [],
      previewImage: "",
      introductionImageUpload: "",
      headers: "",
      previewVisible: false,
      errors: {
        account: "",
        firstName: "",
        lastName: "",
        address: "",
        gender: "",
        phoneNumber: "",
        dateOfBirth: "",
      },
      locale,
      roleOfUser: "",
    };
  },
  created() {
    this.introductionImageUpload = `${CONFIG.apiUrl}/image/uploadImage/{upload-image}?introductionImage=false`;
    this.getUserInfor()
    this.getTotalPostedArticle();
    this.getTotalContributedArticle();
    this.getTotalComments();
    this.roleOfUser = Vue.$cookies.get("roleCode");
  },
  methods: {
    handleCancel() {
      this.previewVisible = false;
    },
      handleImageUploadChange({ fileList }) {
      this.fileList = [];
      this.fileList = fileList;
      if (this.fileList && this.fileList.length > 0 && this.fileList[0].status === "done") {
        this.formEditUser.avatar = this.fileList[0].response;
      } else {
        this.formEditUser.avatar = null
      }
    },
    // handleImageUploadChange(info) {
    //   this.fileList = [];
    //   const status = info.file.status;
    //   if (status !== "uploading") {
    //   }
    //   if (status === "done") {
    //     this.fileList = info.fileList;
    //     this.formEditUser.avatar = info.fileList[0].response;
    //     this.$message.success(`${info.file.name} file uploaded successfully.`);
    //   } else if (status === "error") {
    //     this.fileList = [];
    //     this.$message.error(info.file.response, 5);
    //   }
    // },
    handlePreviewUploadFile(file) {
      this.previewImage = file.thumbUrl;
      if (this.profileOfUser) {
        this.previewImage =
          this.articleImage + this.profileOfUser.avatar;
      }
      this.previewVisible = true;
    },
    generateTime(dateNumber) {
      const date = new Date(dateNumber);
      const m = moment(date);
      return m.isValid() ? m.format("DD-MM-YYYY") : "";
    },
    async getUserInfor() {
      this.loading = true;
      var account = this.$cookies.get("account")
      await UserRepository.getUserByAccount(account).then(res => {
        this.profileOfUser = res.data.data;
        this.avatarImageUrl = `${CONFIG.apiUrl}/image/getImage/{get-image}?id=${this.profileOfUser.avatar}`;
        

        // this.fileList = [
        //   {
        //     uid: this.profileOfUser.avatar,
        //     name: "image.png",
        //     status: "done",
        //     url: this.articleImage + this.profileOfUser.avatar,
        //   },
        // ];
        
        this.loading = false;
      })
    },
    getTotalPostedArticle() {
      ProfileRepository.countProfileArticle("posted").then((res) => {
        this.totalPostedArticle = res.data.data;
      });
    },
    getTotalContributedArticle() {
      ProfileRepository.countProfileArticle("").then((res) => {
        this.totalContributedArticle = res.data.data;
      });
    },
    getTotalComments() {
      ProfileRepository.countProfileComments().then((res) => {
        this.totalComments = res.data.data;
      });
    },
    handleOkEditUser(e) {
      this.loading = true;
      e.preventDefault();
      const validation = this.validateEditUser();
      if (!validation) {
        this.loading = false;
        return;
      }
      // if (this.formEditUser.avatar) {
      //   this.formEditUser.avatar = this.formEditUser.avatar[0].uid;
      // }
      UserRepository.editUserInfor(this.formEditUser).then(res => {
        if (res.data.data) {
          this.$notification["success"]({
            message: "Edit user succeed !",
          });
          this.getUserInfor();
          Vue.$cookies.set("displayName", this.formEditUser.firstName + " " + this.formEditUser.lastName);
          setTimeout(window.location.reload(), 3000)
        } else {
          this.$notification["error"]({
            message: res.data.message,
          });
          this.loading = false;
        }
        this.showModal.modalEditUser = false;
        this.formEditUser = { ...defaultFormEditUser };
        this.errors.account = "";
        this.errors.firstName = "";
        this.errors.lastName = "";
        this.errors.address = "";
        this.errors.gender = "";
        this.errors.phoneNumber = "";
        this.errors.dateOfBirth =  "";
        this.loading = false;
      })
    },
    validateEditUser() {
      let isValid = true;
      if (
        this.formEditUser.account == "" ||
        this.formEditUser.account == null
      ) {
        this.errors.account = requiredError;
        isValid = false;
      }
      if (
        this.formEditUser.firstName == "" ||
        this.formEditUser.firstName == null
      ) {
        this.errors.firstName = requiredError;
        isValid = false;
      }
      if (
        this.formEditUser.lastName == "" ||
        this.formEditUser.lastName == null
      ) {
        this.errors.lastName = requiredError;
        isValid = false;
      }
      if (
        this.formEditUser.address == "" ||
        this.formEditUser.address == null
      ) {
        this.errors.address = requiredError;
        isValid = false;
      }
      if (
        this.formEditUser.gender === "" ||
        this.formEditUser.gender === null
      ) {
        this.errors.gender = requiredError;
        isValid = false;
      }
      if (
        this.formEditUser.phoneNumber == "" ||
        this.formEditUser.phoneNumber == null || 
        this.isNumeric(this.formEditUser.phoneNumber) === false
      ) {
        this.errors.phoneNumber = "This field can't blank and must be a number!";
        isValid = false;
      }
      if (
        this.formEditUser.dateOfBirth == "" ||
        this.formEditUser.dateOfBirth == null
      ) {
        this.errors.dateOfBirth = requiredError;
        isValid = false;
      }
      return isValid;
    },
    isNumeric(str){
      return /^\d+$/.test(str);
    },
    showModalEditUser() {
      this.headers = {
        authorization: "Bearer " + this.$cookies.get("accessToken"),
        "X-Requested-With": "XMLHttpRequest",
      };

      this.formEditUser.account = this.profileOfUser.account
      this.formEditUser.firstName = this.profileOfUser.firstName
      this.formEditUser.lastName = this.profileOfUser.lastName
      this.formEditUser.gender = this.profileOfUser.gender
      this.formEditUser.dateOfBirth = this.profileOfUser.dateOfBirth
      this.formEditUser.address = this.profileOfUser.address
      this.formEditUser.phoneNumber = this.profileOfUser.phoneNumber
      if(this.profileOfUser.avatar > 0) {
        this.formEditUser.avatar = this.profileOfUser.avatar
        this.previewImage = this.articleImage + this.profileOfUser.avatar
        this.fileList = [
          {
            uid: this.profileOfUser.avatar,
            name: "image.png",
            status: "done",
            url: this.articleImage + this.profileOfUser.avatar,
          },
        ];
      } else {
        // this.formEditUser.avatar = this.profileOfUser.avatar
      }
      this.showModal.modalEditUser = true;
    },
    handleCancelEditUser() {
      this.formEditUser = { ...defaultFormEditUser };
      // this.listUserToAddRole = [];
      this.showModal.modalEditUser = false;

      this.errors.account = "";
      this.errors.firstName = "";
      this.errors.lastName = "";
      this.errors.address = "";
      this.errors.gender = "";
      this.errors.phoneNumber = "";
      this.errors.dateOfBirth = "";
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
</style>
