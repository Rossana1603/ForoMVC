using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Forum.Web.Resolver
{
    public static class HttpHelperResolver
    {
        public static void SaveImageAs(this HttpPostedFileBase file, string path, int width, int height)
        {
            WebImage img = new WebImage(file.InputStream);
            if (img.Width > 100 || img.Height > 100)
            {
                img.Resize(100, 100);
            }
            img.Save(path);            
        }
    }
}