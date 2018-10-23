using isRock.LineBot;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MangeAPI
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string channelAccessToken = ConfigurationManager.AppSettings["ChannelAccessToken"];
        string AdminUserId = ConfigurationManager.AppSettings["AdminUserID"];
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Bot bot = new Bot(channelAccessToken);
            //Thread thread = new Thread(() => {
            //    Thread.Sleep(2000);
            //    bot.PushMessage("Ucf64da86af345f8a009970a62e24763a", "Message after 2 second");
            //});
            //按下我知道了按鈕
            bot.PushMessage("Ucf64da86af345f8a009970a62e24763a", 1, 2);
            bot.PushMessage("Ucf64da86af345f8a009970a62e24763a", "客人名稱，服務完成後，我們會再發送通知");
            bot.PushMessage("Ucf64da86af345f8a009970a62e24763a",2,144); 

            bot.PushMessage("Ucf64da86af345f8a009970a62e24763a", "客人名稱，您的服務完成囉。");
            bot.PushMessage("Ucf64da86af345f8a009970a62e24763a", "請至服務台");

            //按下我知道了按鈕
            bot.PushMessage("Ucf64da86af345f8a009970a62e24763a", "Have a nice day!  客人名稱");

            Thread.Sleep(2000); //Delay 1秒
            bot.PushMessage("Ucf64da86af345f8a009970a62e24763a", "Message after 2 second");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Bot bot = new Bot(channelAccessToken);

            //建立actions，作為ButtonTemplate的用戶回覆行為
            var actions = new List<isRock.LineBot.TemplateActionBase>();
            //actions.Add(new isRock.LineBot.MessageAction()
            //{ label = "點選這邊等同用戶直接輸入某訊息", text = "/例如這樣" });
            //actions.Add(new isRock.LineBot.UriAction()
            //{ label = "點這邊開啟網頁", uri = new Uri("http://www.google.com") });
            actions.Add(new isRock.LineBot.PostbackAction()
            { label = "點選這邊模擬掃完QRCode情境", data = "abc=aaa&def=111" });

            //單一Button Template Message
            var ButtonTemplate = new isRock.LineBot.ButtonsTemplate()
            {
                altText = "替代文字(在無法顯示Button Template的時候顯示)",
                text = "文字TEXT",
                title = "文字Title",
                //設定圖片
                thumbnailImageUrl = new Uri("https://chart.googleapis.com/chart?cht=qr&chl=Hello=gff+world=fff&choe=UTF-8&chs=177x177"),
                actions = actions //設定回覆動作
            };
            //單一Button Template Message
            var ButtonTemplate2 = new isRock.LineBot.ButtonsTemplate()
            {
                altText = "替代文字(在無法顯示Button Template的時候顯示)",
                text = "文字TEXT",
                title = "文字Title",
                //設定圖片
                thumbnailImageUrl = new Uri("https://chart.googleapis.com/chart?cht=qr&chl=Hello=gff+world=fff&choe=UTF-8&chs=120x120"),
                actions = actions //設定回覆動作
            };
            //按下我知道了按鈕
            bot.PushMessage("Ucf64da86af345f8a009970a62e24763a", ButtonTemplate);
            bot.PushMessage("Ucf64da86af345f8a009970a62e24763a", ButtonTemplate2);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string jstr = @"[{
                  ""type"": ""flex"",
                  ""altText"": ""ScanQ Queue Service"",
                  ""contents"":
                    {
  ""type"": ""bubble"",
  ""body"": {
                ""type"": ""box"",
    ""layout"": ""vertical"",
    ""spacing"": ""md"",
    ""contents"": [
      {
        ""type"": ""text"",
        ""text"": ""ScanQ Queue Service"",
        ""wrap"": true,
        ""weight"": ""bold"",
        ""align"":""center"",
        ""color"":""#4d6e9f"",
        ""gravity"": ""center"",
        ""size"": ""xl""
      },
      {
        ""type"": ""box"",
        ""layout"": ""vertical"",
        ""margin"": ""xxl"",
        ""contents"": [
          {
            ""type"": ""spacer""
          },
          {
            ""type"": ""image"",
            ""url"": ""https://scdn.line-apps.com/n/channel_devcenter/img/fx/linecorp_code_withborder.png"",
            ""aspectMode"": ""cover"",
            ""size"": ""xl""
          },
          {
            ""type"": ""text"",
            ""text"": ""Simulate scanning QRCode action immediately by clicking\n Start Scanning QRCode Button! "",
            ""align"": ""center"",
            ""color"": ""#aaaaaa"",
            ""wrap"": true,
            ""margin"": ""xxl"",
            ""size"": ""xs""
          },
          {
            ""type"": ""button"",
            ""flex"": 1,
            ""gravity"": ""center"",
            ""action"": {
              ""type"": ""uri"",
              ""label"": ""Start Scanning QRCode"",
              ""uri"": ""https://linecorp.com""
            }
          }
        ]
      }
    ]
  }
}
                }]";
            Bot bot = new Bot(channelAccessToken);
            bot.PushMessageWithJSON("Ucf64da86af345f8a009970a62e24763a",jstr);
        }
    }
}