using System;
using System.CodeDom.Compiler;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ContactService.SourceGenerator.ApiGenerator
{
    [ExcludeFromCodeCoverage]
    internal sealed class SourceBuilder : IDisposable
    {
        private readonly StringWriter _writer;
        private readonly IndentedTextWriter _indentedWriter;
        private const char OpeningCurlyBracket = '{';
        private const char ClosingCurlyBracket = '}';
        private const char OpeningBracket = '(';
        private const char ClosingBracket = ')';
        private const char WhiteSpaceChar = ' ';

        public SourceBuilder()
        {
            _writer = new();
            _indentedWriter = new(_writer, new(WhiteSpaceChar, 4));
        }

        public SourceBuilder WriteLine(string value = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                _indentedWriter.WriteLineNoTabs(string.Empty);
            }
            else
            {
                _indentedWriter.WriteLine(value);
            }

            return this;
        }

        public SourceBuilder Write(string value = null)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                _indentedWriter.Write(value);
            }

            return this;
        }

        public SourceBuilder WriteLineIf(bool condition, string value)
        {
            if (condition)
            {
                WriteLine(value);
            }

            return this;
        }

        public SourceBuilder WriteIf(bool condition, string value)
        {
            if (condition)
            {
                Write(value);
            }

            return this;
        }

        public SourceBuilder WriteOpeningCurlyBracket()
        {
            _indentedWriter.WriteLine(OpeningCurlyBracket);
            _indentedWriter.Indent++;

            return this;
        }

        public SourceBuilder WriteClosingCurlyBracket()
        {
            _indentedWriter.Indent--;
            _indentedWriter.WriteLine(ClosingCurlyBracket);

            return this;
        }

        public SourceBuilder WriteOpeningBracket()
        {
            _indentedWriter.Write(OpeningBracket);
            _indentedWriter.Indent++;
            return this;
        }

        public SourceBuilder WriteClosingBracket()
        {
            _indentedWriter.Indent--;
            _indentedWriter.WriteLine(ClosingBracket);
            return this;
        }

        public override string ToString() => _writer.ToString();

        public void Dispose()
        {
            _writer.Dispose();
            _indentedWriter.Dispose();
        }
    }
}