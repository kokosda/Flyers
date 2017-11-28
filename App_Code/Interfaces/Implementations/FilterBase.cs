using System;

namespace FlyerMe
{
    public class FilterBase : IFilter
    {
        protected FilterBase()
        {
        }

        public static FilterBase Get()
        {
            return new FilterBase();
        }

        public virtual Boolean IsEntityFieldsEmpty
        {
            get 
            {
                return true;
            }
        }

        public virtual String EntityFieldsQueryString
        {
            get 
            {
                return null;
            }
        }
    }
}