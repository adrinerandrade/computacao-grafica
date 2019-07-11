using System;
using System.IO;
using Newtonsoft.Json;

namespace gcgcg
{
  public class Music
  {
    public string name { get; set; }
    public string mp3 { get; set; }
    public int bpm { get; set; }
    public int subdivision { get; set; }
    public int delay { get; set; }
    public byte[][][] notes { get; set; }
  }
  public class MusicProvider
  {
    public static Music get(string music)
    {
      string musicJson = File.ReadAllText("./musicRepository/" + music + ".json");
      return JsonConvert.DeserializeObject<Music>(musicJson);
    }
  }
}