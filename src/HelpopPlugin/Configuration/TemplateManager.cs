using Scriban;
using Scriban.Parsing;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using TerrariaApi.Server;

namespace HelpopPlugin.Configuration
{
    public class TemplateManager
    {
        private readonly TerrariaPlugin _plugin;

        /// <summary>
        /// Gets a value indicating if the templates have loaded.
        /// </summary>
        public bool Loaded { get; private set; }

        /// <summary>
        /// Gets or sets the report template.
        /// </summary>
        public Template ReportTemplate { get; set; }

        internal TemplateManager(TerrariaPlugin plugin)
        {
            _plugin = plugin;
        }

        public void Load()
        {
            if (Loaded)
            {
                return;
            }

            Reload();

            Loaded = true;
        }

        public void Reload()
        {
            var assembly = typeof(Helpop).Assembly;

            ReportTemplate = LoadTemplateFromFS(
                Paths.ReportTemplatePath,
                assembly,
                $"{nameof(HelpopPlugin)}.{nameof(Configuration)}.ReportTemplate.scriban-txt");
        }

        private Template LoadTemplateFromFS(
            string path,
            Assembly assembly,
            string resourcePath,
            ParserOptions? parserOptions = null,
            LexerOptions? lexerOptions = null)
        {
            string templateText;
            if (!File.Exists(path))
            {
                using (var stream = assembly.GetManifestResourceStream(resourcePath))
                {
                    using (var fs = new FileStream(
                        path,
                        FileMode.CreateNew,
                        FileAccess.Write,
                        FileShare.None))
                    {
                        stream.CopyTo(fs);
                    }

                    using (var reader = new StreamReader(stream))
                    {
                        templateText = reader.ReadToEnd();
                    }
                }
            }
            else
            {
                using (var fs = new FileStream(
                    path,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read))
                {
                    using (var reader = new StreamReader(fs))
                    {
                        templateText = reader.ReadToEnd();
                    }
                }
            }

            var template = Template.Parse(
                templateText,
                path,
                parserOptions,
                lexerOptions
            );

            if (template.HasErrors)
            {
                ServerApi.LogWriter.PluginWriteLine(_plugin, "Fatal: template is invalid", TraceLevel.Error);

                foreach (var error in template.Messages)
                {
                    TraceLevel traceLevel = error.Type switch
                    {
                        ParserMessageType.Warning => TraceLevel.Warning,
                        ParserMessageType.Error => TraceLevel.Error,
                        _ => TraceLevel.Info
                    };

                    // TODO: SEE IF MESSAGE INCLUDE SOURCE SPAN
                    ServerApi.LogWriter.PluginWriteLine(_plugin, error.Message, traceLevel);
                }
            }

            return template;
        }
    }
}
