using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPatchLauncher.EventArguments
{
    /// <summary>
    /// Return argument for a data command
    /// </summary>
    internal class DataCommandEventArg : EventArgs
    {
        /// <summary>
        /// The return data of the command
        /// </summary>
        private readonly object data;

        /// <summary>
        /// Create a new return data set
        /// </summary>
        /// <param name="data"></param>
        public DataCommandEventArg(object data)
        {
            this.data = data;
        }

        /// <summary>
        /// Get the type of the object stored in the event argument
        /// </summary>
        /// <returns>The type of the saved data</returns>
        public Type GetDataType()
        {
            return data.GetType();
        }

        /// <summary>
        /// Return the data of this event
        /// </summary>
        /// <typeparam name="T">The type of the data to cast to</typeparam>
        /// <returns>The default value if the internal datatype is not as expected</returns>
        public virtual T GetData<T>()
        {
            Type type = typeof(T);
            return data != null && type == data.GetType() ? (T)Convert.ChangeType(data, type) : default(T);
        }

    }
}
