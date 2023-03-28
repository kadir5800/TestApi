using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.BaseObjects
{
    public class ClientResult : ClientResult<object>, IClientResult
    {

    }

    /// <summary>
    /// Client Result
    /// </summary>
    public class ClientResult<T> : IClientResult
    {
        /// <summary>
        /// Basarili
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Kod
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Mesaj
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Veri
        /// </summary>
        public T Data { get; set; }

    }

    /// <summary>
    /// Client Result
    /// </summary>
    public interface IClientResult
    {
    }
}
