<template>
  <div class="profile-page">
    <section class="section-profile-cover section-shaped my-0">
      <div class="shape shape-style-1 shape-primary shape-skew alpha-4">
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
            <div class="gutter-example">
              <a-page-header
                style="border: 1px solid rgb(235, 237, 240)"
                title="New Article"
                :breadcrumb="{ props: { routes } }"
                sub-title="This is new article screen"
              >
                <template slot="extra">
                  <base-button
                    type="primary"
                    size="sm"
                    class="mr-4"
                    @click="handleLivePreview"
                    >Live preview</base-button
                  >
                  <base-button
                    type="info"
                    size="sm"
                    class="mr-4"
                    @click="handleSubmit"
                    >Submit</base-button
                  >
                  <base-button type="default" size="sm" class="float-right"
                    >Save Draft</base-button
                  >
                </template>
              </a-page-header>
            </div>
            <div class="row justify-content-center">
              <div class="col-lg-3 order-lg-2">
                <div class="card-profile-image">
                  <!-- <a href="#">
                                        <img v-lazy="'img/theme/team-4-800x800.jpg'" class="rounded-circle">
                                    </a> -->
                </div>
              </div>
              <div
                class="col-lg-4 order-lg-3 text-lg-right align-self-lg-center"
              >
                <div class="card-profile-actions py-4 mt-lg-0">
                  <!-- <base-button type="info" size="sm" class="mr-4"
                    >Submit</base-button
                  >
                  <base-button type="default" size="sm" class="float-right"
                    >Save Draft</base-button
                  > -->
                </div>
              </div>
              <div class="col-lg-4 order-lg-1">
                <div class="card-profile-stats d-flex justify-content-center">
                  <!-- <div>
                    <span class="heading">22</span>
                    <span class="description">Friends</span>
                  </div>
                  <div>
                    <span class="heading">10</span>
                    <span class="description">Photos</span>
                  </div>
                  <div>
                    <span class="heading">89</span>
                    <span class="description">Comments</span>
                  </div> -->
                </div>
              </div>
            </div>
            <div class="text-center mt-5">
              <a-form
                id="components-form-demo-validate-other"
                :form="form"
                v-bind="formItemLayout"
                @submit="handleSubmit"
              >
                <a-form-item v-bind="formItemLayout" label="Title">
                  <a-input
                    v-decorator="[
                      'email',
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
                    v-decorator="[
                      'category',
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
                    <a-select-option value="china"> China </a-select-option>
                    <a-select-option value="usa"> U.S.A </a-select-option>
                  </a-select>
                </a-form-item>
                <a-form-item label="Sa-po" has-feedback>
                  <a-textarea
                    v-decorator="[
                      'select',
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

                <a-form-item label="Content">
                  <ckeditor
                    :editor="editor"
                    v-decorator="[
                      'content',
                      {
                        rules: [
                          {
                            required: true,
                            message: 'Please input content!',
                          },
                        ],
                      },
                    ]"
                    :config="editorConfig"
                  ></ckeditor>
                </a-form-item>
                <a-form-item label="Hashtag">
                  <template v-for="tag in hashTags">
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
                <a-form-item label="Upload">
                  <div class="dropbox">
                    <a-upload-dragger
                      v-decorator="[
                        'dragger',
                        {
                          valuePropName: 'fileList',
                          getValueFromEvent: normFile,
                        },
                      ]"
                      name="files"
                      action="/upload.do"
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
            </div>
            <div class="mt-5 py-5 border-top text-center">
              <div class="row justify-content-center">
                <div class="col-lg-9">
                  <p>
                    An artist of considerable range, Ryan — the name taken by
                    Melbourne-raised, Brooklyn-based Nick Murphy — writes,
                    performs and records all of his own music, giving it a warm,
                    intimate feel with a solid groove structure. An artist of
                    considerable range.
                  </p>
                  <a href="#">Show more</a>
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
      title="Title"
      on-ok="handleOk"
    >
      <template slot="footer">
        <a-button key="back" @click="handleCancel"> Return </a-button>
        <a-button
          key="submit"
          type="primary"
          :loading="loading"
          @click="handleOk"
        >
          Submit
        </a-button>
      </template>
      <iframe
        :src="`/article-detail`"
        width="100%"
        height="100"
        frameborder="0"
      >
      </iframe>
    </a-modal>
  </div>
</template>
<script>
// import { ClassicEditor } from '../plugins/ckeditor5/build/ckeditor';

export default {
  data() {
    return {
      routes: [
        {
          path: "/landing",
          breadcrumbName: "Home",
        },
        {
          path: "/new-submission",
          breadcrumbName: "New Article",
        },
      ],
      formItemLayout: {
        labelCol: { span: 6 },
        wrapperCol: { span: 14 },
      },
      hashTags: [],
      inputTagVisible: false,
      inputTagValue: "",
      show: {
        livePreviewModal: false,
      },
      editor: window.ClassicEditor,
      editorConfig: {
        removePlugins: ["MathType", "ChemType"],
        toolbar: {
          items: [
            "undo",
            "redo",
            "heading",
            "|",
            "bold",
            "italic",
            "link",
            "bulletedList",
            "numberedList",
            "|",
            "outdent",
            "indent",
            "|",
            "imageUpload",
            "blockQuote",
            "insertTable",
            "mediaEmbed",
          ],
        },
      },
    };
  },
  beforeCreate() {
    this.form = this.$form.createForm(this, { name: "validate_other" });
  },
  methods: {
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
      let hashTags = this.hashTags;
      if (inputTagValue && hashTags.indexOf(inputTagValue) === -1) {
        hashTags = [...hashTags, "#" + inputTagValue];
      }
      Object.assign(this, {
        hashTags,
        inputTagVisible: false,
        inputTagValue: "",
      });
    },
    handleSubmit(e) {
      e.preventDefault();
      this.form.validateFields((err, values) => {
        if (!err) {
        }
      });
    },
    normFile(e) {
      if (Array.isArray(e)) {
        return e;
      }
      return e && e.fileList;
    },
    handleLivePreview() {
      this.show.livePreviewModal = true;
    },
  },
};
</script>
<style lang="scss" scoped>
.container {
  max-width: 1320px;
}
.main-content {
  border-top: 3px solid #FC5730;
}

.ant-upload.ant-upload-drag p.ant-upload-drag-icon .anticon,
.ant-upload-drag-container {
  color: #FC5730 !important;
}
</style>
<style >
.ck-editor__editable {
  min-height: 400px !important;
}
</style>