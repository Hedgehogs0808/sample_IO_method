using SlackAPI;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Drawing;

class Program
{
    //static void Main(string[] args)
    //{
    //    var url = @"https://hooks.slack.com/services/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";

    //    var payload = new Payload
    //    {
    //        text = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"),
    //    };

    //    Send(url, payload);
    //}

    public static async Task Main(string[] args)
    {
        const string TOKEN = "xoxp-2985615273473-2958349478583-2979409635266-f6487688c54dfa1104885dd966e44bae";  // token from last step in section above
        var slackClient = new SlackTaskClient(TOKEN);

        string[] sts = {"定期投稿"};
        var dat = ImageToByteArray(@"C:\Users\hiro1\Desktop\reps\sample_IO_method\data\base_sample.xlsx");
//        var dat = ImageToByteArray(@"C:\Users\hiro1\Desktop\reps\sample_send_to_slack\data\quickstart.png");
        var res = await slackClient.UploadFileAsync(dat, "test.xlsx", sts);

//        var response = await slackClient.PostMessageAsync("#定期投稿", "hello world");
    }

    static public byte[] ImageToByteArray(string fileName)
    {
        FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);

        byte[] data = new byte[fs.Length];
        fs.Read(data, 0, data.Length);
        fs.Close();

        return data;
    }

    static void Send(string url, Payload payload)
    {
        var json = JsonSerializer.Serialize(payload);

        var client = new HttpClient();
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var res = client.PostAsync(url, content).Result;
        Console.WriteLine(res);
    }
}

public class Payload
{
    public string text { get; set; }
}