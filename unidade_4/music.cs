using System;
using System.IO;
using Newtonsoft.Json;

namespace gcgcg {
  public class Music {
    public string name { get; set; }
    public int bpm { get; set; }
    public string[][] notes { get; set; }

  }

  public class MusicController {

    public Music music { get; set; }
    public MusicController(string music) {
      string musicJson = File.ReadAllText("music/" + music + ".json");
      this.music = JsonConvert.DeserializeObject<Music>(musicJson);
    }
  }

}