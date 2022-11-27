<template>
  <div class="profile-page">
    <section class="section-profile-cover section-shaped my-0">
      <div class="container-fluid">
        <h4 class="text-white screen-name">DATA REPORT</h4>
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

            <div class="gutter-example lg-md ml-4">
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
                    <label><b>Choose range: </b></label>
                    <a-range-picker @change="onChange" />
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
                    <a-spin :spinning="loading">
                      <a-icon
                        slot="indicator"
                        type="loading"
                        style="font-size: 24px;color:#fc5730"
                        spin
                      />
                      <span @click="exportData()">
                        <base-button
                          type="primary"
                          size="md"
                          class="mr-4 btn-neutral btn-filter main-btn"
                        >
                          <i class="fa fa-search"></i>
                          Export
                        </base-button>
                      </span>
                    </a-spin>
                  </div>
                </a-col>

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
                    <span @click="resetFilter()">
                      <base-button
                        type="primary"
                        size="md"
                        class="mr-4 btn-neutral btn-filter main-btn"
                      >
                        <i class="fa fa-ban"></i>
                        Clear
                      </base-button>
                    </span>
                  </div>
                </a-col> -->
              </a-row>
            </div>
            <a-tabs v-model="activeKey" default-active-key="1" @change="changeTab">
              <a-tab-pane key="1">
                <span slot="tab">
                  <a-icon class="icon-flex" type="profile" />
                  Article
                </span>
                <div class="row chart-container">
                  <div class="col-7" :style="{ minHeight: '500px' }">
                    <a-list
                      item-layout="vertical"
                      size="large"
                      :pagination="paginationArticle"
                      :data-source="listTopArticle"
                    >
                      <div style="border-left: 4px solid #fc5730" slot="header">
                        <h4 class="display-5" style="padding-left: 9px">
                          TOP ARTICLES
                        </h4>
                      </div>

                      <a-list-item
                        v-for="item in listTopArticle"
                        slot="renderItem"
                        :key="item.id"
                        slot-scope="item, index"
                      >
                        <template slot="actions">
                          <span>
                            <a-icon
                              class="icon-flex"
                              type="eye"
                              style="margin-right: 8px"
                            />
                            {{ item.totalViews }}
                          </span>
                          <span>
                            <a-icon
                              class="icon-flex"
                              type="message"
                              style="margin-right: 8px"
                            />
                            {{ item.totalComment }}
                          </span>
                        </template>
                        <img
                          slot="extra"
                          width="272"
                          alt="logo"
                          :src="articleImage + item.introductionImage"
                        />
                        <!-- <img
                          v-else
                          slot="extra"
                          width="272"
                          alt="logo"
                          src="../../public/img/icons/common/default-image.jpg"
                        /> -->
                        <a-list-item-meta :description="item.description">
                          <a slot="title" :href="item.href">{{ item.title }}</a>
                          <a-avatar slot="avatar" :src="item.avatar" />
                        </a-list-item-meta>
                        <!-- {{ item.content }} -->
                      </a-list-item>
                    </a-list>
                  </div>
                  <div
                    class="col-5"
                    :style="{ minHeight: '500px', maxHeight: '500px' }"
                  >
                    <a-list
                      item-layout="vertical"
                      size="large"
                      :pagination="paginationWriter"
                      :data-source="listTopWriter"
                    >
                      <div style="border-left: 4px solid #fc5730" slot="header">
                        <h4 class="display-5" style="padding-left: 9px">
                          TOP WRITERS
                        </h4>
                      </div>

                      <a-list-item
                        v-for="item in listTopWriter"
                        slot="renderItem"
                        :key="item.id"
                        slot-scope="item, index"
                      >
                        <template slot="actions">
                          <span>
                            <a-icon
                              class="icon-flex"
                              type="eye"
                              style="margin-right: 8px"
                            />
                            {{ item.totalViews }}
                          </span>
                          <span>
                            <a-icon
                              class="icon-flex"
                              type="profile"
                              style="margin-right: 8px"
                            />
                            {{ item.totalArticle }}
                          </span>
                          <span>
                            <a-icon
                              class="icon-flex"
                              type="heart"
                              style="margin-right: 8px"
                            />
                            {{ item.totalFollower }}
                          </span>
                        </template>
                        <!-- <img
                          v-if="item.introductionImage"
                          slot="extra"
                          width="272"
                          alt="logo"
                          :src="articleImage + item.introductionImage"
                        /> -->
                        <img
                          slot="extra"
                          width="130"
                          alt="logo"
                          :src="item.avatar"
                        />
                        <a-list-item-meta :description="item.fullName">
                          <a slot="title" :href="item.href">{{
                            item.account
                          }}</a>
                          <a-avatar slot="avatar" :src="item.avatar" />
                        </a-list-item-meta>
                        <!-- {{ item.content }} -->
                      </a-list-item>
                    </a-list>
                  </div>
                </div>
              </a-tab-pane>
              <a-tab-pane key="2">
                <span slot="tab">
                  <a-icon class="icon-flex" type="appstore" />
                  Category
                </span>
                <div class="row chart-container">
                  <div
                    id="categoryColumnChart"
                    class="col-7"
                    :style="{ minHeight: '500px' }"
                  ></div>
                  <div
                    id="categoryPieChart"
                    class="col-5"
                    :style="{ minHeight: '500px' }"
                  ></div>
                </div>
              </a-tab-pane>
              <a-tab-pane key="3">
                <span slot="tab">
                  <a-icon class="icon-flex" type="appstore" />
                  Timeline
                </span>
                <div class="row">
                  <div class="col-9"></div>
                  <div class="col-3">
                    <a-radio-group
                      default-value="%d/%m/%Y"
                      button-style="solid"
                      @change="viewTypeChange"
                    >
                      <a-radio-button value="%d/%m/%Y"> Daily </a-radio-button>
                      <a-radio-button value="%m/%Y"> Monthly </a-radio-button>
                      <a-radio-button value="%Y"> Yearly </a-radio-button>
                    </a-radio-group>
                  </div>
                </div>
                <div class="row chart-container">
                  <div
                    id="timelineChart"
                    class="col-12"
                    :style="{ minHeight: '600px' }"
                  ></div>
                </div>
              </a-tab-pane>
            </a-tabs>
          </div>
        </card>
      </div>
    </section>
  </div>
</template>
<script>
import Highcharts from "highcharts";
import HighchartsMore from "highcharts/highcharts-more";
import DataReportRepository from "../api/dataReport";
import CONFIG from "../config";
const defaultSearchForm = {
  fromDate: "",
  toDate: "",
};

HighchartsMore(Highcharts);
export default {
  components: {
    Highcharts: Highcharts,
  },
  data() {
    return {
      activeKey:"1",
      viewType: "%d/%m/%Y",
      articleImage: `${CONFIG.apiUrl}/image/getImageThumb/{get-image-thumb}?id=`,
      listTopArticle: [],
      listTopWriter: [],
      formSearch: { ...defaultSearchForm },
      chart: {
        categoryColumnChart: {},
        categoryPieChart: {},
        timelineChart: {},
      },
      loading: false,
      paginationArticle: {
        onChange: (page) => {
        },
        pageSize: 3,
      },
      paginationWriter: {
        onChange: (page) => {
        },
        pageSize: 5,
      },
      actions: [
        { type: "star-o", text: "156" },
        { type: "like-o", text: "156" },
      ],
    };
  },
  beforeCreate() {},
  created() {
    this.getTopArticles();
    this.getTopWriter();
  },
  mounted() {},
  methods: {
    exportData() {
      this.loading = true;
      DataReportRepository.exportData(this.formSearch).then((res) => {
        this.loading = false;
        const url = window.URL.createObjectURL(new Blob([res.data]));
        const link = document.createElement("a");
        link.href = url;
        link.setAttribute("download", "RawDataExport.xlsx");
        document.body.appendChild(link);
        link.click();
      });
    },
    getTopWriter() {
      this.listTopWriter = []
      DataReportRepository.getTopWriter(this.formSearch).then((res) => {
        let listWriter = res.data.data;
        if (listWriter) {
          listWriter.forEach((el) => {
            this.listTopWriter.push({
              account: el.firstName + " " + el.lastName,
              fullName: el.account,
              avatar: this.articleImage + el.avatar,
              totalArticle: el.totalArticle,
              totalFollower: el.totalFollower,
              totalViews: el.totalViews,
              lastName: el.lastName,
              firstName: el.firstName,
            });
          });
        }
      });
    },
    changeTab(activeKey) {
      switch (activeKey) {
        case "1":
          this.getTopArticles();
          this.getTopWriter()
          break;
        case "2":
          this.dataReportCategory();
          break;
        case "3":
          this.dataTimelineReport();
          break;
        default:
          break;
      }
    },
    viewTypeChange(e) {
      this.viewType = e.target.value;
      this.dataTimelineReport();
    },
    onChange(date, dateString) {
      this.formSearch.fromDate = dateString[0];
      this.formSearch.toDate = dateString[1];
      console.log(dateString,dateString);

      this.changeTab( this.activeKey)
    },
    getTopArticles() {
      this.listTopArticle = []
      console.log(11111111);
      DataReportRepository.getTopArticles(this.formSearch).then((res) => {
        let listArticles = res.data.data;
        if (listArticles) {
          listArticles.forEach((el) => {
            this.listTopArticle.push({
              href: `${CONFIG.CLIENT_URL}/#/article-detail/${el.id}`,
              title: el.title,
              avatar: this.articleImage + el.authorAvatar,
              description: el.sapo,
              content: el.sapo,
              totalViews: el.totalViews,
              totalComment: el.totalComment,
              introductionImage: el.introductionImage,
            });
          });
        }
      });
    },
    dataReportCategory() {
      DataReportRepository.dataReportCategory(this.formSearch).then((res) => {
        let listDataReportCategory = res.data.data;
        let listDataColumnChart = [];
        let listDataPieChart = [];
        if (listDataReportCategory) {
          listDataReportCategory.forEach((element) => {
            const node = {
              name: element.categoryName,
              y: element.totalArticles,
              drilldown: null,
            };
            const nodePie = {
              name: element.categoryName,
              y: element.totalArticles,
              z: element.totalArticles,
            };
            listDataPieChart.push(nodePie);
            listDataColumnChart.push(node);
          });
          this.chart.categoryColumnChart = Highcharts.chart({
            chart: {
              renderTo: "categoryColumnChart",
              type: "column",
              animation: {
                duration: 1000,
              },
            },
            title: {
              text: "Total articles by categories",
            },

            accessibility: {
              announceNewData: {
                enabled: true,
              },
            },
            xAxis: {
              type: "category",
            },
            yAxis: {
              title: {
                text: "Total Articles By Categories",
              },
            },
            legend: {
              enabled: false,
            },
            plotOptions: {
              series: {
                borderWidth: 0,
                dataLabels: {
                  enabled: true,
                  format: "{point.y}",
                },
              },
            },

            tooltip: {
              headerFormat:
                '<span style="font-size:11px">{series.name}</span><br>',
              pointFormat:
                '<span style="color:{point.color}">{point.name}</span>: <b>{point.y}</b> of total<br/>',
            },

            series: [
              {
                name: "Browsers",
                colorByPoint: true,
                data: listDataColumnChart,
              },
            ],
          });
          this.chart.categoryPieChart = Highcharts.chart({
            chart: {
              renderTo: "categoryPieChart",
              type: "pie",
            },
            title: {
              text: "Total articles by categories",
            },
            tooltip: {
              headerFormat: "",
              pointFormat:
                '<span style="color:{point.color}">\u25CF</span> <b> {point.name} : {point.z}</b><br/>',
            },
            accessibility: {
              point: {
                valueSuffix: "%",
              },
            },
            plotOptions: {
              pie: {
                allowPointSelect: true,
                cursor: "pointer",
                dataLabels: {
                  enabled: true,
                  format: "<b>{point.name}</b>: {point.percentage:.1f} %",
                  connectorColor: "silver",
                },
              },
            },
            series: [
              {
                minPointSize: 10,
                innerSize: "20%",
                zMin: 0,
                name: "countries",
                data: listDataPieChart,
              },
            ],
          });
        }
      });
    },
    dataTimelineReport() {
      DataReportRepository.dataTimelineReport(
        this.formSearch,
        this.viewType
      ).then((res) => {
        let listTimeline = res.data.data;
        if (listTimeline) {
          let listData = listTimeline.map((el) => el.totalArticles);
          let listCategory = listTimeline.map((el) => el.time);

          this.chart.timelineChart = Highcharts.chart({
            chart: {
              renderTo: "timelineChart",
              animation: {
                duration: 1000,
              },
            },
            title: {
              text: "",
            },
            subtitle: {
              text: "",
            },
            xAxis: {
              categories: listCategory,
            },
            yAxis: {
              title: {
                text: "",
                style: {
                  color: "#FC5730",
                  fontSize: "13px",
                },
              },
            },
            series: [
              {
                data: listData,
                color: "#FC5730",
              },
            ],
            legend: {
              enabled: false,
            },
            credits: {
              enabled: false,
            },
            plotOptions: {
              line: {
                dataLabels: {
                  enabled: true,
                },
                enableMouseTracking: false,
              },
            },
          });
        }
      });
    },
  },
};
</script>
<style scoped>
.ant-list-item-meta-description {
  overflow: hidden;
  text-overflow: ellipsis;
  display: -webkit-box;
  -webkit-line-clamp: 3;
  -webkit-box-orient: vertical;
  text-overflow: unset;
}

.container {
  max-width: 1430px;
}
.section-profile-cover {
  background: linear-gradient(
    83deg,
    rgba(131, 58, 180, 1) 0%,
    rgba(253, 29, 29, 1) 41%,
    rgba(252, 148, 69, 1) 100%
  );
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

.ant-page-header {
  padding: 20px 0px;
}
.chart-container {
  padding-bottom: 50px;
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
.cus-active-button {
  color: white !important;
}
.ant-tabs-tab-active {
  color: #fc5730 !important;
}
.ant-tabs-tab:hover {
  color: #fc5730 !important;
}
.ant-tabs-ink-bar {
  background-color: #fc5730 !important;
}
.ant-radio-button-wrapper-checked {
  background-color: #fc5730 !important;
}
.ant-radio-button-wrapper:hover {
  color: #fc5730 !important;
}
.icon-flex {
  display: inline-flex;
}
</style>