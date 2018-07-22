using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Eddo.WebApi.Dynamic.Buider
{
     public interface IApiControllerBuilder<T>
    {
        /// <summary>
        /// To add Action filters for the Dynamic Controller.
        /// </summary>
        /// <param name="filters"> The filters. </param>
        /// <returns>The current Controller Builder </returns>
        IApiControllerBuilder<T> WithFilters(params IFilter[] filters);

        /// <summary>
        /// Used to specify a method definition.
        /// </summary>
        /// <param name="methodName">Name of the method in proxied type</param>
        /// <returns>Action builder</returns>
        IApiControllerActionBuilder<T> ForMethod(string methodName);

        /// <summary>
        /// Use conventional Http Verbs by method names.
        /// By default, it uses <see cref="HttpVerb.Post"/> for all actions.
        /// </summary>
        /// <returns>The current Controller Builder</returns>
        IApiControllerBuilder<T> WithConventionalVerbs();

        /// <summary>
        /// Builds the controller.
        /// This method must be called at last of the build operation.
        /// </summary>
        void Build();
    }
}
