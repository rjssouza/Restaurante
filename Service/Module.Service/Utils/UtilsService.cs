using Microsoft.AspNetCore.Http;
using Module.Service.Base;
using Module.Service.Interface.Utils;
using System;

namespace Module.Service.Utils
{
    public class UtilsService : BaseService, IUtilsService
    {
        private const string BYPASS = "ByPassConfirmation";

        private readonly IHttpContextAccessor _httpContextAcessor;

        public UtilsService(IHttpContextAccessor httpContextAcessor)
        {
            this._httpContextAcessor = httpContextAcessor;
        }

        public bool GetByPassConfirmation()
        {
            if (!this._httpContextAcessor.HttpContext.Request.Headers.ContainsKey(BYPASS))
                return false;

            var byPassConfirmation = Convert.ToBoolean(this._httpContextAcessor.HttpContext.Request.Headers[BYPASS].ToString());

            return byPassConfirmation;
        }
    }
}