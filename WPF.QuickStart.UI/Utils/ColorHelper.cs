using System.Collections.Generic;
using System.Windows.Media;
using System.Globalization;
using System;

namespace WPF.QuickStart.UI.Utils
{
	public static class ColorHelper
    {
        private static Dictionary<string, Color> namedColors = new Dictionary<string, Color>();

        #region ctor

        static ColorHelper()
        {
            InitColors();
        }

        #endregion

        public static Color ToColor(string value)
        {
            if (value == null)
            {
                return Colors.Red;
            }

            // Named Colors
            string valueLower = value.ToLower();
            if (namedColors.ContainsKey(valueLower))
            {
                return namedColors[valueLower];
            }

            // #ARGB and #RGB Hex colors
            if (value[0] == '#')
            {
                value = value.Remove(0, 1);
            }

            int length = value.Length;
            if ((length == 6 || length == 8) && IsHexColor(value))
            {
                if (length == 8)
                {
                    //#FF1CD64D
                    return Color.FromArgb(
                        byte.Parse(value.Substring(0, 2), NumberStyles.HexNumber),
                        byte.Parse(value.Substring(2, 2), NumberStyles.HexNumber),
                        byte.Parse(value.Substring(4, 2), NumberStyles.HexNumber),
                        byte.Parse(value.Substring(6, 2), NumberStyles.HexNumber)
                    );
                }
                else if (length == 6)
                {
                    //#FF1CD64D
                    return Color.FromArgb(0xff,
                        byte.Parse(value.Substring(0, 2), NumberStyles.HexNumber),
                        byte.Parse(value.Substring(2, 2), NumberStyles.HexNumber),
                        byte.Parse(value.Substring(4, 2), NumberStyles.HexNumber)
                    );
                }
            }

            string[] argb = value.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (argb != null)
            {
                if (argb.Length == 4)
                {
                    return Color.FromArgb(
                        byte.Parse(argb[0]),
                        byte.Parse(argb[1]),
                        byte.Parse(argb[2]),
                        byte.Parse(argb[3])
                    );
                }
                else if (argb.Length == 3)
                {
                    return Color.FromArgb(0xff,
                        byte.Parse(argb[0]),
                        byte.Parse(argb[1]),
                        byte.Parse(argb[2])
                    );
                }
            }

            return Colors.Black;
        }

        #region Helper Methods

        private static bool IsHexColor(string value)
        {
            if (value == null)
            {
                return false;
            }

            foreach (char c in value.ToCharArray())
            {
                if (!Uri.IsHexDigit(c))
                {
                    return false;
                }
            }

            return true;
        }

        private static void InitColors()
        {
            namedColors.Add("aliceblue", ToColor("#f0f8ff"));
            namedColors.Add("antiquewhite", ToColor("#faebd7"));
            namedColors.Add("aqua", ToColor("#00ffff"));
            namedColors.Add("aquamarine", ToColor("#7fffd4"));
            namedColors.Add("azure", ToColor("#f0ffff"));
            namedColors.Add("beige", ToColor("#f5f5dc"));
            namedColors.Add("bisque", ToColor("#ffe4c4"));
            namedColors.Add("black", ToColor("#000000"));
            namedColors.Add("blanchedalmond", ToColor("#ffebcd"));
            namedColors.Add("blue", ToColor("#0000ff"));
            namedColors.Add("blueviolet", ToColor("#8a2be2"));
            namedColors.Add("brown", ToColor("#a52a2a"));
            namedColors.Add("burlywood", ToColor("#deb887"));
            namedColors.Add("cadetblue", ToColor("#5f9ea0"));
            namedColors.Add("chartreuse", ToColor("#7fff00"));
            namedColors.Add("chocolate", ToColor("#d2691e"));
            namedColors.Add("coral", ToColor("#ff7f50"));
            namedColors.Add("cornflowerblue", ToColor("#6495ed"));
            namedColors.Add("cornsilk", ToColor("#fff8dc"));
            namedColors.Add("crimson", ToColor("#dc143c"));
            namedColors.Add("cyan", ToColor("#00ffff"));
            namedColors.Add("darkblue", ToColor("#00008b"));
            namedColors.Add("darkcyan", ToColor("#008b8b"));
            namedColors.Add("darkgoldenrod", ToColor("#b8860b"));
            namedColors.Add("darkgray", ToColor("#a9a9a9"));
            namedColors.Add("darkgreen", ToColor("#006400"));
            namedColors.Add("darkkhaki", ToColor("#bdb76b"));
            namedColors.Add("darkmagenta", ToColor("#8b008b"));
            namedColors.Add("darkolivegreen", ToColor("#556b2f"));
            namedColors.Add("darkorange", ToColor("#ff8c00"));
            namedColors.Add("darkorchid", ToColor("#9932cc"));
            namedColors.Add("darkred", ToColor("#8b0000"));
            namedColors.Add("darksalmon", ToColor("#e9967a"));
            namedColors.Add("darkseagreen", ToColor("#8fbc8f"));
            namedColors.Add("darkslateblue", ToColor("#483d8b"));
            namedColors.Add("darkslategray", ToColor("#2f4f4f"));
            namedColors.Add("darkturquoise", ToColor("#00ced1"));
            namedColors.Add("darkviolet", ToColor("#9400d3"));
            namedColors.Add("deeppink", ToColor("#ff1493"));
            namedColors.Add("deepskyblue", ToColor("#00bfff"));
            namedColors.Add("dimgray", ToColor("#696969"));
            namedColors.Add("dodgerblue", ToColor("#1e90ff"));
            namedColors.Add("firebrick", ToColor("#b22222"));
            namedColors.Add("floralwhite", ToColor("#fffaf0"));
            namedColors.Add("forestgreen", ToColor("#228b22"));
            namedColors.Add("fuchsia", ToColor("#ff00ff"));
            namedColors.Add("gainsboro", ToColor("#dcdcdc"));
            namedColors.Add("ghostwhite", ToColor("#f8f8ff"));
            namedColors.Add("gold", ToColor("#ffd700"));
            namedColors.Add("goldenrod", ToColor("#daa520"));
            namedColors.Add("gray", ToColor("#808080"));
            namedColors.Add("green", ToColor("#008000"));
            namedColors.Add("greenyellow", ToColor("#adff2f"));
            namedColors.Add("honeydew", ToColor("#f0fff0"));
            namedColors.Add("hotpink", ToColor("#ff69b4"));
            namedColors.Add("indianred", ToColor("#cd5c5c"));
            namedColors.Add("indigo", ToColor("#4b0082"));
            namedColors.Add("ivory", ToColor("#fffff0"));
            namedColors.Add("khaki", ToColor("#f0e68c"));
            namedColors.Add("lavender", ToColor("#e6e6fa"));
            namedColors.Add("lavenderblush", ToColor("#fff0f5"));
            namedColors.Add("lawngreen", ToColor("#7cfc00"));
            namedColors.Add("lemonchiffon", ToColor("#fffacd"));
            namedColors.Add("lightblue", ToColor("#add8e6"));
            namedColors.Add("lightcoral", ToColor("#f08080"));
            namedColors.Add("lightcyan", ToColor("#e0ffff"));
            namedColors.Add("lightgoldenrodyellow", ToColor("#fafad2"));
            namedColors.Add("lightgreen", ToColor("#90ee90"));
            namedColors.Add("lightgrey", ToColor("#d3d3d3"));
            namedColors.Add("lightpink", ToColor("#ffb6c1"));
            namedColors.Add("lightsalmon", ToColor("#ffa07a"));
            namedColors.Add("lightseagreen", ToColor("#20b2aa"));
            namedColors.Add("lightskyblue", ToColor("#87cefa"));
            namedColors.Add("lightslategray", ToColor("#778899"));
            namedColors.Add("lightsteelblue", ToColor("#b0c4de"));
            namedColors.Add("lightyellow", ToColor("#ffffe0"));
            namedColors.Add("lime", ToColor("#00ff00"));
            namedColors.Add("limegreen", ToColor("#32cd32"));
            namedColors.Add("linen", ToColor("#faf0e6"));
            namedColors.Add("magenta", ToColor("#ff00ff"));
            namedColors.Add("maroon", ToColor("#800000"));
            namedColors.Add("mediumaquamarine", ToColor("#66cdaa"));
            namedColors.Add("mediumblue", ToColor("#0000cd"));
            namedColors.Add("mediumorchid", ToColor("#ba55d3"));
            namedColors.Add("mediumpurple", ToColor("#9370db"));
            namedColors.Add("mediumseagreen", ToColor("#3cb371"));
            namedColors.Add("mediumslateblue", ToColor("#7b68ee"));
            namedColors.Add("mediumspringgreen", ToColor("#00fa9a"));
            namedColors.Add("mediumturquoise", ToColor("#48d1cc"));
            namedColors.Add("mediumvioletred", ToColor("#c71585"));
            namedColors.Add("midnightblue", ToColor("#191970"));
            namedColors.Add("mintcream", ToColor("#f5fffa"));
            namedColors.Add("mistyrose", ToColor("#ffe4e1"));
            namedColors.Add("moccasin", ToColor("#ffe4b5"));
            namedColors.Add("navajowhite", ToColor("#ffdead"));
            namedColors.Add("navy", ToColor("#000080"));
            namedColors.Add("oldlace", ToColor("#fdf5e6"));
            namedColors.Add("olive", ToColor("#808000"));
            namedColors.Add("olivedrab", ToColor("#6b8e23"));
            namedColors.Add("orange", ToColor("#ffa500"));
            namedColors.Add("orangered", ToColor("#ff4500"));
            namedColors.Add("orchid", ToColor("#da70d6"));
            namedColors.Add("palegoldenrod", ToColor("#eee8aa"));
            namedColors.Add("palegreen", ToColor("#98fb98"));
            namedColors.Add("paleturquoise", ToColor("#afeeee"));
            namedColors.Add("palevioletred", ToColor("#db7093"));
            namedColors.Add("papayawhip", ToColor("#ffefd5"));
            namedColors.Add("peachpuff", ToColor("#ffdab9"));
            namedColors.Add("peru", ToColor("#cd853f"));
            namedColors.Add("pink", ToColor("#ffc0cb"));
            namedColors.Add("plum", ToColor("#dda0dd"));
            namedColors.Add("powderblue", ToColor("#b0e0e6"));
            namedColors.Add("purple", ToColor("#800080"));
            namedColors.Add("red", ToColor("#ff0000"));
            namedColors.Add("rosybrown", ToColor("#bc8f8f"));
            namedColors.Add("royalblue", ToColor("#4169e1"));
            namedColors.Add("saddlebrown", ToColor("#8b4513"));
            namedColors.Add("salmon", ToColor("#fa8072"));
            namedColors.Add("sandybrown", ToColor("#f4a460"));
            namedColors.Add("seagreen", ToColor("#2e8b57"));
            namedColors.Add("seashell", ToColor("#fff5ee"));
            namedColors.Add("sienna", ToColor("#a0522d"));
            namedColors.Add("silver", ToColor("#c0c0c0"));
            namedColors.Add("skyblue", ToColor("#87ceeb"));
            namedColors.Add("slateblue", ToColor("#6a5acd"));
            namedColors.Add("slategray", ToColor("#708090"));
            namedColors.Add("snow", ToColor("#fffafa"));
            namedColors.Add("springgreen", ToColor("#00ff7f"));
            namedColors.Add("steelblue", ToColor("#4682b4"));
            namedColors.Add("tan", ToColor("#d2b48c"));
            namedColors.Add("teal", ToColor("#008080"));
            namedColors.Add("thistle", ToColor("#d8bfd8"));
            namedColors.Add("tomato", ToColor("#ff6347"));
            namedColors.Add("turquoise", ToColor("#40e0d0"));
            namedColors.Add("violet", ToColor("#ee82ee"));
            namedColors.Add("wheat", ToColor("#f5deb3"));
            namedColors.Add("white", ToColor("#ffffff"));
            namedColors.Add("whitesmoke", ToColor("#f5f5f5"));
            namedColors.Add("yellow", ToColor("#ffff00"));
            namedColors.Add("yellowgreen", ToColor("#9acd32"));
        }

        #endregion
    };
}
