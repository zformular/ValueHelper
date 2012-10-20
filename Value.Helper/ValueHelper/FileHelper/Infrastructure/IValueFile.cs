using System;

namespace ValueHelper.Infrastructure
{
    public interface IValueFile : IDisposable
    {
        Boolean CreateFile();

        Boolean Write(String context);

        Boolean Write(String context, Boolean append);

        Boolean WriteLine(String context);

        Boolean WriteLine(String context, Boolean append);

        String ReadContext();
    }
}
