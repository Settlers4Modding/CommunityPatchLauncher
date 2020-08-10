using System.Windows;
using System.Windows.Controls;

namespace CommunityPatchLauncher.BehaviorExtensions
{
    /// <summary>
    /// This class adds a new behavior to the webbrowser
    /// </summary>
    public class BrowserBehavior
    {
        /// <summary>
        /// New html property
        /// </summary>
        public static readonly DependencyProperty HtmlProperty = DependencyProperty.RegisterAttached(
            "Html", typeof(string), typeof(BrowserBehavior), new FrameworkPropertyMetadata(OnHtmlChanged));

        /// <summary>
        /// Get the content of the html property
        /// </summary>
        /// <param name="browser">The browser to use</param>
        /// <returns>The content as string</returns>
        [AttachedPropertyBrowsableForType(typeof(WebBrowser))]
        public static string GetHtml(WebBrowser browser)
        {
            return (string)browser.GetValue(HtmlProperty);
        }

        /// <summary>
        /// Set the content of the html property
        /// </summary>
        /// <param name="browser">The browser to use</param>
        /// <param name="value">The value to set</param>
        public static void SetHtml(WebBrowser browser, string value)
        {
            browser.SetValue(HtmlProperty, value);
        }

        /// <summary>
        /// If the html attribute did change
        /// </summary>
        /// <param name="dependencyObject">The depending pbject</param>
        /// <param name="e">Event arguments</param>
        static void OnHtmlChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var browser = dependencyObject as WebBrowser;
            if (browser != null)
            {
                browser.NavigateToString(e.NewValue.ToString());
            }
        }
    }
}
