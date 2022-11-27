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
            <!-- <div class="gutter-example">
              <a-page-header
                style="border: 1px solid rgb(235, 237, 240); margin-top: 10px"
                :title="screenTitle"
                :breadcrumb="{ props: { routes } }"
                sub-title="This is new article screen"
              >
              </a-page-header>
            </div> -->

            <div class="text-center mt-5">
              <a-form
                id="components-form-demo-validate-other"
                :form="form"
                v-bind="formItemLayout"
              >
                <a-form-item v-bind="formItemLayout" label="Title">
                  <a-input
                    v-decorator="[
                      'title',
                      {
                        rules: [
                          {
                            required: true,
                            message: 'Please input title',
                          },
                        ],
                      },
                    ]"
                    placeholder="Please input title"
                  />
                </a-form-item>
                <a-form-item label="Category" has-feedback>
                  <a-select
                    show-arrow
                    allow-clear
                    show-search
                    @search="fetchCategory"
                    v-decorator="[
                      'categoryID',
                      {
                        rules: [
                          {
                            required: true,
                            message: 'Please select category!',
                          },
                        ],
                      },
                    ]"
                    placeholder="Please select category"
                  >
                    <a-select-option
                      v-for="item in listCategory"
                      :key="item.id"
                      :value="item.id"
                    >
                      {{ item.categoryName }}
                    </a-select-option>
                  </a-select>
                </a-form-item>
                <a-form-item
                  label="Upload Introduction Image"
                  class="upload-item"
                >
                  <a-upload
                    v-decorator="[
                      'introductionImage',
                      {
                        valuePropName: 'fileList',
                        getValueFromEvent: normFile,
                        rules: [
                          {
                            required: true,
                            message: 'Please upload introduction image!',
                          },
                        ],
                      },
                    ]"
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
                </a-form-item>
                <a-form-item label="Upload Attachments" class="upload-item">
                  <a-upload-dragger
                    :file-list="attachmentList"
                    name="file"
                    :multiple="true"
                    :remove="handleRemoveUploadAttachments"
                    :before-upload="beforeUploadAttachments"
                  >
                    <p class="ant-upload-drag-icon">
                      <a-icon type="inbox" />
                    </p>
                    <p class="ant-upload-text">
                      Click or drag file to this area to upload
                    </p>
                    <!-- <p class="ant-upload-hint">
                      Support for a single or bulk upload. Strictly prohibit
                      from uploading company data or other band files
                    </p> -->
                  </a-upload-dragger>
                </a-form-item>
                <a-form-item label="Content">
                  <ckeditor
                    :editor="editor"
                    v-model="content"
                    :config="editorConfig"
                    @change="handleContentChange"
                  ></ckeditor>
                  <div v-show="validateContent" class="validate-error">
                    Please input content!
                  </div>
                </a-form-item>
                <a-form-item label="Sa-po" has-feedback>
                  <a-textarea
                    v-decorator="[
                      'sapo',
                      {
                        rules: [
                          {
                            required: true,
                            message: 'Please input your sa-po!',
                          },
                        ],
                      },
                    ]"
                    placeholder="Please input your sa-po"
                    :auto-size="{ minRows: 3, maxRows: 5 }"
                  />
                </a-form-item>
                <a-form-item label="Hashtag">
                  <template v-for="tag in listHashTags">
                    <a-tooltip v-if="tag.length > 20" :key="tag" :title="tag">
                      <a-tag
                        :key="tag"
                        :closable="true"
                        @close="() => handleClose(tag)"
                      >
                        {{ `${tag.slice(0, 20)}...` }}
                      </a-tag>
                    </a-tooltip>
                    <a-tag
                      v-else
                      :key="tag"
                      :closable="true"
                      @close="() => handleClose(tag)"
                    >
                      {{ tag }}
                    </a-tag>
                  </template>
                  <a-input
                    v-if="inputTagVisible"
                    ref="input"
                    type="text"
                    size="small"
                    :style="{ width: '78px' }"
                    :value="inputTagValue"
                    @change="handleInputChange"
                    @blur="handleInputConfirm"
                    @keyup.enter="handleInputConfirm"
                  />
                  <a-tag
                    v-else
                    style="background: #fff; borderstyle: dashed"
                    @click="showInput"
                  >
                    <a-icon type="plus" /> New Tag
                  </a-tag>
                </a-form-item>
                <a-form-item label="Choose related articles: " has-feedback>
                  <a-select
                    mode="multiple"
                    show-arrow
                    allow-clear
                    show-search
                    :filter-option="false"
                    @search="fetchRelatedArticle"
                    @change="changeRelatedArticle($event)"
                    v-decorator="[
                      'relatedArticle',
                      {
                        rules: [
                          {
                            required: false,
                            message: 'Please choose  related articles!',
                          },
                        ],
                      },
                    ]"
                    placeholder="Please choose related articles"
                  >
                    <a-select-option
                      v-for="item in listRelated"
                      :key="item.id"
                      :value="item.id"
                    >
                      {{ item.title }}
                    </a-select-option>
                  </a-select>
                </a-form-item>
                <a-form-item label="Chosen articles: " has-feedback>
                  <a-row :gutter="16">
                    <a-col
                      :span="8"
                      v-for="item in choosedArticle"
                      :key="item.id"
                    >
                      <a-card :bordered="false" hoverable>
                        <img
                          slot="cover"
                          alt="example"
                          class="choosed-article"
                          v-lazy="relatedArticleImage + item.introductionImage"
                        />
                        <template slot="actions" class="ant-card-actions">
                          <!-- <a-icon type="close" key="edit" /> -->
                          <!-- <a-icon key="edit" type="edit" /> -->
                          <!-- <a-icon key="ellipsis" type="ellipsis" /> -->
                        </template>
                        <a-card-meta :title="item.title">
                          <!-- <a-avatar
                              slot="avatar"
                              src="https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png"
                            /> -->
                        </a-card-meta>
                      </a-card>
                    </a-col>
                  </a-row>
                  <!-- <a-col :span="8">
                        <a-card title="Card title" :bordered="false">
                          <p>card content</p>
                        </a-card>
                      </a-col>
                      <a-col :span="8">
                        <a-card title="Card title" :bordered="false">
                          <p>card content</p>
                        </a-card>
                      </a-col> -->
                  <!-- </a-row> -->
                </a-form-item>
              </a-form>
            </div>
            <div class="mt-5 py-5 border-top text-center">
              <div class="row justify-content-center">
                <div class="col-lg-9">
                  <a-affix :offset-bottom="20">
                    <base-button
                      type="primary"
                      size="sm"
                      class="mr-4"
                      :disabled="loading"
                      @click="handleLivePreview($event)"
                      >Live preview</base-button
                    >
                    <base-button
                      type="info"
                      size="sm"
                      class="mr-4"
                      :disabled="loading"
                      @click="handleSubmit($event, true)"
                      >Approve</base-button
                    >
                    <base-button
                      @click="showRejectForm()"
                      type="default"
                      size="sm"
                      :disabled="loading"
                      >Reject</base-button
                    >
                  </a-affix>
                </div>
              </div>
            </div>
          </div>
        </card>
      </div>
    </section>
    <a-modal
      :width="1500"
      v-model="show.livePreviewModal"
      title="Live Preview"
      :maskClosable="false"
      on-ok="handleOk"
      :destroyOnClose="true"
      @cancel="handleCancel"
    >
      <Home
        :previewArticle="previewArticle"
        @checkShowComponent="checkShowComponent = true"
        v-if="checkShowComponent === false"
      />
      <ArticleDetail
        :previewArticle="previewArticle"
        v-if="checkShowComponent === true"
      />

      <template slot="footer">
        <a-button key="back" @click="handleCancel"> Close </a-button>
      </template>
    </a-modal>
    <a-modal
      :width="1500"
      v-model="show.rejectArticle"
      title="Reject Form"
      :maskClosable="false"
      :destroyOnClose="true"
      @cancel="closeRejectArticle"
    >
      <div class="text-center mt-5">
        <a-form
          id="components-form-demo-validate-other"
          :form="formReject"
          v-bind="formItemLayout"
        >
          <a-form-item v-bind="formItemLayout" label="Note">
            <a-textarea
              v-decorator="[
                'note',
                {
                  rules: [
                    {
                      required: true,
                      message: 'Please input your note',
                    },
                  ],
                },
              ]"
              placeholder="Please input your note"
              :auto-size="{ minRows: 3, maxRows: 5 }"
              class="full-width--i"
            />
          </a-form-item>
        </a-form>
      </div>

      <template slot="footer">
        <a-button
          key="back"
          type="primary"
          @click="rejectArticleMethod"
          :loading="loading"
        >
          Reject
        </a-button>
        <a-button key="black" @click="closeRejectArticle" :loading="loading">
          Cancel
        </a-button>
      </template>
    </a-modal>
  </div>
</template>
<script>
// import { ClassicEditor } from '../plugins/ckeditor5/build/ckeditor';
import CategoryRepository from "../api/category";
import ArticleRepository from "../api/article";
import ReviewArticleRepository from "../api/reviewArticle";
import ArticleDetail from "./ArticleDetail.vue";
import HashtagRepository from "../api/hashtag";
import Home from "../views/Landing.vue";
import vue2Dropzone from "vue2-dropzone";
import CONFIG from "../config/index";
import AttachmentRepository from "../api/attachments";

export default {
  components: {
    vueDropzone: vue2Dropzone,
    Home,
    ArticleDetail,
  },
  getBase64(file) {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => resolve(reader.result);
      reader.onerror = (error) => reject(error);
    });
  },
  data() {
    return {
      routes: [
        {
          path: "/landing",
          breadcrumbName: "Home",
        },
        {
          path: "/edit-article-by-reviewer/" + this.id,
          breadcrumbName: "Edit Article By Reviewer",
        },
      ],
      formItemLayout: {
        labelCol: { span: 6 },
        wrapperCol: { span: 14 },
      },
      listHashTags: [],
      previewArticle: [],
      checkShowComponent: false,
      screenTitle: "New Article",
      content: "",
      inputTagVisible: false,
      validateContent: false,
      inputTagValue: "",
      article: {},
      fileInfo: {},
      show: {
        livePreviewModal: false,
        rejectArticle: false,
      },
      listCategory: [],
      introductionImageUpload: "",
      editor: window.ClassicEditor,
      editorConfig: {
        removePlugins: ["MathType", "ChemType"],
        toolbar: {
         items: [
            "heading",
            "|",
            "alignment",
            "|",
            "bold",
            "italic",
            "strikethrough",
            "underline",
            "subscript",
            "superscript",
            "|",
            "link",
            "|",
            "bulletedList",
            "numberedList",
            "todoList",
            "-", // break point
            "fontfamily",
            "fontsize",
            "fontColor",
            "fontBackgroundColor",
            "|",
            "code",
            "codeBlock",
            "|",
            "insertTable",
            "|",
            "outdent",
            "indent",
            "|",
            "uploadImage",
            "blockQuote",
            "|",
            "undo",
            "redo",
            "mediaEmbed",
          ],
          shouldNotGroupWhenFull: true,
        },
        mediaEmbed: {
          previewsInData: true,
        },
        simpleUpload: {
          uploadUrl: `${CONFIG.apiUrl}/image/uploadImageCkEditor/{upload-ck-editor}`,
          withCredentials: false,
          headers: {
            Authorization: "Bearer " + this.$cookies.get("accessToken"),
          },
        },
        headers: "",
      },

      previewVisible: false,
      previewImage: "",
      listRelated: [],
      choosedArticle: [],
      relatedArticleImage: `${CONFIG.apiUrl}/image/getImageThumb/{get-image-thumb}?id=`,
      fileList: [],
      loading: false,
      attachmentList: [],
      removedAttachmentList: [],
      contentOriginal: "",
    };
  },
  computed: {
    imageUploadOption() {
      return {
        url: `${CONFIG.apiUrl}/image/uploadImage/{upload}`,
        maxFilesize: 20,
        addRemoveLinks: true,
        thumbnailWidth: 100,
        maxFiles: 1,
        timeout: 7200000,
        acceptedFiles: "image/*",
        // headers: this.authHeader
      };
    },
  },
  props: {
    id: {
      type: String,
      default: null,
      description: "Whether badge is of pill type",
    },
  },
  watch: {
    // content(value) {
    //   const paragraphs = value.split("</p>");
    //   var sapo = document.createElement("p");
    //   sapo.innerHTML = paragraphs[0] + "</p>";
    //   this.form.setFieldsValue({
    //     sapo: sapo.textContent || sapo.innerText,
    //   });
    // },
  },
  beforeCreate() {
    this.form = this.$form.createForm(this, { name: "validate_other" });
    this.formReject = this.$form.createForm(this, { name: "validate_other" });
    this.headers = {
      authorization: "Bearer " + this.$cookies.get("accessToken"),
      "X-Requested-With": "XMLHttpRequest",
    };
  },
  created() {
    this.introductionImageUpload = `${CONFIG.apiUrl}/image/uploadImage/{upload-image}?introductionImage=true`;
    const id = this.id;
    if (id) {
      this.screenTitle = "Review Article";
      this.getArticleById(id);
    }
    this.getAllCategory();
    this.fetchRelatedArticle("");
    this.findDiff();
  },
  methods: {
    deleteAttachmentByListIds() {
      AttachmentRepository.deleteByIds(this.removedAttachmentList).then(
        (res) => {
          if (res.data.data) {
            this.removedAttachmentList = [];
          }
        }
      );
    },
    handleEditAttachment(articleId) {
      this.deleteAttachmentByListIds();
      const { attachmentList } = this;
      const formData = new FormData();
      if (attachmentList.length <= 0) {
        return;
      }
      attachmentList.forEach((file) => {
        if (file.status === "pending") {
          formData.append("files[]", file);
        }
      });
      this.uploading = true;
      if (Object.keys(formData).length > 0) {
        reqwest({
        url: `${CONFIG.apiUrl}/attachments/uploadAttachments/{upload-attachments}?articleId=${articleId}`,
        method: "post",
        processData: false,
        data: formData,
        headers: {
          Authorization: "Bearer " + this.$cookies.get("accessToken"),
        },
        success: () => {
          this.attachmentList = [];
          this.uploading = false;
          // this.$message.success('upload successfully.');
        },
        error: () => {
          this.uploading = false;
          // this.$message.error('upload failed.');
        },
      });
      }
      // You can use any AJAX library you like
      
    },
    handleUploadAttachment(articleId) {
      const { attachmentList } = this;
      const formData = new FormData();
      if (attachmentList.length <= 0) {
        return;
      }
      attachmentList.forEach((file) => {
        formData.append("files[]", file);
      });
      this.uploading = true;
      // You can use any AJAX library you like
      reqwest({
        url: `${CONFIG.apiUrl}/attachments/uploadAttachments/{upload-attachments}?articleId=${articleId}`,
        method: "post",
        processData: false,
        data: formData,
        headers: {
          Authorization: "Bearer " + this.$cookies.get("accessToken"),
        },
        success: () => {
          this.attachmentList = [];
          this.uploading = false;
          // this.$message.success('upload successfully.');
        },
        error: () => {
          this.uploading = false;
          // this.$message.error('upload failed.');
        },
      });
    },
    handleRemoveUploadAttachments(file) {
      const index = this.attachmentList.indexOf(file);
      const newFileList = this.attachmentList.slice();
      newFileList.splice(index, 1);
      this.attachmentList = newFileList;
      if (file.type === "uploaded") {
        this.removedAttachmentList.push(file.uid);
      }
    },
    beforeUploadAttachments(file) {
      if (file.size > 2.5e6) {
        this.$notification["error"]({
          message: "Please choose an attachment smaller than 2,5MB !",
        });
        return false;
      }
      file.status = "pending";
      this.attachmentList = [...this.attachmentList, file];
      return false;
    },
    getBase64(file) {
      return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = (error) => reject(error);
      });
    },
    changeRelatedArticle(e) {
      let result = this.listRelated.filter((r) => e.includes(r.id));
      this.choosedArticle = result;
    },
    fetchCategory(value) {
      CategoryRepository.searchCategory(value, 1).then((res) => {
        this.listCategory = res.data.data.items;
      });
    },
    fetchRelatedArticle(value) {
      ArticleRepository.searchRelatedArticle(value).then((res) => {
        this.listRelated = res.data.data.items;
      });
    },

    handleClose(removedTag) {
      const hashTags = this.hashTags.filter((tag) => tag !== removedTag);
      this.hashTags = hashTags;
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
      let hashTags = this.listHashTags;
      if (inputTagValue && hashTags.indexOf(inputTagValue) === -1) {
        hashTags = [...hashTags, "#" + inputTagValue];
      }
      this.listHashTags = hashTags;
      Object.assign(this, {
        hashTags,
        inputTagVisible: false,
        inputTagValue: "",
      });
    },
    rejectArticleMethod(e) {
      this.loading = true;
      if (!this.content || this.content == "") {
        this.validateContent = true;
      } else {
        this.validateContent = false;
      }
      e.preventDefault();
      this.form.validateFields((err, values) => {
        if (!err) {
          values.content = this.content;
          values.confirm = false;
          values.id = this.article.id;
          if (values.introductionImage[0].response) {
            values.introductionImage = values.introductionImage[0].response;
          } else {
            values.introductionImage = this.article.introductionImage;
          }
          if (values.relatedArticle) {
            values.relatedArticle = values.relatedArticle.join();
          }
          this.formReject.validateFields((err, values1) => {
            if (!err) {
              values.note = values1.note;
              this.approveArticle(values, false);
            }
          });
        } else {
          this.loading = false;
        }
      });
    },
    handleSubmit(e, confirm) {
      this.loading = true;
      if (!this.content || this.content == "") {
        this.validateContent = true;
      } else {
        this.validateContent = false;
      }
      e.preventDefault();
      this.form.validateFields((err, values) => {
        if (!err) {
          values.content = this.content;
          values.confirm = confirm;
          values.id = this.article.id;
          // values.status = 1;
          if (values.introductionImage[0].response) {
            values.introductionImage = values.introductionImage[0].response;
          } else {
            values.introductionImage = this.article.introductionImage;
          }
          if (values.relatedArticle) {
            values.relatedArticle = values.relatedArticle.join();
          }
          if (confirm) {
            this.approveArticle(values, confirm);
          }
        } else {
          this.loading = false;
        }
      });
    },
    showRejectForm() {
      this.show.rejectArticle = true;
    },
    closeRejectArticle() {
      // this.show.rejectArticle.note = null;
      this.show.rejectArticle = false;
      this.loading = false;
    },
    addHashtag(formHashtag) {
      HashtagRepository.addHastag(formHashtag).then((res) => {});
    },
    getHashtagByNewId(newID) {
      HashtagRepository.getByNewId(newID).then((res) => {
        this.listHashTags = res.data.data.map((hashTag) => hashTag.tagContent);
      });
    },
    addArticle(form, draft) {
      ArticleRepository.submitArticle(form, draft)
        .then((res) => {
          this.$notification["success"]({
            message: "Submit article succeed !",
          });
          if (this.listHashTags) {
            const hashtagForm = {
              tagContent: this.listHashTags,
              id: res.data.data,
            };
            this.addHashtag(hashtagForm);
          }
          this.$router.push("/my-article");
        })
        .catch((err) => {
          this.$notification["error"]({
            message: "Submit article failed !",
          });
        });
    },
    // editContent, originalContent
    findDiff(editContent, originalContent) {
      // // str2 is the text which I want to compare with str1.
      // var originalContent = "I was to sent to moon in order to protect you"
      // // str1 is the reference text.
      // var editContent = "I was sent to moon in order to protect you anyway";

      let editWord = editContent.split(" ");
      let originalWord = originalContent.split(" ");
      let diffWord = originalWord.filter(
        (element) => !editWord.includes(element)
      );
      // let res2 = a.filter(i => !b.includes(i));

      this.highlight(originalWord, diffWord);
      // this.highlight(a, "str1", res2);
    },
    highlight(str, res) {
      const div = document.createElement("div");
      var text = "";
      for (var i = 0; i < str.length; i++) {
        var hasVal = res.includes(str[i]);
        if (hasVal) {
          text += " <span class='hight-light-content'>" + str[i] + "</span> ";
        } else {
          text += " " + str[i] + " ";
        }
      }
      // div.innerHTML = text;
      // document.body.appendChild(div);
    },
    words(s) {
      return s.toLowerCase().match(/\w+/g);
    },
    approveArticle(form, confirm) {
      form.tagContent = this.listHashTags;

      // var contentEdited = document.querySelector('.ck-content').innerText;
      // thiś.findDiff(contentEdited, this.contentOriginal)

      ReviewArticleRepository.reviewArticle(form)
        .then((res) => {
          if (res.data.success) {
            this.handleEditAttachment(this.article.id);
            if (confirm) {
              this.$notification["success"]({
                message: "Approve article succeed !",
              });
            } else {
              this.$notification["success"]({
                message: "Reject article succeed !",
              });
              this.$emit("re-load", "ReviewArticle");
              return;
            }
            this.$router.push("/article-detail/" + this.article.id);
          } else {
            if (confirm) {
              this.$notification["error"]({
                message: "Approve article failed !",
              });
            } else {
              this.$notification["error"]({
                message: "Reject article failed !",
              });
            }
            this.loading = false;
          }
        })
        .catch((err) => {
          if (confirm) {
            this.$notification["error"]({
              message: "Approve article failed !",
            });
          } else {
            this.$notification["error"]({
              message: "Reject article failed !",
            });
          }
          this.loading = false;
        });
    },
    normFile(e) {
      if (e.file.status === "error") {
        return;
      }
      if (Array.isArray(e)) {
        return e;
      }
      return e && e.fileList;
    },
    handleImageUploadChange(info) {
      this.fileList = [];
      const status = info.file.status;
      if (status !== "uploading") {
      }
      if (status === "done") {
        this.fileList = info.fileList;
        this.$message.success(`${info.file.name} file uploaded successfully.`);
      } else if (status === "error") {
        this.fileList = [];
        this.$message.error(info.file.response, 5);
      }
    },
    handleLivePreview(e) {
      if (!this.content || this.content == "") {
        this.validateContent = true;
      } else {
        this.validateContent = false;
      }
      e.preventDefault();
      this.form.validateFields((err, values) => {
        if (!err) {
          values.content = this.content;
          values.draftFlag = false;
          values.totalViews = 0;
          values.deleteFlag = false;
          values.confirm = confirm;
          let categorySearch = this.listCategory.find(function (category) {
            if (category.id === values.categoryID) {
              return category;
            }
          });
          values.categoryName = categorySearch.categoryName;
          values.id = this.article.id;
          values.firstName = this.article.firstName;
          values.lastName = this.article.lastName;
          values.attachments = this.attachmentList;
          values.relatedArticle = this.choosedArticle.map((element) => {
            return element.id;
          });
          // values.status = 1;
          if (
            typeof values.introductionImage[0].response !== undefined &&
            values.introductionImage[0].response
          ) {
            values.introductionImage = values.introductionImage[0].response;
          } else {
            values.introductionImage = this.article.introductionImage;
          }
          if (values.relatedArticle) {
            values.relatedArticle = values.relatedArticle.join();
          }
          this.previewArticle = values;
        }
      });
      this.show.livePreviewModal = true;
    },
    async handlePreviewUploadFile(file) {
      // this.show.livePreviewModal = true;
      // if (!file.url && !file.preview) {
      //   file.preview = await getBase64(file.originFileObj);
      // }
      this.previewImage = file.thumbUrl;
      if (this.article) {
        this.previewImage =
          this.relatedArticleImage + this.article.introductionImage;
      }
      // this.previewImage = file.url || file.preview;
      this.previewVisible = true;
    },
    handleCancel() {
      this.previewVisible = false;
      this.show.livePreviewModal = false;
      this.checkShowComponent = false;
    },
    getArticleById(id) {
      ArticleRepository.getById(id, "").then((res) => {
        this.article = res.data.data;
        // this.contentOriginal = this.article.content
        this.$nextTick(() => {
          let relatedArticles = [];
          if (this.article.relatedArticle) {
            relatedArticles = this.article.relatedArticle
              .split(",")
              .map((x) => parseInt(x));
            ArticleRepository.getRelatedArticlesByListId(relatedArticles).then(
              (res) => {
                if (res.data.data) {
                  this.listRelated.push.apply(this.listRelated, res.data.data);
                  this.choosedArticle.push(...res.data.data);
                }
              }
            );
          }
          console.log(this.article.sapo,"this.article.sapo");
          this.form.setFieldsValue({
            title: this.article.title,
            categoryID: this.article.categoryID,
            sapo: this.article.sapo,
            relatedArticle: relatedArticles,
            introductionImage: [
              {
                uid: this.article.introductionImage,
                name: "image.png",
                status: "done",
                url: this.relatedArticleImage + this.article.introductionImage,
              },
            ],
          });
          this.content = this.article.content;
          this.fileList = [
            {
              uid: this.article.introductionImage,
              name: "image.png",
              status: "done",
              url: this.relatedArticleImage + this.article.introductionImage,
            },
          ];
          if (this.article.attachments) {
            if (this.article.attachments.length > 0) {
              this.article.attachments.forEach((element) => {
                this.attachmentList.push({
                  uid: element.id,
                  name: element.name,
                  status: "done",
                  url: `${CONFIG.apiUrl}/attachments/getAttachment/{get-attachment}?id=${element.id}`,
                  type: "uploaded",
                });
              });
            }
          }

          this.$nextTick(() => {
            this.contentOriginal =
              document.querySelector(".ck-content").innerText;
          });
          this.getHashtagByNewId(id);
        });
      });
    },
    onUploadSlideShowSuccess: function (file, response) {
      this.fileInfo = response;
      if (this.fileInfo.length > 0) {
        // this.editForm.fileId= this.fileInfo[0].id;
      }
    },
    onRemoveSlideShow: function (file, error, xhr) {
      // this.editForm.fileId = null
      // const removeId = JSON.parse(file.xhr.response)[0].id
    },
    handleContentChange(e) {},
    getAllCategory() {
      CategoryRepository.getAllCategory().then((res) => {
        this.listCategory = res.data.data.items;
      });
    },
  },
};
</script>
<style lang="scss" scoped>
.section-profile-cover {
  background: linear-gradient(
    83deg,
    rgba(131, 58, 180, 1) 0%,
    rgba(253, 29, 29, 1) 41%,
    rgba(252, 148, 69, 1) 100%
  );
}
.container {
  max-width: 1320px;
}
.main-content {
  border-top: 3px solid #fc5730;
}

.ant-upload.ant-upload-drag p.ant-upload-drag-icon .anticon,
.ant-upload-drag-container {
  color: #fc5730 !important;
}
</style>
<style >
.ck-editor__editable {
  min-height: 400px !important;
}

.section-skew {
  margin-top: -15rem;
}
.ant-card-meta-description {
  text-overflow: ellipsis;
}
.choosed-article {
  max-height: 150px;
}
.upload-item .ant-upload-picture-card-wrapper {
  width: 20%;
}
</style>