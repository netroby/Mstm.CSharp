using Microsoft.Extensions.Configuration;
using Mstm.OAuth.Core;
using Newtonsoft.Json;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.OAuth.QQ
{
    /// <summary>
    /// QQ互联OAuth服务
    /// </summary>
    class QQOAuthProvider : IOAuthProvider
    {
        static IConfigurationRoot config;
        static QQOAuthProvider()
        {
            config = new ConfigurationBuilder()
             .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
             .AddJsonFile("appsettings.json").Build();

            _appId = config["QQAuth:AppId"];
            _appKey = config["QQAuth:AppKey"];
            _returnUrl = config["QQAuth:CallbackUrl"];
        }
        /// <summary>
        /// QQ互联-APPID
        /// </summary>
        private static readonly string _appId;

        /// <summary>
        /// QQ互联-APPKEY
        /// </summary>
        private static readonly string _appKey;

        /// <summary>
        /// 回调地址
        /// </summary>
        private static readonly string _returnUrl;

        /// <summary>
        /// 登录地址
        /// </summary>
        private static readonly string _loginUrl = "https://graph.qq.com/oauth2.0/authorize?response_type={0}&client_id={1}&redirect_uri={2}&state={3}&scope={4}&display={5}";

        /// <summary>
        /// 获取Token地址
        /// </summary>
        private static readonly string _getAccessTokenUrl = "https://graph.qq.com/oauth2.0/token?grant_type=authorization_code&client_id={0}&client_secret={1}&code={2}&redirect_uri={3}";

        /// <summary>
        /// 获取OpenId地址
        /// </summary>
        private static readonly string _getOpenIdUrl = "https://graph.qq.com/oauth2.0/me?access_token={0}";

        /// <summary>
        /// 获取用户信息地址
        /// </summary>
        private static readonly string _getUserInfoUrl = "https://graph.qq.com/user/get_user_info?access_token={0}&oauth_consumer_key={1}&openid={2}";

        /// <summary>
        /// Http请求代理
        /// </summary>
        private IHttpClient _httpClient;

        /// <summary>
        /// QQ互联响应解析工厂
        /// </summary>
        private IQQResponseAnalysisFactory _analysisFactory;

        public QQOAuthProvider()
        {
            _httpClient = new DefaultHttpClientFactory().GetHttpClient();
            _analysisFactory = new DefaultQQResponseAnalysisFactory();
        }

        public string GenerateLoginUrl(string state)
        {
            string response_type = "code";
            string client_id = _appId;
            string redirect_uri = StringExtensions.UrlEncode(_returnUrl);
            if (string.IsNullOrEmpty(state)) { return null; }
            string scope = QQAPIList.GetUserInfoApi;
            string display = "mobile";
            return string.Format(_loginUrl, response_type, client_id, redirect_uri, state, scope, display);
        }




        public T GetUserInfo<T>(string token, string openId)
        {
            try
            {
                string jsonStr = GetUserInfoString(token, openId);
                if (jsonStr == null) { return default(T); }
                T result = JsonConvert.DeserializeObject<T>(jsonStr);
                return result;
            }
            catch (Exception)
            {
                return default(T);
            }
        }


        public OAuthResponse GetOAuthResponse(string code)
        {
            OAuthResponse tokenResp = this.GetTokenResponse(code);
            string successCode = "0";
            if (tokenResp == null || tokenResp.ErrorCode != successCode) { return tokenResp; }
            OAuthResponse openIdResp = this.GetOpenIdResponse(tokenResp.AccessToken);
            if (openIdResp != null && !string.IsNullOrEmpty(openIdResp.OpenId))
            {
                openIdResp.AccessToken = tokenResp.AccessToken;
                openIdResp.ExpiresIn = tokenResp.ExpiresIn;
                openIdResp.RefreshToken = tokenResp.RefreshToken;
            }
            return openIdResp;
        }

        /// <summary>
        /// 获取Token信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private OAuthResponse GetTokenResponse(string code)
        {
            try
            {
                if (string.IsNullOrEmpty(code)) { return null; }
                string client_id = _appId;
                string client_secret = _appKey;
                string redirect_uri = _returnUrl;
                var result = _httpClient.GetString(string.Format(_getAccessTokenUrl, client_id, client_secret, code, redirect_uri));
                OAuthResponse data = _analysisFactory.GetQQResponseAnalysisProvider().AnalysisAccessToken(result);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取用户唯一标识
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private OAuthResponse GetOpenIdResponse(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token)) { return null; }
                var result = _httpClient.GetString(string.Format(_getOpenIdUrl, token));
                OAuthResponse data = _analysisFactory.GetQQResponseAnalysisProvider().AnalysisOpenId(result);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string GetUserInfoString(string token, string openId)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(openId)) { return null; }
            string oauth_consumer_key = _appId;
            var result = _httpClient.GetString(string.Format(_getUserInfoUrl, token, oauth_consumer_key, openId));
            return result;
        }

    }
}
