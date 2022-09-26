using Newtonsoft.Json;

namespace valeo_build_light.Models;

public class HueDeviceIpRecord
{
    public string Id { get; set; } = "";

    [JsonProperty("internalipaddress")]
    public string IpAddress { get; set; } = "";

    public int Port { get; set; }
}

public class HueDeviceService
{
    public string Rid { get; set; } = "";
    public string Rtype { get; set; } = "";
}

public class HueDeviceData
{
    public List<HueDeviceService> Services { get; set; } = new();
}

public class HueResourceDeviceResponse
{
    public List<HueDeviceData> Data { get; set; } = new();
}

public class HueResourceLightRequest
{
    public class OnOff
    {
        [JsonProperty("on")]
        public bool On { get; set; }
    }

    public class DimmingValue
    {
        [JsonProperty("brightness")]
        public int Brightness { get; set; }
    }

    public class ColorValue
    {
        [JsonProperty("xy")]
        public HueXY XY { get; set; }
    }

    [JsonProperty("on")]
    public OnOff? On { get; set; }

    [JsonProperty("dimming")]
    public DimmingValue? Dimming { get; set; }

    [JsonProperty("color")]
    public ColorValue? Color { get; set; }
}

public class HueXY
{
    public HueXY(double x, double y)
    {
        X = x;
        Y = y;
    }

    [JsonProperty("x")] 
    public double X { get; set; }

    [JsonProperty("y")] 
    public double Y { get; set; }
}

public static class Hue
{
    public static readonly HueXY lightsalmon = new(0.5015, 0.3530);
    public static readonly HueXY salmon = new(0.5347, 0.3256);
    public static readonly HueXY darksalmon = new(0.4849, 0.3476);
    public static readonly HueXY lightcoral = new(0.5065, 0.3145);
    public static readonly HueXY indianred = new(0.5475, 0.3113);
    public static readonly HueXY crimson = new(0.6435, 0.3045);
    public static readonly HueXY firebrick = new(0.6554, 0.3111);
    public static readonly HueXY red = new(0.675, 0.322);
    public static readonly HueXY darkred = new(0.675, 0.322);

    public static readonly HueXY coral = new(0.5754, 0.3480);
    public static readonly HueXY tomato = new(0.6111, 0.3260);
    public static readonly HueXY orangered = new(0.6725, 0.3230);
    public static readonly HueXY gold = new(0.4852, 0.4619);
    public static readonly HueXY orange = new(0.5567, 0.4091);
    public static readonly HueXY darkorange = new(0.5921, 0.3830);

    public static readonly HueXY lightyellow = new(0.3435, 0.3612);
    public static readonly HueXY lemonchiffon = new(0.3594, 0.3756);
    public static readonly HueXY lightgoldenrodyellow = new(0.3502, 0.3715);
    public static readonly HueXY papayawhip = new(0.3598, 0.3546);
    public static readonly HueXY moccasin = new(0.3913, 0.3755);
    public static readonly HueXY peachpuff = new(0.3948, 0.3597);
    public static readonly HueXY palegoldenrod = new(0.3762, 0.3978);
    public static readonly HueXY khaki = new(0.4023, 0.4267);
    public static readonly HueXY darkkhaki = new(0.4019, 0.4324);
    public static readonly HueXY yellow = new(0.4325, 0.5007);

    public static readonly HueXY lawngreen = new(0.4091, 0.518);
    public static readonly HueXY chartreuse = new(0.4091, 0.518);
    public static readonly HueXY limegreen = new(0.4091, 0.518);
    public static readonly HueXY lime = new(0.4091, 0.518);
    public static readonly HueXY forestgreen = new(0.4091, 0.518);
    public static readonly HueXY green = new(0.4091, 0.518);
    public static readonly HueXY darkgreen = new(0.4091, 0.518);
    public static readonly HueXY greenyellow = new(0.4091, 0.518);
    public static readonly HueXY yellowgreen = new(0.4091, 0.518);
    public static readonly HueXY springgreen = new(0.3883, 0.4771);
    public static readonly HueXY mediumspringgreen = new(0.3620, 0.4250);
    public static readonly HueXY lightgreen = new(0.3673, 0.4356);
    public static readonly HueXY palegreen = new(0.3674, 0.4358);
    public static readonly HueXY darkseagreen = new(0.3423, 0.3862);
    public static readonly HueXY mediumseagreen = new(0.3584, 0.4180);
    public static readonly HueXY seagreen = new(0.3580, 0.4172);
    public static readonly HueXY olive = new(0.4325, 0.5007);
    public static readonly HueXY darkolivegreen = new(0.3886, 0.4776);
    public static readonly HueXY olivedrab = new(0.4091, 0.518);

    public static readonly HueXY lightcyan = new(0.3096, 0.3216);
    public static readonly HueXY cyan = new(0.2857, 0.2744);
    public static readonly HueXY aqua = new(0.2857, 0.2744);
    public static readonly HueXY aquamarine = new(0.3230, 0.3480);
    public static readonly HueXY mediumaquamarine = new(0.3231, 0.3483);
    public static readonly HueXY paleturquoise = new(0.3032, 0.3090);
    public static readonly HueXY turquoise = new(0.3005, 0.3036);
    public static readonly HueXY mediumturquoise = new(0.2937, 0.2902);
    public static readonly HueXY darkturquoise = new(0.2834, 0.2698);
    public static readonly HueXY lightseagreen = new(0.2944, 0.2916);
    public static readonly HueXY cadetblue = new(0.2963, 0.2953);
    public static readonly HueXY darkcyan = new(0.2857, 0.2744);
    public static readonly HueXY teal = new(0.2857, 0.2744);

    public static readonly HueXY powderblue = new(0.3015, 0.3057);
    public static readonly HueXY lightblue = new(0.2969, 0.2964);
    public static readonly HueXY lightskyblue = new(0.2706, 0.2447);
    public static readonly HueXY skyblue = new(0.2788, 0.2630);
    public static readonly HueXY deepskyblue = new(0.2425, 0.1892);
    public static readonly HueXY lightsteelblue = new(0.2926, 0.2880);
    public static readonly HueXY dodgerblue = new(0.2124, 0.1297);
    public static readonly HueXY cornflowerblue = new(0.2355, 0.1753);
    public static readonly HueXY steelblue = new(0.2491, 0.2021);
    public static readonly HueXY royalblue = new(0.2051, 0.1152);
    public static readonly HueXY blue = new(0.167, 0.04);
    public static readonly HueXY mediumblue = new(0.167, 0.04);
    public static readonly HueXY darkblue = new(0.167, 0.04);
    public static readonly HueXY navy = new(0.167, 0.04);
    public static readonly HueXY midnightblue = new(0.1821, 0.0698);
    public static readonly HueXY mediumslateblue = new(0.2186, 0.1419);
    public static readonly HueXY slateblue = new(0.2198, 0.1443);
    public static readonly HueXY darkslateblue = new(0.2235, 0.1502);

    public static readonly HueXY lavender = new(0.3085, 0.3071);
    public static readonly HueXY thistle = new(0.3342, 0.2970);
    public static readonly HueXY plum = new(0.3495, 0.2545);
    public static readonly HueXY violet = new(0.3645, 0.2128);
    public static readonly HueXY orchid = new(0.3716, 0.2102);
    public static readonly HueXY fuchsia = new(0.3826, 0.1597);
    public static readonly HueXY magenta = new(0.3826, 0.1597);
    public static readonly HueXY mediumorchid = new(0.3362, 0.1743);
    public static readonly HueXY mediumpurple = new(0.2629, 0.1772);
    public static readonly HueXY blueviolet = new(0.2524, 0.1062);
    public static readonly HueXY darkviolet = new(0.2852, 0.1086);
    public static readonly HueXY darkorchid = new(0.2986, 0.1335);
    public static readonly HueXY darkmagenta = new(0.3826, 0.1597);
    public static readonly HueXY purple = new(0.3826, 0.1597);
    public static readonly HueXY indigo = new(0.2485, 0.0917);

    public static readonly HueXY pink = new(0.3947, 0.3114);
    public static readonly HueXY lightpink = new(0.4105, 0.3102);
    public static readonly HueXY hotpink = new(0.4691, 0.2468);
    public static readonly HueXY deeppink = new(0.5388, 0.2464);
    public static readonly HueXY palevioletred = new(0.4657, 0.2773);
    public static readonly HueXY mediumvioletred = new(0.4997, 0.2247);

    public static readonly HueXY white = new(0.3227, 0.3290);
    public static readonly HueXY snow = new(0.3280, 0.3286);
    public static readonly HueXY honeydew = new(0.3210, 0.3441);
    public static readonly HueXY mintcream = new(0.3162, 0.3346);
    public static readonly HueXY azure = new(0.3125, 0.3274);
    public static readonly HueXY aliceblue = new(0.3098, 0.3220);
    public static readonly HueXY ghostwhite = new(0.3098, 0.3220);
    public static readonly HueXY whitesmoke = new(0.3227, 0.3290);
    public static readonly HueXY seashell = new(0.3385, 0.3353);
    public static readonly HueXY beige = new(0.3401, 0.3559);
    public static readonly HueXY oldlace = new(0.3377, 0.3376);
    public static readonly HueXY floralwhite = new(0.3349, 0.3388);
    public static readonly HueXY ivory = new(0.3327, 0.3444);
    public static readonly HueXY antiquewhite = new(0.3546, 0.3488);
    public static readonly HueXY linen = new(0.3410, 0.3386);
    public static readonly HueXY lavenderblush = new(0.3357, 0.3226);
    public static readonly HueXY mistyrose = new(0.4212, 0.1823);
}