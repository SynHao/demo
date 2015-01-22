using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Analytics.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.Office.Tools.Ribbon;

namespace SynHaoExcel
{
    public partial class GoogleRibbon
    {
        private AnalyticsService _service;

        private void GoogleRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        [STAThread]
        private void btnOAuth_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
               Run().Wait();
            }
            catch (AggregateException ex)
            {
            }
        }
        
        private async Task Run()
        {
            UserCredential credential;
            credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = "587309872788-g295nn514r7jf2k79g8108rp6cr08hor.apps.googleusercontent.com",
                    ClientSecret = "uRt6iXDwVly2mZY_fsOi4jF4"
                },
                new[] { AnalyticsService.Scope.Analytics }, "SynHao", CancellationToken.None);
            _service = new AnalyticsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Google Analytics Data Export"
            });
        }
    }
}
