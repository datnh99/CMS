<template>
  <loading
    v-if="loadPage"
    v-model="loadPage"
    :can-cancel="true"
    loader="dots"
    :is-full-page="false"
    color="#FC5730"
  ></loading>
  <div v-else>
    <div class="position-relative profile-page">
      <!-- shape Hero -->
      <section class="section-shaped my-0">
        <div class="image-container">
          <img
            :src="introductionImageUrl"
            class="bg-image"
            :style="styleBgImage()"
            alt=""
            onerror="this.src='https://dnuni.fpt.edu.vn/wp-content/uploads/2020/02/bg-footer-1.jpg'"
          />
        </div>
        <div class="container">
          <div class="col px-0">
            <div class="row">
              <div class="col-lg-12 title">
                <span class="display-4">
                  {{ article.title }}
                </span>
              </div>
            </div>
          </div>
        </div>
      </section>
      <section class="section section-skew">
        <div class="container">
          <card shadow class="card-profile" no-body>
            <div class="px-4">
              <div class="row justify-content-center">
                <div class="col-lg-3 order-lg-2">
                  <div class="card-profile-image"></div>
                </div>
                <div
                  class="col-lg-4 order-lg-3 text-lg-right align-self-lg-center"
                >
                  <div class="card-profile-actions py-4 mt-lg-0">
                    <span class="created-date"
                      >Last modified :
                      {{ generateTime(article.modifiedTime) }}</span
                    >
                  </div>
                </div>
                <div class="col-lg-4 order-lg-1">
                  <div class="card-profile-stats d-flex">
                    <div class="author-info">
                      <span class="heading">
                        <a-avatar :src="imageApiUrl + authorDetail.avatar">
                        </a-avatar
                      ></span>
                      <span
                        class="description"
                        style="min-width: 110px"
                        v-if="fullName"
                        >{{ fullName }}</span
                      >
                      <span
                        class="description"
                        style="min-width: 110px"
                        v-else
                        >{{
                          authorDetail.firstName + " " + authorDetail.lastName
                        }}</span
                      >
                    </div>
                    <div v-if="!isFollowed && article.author !== username">
                      <span class="heading">
                        <!-- <a-button
                          :disabled="article.author === username"
                          type="secondary"
                          shape="circle"
                          @click="followUser"
                        > -->
                        <a
                          href="javascript:void(0)"
                          :disabled="previewArticle !== null"
                        >
                          <a-icon
                            @click="followUser"
                            type="heart"
                            theme="twoTone"
                            class="follow-user-btn article-action"
                          />
                        </a>
                        <!-- </a-button> -->
                      </span>
                      <span class="description">Follow</span>
                    </div>
                    <div v-else-if="isFollowed && article.author !== username">
                      <span class="heading">
                        <!-- <a-button
                          class="btn-unfollow"
                          type="danger"
                          shape="circle"
                          @click="unfollowUser"
                        > -->
                        <a
                          href="javascript:void(0)"
                          :disabled="previewArticle !== null"
                        >
                          <a-icon
                            type="heart"
                            class="article-action"
                            theme="twoTone"
                            two-tone-color="#eb2f96"
                            @click="unfollowUser"
                          />
                        </a>
                        <!-- </a-button> -->
                      </span>
                      <span class="description">Unfollow</span>
                    </div>

                    <!-- <div>
                      <span class="heading">10</span>
                      <span class="description">Photos</span>
                    </div> -->
                    <div>
                      <!-- <a-button type="secondary" shape="circle"> -->
                      <span class="heading article-action">{{
                        totalComment
                      }}</span>
                      <!-- </a-button> -->
                      <span class="description">Comments</span>
                    </div>
                    <div>
                      <!-- <a-button
                        type="secondary"
                        shape="circle"
                       
                      > -->
                      <a
                        href="javascript:void(0)"
                        :disabled="previewArticle !== null"
                      >
                        <a-icon
                          class="article-action"
                          type="share-alt"
                          style="color: #fc5730; font-size: 26px"
                          @click="openShareForm"
                        />
                      </a>
                      <!-- </a-button> -->
                      <span class="description">Share</span>
                    </div>
                    <div>
                      <span>
                        <span class="heading">
                          <a
                            href="javascript:void(0)"
                            rel="noopener noreferrer"
                            :disabled="reported || previewArticle !== null"
                            @click="openReportArticleModal"
                          >
                            <a-icon
                              class="article-action"
                              style="color: red"
                              type="exclamation-circle"
                          /></a>
                        </span>
                        <span class="description" v-if="!reported">Report</span>
                        <span class="description" v-if="reported"
                          >Reported</span
                        >
                      </span>
                    </div>
                    <div>
                      <span>
                        <span class="heading">
                          <a-popover
                            v-model="visibleAttachments"
                            title="Download"
                            trigger="click"
                            placement="bottom"
                          >
                            <div slot="content" style="width: 200px">
                              <div
                                class="row attachments-content"
                                v-for="item in attachmentList"
                                :key="item.uid"
                              >
                                <a
                                  href="javascript:void(0)"
                                  v-if="previewArticle"
                                  >{{ item.name }}</a
                                >
                                <a :href="item.url" v-else>{{ item.name }}</a>
                              </div>
                              <!-- <div class="row"><a @click="hideAttachments">Close</a></div>  -->
                            </div>
                            <a
                              href="javascript:void(0)"
                              rel="noopener noreferrer"
                            >
                              <a-icon class="article-action" type="file-zip" />
                            </a>
                          </a-popover>
                        </span>
                        <span class="description">Attachments</span>
                      </span>
                    </div>
                  </div>
                </div>
              </div>
              <div class="mt-5 main-content">
                <div v-html="article.content"></div>
                <div class="authored">
                  <b>Written by:</b>
                  <strong
                    class="description"
                    style="font-style: italic"
                    v-if="fullName"
                  >
                    {{ fullName }}
                  </strong>
                  <strong class="description" style="font-style: italic" v-else>
                    {{ authorDetail.firstName + " " + authorDetail.lastName }}
                  </strong>
                </div>
              </div>
              <div class="mt-5 py-5 border-top" v-if="previewArticle === null">
                <div class="row">
                  <div class="col-lg-12">
                    <div class="row">
                      <div class="col-12">
                        <a-comment v-for="item in listComment" :key="item.id">
                          <span slot="actions" @click="showModal(item)"
                            >Reply to</span
                          >
                          <span
                            slot="actions"
                            @click="showEditCommentModal(item)"
                          >
                            <a-icon type="edit" theme="filled" />
                          </span>
                          <span
                            slot="actions"
                            v-if="visibleDeleteComment(item.createdBy)"
                          >
                            <a-popconfirm
                              title="Are you sure delete this comment?"
                              ok-text="Yes"
                              cancel-text="No"
                              @confirm="deleteComment(item.id)"
                            >
                              <a-icon type="delete" theme="filled" />
                            </a-popconfirm>
                          </span>

                          <a slot="author">{{ item.displayName }}</a>
                          <a-avatar slot="avatar" :alt="item.displayName">
                            <a-icon slot="icon" type="user" />
                          </a-avatar>
                          <!-- <template slot="actions">
                            <span
                              v-for="(action, index) in item.actions"
                              :key="index"
                              >{{ action }}</span
                            >
                          </template> -->
                          <p slot="content">
                            {{ item.content }}
                          </p>
                          <a-tooltip
                            slot="datetime"
                            :title="
                              item.modifiedTime.format('YYYY-MM-DD HH:mm:ss')
                            "
                          >
                            <span>{{ item.modifiedTime.fromNow() }} </span>
                          </a-tooltip>
                          <a-comment
                            v-for="child in item.children"
                            :key="child.id"
                          >
                            <!-- <span slot="actions" @click="showModal(item)"
                        >Reply to</span
                      > -->
                            <a slot="author">{{ child.createdBy }}</a>
                            <a-avatar slot="avatar" :alt="child.createdBy">
                              <a-icon slot="icon" type="user" />
                            </a-avatar>
                            <span
                              slot="actions"
                              @click="showEditCommentModal(child)"
                            >
                              <a-icon type="edit" theme="filled" />
                            </span>
                            <span
                              slot="actions"
                              v-if="username === child.createdBy"
                            >
                              <a-popconfirm
                                title="Are you sure delete this comment?"
                                ok-text="Yes"
                                cancel-text="No"
                                @confirm="deleteComment(child.id)"
                              >
                                <a-icon type="delete" theme="filled" />
                              </a-popconfirm>
                            </span>
                            <p slot="content">
                              {{ child.content }}
                            </p>
                            <a-tooltip
                              slot="datetime"
                              :title="
                                moment(child.modifiedTime).format(
                                  'YYYY-MM-DD HH:mm:ss'
                                )
                              "
                            >
                              <span>{{
                                moment(child.modifiedTime).fromNow()
                              }}</span>
                            </a-tooltip>
                          </a-comment>
                        </a-comment>
                      </div>
                    </div>
                    <div class="row">
                      <div class="col-12">
                        <div class="gutter-example pt-md pagnigation-custom">
                          <a-pagination
                            v-model="current"
                            show-quick-jumper
                            :default-current="1"
                            :total="totalComment"
                            @change="paginate"
                          />
                        </div>
                      </div>
                    </div>

                    <a-comment v-if="!visible && !visibleEditComment">
                      <a-avatar slot="avatar" alt="Han Solo">
                        <a-icon slot="icon" type="user" />
                      </a-avatar>
                      <div slot="content">
                        <a-form-item>
                          <a-textarea
                            placeholder="Write something..."
                            :rows="4"
                            @change="commentContentChange"
                            v-model="formComment.content"
                          />
                          <p v-if="commentError" style="color: red">
                            {{ commentError }}
                          </p>
                        </a-form-item>

                        <a-form-item>
                          <base-button
                            @click="addComment"
                            :loading="loading"
                            :disabled="!formComment.content || sensitiveCheck"
                            type="primary"
                            size="md"
                            class="btn btn-primary main-btn mr-4"
                          >
                            <i class="fa fa-plus-circle"></i>

                            Comment
                          </base-button>
                        </a-form-item>
                      </div>
                    </a-comment>
                    <!-- <a href="#" cs>Show more</a> -->
                  </div>
                </div>
              </div>
            </div>
          </card>
        </div>
      </section>
      <!-- 1st Hero Variation -->
    </div>
    <div class="container container-lg related-topic">
      <div class="row justify-content-center" style="padding-bottom: 2%">
        <div class="col-lg-12 text-center">
          <h3 class="text-primary related-topic-text">Related topic</h3>
        </div>
      </div>

      <div class="row">
        <div class="col-12">
          <a-carousel dotsClass="dotStyle" autoplay arrows>
            <div v-for="(sublist, index) in listRelated" :key="index">
              <a-row :gutter="(sublist.length - 1) * 8">
                <CardArticle
                  v-for="item in sublist"
                  :key="item.id"
                  :article="item"
                  :weight="8"
                >
                </CardArticle>
              </a-row>
            </div>
          </a-carousel>
        </div>
      </div>
    </div>
    <a-modal
      v-model="visibleShare"
      title="Share article"
      on-ok="handleOk"
      :closable="false"
      :maskClosable="false"
    >
      <template slot="footer">
        <a-button key="back" @click="handleShareCancel"> Cancel </a-button>
        <a-button
          key="submit"
          type="danger"
          :loading="loading"
          :disabled="listMail.length === 0"
          @click="handleShareOk"
        >
          Share
        </a-button>
      </template>
      <div class="row">
        <div class="col-8">
          <a-input
            :disabled="true"
            v-model="shareForm.linkReference"
            placeholder="input search text"
            size="large"
          >
          </a-input>
        </div>
        <div class="col-4">
          <base-button
            class="btn btn-neutral btn-filter main-btn"
            size="md"
            v-clipboard:copy="shareForm.linkReference"
            v-clipboard:success="onCopy"
            v-clipboard:error="onError"
          >
            Copy
          </base-button>
        </div>
      </div>
      <br />
      <template v-for="mail in listMail">
        <a-tooltip v-if="mail.length > 20" :key="mail" :title="mail">
          <a-tag
            :key="mail"
            :closable="true"
            @close="() => handleCloseMailto(mail)"
          >
            {{ `${mail.slice(0, 20)}...` }}
          </a-tag>
        </a-tooltip>
        <a-tag
          v-else
          :key="mail"
          :closable="true"
          @close="() => handleCloseMailto(mail)"
        >
          {{ mail }}
        </a-tag>
      </template>
      <a-input
        v-if="inputTagVisible"
        ref="input"
        type="text"
        size="small"
        :style="{ width: '110px' }"
        :value="inputTagValue"
        @change="handleInputChange"
        @blur="handleInputConfirm"
        @keyup.enter="handleInputConfirm"
      />
      <a-tag
        v-else
        style="
          background: #fff;
          borderstyle: dashed;
          font-size: 13px;
          border-radius: 25px;
        "
        @click="showInput"
      >
        <a-icon type="plus" /> <span> New email</span>
      </a-tag>
    </a-modal>

    <a-modal
      v-model="visible"
      :maskClosable="false"
      :title="`Reply to: ${replyTo.createdBy}`"
      on-ok="handleOk"
    >
      <template slot="footer">
        <a-button key="back" @click="handleCancel"> Cancel </a-button>
        <a-button
          key="submit"
          type="primary"
          :disabled="!formComment.content"
          :loading="loading"
          @click="handleOk"
        >
          Submit
        </a-button>
      </template>
      <a-form-item>
        <a-textarea
          placeholder="Write something..."
          :rows="4"
          v-model="formComment.content"
        />
        <p v-if="commentError" style="color: red">{{ commentError }}</p>
      </a-form-item>
    </a-modal>
    <a-modal
      v-model="visibleEditComment"
      :maskClosable="false"
      :closable="false"
      :title="`Edit comment`"
      on-ok="handleOk"
    >
      <template slot="footer">
        <a-button key="back" @click="handleCancelEditComment">
          Cancel
        </a-button>
        <a-button
          key="submit"
          type="primary"
          :disabled="!formComment.content"
          :loading="loading"
          @click="handleOkEditComment"
        >
          Submit
        </a-button>
      </template>
      <a-form-item>
        <a-textarea :rows="4" v-model="formComment.content" />
        <p v-if="commentError" style="color: red">{{ commentError }}</p>
      </a-form-item>
    </a-modal>

    <a-modal
      v-model="reportModal"
      :maskClosable="false"
      :title="`Please choose at least one reason to denounce the article`"
      @ok="handleOkReport"
      @cancel="handleCancel"
    >
      <template v-for="reason in reasonList">
        <a-checkable-tag
          :key="reason.id"
          :checked="selectedReason.indexOf(reason) > -1"
          @change="(checked) => handleChange(reason, checked)"
        >
          {{ reason.name }}
        </a-checkable-tag>
      </template>
      <br />

      <template v-if="checkShowOther">
        <a-textarea
          placeholder="Write reason less than 255 characters..."
          :rows="4"
          v-model="formReport.otherText"
        />
        <span v-if="errors.otherTextNull" class="red">
          {{ errors.otherTextNull }}
        </span>
        <span v-if="errors.otherTextTooLong" class="red">
          {{ errors.otherTextTooLong }}
        </span>
      </template>

      <template slot="footer">
        <a-button key="back" @click="handleCancel"> Cancel </a-button>
        <a-button
          key="submit"
          type="primary"
          :disabled="!selectedReason.length"
          :loading="loading"
          @click="handleOkReport"
        >
          Report
        </a-button>
      </template>
    </a-modal>
  </div>
</template>

<script>
import CONFIG from "../config/index";
import ArticleRepository from "../api/article";
import CommentRepository from "../api/comment";
import DenounceRepository from "../api/denounce";
import MasterDataRepository from "../api/masterData";
import moment from "moment";
import UserFollowRepository from "../api/userFollow";
import EmailRepository from "../api/email";
import UserRepository from "../api/user";
import SystemConfigRepository from "../api/systemConfig";
import CardArticle from "./components/CardArticle.vue";
import { generateTime } from "../api/date";
import "vue-loading-overlay/dist/vue-loading.css";
import Loading from "vue-loading-overlay";
const requiredError = "This field can't blank";

const defaultCommentForm = {
  id: -1,
  articleID: -1,
  content: "",
  parentID: -1,
};
const defaultReportForm = {
  id: -1,
  articleID: -1,
  reason: undefined,
  otherText: "",
};
const defaultShareForm = {
  mailTo: "",
  linkReference: undefined,
};
export default {
  name: "home",
  components: { CardArticle, Loading },
  data() {
    return {
      imageApiUrl: `${CONFIG.apiUrl}/image/getImage/{get-image}?id=`,
      articleId: 0,
      article: {},
      moment,
      introductionImageUrl: "",
      listRelated: [],
      loading: false,
      visible: false,
      reportModal: false,
      formComment: { ...defaultCommentForm },
      formReport: { ...defaultReportForm },
      checked1: false,
      reasonList: [],
      selectedReason: [],
      listComment: [],
      totalComment: 0,
      replyTo: {},
      tempComment: {},
      sensitiveCheck: false,
      username: this.$cookies.get("account"),
      isFollowed: false,
      visibleEditComment: false,
      visibleShare: false,
      shareForm: { ...defaultShareForm },
      inputTagVisible: false,
      inputTagValue: "",
      listMail: [],
      reported: false,
      authorDetail: {},
      checkShowOther: false,
      errors: {
        otherTextNull: "",
        otherTextTooLong: "",
      },
      imageLoaded: false,
      visibleAttachments: false,
      attachmentList: [],
      blacklistWords: [],
      commentError: null,
      loadPage: false,
      current: 1,
      fullName: "",
    };
  },
  props: {
    previewArticle: {
      type: Object,
      required: false,
      default: null,
      description: "This object is preview version of article!",
    },
  },
  created() {
    this.articleId = this.$route.params.id;
    this.getArticleById(this.articleId);
    this.getCommentByArticleId();
    this.checkReport();
    this.getBlacklistWords();
  },
  mounted() {},
  methods: {
    styleBgImage() {
      if (this.previewArticle) {
        return "z-index:0";
      }
    },
    visibleDeleteComment(createdBy) {
      if (
        this.username == createdBy ||
        this.$cookies.get("roleCode") === "editor"
      ) {
        return true;
      }
      return false;
    },
    getBlacklistWords() {
      SystemConfigRepository.getAllBlacklistWords().then((res) => {
        this.blacklistWords = res.data.data;
      });
    },
    hideAttachments() {
      this.visibleAttachments = false;
    },
    highlightSapo() {
      const paragraphs = this.article.content.split("</p>");
      var sapo = document.createElement("p");
      sapo.innerHTML = "<b>" + paragraphs[0] + "</p> </b>";
      this.article.content.replace(paragraphs[0] + "</p>", sapo.innerHTML);
    },
    checkReport() {
      DenounceRepository.checkReportByArticleID(this.articleId).then((res) => {
        if (res.data.data.items) {
          this.reported = res.data.data.items;
        }
      });
    },
    getAuthorInfor() {
      UserRepository.getUserByAccount(this.article.author).then((res) => {
        this.authorDetail = res.data.data;
      });
    },
    getAuthorInforByArticleID(id) {
      ArticleRepository.getAuthorInforByArticleID(id).then((res) => {
        this.authorDetail = res.data.data;
      });
    },
    onCopy: function (e) {
      this.$message.success("You just copied a share link to clipboard ! ");
    },
    onError: function (e) {
      alert("Failed to copy texts");
    },
    sendMail() {
      this.shareForm.mailTo = this.listMail.join();
      EmailRepository.sendMail(this.shareForm).then((res) => {
        this.$notification.success({
          message: "Share article successfully!",
        });
        this.visibleShare = false;
        this.shareForm = { ...defaultShareForm };
        this.listMail = [];
      });
    },
    showInput() {
      this.inputTagVisible = true;
      this.$nextTick(function () {
        this.$refs.input.focus();
      });
    },
    handleInputChange(e) {
      this.inputTagValue = e.target.value;
    },

    handleInputConfirm() {
      const inputTagValue = this.inputTagValue;
      let listMail = this.listMail;
      if (inputTagValue && listMail.indexOf(inputTagValue) === -1) {
        listMail = [...listMail, inputTagValue];
      }
      this.listMail = listMail;
      Object.assign(this, {
        listMail,
        inputTagVisible: false,
        inputTagValue: "",
      });
    },
    handleCloseMailto(removedTag) {
      const mailTo = this.listMail.filter((tag) => tag !== removedTag);
      this.listMail = mailTo;
    },
    handleShareCancel() {
      this.shareForm = { ...defaultShareForm };
      this.listMail = [];
      this.visibleShare = false;
    },
    handleShareOk() {
      this.sendMail();
    },
    handleOkEditComment() {
      this.editComment();
      this.handleCancelEditComment();
    },
    showEditCommentModal(item) {
      this.formComment.id = item.id;
      this.formComment.content = item.content;
      this.visibleEditComment = true;
    },
    handleCancelEditComment() {
      this.visibleEditComment = false;
      this.formComment = { ...defaultCommentForm };
    },
    editComment() {
      CommentRepository.updateCommentByCommentID(this.formComment).then(
        (res) => {
          this.getCommentByArticleId();
        }
      );
    },
    getReasonList() {
      this.loading = true;
      MasterDataRepository.searchAllByType("reason_denounce").then((res) => {
        this.reasonList = res.data.data.items;
        this.loading = false;
      });
    },
    followUser() {
      UserFollowRepository.follow(this.article.author).then((res) => {
        this.isFollowed = true;
      });
    },
    unfollowUser() {
      UserFollowRepository.unfollow(this.article.author).then((res) => {
        this.isFollowed = false;
      });
    },
    handleChange(reason, checked) {
      const { selectedReason } = this;
      let nextSelectedTags = checked
        ? [...selectedReason, reason]
        : selectedReason.filter((t) => t !== reason);
      if (reason.code === "OTHER" && checked) {
        nextSelectedTags = [];
        nextSelectedTags.push(reason);
        this.checkShowOther = true;
      }
      if (reason.code !== "OTHER" && checked) {
        for (var i = 0; i < nextSelectedTags.length; i++) {
          if (nextSelectedTags[i].code === "OTHER") {
            // nextSelectedTags.remove(nextSelectedTags[i]);
            nextSelectedTags.splice(i, 1);
            this.formReport.otherText = "";
            this.checkShowOther = false;
          }
        }
      }
      this.selectedReason = nextSelectedTags;
    },
    async openReportArticleModal() {
      await this.getReasonList();
      this.reportModal = true;
      // UserFollowRepository.unfollow(this.article.author).then(res=>{
      //   this.isFollowed = false;
      // })
    },
    getFollowedUser() {
      UserFollowRepository.getFollowedUser(this.article.author).then((res) => {
        this.isFollowed = res.data.data;
      });
    },
    deleteComment(id) {
      CommentRepository.deleteCommentByCommentID(id).then(() => {
        this.getCommentByArticleId();
      });
    },
    commentContentChange(value) {
      this.commentError = null;
    },
    showModal(item) {
      this.replyTo = item;
      this.visible = true;
    },
    handleOk(e) {
      this.formComment.parentID = this.replyTo.id;
      this.addComment();
    },
    validate() {
      let isValid = true;
      if (this.checkShowOther) {
        if (
          this.formReport.otherText.trim() == "" ||
          this.formReport.otherText.trim() == " " ||
          this.formReport.otherText == null
        ) {
          this.errors.otherTextNull = requiredError;
          this.errors.otherTextTooLong = "";
          isValid = false;
        }
        if (this.formReport.otherText.length > 255) {
          this.errors.otherTextNull = "";
          this.errors.otherTextTooLong =
            "Please input reason less than 255 characters.";
          isValid = false;
        } else if (this.formReport.otherText.length > 0) {
          this.errors.otherTextNull = "";
          this.errors.otherTextTooLong = "";
        }
      }
      return isValid;
    },
    handleOkReport(e) {
      this.loading = true;

      if (this.selectedReason === null || this.selectedReason.length <= 0) {
        return;
      }
      var listReasonID = this.selectedReason.map((element) => element.id);
      var reportForm = {
        articleID: this.articleId > 0 ? this.articleId : this.$route.params.id,
        reason: listReasonID,
        reasonOther: this.formReport.otherText,
      };

      const validation = this.validate();
      if (!validation) {
        this.loading = false;
        return;
      }
      DenounceRepository.addDenounceArticle(reportForm).then((res) => {
        if (res.data.data > 0) {
          this.$notification.success({
            message: "Report successfully!",
          });
          this.checkShowOther = false;
          this.formReport.otherText = "";
          this.errors.otherTextNull = "";
          this.errors.otherTextTooLong = "";
          this.reported = true;
        } else {
          this.$notification.error({
            message: res.data.message,
          });
        }
      });
      this.selectedReason = [];
      this.reportModal = false;
      this.loading = false;
    },
    handleCancel(e) {
      this.replyTo = {};
      this.formComment = { ...defaultCommentForm };
      this.visible = false;
      this.selectedReason = [];
      this.reportModal = false;
      this.checkShowOther = false;
      this.formReport.otherText = "";
      this.errors.otherTextNull = "";
      this.errors.otherTextTooLong = "";
      this.commentError = null;
    },
    validateComment() {
      if (this.formComment.content) {
        let existForbiddenWords = this.blacklistWords
          .filter((el) => this.formComment.content.includes(el))
          .join();
        if (existForbiddenWords) {
          this.commentError = `Your comment contains forbidden words: "${existForbiddenWords}"`;
          return false;
        }
      }
      return true;
    },
    addComment() {
      if (!this.validateComment()) {
        return;
      }
      this.loading = true;
      this.formComment.articleID = this.articleId;
      CommentRepository.addComment(this.formComment).then((res) => {
        this.loading = false;
        this.visible = false;
        this.formComment = { ...defaultCommentForm };
        this.getCommentByArticleId();
      });
    },
    removeElement(array, elem) {
      var index = array.indexOf(elem);
      if (index > -1) {
        array.splice(index, 1);
      }
    },
    getCommentByArticleId() {
      this.listComment = [];
      CommentRepository.getCommentByArticleId(
        this.articleId,
        this.current
      ).then((res) => {
        let dataComment = res.data.data.items;
        let dataChildren = res.data.data.children;
        this.totalComment = res.data.data.totals;
        if (dataComment) {
          dataComment.forEach((cm) => {
            if (cm.parentID === -1) {
              let children = dataChildren.filter((el) => el.parentID == cm.id);
              cm.children = children;
              cm.modifiedTime = moment(cm.modifiedTime);
              this.listComment.push(cm);
            }
          });
        }
      });
    },
    paginate(current = 1) {
      this.current = current;
      this.getCommentByArticleId();
    },
    goToArticleDetail(id) {
      let routeData = this.$router.resolve({
        path: `/article-detail/${id}`,
      });
      window.open(routeData.href, "_blank");
    },
    generateTime(time) {
      return generateTime(time);
    },
    getRelatedArticles(relatedArticles) {
      ArticleRepository.getRelatedArticlesByListId(relatedArticles).then(
        (res) => {
          if (res.data.data) {
            let listRelated = res.data.data;
            if (listRelated) {
              var i,
                j,
                temporary,
                chunk = 3;
              for (i = 0, j = listRelated.length; i < j; i += chunk) {
                temporary = listRelated.slice(i, i + chunk);
                this.listRelated.push(temporary);
              }
            }
          }
        }
      );
    },

    getArticleById(id) {
      this.loadPage = true;
      if (this.previewArticle) {
        this.article = this.previewArticle;
        if (this.article.relatedArticle) {
          const relatedIds = this.article.relatedArticle.split(",");
          this.getRelatedArticles(relatedIds);
        }
        // this.introductionImageUrl = `${CONFIG.apiUrl}/image/getImage/{get-image}?id=24`
        this.authorDetail.firstName = this.article.firstName;
        this.authorDetail.lastName = this.article.lastName;
        if (this.article.fullName) {
          this.fullName = this.article.fullName;
          console.log("this.fullName", this.fullName);
        }
        console.log("this.article ===>", this.article);
        if (this.article.attachments) {
          if (this.article.attachments.length > 0) {
            this.article.attachments.forEach((element) => {
              this.attachmentList.push({
                uid: element.uid,
                name: element.name,
                status: "done",
                url: `${CONFIG.apiUrl}/attachments/getAttachment/{get-attachment}?id=${element.id}`,
                type: "uploaded",
              });
            });
          }
        }
        this.introductionImageUrl = `${CONFIG.apiUrl}/image/getImage/{get-image}?id=${this.article.introductionImage}`;
        this.loadPage = false;
      } else {
        ArticleRepository.getByIdForViewer(id).then((res) => {
          this.article = res.data.data;
          // this.highlightSapo();
          this.getFollowedUser();
          if (this.article.attachments) {
            if (this.article.attachments.length > 0) {
              this.article.attachments.forEach((element) => {
                this.attachmentList.push({
                  uid: element.id,
                  name: element.originalName,
                  status: "done",
                  url: `${CONFIG.apiUrl}/attachments/getAttachment/{get-attachment}?id=${element.id}`,
                  type: "uploaded",
                });
              });
            }
          }
          this.shareForm.linkReference = `${
            CONFIG.CLIENT_URL
          }/#/login/${window.btoa(this.article.id)}`;
          //   this.shareForm.linkReference = `${
          //   CONFIG.CLIENT_URL
          // }/#/login/${window.btoa(this.article.id)}`;
          this.getAuthorInfor();
          if (this.article.relatedArticle) {
            const relatedIds = this.article.relatedArticle.split(",");
            this.getRelatedArticles(relatedIds);
          }
          if (!res.data.success) {
            this.loadPage = false;
            this.$router.push("/not-found");
          }
          this.introductionImageUrl = `${CONFIG.apiUrl}/image/getImage/{get-image}?id=${this.article.introductionImage}`;
          this.loadPage = false;
        });
      }
    },
    openShareForm() {
      this.shareForm.linkReference = `${
        CONFIG.CLIENT_URL
      }/#/login/${window.btoa(this.article.id)}`;
      this.visibleShare = true;
    },
  },
};
</script>
<style scoped>
.attachments-content {
  padding: 10px 10px 10px 10px;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}
.card-profile {
  margin-top: 12% !important;
  /* background-color: transparent */
}
.image-container {
  background: linear-gradient(
    360deg,
    rgba(2, 0, 36, 1) 0%,
    rgba(0, 0, 0, 1) 0%,
    rgba(0, 0, 0, 0) 30%
  );
}
.article-action {
  transition: transform 250ms;

  font-size: 24px;
}
.article-action:hover {
  transform: translateY(-5px);
}
.follow-user-btn :hover {
  color: #eb2f96;
}
.author-info {
  display: grid;
}
.btn-unfollow :hover {
  color: #fff !important;
  border-color: #fff !important;
}
.ant-comment-content-author > a,
.ant-comment-content-author > span {
  font-size: 14px !important;
  font-weight: 500;
}
.authored {
  float: right;
}

.ant-carousel >>> .slick-slide {
  height: 500px;
}

.detail-image {
  display: inline-block;
  overflow: hidden;
  position: relative;
  width: 100%;
  /* background-image: url("../../public/img/theme/48pz1hd6ktp51.png"); */
}
.bg-image {
  pointer-events: none;
  position: relative;
  width: 100%;
  object-fit: cover;
  height: 700px;
  z-index: -1;
}
.lead {
  margin-bottom: 10%;
}
.title {
  position: absolute;
  z-index: 99;
  display: block;
  width: 100%;
  max-width: 979px;
  left: 47%;
  -webkit-transform: translate(-50%, 0);
  -moz-transform: translate(-50%, 0);
  -o-transform: translate(-50%, 0);
  -ms-transform: translate(-50%, 0);
  transform: translate(-50%, 0);
  font-size: 1.5rem;
  line-height: 1.8rem;
  letter-spacing: 1.3px;
  font-weight: 700;
  max-height: 100px;
  overflow: hidden;
  padding: 0 16px;
  bottom: 18px;
  margin-bottom: 4%;
}
.title > span {
  line-height: 48px;
  color: #fff;
  padding: 2px 0;
}

.created-date {
  font-size: 0.875rem;
  color: #adb5bd;
}
.anticon {
  vertical-align: 0em;
}
.related-topic {
  padding-bottom: 40px;
}

.related-topic-text {
  font-size: 1rem;
  line-height: 1.2rem;
  font-weight: 600;
  letter-spacing: 0.15px;
  padding-bottom: 8px;
  text-transform: uppercase;
  position: relative;
  display: inline-block;
  margin-bottom: 30px;
  border-bottom: 3px solid #fc5730;
}
</style>
<style >
.media {
  display: block !important;
}
.ant-carousel .slick-dots li button {
  background: #fc5730 !important;
}
.main-content .image img {
  max-width: 80%;
  max-height: 100%;
  display: block;
  margin-left: auto;
  margin-right: auto;
}
.main-content .image {
  display: block;
  margin-left: auto;
  margin-right: auto;
}
/* .ant-comment-content-detail{
max-height: 51px;
} */
.ant-comment-content-detail p {
  padding: 8px 33px 4px 24px;
  white-space: nowrap !important;
}
</style>