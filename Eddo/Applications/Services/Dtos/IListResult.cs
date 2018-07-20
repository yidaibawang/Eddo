using System.Collections.Generic;

namespace Eddo.Applications.Services.Dtos
{
    public interface IListResult<T>
    {
        /// <summary>
        /// List of items.
        /// </summary>
        IReadOnlyList<T> Items { get; set; }
    }
}
