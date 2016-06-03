using Microsoft.WindowsAzure.Mobile.Service;

namespace MindgemAPI.DataObjects
{
    public class WalletItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }
    }
}