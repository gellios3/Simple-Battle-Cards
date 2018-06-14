using System.Collections.Generic;

namespace Models.State
{
    /// <summary>
    /// Container struct for the List
    /// </summary>
    public struct ListContainer<T>
    {
        public T DataList;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataList">Data list value</param>
        public ListContainer(T dataList)
        {
            DataList = dataList;
        }
    }
}