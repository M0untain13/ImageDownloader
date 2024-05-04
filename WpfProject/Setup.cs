using Microsoft.Extensions.Logging;
using MvvmCross.Platforms.Wpf.Core;

namespace WpfProject;

class Setup : MvxWpfSetup<CoreProject.Core>
{
    protected override ILoggerFactory? CreateLogFactory() => null;
    protected override ILoggerProvider? CreateLogProvider() => null;
}
