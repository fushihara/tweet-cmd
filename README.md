# tweet-cmd
visual studio 2013 communityにてビルド
Twitterは2015/08/08時点のAPI 1.1対応

twitter.exe -at "AccessToken" -ats "AccessTokenSecret" -ck "ConsumerKey" -cs "ConsumerSecret" -message "Message" -image "C:\hoge.png"  -image "C:\kage.png" 
-imageは任意。複数対応。それ以外は必須

sleepStr.exe "CMD"
CMD=2015/01/01 00:00:00
CMD=00:00:00
CMD=1d2h3m4s
年月日時分秒が指定の時はその時刻まで、時分秒のみ指定は次回のその時
