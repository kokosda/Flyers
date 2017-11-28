using System;

namespace FlyerMe
{
    public interface IFilter
    {
        Boolean IsEntityFieldsEmpty { get; }

        String EntityFieldsQueryString { get; }
    }
}