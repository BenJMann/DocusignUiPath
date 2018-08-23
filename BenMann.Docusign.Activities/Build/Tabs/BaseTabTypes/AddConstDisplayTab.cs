using System.Activities;
using System.ComponentModel;

namespace BenMann.Docusign.Activities.Tabs
{
    public enum FontNames
    {
        Default,
        Arial,
        ArialNarrow,
        Calibri,
        CourierNew,
        Garamond,
        Georgia,
        Helvetica,
        LucidaConsole,
        Tahoma,
        TimesNewRoman,
        Trebuchet,
        Verdana,
        MSGothic,
        MSMincho 
    }
    public enum FontColors
    {
        Black,
        BrightBlue,
        BrightRed,
        DarkGreen,
        DarkRed,
        Gold,
        Green,
        NavyBlue,
        Purple,
        White
    }
    public abstract class AddConstDisplayTab : AddTabBase
    {
        [Category("Formatting")]
        public bool Bold { get; set; }
        [Category("Formatting")]
        public bool Italic { get; set; }
        [Category("Formatting")]
        public bool Underline { get; set; }
        [Category("Formatting")]
        public FontNames Font { get; set; }
        [Category("Formatting")]
        public FontColors FontColor { get; set; }
        [Category("Formatting")]
        public InArgument<int> FontSize { get; set; }

        public bool bold;
        public bool italic;
        public bool underline;
        public string font;
        public string fontColor;
        public int fontSize;

        protected new void Initialize(CodeActivityContext context)
        {
            base.Initialize(context);

            bold = Bold;
            italic = Italic;
            underline = Underline;
            font = Font.ToString();
            fontColor = FontColor.ToString();
            fontSize = FontSize.Get(context);
            if (fontSize == 0) fontSize = 12;
        }
    }
}
