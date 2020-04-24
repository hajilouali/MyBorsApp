using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MyBorsApp.Tools
{
    public class CompressAttribute: ActionFilterAttribute
    {
        #region Methods (2)

        // Public Methods (1)

        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var response = filterContext.HttpContext.Response;
            if (IsGZipSupported(filterContext.HttpContext.Request))
            {
                String acceptEncoding = filterContext.HttpContext.Request.Headers["Accept-Encoding"];
                if (acceptEncoding.Contains("gzip"))
                {
                    response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
                    response.AppendHeader("Content-Encoding", "gzip");
                }
                else
                {
                    response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
                    response.AppendHeader("Content-Encoding", "deflate");
                }
            }
            // Allow proxy servers to cache encoded and unencoded versions separately
            response.AppendHeader("Vary", "Content-Encoding");
            //حذف فضاهای خالی





            response.Filter = new WhitespaceFilter(response.Filter);
        }
        // Private Methods (1)

        /// <summary>
        /// Determines whether [is G zip supported] [the specified request].
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private Boolean IsGZipSupported(HttpRequestBase request)
        {
            String acceptEncoding = request.Headers["Accept-Encoding"];

            if (acceptEncoding == null) return false;
            return !String.IsNullOrEmpty(acceptEncoding) && acceptEncoding.Contains("gzip") || acceptEncoding.Contains("deflate");
        }

        #endregion Methods
    }

    /// <summary>
    /// Whitespace Filter
    /// </summary>
    public class WhitespaceFilter : Stream
    {
        #region Fields (3)

        private readonly Stream _filter;
        /// <summary>
        ///
        /// </summary>
        private static readonly Regex RegexAll = new Regex(@"\s+|\t\s+|\n\s+|\r\s+", RegexOptions.Compiled);
        /// <summary>
        ///
        /// </summary>
        private static readonly Regex RegexTags = new Regex(@">\s+<", RegexOptions.Compiled);

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="WhitespaceFilter" /> class.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public WhitespaceFilter(Stream filter)
        {
            _filter = filter;
        }

        #endregion Constructors

        #region Properties (5)

        //methods that need to be overridden from stream
        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the current stream supports reading.
        /// </summary>
        /// <returns>true if the stream supports reading; otherwise, false.</returns>
        public override bool CanRead
        {
            get { return true; }
        }

        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the current stream supports seeking.
        /// </summary>
        /// <returns>true if the stream supports seeking; otherwise, false.</returns>
        public override bool CanSeek
        {
            get { return true; }
        }

        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the current stream supports writing.
        /// </summary>
        /// <returns>true if the stream supports writing; otherwise, false.</returns>
        public override bool CanWrite
        {
            get { return true; }
        }

        /// <summary>
        /// When overridden in a derived class, gets the length in bytes of the stream.
        /// </summary>
        /// <returns>A long value representing the length of the stream in bytes.</returns>
        public override long Length
        {
            get { return 0; }
        }

        /// <summary>
        /// When overridden in a derived class, gets or sets the position within the current stream.
        /// </summary>
        /// <returns>The current position within the stream.</returns>
        public override long Position { get; set; }

        #endregion Properties

        #region Methods (6)

        // Public Methods (6)

        /// <summary>
        /// Closes the current stream and releases any resources (such as sockets and file handles) associated with the current stream. Instead of calling this method, ensure that the stream is properly disposed.
        /// </summary>
        public override void Close()
        {
            _filter.Close();
        }

        /// <summary>
        /// When overridden in a derived class, clears all buffers for this stream and causes any buffered data to be written to the underlying device.
        /// </summary>
        public override void Flush()
        {
            _filter.Flush();
        }

        /// <summary>
        /// When overridden in a derived class, reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
        /// </summary>
        /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced by the bytes read from the current source.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin storing the data read from the current stream.</param>
        /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
        /// <returns>
        /// The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.
        /// </returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            return _filter.Read(buffer, offset, count);
        }

        /// <summary>
        /// When overridden in a derived class, sets the position within the current stream.
        /// </summary>
        /// <param name="offset">A byte offset relative to the <paramref name="origin" /> parameter.</param>
        /// <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin" /> indicating the reference point used to obtain the new position.</param>
        /// <returns>
        /// The new position within the current stream.
        /// </returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            return _filter.Seek(offset, origin);
        }

        /// <summary>
        /// When overridden in a derived class, sets the length of the current stream.
        /// </summary>
        /// <param name="value">The desired length of the current stream in bytes.</param>
        public override void SetLength(long value)
        {
            _filter.SetLength(value);
        }

        /// <summary>
        /// When overridden in a derived class, writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <param name="buffer">An array of bytes. This method copies <paramref name="count" /> bytes from <paramref name="buffer" /> to the current stream.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
        /// <param name="count">The number of bytes to be written to the current stream.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            string html = Encoding.Default.GetString(buffer);

            //remove whitespace
            html = RegexTags.Replace(html, "> <");
            html = RegexAll.Replace(html, " ");

            byte[] outdata = Encoding.Default.GetBytes(html);

            //write bytes to stream
            _filter.Write(outdata, 0, outdata.GetLength(0));
        }

        #endregion Methods
    }
}