using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Threading.Tasks;
using Google.Apis.Dfareporting.v1_3;
using Google.Apis.Auth.OAuth2;
using System.IO;
using System.Threading;
using Google.Apis.Util.Store;

namespace SynHaoData
{
    public partial class RibbonData
    {
        private void RibbonData_Load(object sender, RibbonUIEventArgs e)
        {

        }

        [STAThread]
        private void btnLogin_Click(object sender, RibbonControlEventArgs e)
        {

        }

        private async Task Run()
        {
            UserCredential credential;
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { DfareportingService.Scope.Dfareporting },
                    "user", CancellationToken.None, new FileDataStore("SynHaoData"));
            }
        }
    }
}
