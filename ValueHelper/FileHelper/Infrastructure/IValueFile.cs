using System;

namespace ValueHelper.Infrastructure
{
    public interface IValueFile
    {
        String FileFullName { get; set; }

        String FileName { get; set; }

        String Content { get; set; }

        Boolean ContentExsist(String content);

        void WriteContent();

        void WriteContent(String content);

        String ReadContent();

        String ReadContent(String content);
    }
}
