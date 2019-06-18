using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Heznek.Common.Email
{
    public interface IRazorViewRenderer
    {
        Task<string> Render<TModel>(string name, TModel model);
    }
}
