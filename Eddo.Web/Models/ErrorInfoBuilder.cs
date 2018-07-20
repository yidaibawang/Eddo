using Eddo.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eddo.Web.Configuration;
namespace Eddo.Web.Models
{
    public class ErrorInfoBuilder: IErrorInfoBuilder, ISingletonDependency
    {
        public static IErrorInfoBuilder Instance { get { return IocManager.Instance.Resolve<IErrorInfoBuilder>(); } }

        private IExceptionToErrorInfoConverter Converter { get; set; }

        /// <inheritdoc/>
        public ErrorInfoBuilder(IEddoWebModuleConfiguration configuration)
        {
            Converter = new DefaultErrorInfoConverter(configuration);
        }

        /// <inheritdoc/>
        public ErrorInfo BuildForException(Exception exception)
        {
            return Converter.Convert(exception);
        }

        /// <summary>
        /// Adds an exception converter that is used by <paramref name="ForException"/> method.
        /// </summary>
        /// <param name="converter">Converter object</param>
        public void AddExceptionConverter(IExceptionToErrorInfoConverter converter)
        {
            converter.Next = Converter;
            Converter = converter;
        }
    }
}
