using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace TrailHeadTestApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GenerateQRPage : ContentPage
	{
        ZXingBarcodeImageView barcode;

        public GenerateQRPage ()
		{
			InitializeComponent ();

            var stackLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            barcode = new ZXingBarcodeImageView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "zxingBarcodeImageView",
            };
            barcode.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE;
            barcode.BarcodeOptions.Width = 300;
            barcode.BarcodeOptions.Height = 300;
            barcode.BarcodeOptions.Margin = 10;
            barcode.BarcodeValue = "ZXing.Net.Mobile";

            var text = new Entry
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "Random Seed"
            };
            text.TextChanged += Text_TextChanged;

            stackLayout.Children.Add(barcode);
            stackLayout.Children.Add(text);

            Content = stackLayout;
        }

        void Text_TextChanged(object sender, TextChangedEventArgs e)
            => barcode.BarcodeValue = e.NewTextValue;
    }
}