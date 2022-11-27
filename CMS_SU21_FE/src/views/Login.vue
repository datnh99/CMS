<template>
  <section class="section section-shaped section-lg my-0">
    <!-- <base-button @click="handleSignin" type="neutral">
                  <img slot="icon" src="img/icons/common/google.svg" />
                  FPT.EDU.VN
                </base-button> -->
    <div class="shape shape-style-1">
      <img
        src="../../public/img/theme/sanhAlpha.jpg"
        style="max-width: 100%"
        alt=""
      />
      <!-- <img
        src="https://scontent.fhan2-4.fna.fbcdn.net/v/t1.6435-9/66150955_1220380638144699_1312774122870145024_n.jpg?_nc_cat=105&ccb=1-3&_nc_sid=8bfeb9&_nc_ohc=4pGAUlhe6QAAX_k_hfD&_nc_ht=scontent.fhan2-4.fna&oh=0929b97e167401c5f895564bf7c3122b&oe=6130DBE0"
        style="max-width: 100%"
        alt=""
      /> -->
      <!-- <span></span>
      <span></span>
      <span></span>
      <span></span>
      <span></span>
      <span></span>
      <span></span>
      <span></span> -->
    </div>
    <div class="container pt-lg-md">
      <div class="row justify-content-center">
        <div class="col-lg-5">
          <a-spin :spinning ="loading">
            <a-icon
              slot="indicator"
              type="loading"
              :spinning="loading"
              style="font-size: 24px;color:#fc5730"
            />
            <card
              type="secondary"
              shadow
              header-classes="bg-white pb-5"
              body-classes="px-lg-5 py-lg-5"
              class="border-0"
            >
              <template>
                <div class="text-muted text-center mb-3">
                  <small>Sign in with</small>
                </div>
                <div class="btn-wrapper text-center">
                  <!-- <base-button type="neutral">
                                    <img slot="icon" src="img/icons/common/github.svg">
                                    Github
                                </base-button> -->

                  <base-button @click="handleSignin" type="neutral">
                    <img slot="icon" src="img/icons/common/google.svg" />
                    FPT.EDU.VN
                  </base-button>
                </div>
              </template>
              <template>
                <div class="text-center text-muted mb-4">
                  <small>Or sign in with credentials</small>
                </div>
                <form role="form">
                  <base-input
                    alternative
                    v-model="username"
                    class="mb-3"
                    placeholder="Email"
                    addon-left-icon="ni ni-email-83"
                  >
                  </base-input>
                  <base-input
                    alternative
                    v-model="password"
                    type="password"
                    placeholder="Password"
                    addon-left-icon="ni ni-lock-circle-open"
                  >
                  </base-input>
                  <base-checkbox> Remember me </base-checkbox>
                  <div class="text-center">
                    <base-button type="primary" class="my-4" @click="basicLogin"
                      >Sign In</base-button
                    >
                  </div>
                </form>
              </template>
            </card>
          </a-spin>

          <div class="row mt-3">
            <div class="col-6">
              <!-- <a href="#" class="text-light">
                                <small>Forgot password?</small>
                            </a> -->
            </div>
            <div class="col-6 text-right">
              <!-- <a href="#" class="text-light">
                                <small>Create new account</small>
                            </a> -->
            </div>
          </div>
        </div>
      </div>
    </div>
    <a-modal
      v-model="visible"
      title="Choose your roles"
      on-ok="handleOk"
      :maskClosable="false"
    >
      <template slot="footer">
        <a-button
          key="submit"
          type="primary"
          :loading="loading"
          @click="handleOk"
        >
          Continue
        </a-button>
      </template>
      <a-form
        :form="form"
        :label-col="{ span: 8 }"
        :wrapper-col="{ span: 14 }"
        @submit="handleSubmit"
      >
        <a-form-item label="Your roles">
          <a-select
            v-decorator="[
              'role',
              {
                rules: [
                  { required: true, message: 'Please select your role!' },
                ],
              },
            ]"
            placeholder="Please select your role!"
          >
            <a-select-option
              v-for="item in listRoles"
              :key="item.roleCode"
              :value="item.roleCode"
            >
              {{ item.roleName }}
            </a-select-option>
          </a-select>
        </a-form-item>
      </a-form>
    </a-modal>

    <!-- Add user -->
    <a-modal
      v-model="showModalAddUser"
      title="User Information"
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
            <img alt="example" style="width: 100%" :src="previewImage" />
          </a-modal>
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
            <a-radio :value="1"> Male </a-radio>
            <a-radio :value="0"> Female </a-radio>
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
            <template slot="renderExtraFooter"> </template>
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
  </section>
</template>
<script>
import { processGoogleToken, basicProcessLogins } from "../api/processLogin";
import RoleRepository from "../api/role";
import UserRepository from "../api/user";
import axios from "axios";
import CONFIG from "../config/index";

const requiredError = "This field can't blank";
const defaultFormAddUser = {
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
      username: "",
      password: "",
      loading: false,
      visible: false,
      listRoles: [],
      userData: {},
      formLayout: "horizontal",
      form: this.$form.createForm(this, { name: "coordinated" }),
      showModalAddUser: false,
      errors: {
        account: "",
        category: "",
        firstName: "",
        lastName: "",
        address: "",
        gender: "",
        phoneNumber: "",
        dateOfBirth: "",
      },
      formAddUser: { ...defaultFormAddUser },
      fileList: [],
      previewImage: "",
      headers: "",
      previewVisible: false,
      introductionImageUpload: "",
      shareId: null,
      userLogin:null
    };
  },
  created() {
    this.introductionImageUpload = `${CONFIG.apiUrl}/image/uploadImage/{upload-image}?introductionImage=false`;
    const { shareId } = this.$route.params;
    if (shareId) {
      this.shareId = window.atob(shareId);
      if (this.$cookies.get("account")) {
        this.$router.push("/article-detail/" + this.shareId);
      }
    }
  },
  methods: {
    async basicLogin() {
      this.userData = await basicProcessLogins(this.username, this.password);
      if (this.userData) {
        this.listRoles = this.userData.roleList;
        this.showModal();
      }
    },
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
    checkUserInfor() {
      //Check have user infor ? if not ==> add
      let account = this.userData.account;
      this.headers = {
        authorization: "Bearer " + this.userData.token,
        "X-Requested-With": "XMLHttpRequest",
      };
      let existAccount = false;
      UserRepository.searchUserByAccount(account).then((res) => {
        var listUser = res.data.data.items;
        if (listUser != null && listUser.length > 0) {
          for (let i = 0; i < listUser.length; i++) {
            if (listUser[i].account === account) {
              existAccount = true;
              break;
            }
          }
        }
        if (existAccount) {
          this.gotoSetlectRole();
          this.loading = false;
        } else {
          this.formAddUser.firstName = this.userData.gg_profile.name.split(' ').slice(0, -1).join(' ');
                    this.formAddUser.lastName = this.userData.gg_profile.name.split(' ').slice(-1).join(' ');

          this.showModalAddUser = true;
          this.loading = false;
        }
      });
    },
    gotoSetlectRole() {
      if (this.userData && this.userData.userDetail.roleCode) {
        if (
          this.userData.userDetail.roleCode.includes("super_admin") ||
          this.userData.userDetail.roleCode.includes("editor") ||
          this.userData.userDetail.roleCode.includes("moderator") ||
          this.userData.userDetail.roleCode.includes("writer")
        ) {
          this.visible = true;
          this.listRoles = this.userData.roleList;
        }
      } else if (
        this.userData.userDetail &&
        this.userData.roleList.length <= 0
      ) {
        this.$cookies.set("roleCode", "", this.userData.expriedTime);
        this.$cookies.set(
          "account",
          this.userData.account,
          this.userData.expriedTime
        );
        this.$cookies.set(
          "accessToken",
          this.userData.token,
          this.userData.expriedTime
        );
        this.$cookies.set(
          "google.token",
          this.userData.idToken,
          this.userData.expriedTime
        );
        this.$cookies.set(
          "displayName",
          this.userData.userDetail.firstName +
            " " +
            this.userData.userDetail.lastName,
          this.userData.expriedTime
        );
        this.$cookies.set(
          "dateOfBirth",
          this.userData.userDetail.dateOfBirth,
          this.userData.expriedTime
        );
        this.$cookies.set(
          "avatar",
          window.btoa(this.userData.userDetail.avatar),
          this.userData.expriedTime
        );
        axios.defaults.headers.common["Authorization"] =
          "Bearer " + this.userData.token;
        if (this.shareId) {
          this.$router.push("/article-detail/" + this.shareId);
        } else {
          this.$router.push("/landing");
        }
      }
    },
    handleCancelAddUser() {
      this.formAddUser = { ...defaultFormAddUser };
      // this.listUserToAddRole = [];
      this.showModalAddUser = false;

      this.errors.account = "";
      this.errors.firstName = "";
      this.errors.lastName = "";
      this.errors.address = "";
      this.errors.gender = "";
      this.errors.phoneNumber = "";
      this.errors.dateOfBirth = "";
    },
    handleOkAddUser(e) {
      e.preventDefault();
      const validation = this.validateAddUser();
      if (!validation) {
        return;
      }
      this.formAddUser.account = this.userData.account;

      UserRepository.searchUserByAccount(this.formAddUser.account).then(
        (res) => {
          var listUser = res.data.data.items;

          let existAccount = false;
          if (listUser != null && listUser.length > 0) {
            for (let i = 0; i < listUser.length; i++) {
              if (listUser[i].account === this.formAddUser.account) {
                this.$notification.error({
                  message:
                    "User with account '" +
                    this.formAddUser.account +
                    "' have already exist!",
                });
                existAccount = true;
                break;
              }
            }
          }
          if (!existAccount) {
            this.addUser();
          }
        }
      );
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
            this.$notification["success"]({
              message: "Add user's information successfully!",
            });
            // this.$cookies.set(
            //   "displayName",
            //   this.formAddUser.firstName + " " + this.formAddUser.lastName,
            //   this.userData.expiredTime
            // );
            // this.$cookies.set(
            //   "dateOfBirth",
            //   this.formAddUser.dateOfBirth,
            //   this.userData.expiredTime
            // );
            // this.$cookies.set(
            //   "avatar",
            //   window.btoa(this.formAddUser.avatar),
            //   this.userData.expiredTime
            // );
            this.showModalAddUser = false;
            this.formAddUser = { ...defaultFormAddUser };
            this.errors.account = "";
            this.errors.category = "";
            this.errors.firstName = "";
            this.errors.lastName = "";
            this.errors.address = "";
            this.errors.gender = "";
            this.errors.phoneNumber = "";
            this.errors.dateOfBirth = "";
             //this.gotoSetlectRole();
                         this.processToken(this.userLogin);

            this.$router.push("/landing");

          } else {
            this.$notification["error"]({
              message: res.data.message,
            });
          }
        })
        .catch((err) => {
          this.$notification["error"]({
            message: "Add User's information failed !",
          });
        });
      this.loading = false;
    },
    validateAddUser() {
      let isValid = true;
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
      if (this.formAddUser.address == "" || this.formAddUser.address == null) {
        this.errors.address = requiredError;
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
    saveLoggedInUserRole(role) {
      RoleRepository.saveLoggedInUserRole(role).then((res) => {});
    },
    handleSubmit(e) {
      e.preventDefault();
      this.form.validateFields((err, values) => {
        if (!err) {
          const expiredTime = this.userData.expiredTime;
          this.$cookies.set("accessToken", this.userData.token, expiredTime);
          this.$cookies.set("account", this.userData.account, expiredTime);
          this.$cookies.set("google.token", this.userData.idToken, expiredTime);
          axios.defaults.headers.common["Authorization"] =
            "Bearer " + this.userData.token;
          this.$cookies.set("roleCode", values.role, expiredTime);
          this.$cookies.set(
            "displayName",
            this.userData.userDetail.firstName +
              " " +
              this.userData.userDetail.lastName,
            expiredTime
          );
          this.$cookies.set(
            "dateOfBirth",
            this.userData.userDetail.dateOfBirth,
            expiredTime
          );
          this.$cookies.set(
            "avatar",
            window.btoa(this.userData.userDetail.avatar),
            expiredTime
          );
          this.saveLoggedInUserRole(values.role);
          if (
            values.role === "super_admin" ||
            values.role === "editor" ||
            values.role === "moderator" ||
            values.role === "writer"
          ) {
            // this.$router.push("/landing");
            if (this.shareId) {
              this.$router.push("/article-detail/" + this.shareId);
            } else {
              this.$router.push("/admin-action");
            }
          }

          this.visible = false;
        }
      });
    },
    // handleSelectChange(value) {
    //   this.form.setFieldsValue({
    //     note: `Hi, ${value === "male" ? "man" : "lady"}!`,
    //   });
    // },
    showModal() {
      this.visible = true;
    },
    handleOk(e) {
      this.loading = true;
      setTimeout(() => {
        this.loading = false;
        this.handleSubmit(e);
      }, 500);
    },
    processToken: async function (userLogin) {
      this.userData = await processGoogleToken(userLogin);
      console.log(this.userData.gg_profile);
      if (!this.userData) {
        this.$notification.error({
          message: "Please use FPT University's account to login!",
        });
        return;
      }
      this.checkUserInfor();
    },
    handleSignin() {
      this.loading = true;
      this.$gAuth
        .signIn()
        .then((GoogleUser) => {
          // on success do something
          var userInfo = {
            loginType: "google",
            google: GoogleUser,
          };
          const username = GoogleUser.getBasicProfile().pu;
          const userLogin = {
            username: username,
            idToken: GoogleUser.getAuthResponse().id_token,
          };
          this.userLogin = userLogin
          this.processToken(userLogin);
          //   this.$store.commit('setLoginUser', userInfo)
        })
        .catch((error) => {
        });
      // testApi.testLogin().then(res=>{
      // })
    },
  },
};
</script>
<style>
</style>
