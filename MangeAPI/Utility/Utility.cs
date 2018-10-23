using MangeAPI.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MangeAPI
{
    public partial class Utility
    {
        public class flexInf
        {
            public string user_id;
            public string shop_name;
            public int queue_number;
            public string wait_time;
            public string foot_print;
            public string img_url;
            public string counter_id;
        }
        public static string createFlexMessage(flexInf inf)
        {
            string jsonMessage = @"[
                {  
                  ""type"": ""flex"",
                  ""altText"": ""您的號碼是 " + inf.queue_number.ToString() + @" 號"",
                  ""contents"": {
                    ""type"": ""bubble"",
                      ""header"": {
                        ""type"": ""box"",
                        ""layout"": ""vertical"",
                        ""contents"": [
                          {
                            ""type"": ""text"",
                            ""text"": """ + inf.shop_name + @"""
                          }
                        ]
                      },
                  ""hero"": {
                    ""type"": ""image"",
                    ""url"": """ + inf.img_url + @""",
                    ""size"":""full"",
                    ""aspectRatio"":""2:1""
                  },
                  ""body"": {
                    ""type"": ""box"",
                    ""layout"": ""vertical"",
                    ""contents"": [
                      {
                        ""type"": ""text"",
                        ""text"": ""您的號碼是 " + inf.queue_number.ToString() + @" 號"",
                        ""size"":""lg"",
                        ""align"":""center""
                      },
                      {
                        ""type"": ""text"",
                        ""text"": """ + inf.wait_time + @""",
                        ""size"":""lg"",
                        ""align"":""center""
                      }
                    ]
                  },
                  ""footer"": {
                    ""type"": ""box"",
                    ""layout"": ""vertical"",
                    ""contents"": [
                      {
                        ""type"": ""text"",
                        ""text"": """ + inf.foot_print + @"""
                      }
                    ]
                  },
                  ""styles"": {
                   ""header"": {
                      ""backgroundColor"": ""#ffffff"",
                      ""separator"": true,
                      ""separatorColor"": ""#000000""
                    },
                    ""hero"":{
                      ""separator"": true,
                      ""separatorColor"": ""#000000""
                    },
                    ""body"":{
                      ""separator"": true,
                      ""separatorColor"": ""#000000""
                    },
                    ""footer"": {
                      ""backgroundColor"": ""#ffffff"",
                      ""separator"": true,
                      ""separatorColor"": ""#000000""
                    }
                  }
                                  }
                                }
                            ]";


            return jsonMessage;
        }

        public static string CompleteFlexMessage(QueueListItem qItem)
        {

            string jstr = @"[{
               ""type"": ""flex"",
                              ""altText"": ""您的號碼是 " + qItem.queue_number.ToString() + @" 號"",
                              ""contents"": {
                  ""type"": ""bubble"",
                  ""hero"": {
                            ""type"": ""image"",
                    ""size"": ""full"",
                    ""aspectRatio"": ""2:1"",
                    ""aspectMode"": ""cover"",
                    ""url"": ""https://mangeapi.azurewebsites.net/image/ScanQ_Welcome_bg.png""
                  },
                  ""body"": {
                            ""type"": ""box"",
                    ""layout"": ""vertical"",
                    ""spacing"": ""sm"",
                    ""contents"": [
                          {
                            ""type"": ""box"",
                            ""layout"": ""baseline"",
                            ""contents"": [
                              {
                                ""type"": ""text"",
                                ""text"": """ + qItem.queue_number.ToString() + @"號"",
                                ""wrap"": true,
                                ""size"": ""xxl"",
                                ""flex"": 1,
                                ""align"": ""center""
                              }
                            ]
                          },
                          {
                            ""margin"": ""xl"",
                            ""type"": ""text"",
                            ""text"": """ + qItem.display_name.ToString() + @""",
                            ""wrap"": true,
                            ""size"": ""xl"",
                            ""align"": ""center""
                          },
                          {
                            ""margin"": ""xl"",
                            ""type"": ""text"",
                            ""text"": ""It is your turn!!"",
                            ""wrap"": true,
                            ""weight"": ""bold"",
                            ""size"": ""xl"",
                            ""align"": ""center""
                          },
                          {
                            ""type"": ""separator"",
                            ""margin"": ""xxl""
                          },
                          {
                            ""type"": ""button"",
                            ""action"": {
                              ""type"": ""message"",
                              ""label"": ""Confirm"",
                              ""text"": ""@已讀F""
                            }
                          }
                        ]
                      }
            }

            }]";

            return jstr;
        }

        public static string CompleteFlexMessageTwo(TaskComplete qItem)
        {

            string jstr = @"[{
               ""type"": ""flex"",
                              ""altText"": ""您的號碼是 " + qItem.queue_number.ToString() + @" 號"",
                              ""contents"": {
                  ""type"": ""bubble"",
                  ""hero"": {
                            ""type"": ""image"",
                    ""size"": ""full"",
                    ""aspectRatio"": ""2:1"",
                    ""aspectMode"": ""cover"",
                    ""url"": ""https://mangeapi.azurewebsites.net/image/ScanQ_Welcome_bg.png""
                  },
                  ""body"": {
                            ""type"": ""box"",
                    ""layout"": ""vertical"",
                    ""spacing"": ""sm"",
                    ""contents"": [
                          {
                            ""type"": ""box"",
                            ""layout"": ""baseline"",
                            ""contents"": [
                              {
                                ""type"": ""text"",
                                ""text"": """ + qItem.queue_number.ToString() + @"號"",
                                ""wrap"": true,
                                ""size"": ""xxl"",
                                ""flex"": 1,
                                ""align"": ""center""
                              }
                            ]
                          },
                          {
                            ""margin"": ""xl"",
                            ""type"": ""text"",
                            ""text"": """ + qItem.display_name.ToString() + @""",
                            ""wrap"": true,
                            ""size"": ""xl"",
                            ""align"": ""center""
                          },
                          {
                            ""margin"": ""xl"",
                            ""type"": ""text"",
                            ""text"": ""It is your turn!!"",
                            ""wrap"": true,
                            ""weight"": ""bold"",
                            ""size"": ""xl"",
                            ""align"": ""center""
                          },
                          {
                            ""type"": ""separator"",
                            ""margin"": ""xxl""
                          },
                          {
                            ""type"": ""button"",
                            ""action"": {
                              ""type"": ""message"",
                              ""label"": ""Confirm"",
                              ""text"": ""@已讀F""
                            }
                          }
                        ]
                      }
            }

            }]";

            return jstr;
        }

        public static string ReceiveNotifyFlexMessage(QueueListItem qItem)
        {

            string jstr = @"[{
                ""type"": ""flex"",
                ""altText"": ""Your Queue Number is" + qItem.queue_number.ToString() + @" 號"",
                ""contents"": {
                  ""type"": ""bubble"",
                  ""hero"": {
                            ""type"": ""image"",
                    ""size"": ""full"",
                    ""aspectRatio"": ""2:1"",
                    ""aspectMode"": ""cover"",
                    ""url"": ""https://mangeapi.azurewebsites.net/image/ScanQ_Welcome_bg.png""
                  },
                  ""body"": {
                            ""type"": ""box"",
                    ""layout"": ""vertical"",
                    ""spacing"": ""sm"",
                    ""contents"": [
                          {
                            ""type"": ""text"",
                            ""text"": """ + qItem.display_name.ToString() + @""",
                            ""wrap"": true,
                            ""size"": ""xl"",
                            ""align"": ""center"",
                            ""margin"": ""xxl""
                          },

             {
                            ""type"": ""text"",
                            ""text"": ""Your Queue Number is"",
                            ""wrap"": true,
                            ""size"": ""xl"",
                            ""align"": ""center"",
                            ""margin"": ""xxl""
                          },
                          {
                            ""type"": ""box"",
                            ""margin"": ""xl"",
                            ""layout"": ""baseline"",
                            ""contents"": [
                              {
                                ""type"": ""text"",
                                ""text"": """ + qItem.queue_number.ToString() + @"號"",
                                ""wrap"": true,
                                ""weight"": ""bold"",
                                ""size"": ""xxl"",
                                ""flex"": 1,
                                ""align"": ""center""
                              }
                            ]
                          },
                          {
                            ""type"": ""separator"",
                            ""margin"": ""xxl""
                          },
              {
        ""type"": ""box"",
        ""layout"": ""horizontal"",
        ""spacing"": ""md"",
        ""contents"": [
          {
            ""type"": ""button"",
            ""flex"": 1,
            ""gravity"": ""center"",
            ""action"": {
              ""type"": ""message"",
              ""label"": ""Confirm"",
               ""text"": ""@已讀""
            }
},
          {
            ""type"": ""separator""
          },
          {
            ""type"": ""button"",
            ""action"": {
              ""type"": ""message"",
              ""label"": ""Cancel"",
              ""text"": ""@取消""
            }
          }
        ]
      }
                        ]
                      }
            }

            }]";

            //            string jstr = @"[{
            //   ""type"": ""flex"",
            //                  ""altText"": ""您的號碼是 " + qItem.queue_number.ToString() + @" 號"",
            //                  ""contents"": {
            //      ""type"": ""bubble"",
            //      ""hero"": {
            //                ""type"": ""image"",
            //        ""size"": ""full"",
            //        ""aspectRatio"": ""2:1"",
            //        ""aspectMode"": ""cover"",
            //        ""url"": ""https://mangeapi.azurewebsites.net/image/ScanQ_Welcome_bg.png""
            //      },
            //      ""body"": {
            //                ""type"": ""box"",
            //        ""layout"": ""vertical"",
            //        ""spacing"": ""sm"",
            //        ""contents"": [
            //            {
            //                ""type"": ""box"",
            //                ""layout"": ""baseline"",
            //                ""spacing"": ""sm"",
            //                ""contents"": [
            //                  {
            //                    ""type"": ""text"",
            //                    ""text"": """ + qItem.shop_name.ToString() + @""",
            //                    ""color"": ""#666666"",
            //                    ""size"": ""lg"",
            //                    ""flex"": 1,
            //                    ""align"": ""center""
            //                  }
            //                ]
            //              },
            //              {
            //                ""type"": ""box"",
            //                ""layout"": ""baseline"",
            //                ""contents"": [
            //                  {
            //                    ""type"": ""text"",
            //                    ""text"": """ + qItem.queue_number.ToString() + @"號"",
            //                    ""wrap"": true,
            //                    ""weight"": ""bold"",
            //                    ""size"": ""xxl"",
            //                    ""flex"": 1,
            //                    ""align"": ""center""
            //                  }
            //                ]
            //              },
            //              {
            //                ""type"": ""text"",
            //                ""text"": """ + qItem.display_name.ToString() + @""",
            //                ""wrap"": true,
            //                ""weight"": ""bold"",
            //                ""size"": ""xl"",
            //                ""align"": ""center""
            //              },
            //              {
            //                ""type"": ""text"",
            //                ""text"": ""服務已經完成"",
            //                ""wrap"": true,
            //                ""weight"": ""bold"",
            //                ""size"": ""xl"",
            //                ""align"": ""center""
            //              },
            //              {
            //                ""type"": ""separator"",
            //                ""margin"": ""xxl""
            //              },
            //              {
            //        ""type"": ""box"",
            //        ""layout"": ""horizontal"",
            //        ""spacing"": ""md"",
            //        ""contents"": [
            //          {
            //            ""type"": ""button"",
            //            ""flex"": 1,
            //            ""gravity"": ""center"",
            //            ""action"": {
            //              ""type"": ""uri"",
            //              ""label"": ""Confirm"",
            //              ""uri"": ""https://linecorp.com""
            //            }
            //},
            //          {
            //            ""type"": ""separator""
            //          },
            //          {
            //            ""type"": ""button"",
            //            ""action"": {
            //              ""type"": ""uri"",
            //              ""label"": ""Cancel"",
            //              ""uri"": ""https://linecorp.com""
            //            }
            //          }
            //        ]
            //      }
            //            ]
            //          }
            //}

            //}]";
            return jstr;
        }
    }
        }


