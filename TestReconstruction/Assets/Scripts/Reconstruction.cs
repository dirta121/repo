using System;
using Newtonsoft.Json;

public class Data
{
    [JsonProperty("reconstruction")]
    public Reconstruction reconstruction { get; set; }
}
[Serializable]
public partial class Reconstruction
{
    [JsonProperty("hash")]
    public string Hash { get; set; }

    [JsonProperty("images")]
    public ImageElement[] Images { get; set; }

    [JsonProperty("comments")]
    public string Comments { get; set; }

    [JsonProperty("description")]
    public Description Description { get; set; }

    [JsonProperty("images_size")]
    public long ImagesSize { get; set; }

    [JsonProperty("images_number")]
    public long ImagesNumber { get; set; }

    [JsonProperty("reconstruction_id")]
    public long ReconstructionId { get; set; }
}
[Serializable]
public partial class Description
{
    [JsonProperty("skip")]
    public bool Skip { get; set; }
}
[Serializable]
public partial class ImageElement
{
    [JsonProperty("image")]
    public ImageImage Image { get; set; }
}
[Serializable]
public partial class ImageImage
{
    [JsonProperty("hash")]
    public string Hash { get; set; }

    [JsonProperty("image_id")]
    public long ImageId { get; set; }

    [JsonProperty("camera_description")]
    public CameraDescription CameraDescription { get; set; }
}
[Serializable]
public partial class CameraDescription
{
    [JsonProperty("position")]
    public float[] Position { get; set; }

    [JsonProperty("orientation")]
    public float[] Orientation { get; set; }
}