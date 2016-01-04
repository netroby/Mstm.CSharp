using Mstm.OAuth.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.OAuth.Weibo
{
    /// <summary>
    /// 微博登录服务
    /// http://open.weibo.com/wiki/授权机制说明
    /// </summary>
    class WeiboOAuthProvider : IOAuthProvider
    {

        /// <summary>
        /// 微博开放平台-AppKey
        /// </summary>
        private static readonly string _appKey = System.Configuration.ConfigurationManager.AppSettings["Weibo_App_Key"];

        /// <summary>
        /// 微博开放平台-AppSercet
        /// </summary>
        private static readonly string _appSercet = System.Configuration.ConfigurationManager.AppSettings["Weibo_App_Sercet"];

        /// <summary>
        /// 回调地址
        /// </summary>
        private static readonly string _returnUrl = System.Configuration.ConfigurationManager.AppSettings["WeiboCallbackUrl"];

        /// <summary>
        /// 是否强制重新登录微博
        /// </summary>
        private static readonly string _forcelogin = System.Configuration.ConfigurationManager.AppSettings["WeiboForcelogin"];

        /// <summary>
        /// 登录地址
        /// </summary>
        private static readonly string _loginUrl = "https://api.weibo.com/oauth2/authorize?client_id={0}&response_type=code&redirect_uri={1}&state={2}&forcelogin={3}";

        /// <summary>
        /// 获取Token地址
        /// </summary>
        private static readonly string _getAccessTokenUrl = "https://api.weibo.com/oauth2/access_token";


        /// <summary>
        /// 获取用户信息地址
        /// http://open.weibo.com/wiki/2/users/show
        /// </summary>
        private static readonly string _getUserInfoUrl = "https://api.weibo.com/2/users/show.json?access_token={0}&uid={1}";


        /// <summary>
        /// Http请求代理
        /// </summary>
        private IHttpClient _httpClient;

        public WeiboOAuthProvider()
        {
            _httpClient = new DefaultHttpClientFactory().GetHttpClient();
        }


        public string GenerateLoginUrl(string state)
        {
            if (string.IsNullOrEmpty(state))
            {
                return null;
            }
            string redirect_uri = System.Web.HttpUtility.UrlEncode(_returnUrl);
            //设置为true则强制用户重新登录微博
            string forcelogin = _forcelogin == "true" ? "true" : "false";
            string url = string.Format(_loginUrl, _appKey, redirect_uri, state, forcelogin);
            return url;
        }



        public OAuthResponse GetOAuthResponse(string code)
        {
            var response = GetWeiboTokenResponse(code);
            if (response == null) { return null; }
            string successCode = "0";
            OAuthResponse oauthResp = new OAuthResponse()
            {
                AccessToken = response.AccessToken,
                ClientId = _appKey,
                OpenId = response.Uid,
                ExpiresIn = response.ExpiresIn,
                ErrorCode = successCode,
                ErrorDescription = "success"
            };
            return oauthResp;
        }

        public T GetUserInfo<T>(string token, string openId)
        {
            try
            {
                string url = string.Format(_getUserInfoUrl, token, openId);
                T userInfo = _httpClient.GetAsJson<T>(url);
                return userInfo;
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// 根据code获取相应的授权信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private WeiboTokenResponse GetWeiboTokenResponse(string code)
        {
            try
            {
                if (string.IsNullOrEmpty(code)) { return null; }
                Dictionary<string, string> data = new Dictionary<string, string>() { 
                 {"client_id",_appKey},
                 {"client_secret",_appSercet},
                 {"grant_type","authorization_code"},
                 {"redirect_uri",_returnUrl},
                 {"code",code}
                };
                var response = _httpClient.Post<WeiboTokenResponse>(_getAccessTokenUrl, data);
                return response;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
