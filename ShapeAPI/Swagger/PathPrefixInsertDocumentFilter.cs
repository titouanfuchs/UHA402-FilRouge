using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Swagger;
using System.Xml.XPath;
using IDocumentFilter = Swashbuckle.AspNetCore.SwaggerGen.IDocumentFilter;
using Microsoft.OpenApi.Models;

namespace back.Swagger
{
    public class PathPrefixInsertDocumentFilter:IDocumentFilter
    {
        private readonly string _pathPrefix;

        public PathPrefixInsertDocumentFilter(string prefix)
        {
            this._pathPrefix = prefix;
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = swaggerDoc.Paths.Keys.ToList();
            foreach (var path in paths)
            {
                var pathToChange = swaggerDoc.Paths[path];
                swaggerDoc.Paths.Remove(path);
                swaggerDoc.Paths.Add("/" + _pathPrefix + path, pathToChange);
            }
        }
    }
}
