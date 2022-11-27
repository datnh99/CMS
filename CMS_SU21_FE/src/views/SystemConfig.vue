<template>
  <div class="profile-page">
    <section class="section-profile-cover section-shaped my-0">
      <div class="container-fluid">
        <h4 class="text-white screen-name">SYSTEM CONFIGURATION</h4>
      </div>
    </section>
    <section class="section section-skew">
      <div class="container">
        <card shadow class="card-profile mt-200" no-body>
          <div class="px-4">
            <div class="gutter-example">
              <!-- <div class="page-header">
                <h2 class="display-4">Category Management</h2>
              </div> -->
            </div>

            <div class="gutter-example lg-md">
              <a-row :gutter="36">
                <!-- <a-col
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
                </a-col> -->

                <!-- <a-col
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
                </a-col> -->
                <a-col class="gutter-row mt-10" :span="5" :col="6">
                  <div class="gutter-box">
                    <br />
                    <span @click="showModalEmail()">
                      <base-button
                        type="primary"
                        size="md"
                        :disabled="loading"
                        class="mr-4 btn-neutral btn-filter main-btn"
                      >
                        <i class="fa fa-edit"></i>
                        Change system email
                      </base-button>
                    </span>
                  </div>
                </a-col>
                <a-col class="gutter-row mt-10" :span="4" :col="4">
                  <div class="gutter-box">
                    <br />
                    <span @click="showModalAddWords()">
                      <base-button
                        type="primary"
                        size="md"
                        :disabled="loading"
                        class="mr-4 btn-neutral btn-filter main-btn"
                      >
                        <i class="fa fa-plus-circle"></i>
                        Add blacklist words
                      </base-button>
                    </span>
                  </div>
                </a-col>
                <a-col class="gutter-row mt-10" :span="4" :col="4">
                  <div class="gutter-box">
                    <br />
                    <span @click="searchBlackListWords">
                      <base-button
                        type="primary"
                        size="md"
                        :disabled="loading"
                        class="mr-4 ml-4 btn-neutral btn-filter main-btn"
                      >
                        <i class="fa fa-search"></i>
                        Show blacklist words
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
              <div
                :style="{ background: '#fff', minHeight: '280px' }"
                v-if="listWords.length > 0"
              >
                <a-table
                  :columns="columns"
                  :data-source="listWords"
                  :scroll="{ x: 1000 }"
                  :pagination="false"
                >
                  <template #createDateCustom="item">
                    <span>{{ generateTime(item.createdTime) }}</span>
                  </template>
                  <template #action="item">
                    <span @click="deleteBlaclistWord(item.id)">
                      <base-button
                        type="primary"
                        size="md"
                        :disabled="loading"
                        class="mr-4 btn-neutral btn-filter main-btn"
                      >
                        <i class="fa fa-trash"></i>
                      </base-button>
                    </span>
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
    <a-modal
      :maskClosable="false"
      :closable="false"
      v-model="show.visibleEmail"
      title="Change email"
      on-ok="handleOkEmail"
    >
      <template slot="footer">
        <a-button key="back" @click="handleCancelEmail"> Return </a-button>
        <a-button type="default" @click="testEmailConnection()">
          Test connection
        </a-button>
        <a-button
          key="submit"
          type="primary"
          :loading="loading"
          @click="handleOkEmail"
          :disabled="hasErrors(form.getFieldsError())"
        >
          Submit
        </a-button>
      </template>
      <a-form v-bind="formItemLayout" :form="form" @submit="handleSubmit">
        <a-form-item
          label="Email"
          :validate-status="userNameError() ? 'error' : ''"
          :help="userNameError() || ''"
        >
          <a-input
            v-decorator="[
              'userName',
              {
                rules: [
                  { required: true, message: 'Please input your username!' },
                ],
              },
            ]"
            placeholder="Username"
          >
            <a-icon
              slot="prefix"
              type="user"
              style="color: rgba(0, 0, 0, 0.25)"
            />
          </a-input>
        </a-form-item>
        <a-form-item
          label="Password"
          :validate-status="passwordError() ? 'error' : ''"
          :help="passwordError() || ''"
        >
          <a-input
            v-decorator="[
              'password',
              {
                rules: [
                  { required: true, message: 'Please input your Password!' },
                ],
              },
            ]"
            type="password"
            placeholder="Password"
          >
            <a-icon
              slot="prefix"
              type="lock"
              style="color: rgba(0, 0, 0, 0.25)"
            />
          </a-input>
        </a-form-item>
        <a-form-item> </a-form-item>
      </a-form>
    </a-modal>
    <a-modal
      :maskClosable="false"
      :closable="false"
      v-model="show.visibleAddWords"
      title="Add blacklist words "
      on-ok="handleOkEmail"
    >
      <template slot="footer">
        <a-button key="back" @click="handleCancelAddWords"> Return </a-button>
        <a-button
          key="submit"
          type="primary"
          :loading="loading"
          @click="handleOkAddWords"
          :disabled="words.length == 0"
        >
          Submit
        </a-button>
      </template>
      <div>
        <template v-for="(tag, index) in words">
          <a-tooltip v-if="tag.length > 20" :key="tag" :title="tag">
            <a-tag
              :key="tag"
              :closable="index !== 0"
              @close="() => handleClose(tag)"
            >
              {{ `${tag.slice(0, 20)}...` }}
            </a-tag>
          </a-tooltip>
          <a-tag
            v-else
            :key="tag"
            :closable="index !== 0"
            @close="() => handleClose(tag)"
          >
            {{ tag }}
          </a-tag>
        </template>
        <a-input
          v-if="inputVisible"
          ref="input"
          type="text"
          size="small"
          :style="{ width: '78px' }"
          :value="inputValue"
          @change="handleInputChange"
          @blur="handleInputConfirm"
          @keyup.enter="handleInputConfirm"
        />
        <a-tag
          v-else
          style="background: #fff; borderstyle: dashed"
          @click="showInput"
        >
          <a-icon type="plus" /> New word
        </a-tag>
      </div>
    </a-modal>
  </div>
</template>
<script>
import moment from "moment";
import SystemConfigRepository from "../api/systemConfig";
import EmailRepository from "../api/email";
function hasErrors(fieldsError) {
  return Object.keys(fieldsError).some((field) => fieldsError[field]);
}
const defaultShowModal = {
  visibleEmail: false,
  visibleAddWords: false,
};
const columns = [
  {
    title: "Index",
    width: 100,
    dataIndex: "index",
    key: "index",
    fixed: "left",
  },
  {
    title: "Content",
    width: 150,
    dataIndex: "content",
    key: "content",
    fixed: "left",
  },
  {
    title: "Created By",
    width: 140,
    dataIndex: "createdBy",
    key: "createdBy",
  },
  {
    title: "Created Date",
    width: 120,
    // dataIndex: "CreateDate",
    scopedSlots: { customRender: "createDateCustom" },
    key: "createdTime",
  },
  {
    title: "Action",
    key: "operation",
    fixed: "right",
    width: 120,
    scopedSlots: { customRender: "action" },
  },
];
export default {
  name: "my-article",
  data() {
    return {
      columns,
      show: { ...defaultShowModal },
      hasErrors,
      form: this.$form.createForm(this, { name: "horizontal_login" }),
      formItemLayout: {
        labelCol: { span: 6 },
        wrapperCol: { span: 14 },
      },
      loading: false,
      totals: 0,
      current: 1,
      contentId: "",
      paginationRange: [0, 0],
      pageSize: 10,
      words: [],
      inputVisible: false,
      inputValue: "",
      searchBlackListWord: "",
      listWords: [],
    };
  },
  beforeCreate() {
    this.form = this.$form.createForm(this, { name: "validate_other" });
  },
  created() {
    this.getEmailConfig();
  },
  mounted() {},
  methods: {
    deleteBlaclistWord(id) {
      this.loading = true;
      SystemConfigRepository.deleteBlackListWord(id).then((res) => {
        this.loading = false;
        if (res.success) {
          this.$$notification.success({
            message: "Delete word successfully !",
          })
          this.searchBlackListWords()
        } else {
          this.$$notification.error();
          ({
            message: "Delete word failed !",
          });
        }
      });
    },
    searchBlackListWords() {
      this.loading = true;
      SystemConfigRepository.searchBlacklistWords(
        this.searchBlackListWord,
        this.current
      ).then((res) => {
        this.loading = false;
        this.listWords = res.data.data.items;
        this.totals = res.data.data.totals;
      });
    },
    handleClose(removedTag) {
      const words = this.words.filter((tag) => tag !== removedTag);
      this.words = words;
    },

    showInput() {
      this.inputVisible = true;
      this.$nextTick(function () {
        this.$refs.input.focus();
      });
    },

    handleInputChange(e) {
      this.inputValue = e.target.value;
    },

    handleInputConfirm() {
      const inputValue = this.inputValue;
      let words = this.words;
      if (inputValue && words.indexOf(inputValue) === -1) {
        words = [...words, inputValue];
      }
      Object.assign(this, {
        words,
        inputVisible: false,
        inputValue: "",
      });
    },
    userNameError() {
      const { getFieldError, isFieldTouched } = this.form;
      return isFieldTouched("userName") && getFieldError("userName");
    },
    // Only show error after a field is touched.
    passwordError() {
      const { getFieldError, isFieldTouched } = this.form;
      return isFieldTouched("password") && getFieldError("password");
    },
    handleSubmit(e) {
      e.preventDefault();
      this.form.validateFields((err, values) => {
        if (!err) {
          const emailInfor = window.btoa(
            values.userName + ":" + values.password
          );
          SystemConfigRepository.setEmailConfig(emailInfor).then((res) => {
            if (res.data.message) {
              this.$notification.success({
                message: "Set email successfully!",
              });
              this.handleCancelEmail(e);
            }
          });
        }
      });
    },
    getEmailConfig() {
      SystemConfigRepository.getEmailConfig().then((res) => {
        if (res.data.data) {
          this.form.setFieldsValue({
            userName: res.data.data,
          });
        }
      });
    },
    testEmailConnection() {
      EmailRepository.testEmailConnection().then((res) => {
        if (res.data.data) {
          this.$notification.success({
            message: "Test email was sent to your email !",
          });
        } else {
          this.$notification.success({
            message: "Please check your password or email again !",
          });
        }
      });
    },
    addBlacklistWords() {
      this.loading = true;
      SystemConfigRepository.addBlacklistWords(this.words).then((res) => {
        if (res.data.success) {
          this.loading = false;
          this.$notification.success({
            message: "Add blacklist words successfully !",
          });
          this.handleCancelAddWords();
        } else {
          this.$notification.error({
            message: "Add blacklist words failed !",
          });
        }
      });
    },
    showModalAddWords() {
      this.show.visibleAddWords = true;
    },
    showModalEmail() {
      this.getEmailConfig();
      this.show.visibleEmail = true;
    },
    handleCancelAddWords(e) {
      this.show = { ...defaultShowModal };
      this.words = [];
    },
    handleOkAddWords(e) {
      this.addBlacklistWords();
    },
    handleOkEmail(e) {
      this.loading = true;
      setTimeout(() => {
        this.show = { ...defaultShowModal };
        this.loading = false;
        this.handleSubmit(e);
      }, 3000);
    },
    handleCancelEmail(e) {
      this.show = { ...defaultShowModal };
      this.form.resetFields();
    },
    generateTime(dateNumber) {
      const date = new Date(dateNumber);
      const m = moment(date);
      return m.isValid() ? m.format("DD-MM-YYYY") : "";
    },

    submitForm() {
      this.resetsearchForm();
    },
    resetFilter() {},
    resetsearchForm() {},
    paginate(current = 1) {},
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