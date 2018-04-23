using System;
using System.ComponentModel;

using DevExpress.ExpressApp;

namespace WebExample.Module.Web {
    [ToolboxItemFilter("Xaf.Platform.Web")]
    public sealed partial class WebExampleAspNetModule : ModuleBase {
        public WebExampleAspNetModule() {
            ModelDifferenceResourceName = "WebExample.Module.Web.Model.DesignedDiffs";
            InitializeComponent();
        }
    }
}
