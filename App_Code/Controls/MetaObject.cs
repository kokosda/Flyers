using System;

namespace FlyerMe.Controls
{
    public sealed class MetaObject
    {
        private MetaObject()
        {
        }

        public String PageTitle { get; private set; }

        public String Title { get; private set; }

        public String Keywords { get; private set; }

        public String Description { get; private set; }

        public static MetaObject Create()
        {
            return new MetaObject();
        }

        public MetaObject SetPageTitle(String value)
        {
            PageTitle = value;

            return this;
        }

        public MetaObject SetPageTitle(String format, params Object[] values)
        {
            PageTitle = String.Format(format, values);

            return this;
        }

        public MetaObject SetTitle(String value)
        {
            Title = value;

            return this;
        }

        public MetaObject SetTitle(String format, params Object[] values)
        {
            Title = String.Format(format, values);

            return this;
        }

        public MetaObject SetKeywords(String value)
        {
            Keywords = value;

            return this;
        }

        public MetaObject SetKeywords(String format, params Object[] values)
        {
            Keywords = String.Format(format, values);

            return this;
        }

        public MetaObject SetDescription(String value)
        {
            Description = value;

            return this;
        }

        public MetaObject SetDescription(String format, params Object[] values)
        {
            Description = String.Format(format, values);

            return this;
        }
    }
}