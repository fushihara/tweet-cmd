using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Core.Credentials;
using Tweetinvi.Core.Interfaces.DTO;
using Tweetinvi.Core.Parameters;

namespace twitter {
    class Program {
        private String AccessToken = "";
        private String AccessTokenSecret = "";
        private String ConsumerKey = "";
        private String ConsumerSecret = "";
        private String Message;
        private List<String> Images = new List<string>();
        static void Main(string[] args) {
            Program pg = new Program();
            pg.readCommandLineArgs(args);
            pg.doPost();
        }
        public void readCommandLineArgs(string[] args) {
            for (int i = 0; i < args.Length; i++) {
                String val = args[i];
                Boolean hasNext = (i + 1) < args.Length;
                String nextVal = hasNext ? args[i + 1] : "";
                if (!hasNext) {
                    return;
                } else if (val == "-at") {
                    AccessToken = nextVal;
                    i++;
                } else if (val == "-ats") {
                    AccessTokenSecret = nextVal;
                    i++;
                } else if (val == "-ck") {
                    ConsumerKey = nextVal;
                    i++;
                } else if (val == "-cs") {
                    ConsumerSecret = nextVal;
                    i++;
                } else if (val == "-message") {
                    Message = nextVal;
                    i++;
                } else if (val == "-image") {
                    Images.Add(nextVal);
                    i++;
                }
            }
            if (AccessToken == "") {
                throw new ArgumentException("AccessToken is empty.");
            }
            if (AccessTokenSecret == "") {
                throw new ArgumentException("AccessTokenSecret is empty.");
            }
            if (ConsumerKey == "") {
                throw new ArgumentException("ConsumerKey is empty.");
            }
            if (ConsumerSecret == "") {
                throw new ArgumentException("ConsumerSecret is empty.");
            }
            if (Message == "") {
                throw new ArgumentException("Message is empty.");
            }
        }
        public void doPost() {
            var creds = new TwitterCredentials(ConsumerKey, ConsumerSecret, AccessToken, AccessTokenSecret);
            var tweet = Auth.ExecuteOperationWithCredentials(creds, () => {
                List<IMedia> medias = new List<IMedia>();
                foreach (String path in Images) {
                    var image = File.ReadAllBytes(path);
                    var media = Upload.UploadImage(image);
                    medias.Add(media);
                }
                if (medias.Count == 0) {
                    //テキストのみ
                    return Tweet.PublishTweet(Message);
                } else {
                    //画像付き
                    return Tweet.PublishTweet(Message, new PublishTweetOptionalParameters {
                        Medias = medias
                    });
                }
            });
            Console.WriteLine("https://twitter.com/" + tweet.CreatedBy.ScreenName + "/status/" + tweet.IdStr);
        }
    }
}
