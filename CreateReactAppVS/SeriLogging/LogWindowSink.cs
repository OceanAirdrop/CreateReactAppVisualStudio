using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;
using SetupReactApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreateReactAppVS.SeriLogging
{
    class LogWindowSink : ILogEventSink
    {
        readonly ITextFormatter _textFormatter = new MessageTemplateTextFormatter("{Timestamp:dd/MM/yyyy HH:mm:ss} [{Level}] {Message}{Exception}", null);
        private RichTextBox m_rtb;

        public LogWindowSink(RichTextBox rtb)
        {
            m_rtb = rtb;
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null)
                throw new ArgumentNullException(nameof(logEvent));

            var renderSpace = new StringWriter();
            _textFormatter.Format(logEvent, renderSpace);

            MainForm.GetInstance().OutputToRichTextBox(renderSpace.ToString());
        }
    }
}
