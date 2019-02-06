using AdaptiveCards;
using AdaptiveCards.Rendering;
using AdaptiveCards.Rendering.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CortanaClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            UpdateCycle();
        }

        private bool _isOutdated = true;
        private async void UpdateCycle()
        {
            await Task.Delay(1000);

            if (_isOutdated)
            {
                _isOutdated = false;

                Update();
            }
        }

        private void Update()
        {
            try
            {
                var renderer = new AdaptiveCardRenderer(AdaptiveHostConfig.FromJson(HostConfigs.CortanaHostConfig));
                CardContainer.Child = renderer.RenderCard(AdaptiveCard.FromJson(TextBoxCard.Text).Card).FrameworkElement;
            }
            catch { }
        }

        private void TextBoxCard_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isOutdated = true;
        }
    }
}
