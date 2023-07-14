using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMe.Engine.API.Classes.Enumerations
{
#if WITH_SERVER_CODE
    public struct EHangHTTPContentTypes
    {
        public static string PlainText = "text/plain"; // Plain text content type
        public static string HTML = "text/html"; // HTML content type
        public static string CSS = "text/css"; // CSS content type
        public static string JS = "text/javascript"; // JavaScript content type
        public static string JSON = "application/json"; // JSON content type
        public static string XML = "application/xml"; // XML content type
        public static string PDF = "application/pdf"; // PDF content type
        public static string JPEG = "image/jpeg"; // JPEG image content type
        public static string PNG = "image/png"; // PNG image content type
        public static string GIF = "image/gif"; // GIF image content type
        public static string SVGXML = "image/svg+xml"; // SVG image content type
        public static string MPEG = "audio/mpeg"; // MPEG audio content type
        public static string WAV = "audio/wav"; // WAV audio content type
        public static string MP4 = "video/mp4"; // MP4 video content type
        public static string MPEGVideo = "video/mpeg"; // MPEG video content type
        public static string OCTET = "application/octet-stream"; // Binary content type
    }
#endif
}
