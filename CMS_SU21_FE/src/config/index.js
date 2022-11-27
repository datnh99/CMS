const config = {
  apiUrl: 'https://localhost:44345/api',
  CLIENT_URL :'http://localhost:8080',
  SCREEN_CODE : {
    REVIEW_ARTICLE : 'REVIEW_ARTICLE',
    ROLE_MANAGEMENT : 'ROLE_MANAGEMENT',
    CATEGORY_MANAGEMENT : 'CATEGORY_MANAGEMENT',
    ARTICLE_MANAGEMENT : 'ARTICLE_MANAGEMENT',
    NEW_ARTICLE : 'NEW_ARTICLE',
    ADMIN_ACTION : 'ADMIN_ACTION',
    MY_ARTICLE : 'MY_ARTICLE',
    DATA_REPORT : 'DATA_REPORT',
    ORIGINAL_ARTICLE_DETAIL : 'ORIGINAL_ARTICLE_DETAIL',
    SYSTEM_CONFIG: 'SYSTEM_CONFIG'
  },
  PERMISSION_SCREEN_MAP : {
    'ReviewArticle' : 'REVIEW_ARTICLE',
    'UserRoleManagerment' : 'ROLE_MANAGEMENT',
    'Category' : 'CATEGORY_MANAGEMENT',
    'ArticleManagement' : 'ARTICLE_MANAGEMENT',
    'MyArticle' : 'MY_ARTICLE',
    'NewArticle' : 'NEW_ARTICLE',
    'OriginalArticleDetail' : 'ORIGINAL_ARTICLE_DETAIL'
  },
  LOGIN_URL : 'http://localhost:8080/#/login',
  DISPLAY_SCREEN_MAP:{
    ADMIN_ACTION: "super_admin,editor,moderator,writer",
    CATEGORY_MANAGEMENT: "editor",
    ARTICLE_MANAGEMENT: "editor,moderator",
    REVIEW_ARTICLE : "editor,moderator",
    MY_ARTICLE: "writer,editor,moderator",
    NEW_ARTICLE: "writer,editor,moderator",
    ROLE_MANAGEMENT: "super_admin,editor,moderator",
    DATA_REPORT: "editor",
    ORIGINAL_ARTICLE_DETAIL: "writer,editor,moderator",
    SYSTEM_CONFIG: "super_admin"
  }
};
export default  config
