namespace ChatHub
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/chat/[controller]")]
    [ApiController]
    public class DocuController : ControllerBase
    {
        [HttpGet]
        public ActionResult<DocuResult> Get()
        {
            Func<MethodInfo, SimpleMethodInfo> func = Converter;

            var model = new DocuResult
            {
                HubMethods =
                    typeof(ChatHub).GetMethods()
                    .Where(x => x.DeclaringType.Name == typeof(ChatHub).Name)
                    .Select(func),

                ClientMethods =
                    typeof(IChatClient).GetMethods()
                    .Where(x => x.DeclaringType.Name == typeof(IChatClient).Name)
                    .Select(func)
            };

            return new JsonResult(model);
        }

        private SimpleMethodInfo Converter<T>(T info) where T : MethodInfo
        {
            Dictionary<string, string> parameters = this.GetParameters(info);
            
            return new SimpleMethodInfo
            {
                Name = info.Name,
                ReturnType = info.ReturnType.Name,
                Parameters = parameters,
                wat = info.DeclaringType.BaseType?.Name
            };
        }

        private Dictionary<string, string> GetParameters(MethodInfo info)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            foreach (var item in info.GetParameters())
            {
                parameters.Add(item.ParameterType.Name, item.Name);
            }

            return parameters;
        }
    }



    public class SimpleMethodInfo
    {
        public string Name { get; set; }
        public string ReturnType { get; set; }

        public Dictionary<string, string> Parameters { get; set; }

        public string BaseType { get; set; }
    }

    public class DocuResult
    {
        public IEnumerable<SimpleMethodInfo> HubMethods { get; set; }

        public IEnumerable<SimpleMethodInfo> ClientMethods { get; set; }
    }
}
