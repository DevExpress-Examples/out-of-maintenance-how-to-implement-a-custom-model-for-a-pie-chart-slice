using DevExpress.Utils.Svg;
using DevExpress.Xpf.Core.Native;
using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfApplication2 {
    public class ItemToBitmapConverter : MarkupExtension, IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string path = @"..\..\Images\" + value.ToString() + ".svg";
            var svgStream = File.OpenRead(path);
            if (svgStream == null)
                return null;
            var svgImage = SvgLoader.LoadFromStream(svgStream);
            var size = new Size(svgImage.Width * ScreenHelper.ScaleX, svgImage.Height * ScreenHelper.ScaleX);
            return WpfSvgRenderer.CreateImageSource(svgImage, size, null, null, true);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}