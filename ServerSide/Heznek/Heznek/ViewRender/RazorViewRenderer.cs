using Heznek.Common.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Heznek.API.ViewRender
{
    public class RazorViewRenderer : IRazorViewRenderer
    {
        private IRazorViewEngine _viewEngine;
        private ITempDataProvider _tempDataProvider;
        private IHttpContextAccessor _httpContextAccessor;
        IServiceProvider _serviceProvider;

        public RazorViewRenderer(
            IRazorViewEngine viewEngine,
            ITempDataProvider tempDataProvider, IServiceProvider serviceProvider,
        IHttpContextAccessor hca)
        {
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
            _httpContextAccessor = hca;
            _serviceProvider = serviceProvider;
        }
        public async Task<string> Render<TModel>(string name, TModel model)
        {
            var actionContext = GetActionContext();//new ActionContext(controller.HttpContext, new RouteData(), new ActionDescriptor());

            var viewEngineResult = _viewEngine.FindView(actionContext, name, false);
            //var re = _viewEngine.GetView(en)

            if (!viewEngineResult.Success)
            {
                // throw new InvalidOperationException(string.Format("Couldn't find view '{0}'", name));
                throw new InvalidOperationException(String.Join('#', viewEngineResult.SearchedLocations));
            }

            var view = viewEngineResult.View;

            using (var output = new StringWriter())
            {
                var viewContext = new ViewContext(
                    actionContext,
                    view,
                    new ViewDataDictionary<TModel>(
                        metadataProvider: new EmptyModelMetadataProvider(),
                        modelState: new ModelStateDictionary())
                    {
                        Model = model
                    },
                    new TempDataDictionary(
                        actionContext.HttpContext,
                        _tempDataProvider),
                    output,
                    new HtmlHelperOptions());

                await view.RenderAsync(viewContext)
                    .ConfigureAwait(false);

                return output.ToString();
            }


        }
        private ActionContext GetActionContext()
        {
            var httpContext = new DefaultHttpContext
            {
                RequestServices = _serviceProvider
            };

            return new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        }
    }
}
