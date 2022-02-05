using System;
using System.Reflection;
using Terraria;
using TerrariaApi.Server;

namespace HelpopPlugin
{
    [ApiVersion(2, 1)]
    public partial class Helpop : TerrariaPlugin
    {
        /// <inheritdoc />
        public override string Name => typeof(Helpop).Assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title;

        /// <inheritdoc />
        public override string Description => typeof(Helpop).Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description;

        /// <inheritdoc />
        public override Version Version => typeof(Helpop).Assembly.GetName().Version;

        /// <inheritdoc />
        public override string Author => typeof(Helpop).Assembly.GetCustomAttribute<AssemblyCompanyAttribute>().Company;

        public Helpop(Main game) : base(game)
        {
        }

        /// <inheritdoc />
        public override void Initialize()
        {
            Initialize_Config();
            Initialize_Credits();
            Initialize_Redis();
            Initialize_Issues();
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose_Redis();

                Events.OnIssue -= HandleOnIssue;
            }
            base.Dispose(disposing);
        }
    }
}
